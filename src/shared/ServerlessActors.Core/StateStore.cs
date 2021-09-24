// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using Newtonsoft.Json;

namespace ServerlessActors.Core
{
	public class StateStore
	{
		private readonly AmazonDynamoDBClient    _dynamoDbClient;
		private readonly AmazonEventBridgeClient _eventBridge;

		public StateStore()
		{
			this._dynamoDbClient = new AmazonDynamoDBClient();
			this._eventBridge = new AmazonEventBridgeClient();
		}

		/// <inheritdoc />
		public async Task Store<T>(string id, T toStore) where T : StatefulEntity
		{
			var document = Document.FromJson(JsonConvert.SerializeObject(toStore));
			var documentAttributeMap = document.ToAttributeMap();
			
			documentAttributeMap.Remove("Events"); // No need to persist the events to the database.

			await this._dynamoDbClient.PutItemAsync(Environment.GetEnvironmentVariable("STATE_STORE"),
				new Dictionary<string, AttributeValue>(2)
				{
					{"PK", new AttributeValue($"{toStore.EntityType.ToUpper()}#{id}")},
					{
						"Data", new AttributeValue()
						{
							M = documentAttributeMap
						}
					}
				});

			if (toStore.Events.Any())
			{
				await this._eventBridge.PutEventsAsync(new PutEventsRequest()
				{
					Entries = toStore.Events.Select(evt => new PutEventsRequestEntry()
					{
						Source = evt.Source, Detail = JsonConvert.SerializeObject(evt), DetailType = evt.Name,
						Time = evt.Time
					}).ToList()
				});
			}
		}
	}
}