using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using JEasthamDev.Aws.ServerlessActors;
using JEasthamDev.Aws.ServerlessActors.Commands;
using Newtonsoft.Json;
using ServerlessActors.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CartActor
{
	public class Function
	{
		private readonly StateStore _stateStore;
		private readonly StateQuery _stateQuery;
		
		public Function()
		{
			this._stateStore = new StateStore();
			this._stateQuery = new StateQuery();
		}

		public async Task HandlerFunction(SQSEvent evt, ILambdaContext context)
		{
			foreach (var record in evt.Records)
			{
				context.Logger.LogLine($"Received actor message {record.Body}");
				
				var actorMessage = JsonConvert.DeserializeObject<ActorMessage>(record.Body);

				switch (actorMessage.MessageType.ToLower())
				{
					case "add.product":
						await this.AddProduct(actorMessage);
						break;
					case "remove.product":
						await this.RemoveProduct(actorMessage);
						break;
					case "clear.cart":
						await this.ClearCart(actorMessage);
						break;
				}
			}
		}

		private async Task AddProduct(ActorMessage message)
		{
			var command = JsonConvert.DeserializeObject<AddProductCommand>(message.Payload);
						
			var cart = await this._stateQuery.Retrieve<Cart>(Cart.Type, command.CustomerId);

			if (cart == null)
			{
				cart = new Cart(command.CustomerId);
			}

			cart.AddProduct(command.ProductName, command.Quantity);

			await this._stateStore.Store(command.CustomerId, cart);
		}

		private async Task RemoveProduct(ActorMessage message)
		{
			var removeCommand = JsonConvert.DeserializeObject<RemoveProductCommand>(message.Payload);
						
			var cart = await this._stateQuery.Retrieve<Cart>(Cart.Type, removeCommand.CustomerId);

			if (cart == null)
			{
				return;
			}

			cart.RemoveProduct(removeCommand.ProductName);

			await this._stateStore.Store(removeCommand.CustomerId, cart);
		}

		private async Task ClearCart(ActorMessage message)
		{
			var removeCommand = JsonConvert.DeserializeObject<ClearCartCommand>(message.Payload);
						
			var cart = await this._stateQuery.Retrieve<Cart>(Cart.Type, removeCommand.CustomerId);

			cart.Clear();

			await this._stateStore.Store(removeCommand.CustomerId, cart);
		}
	}
}