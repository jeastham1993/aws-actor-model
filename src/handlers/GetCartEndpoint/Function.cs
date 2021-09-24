using System.Threading.Tasks;
using System.Web;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using JEasthamDev.Aws.ServerlessActors;
using JEasthamDev.Aws.ServerlessActors.Commands;
using Newtonsoft.Json;
using ServerlessActors.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GetCartEndpoint
{
	public class Function
	{
		private StateQuery _stateQuery;
		
		public Function()
		{
			this._stateQuery = new StateQuery();
		}

		public async Task<APIGatewayProxyResponse> HandlerFunction(APIGatewayProxyRequest request, ILambdaContext context)
		{
			var result = await this._stateQuery.Retrieve<Cart>(Cart.Type, HttpUtility.UrlDecode(request.PathParameters["customerId"]));

			if (result == null)
			{
				return new APIGatewayProxyResponse()
				{
					StatusCode = 200,
					Body = "No active cart for customer"
				};	
			}

			return new APIGatewayProxyResponse()
			{
				StatusCode = 200,
				Body = JsonConvert.SerializeObject(result.Products)
			};
		}
	}
}