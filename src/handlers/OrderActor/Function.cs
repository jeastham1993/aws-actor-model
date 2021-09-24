using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using JEasthamDev.Aws.ServerlessActors;
using JEasthamDev.Aws.ServerlessActors.Commands;
using Newtonsoft.Json;
using ServerlessActors.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace OrderActor
{
	public class Function
	{
		private readonly StateStore _stateStore;
		private readonly Dispatcher _dispatcher;
		
		public Function()
		{
			this._stateStore = new StateStore();
			this._dispatcher = new Dispatcher();
		}

		public async Task HandlerFunction(SQSEvent evt, ILambdaContext context)
		{
			foreach (var record in evt.Records)
			{
				context.Logger.LogLine($"Received actor message {record.Body}");
				
				var actorMessage = JsonConvert.DeserializeObject<ActorMessage>(record.Body);

				switch (actorMessage.MessageType.ToLower())
				{
					case "create.order":
						await this.CreateOrder(actorMessage);
						break;
				}
			}
		}

		private async Task CreateOrder(ActorMessage message)
		{
			var command = JsonConvert.DeserializeObject<CreateOrderCommand>(message.Payload);

			var order = new Order()
			{
				CustomerId = command.CustomerId
			};

			await this._stateStore.Store(command.CustomerId, order);

			await this._dispatcher.Dispatch(new ClearCartCommand()
			{
				CustomerId = command.CustomerId
			});
		}
	}
}