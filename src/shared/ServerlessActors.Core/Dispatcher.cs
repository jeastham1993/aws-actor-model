// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using ServerlessActors.Core.Commands;

namespace ServerlessActors.Core
{
	public class Dispatcher
	{
		private AmazonSQSClient _sqsClient;

		private readonly Dictionary<string, string> _actorAddresses;

		public IReadOnlyDictionary<string, string> Actors => this._actorAddresses;

		public Dispatcher()
		{
			this._sqsClient = new AmazonSQSClient();
			this._actorAddresses = new Dictionary<string, string>();

			this.LoadAddresses().Wait();
		}

		private async Task LoadAddresses()
		{
			var queues = await this._sqsClient.ListQueuesAsync(new ListQueuesRequest("actor"));

			foreach (var queue in queues.QueueUrls)
			{
				var queueParts = queue.Split('/');

				var queueName = queueParts[queueParts.Length - 1].Replace("actor-", string.Empty)
					.Replace(".fifo", string.Empty);
					
				this._actorAddresses.Add(queueName.ToLower(), queue);
			}
		}

		public async Task Dispatch(ActorCommand command)
		{
			if (this._actorAddresses.ContainsKey(command.Actor) == false)
			{
				throw new Exception($"Unknown actor {command.Actor}");
			}
			
			await this._sqsClient.SendMessageAsync(new SendMessageRequest()
			{
				QueueUrl = this._actorAddresses[command.Actor],
				MessageBody = JsonConvert.SerializeObject(new ActorMessage()
				{
					Payload = JsonConvert.SerializeObject(command),
					MessageType = command.Type,
				}),
				MessageDeduplicationId = Guid.NewGuid().ToString(),
				MessageGroupId = command.Actor
			});
		}
	}
}