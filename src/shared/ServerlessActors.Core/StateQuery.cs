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
using Newtonsoft.Json;

namespace ServerlessActors.Core
{
	public class StateQuery
	{
		private readonly AmazonDynamoDBClient    _dynamoDbClient;

		public StateQuery()
		{
			this._dynamoDbClient = new AmazonDynamoDBClient();
		}

		/// <inheritdoc />
		public async Task<T> Retrieve<T>(string entityType, string id) where T : StatefulEntity
		{
			var item = await this._dynamoDbClient.GetItemAsync(Environment.GetEnvironmentVariable("STATE_STORE"),
				new Dictionary<string, AttributeValue>(1)
				{
					{"PK", new AttributeValue($"{entityType.ToUpper()}#{id}")}
				});

			if (item.IsItemSet == false)
			{
				return null;
			}

			var data = item.Item.FirstOrDefault(p => p.Key == "Data");

			return JsonConvert.DeserializeObject<T>(Document.FromAttributeMap(data.Value.M).ToJson());
		}
	}
}