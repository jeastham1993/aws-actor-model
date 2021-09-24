using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.SQS;
using Amazon.SQS.Model;
using JEasthamDev.Aws.ServerlessActors;
using JEasthamDev.Aws.ServerlessActors.Commands;
using Newtonsoft.Json;
using ServerlessActors.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AddProductEndpoint
{
	public class Function
	{
		private Dispatcher _dispatcher;
		
		public Function()
		{
			this._dispatcher = new Dispatcher();
		}

		public async Task<APIGatewayProxyResponse> HandlerFunction(APIGatewayProxyRequest request, ILambdaContext context)
		{
			var inboundRequest = JsonConvert.DeserializeObject<AddProductCommand>(request.Body);

			await this._dispatcher.Dispatch(inboundRequest);

			return new APIGatewayProxyResponse()
			{
				StatusCode = 202
			};
		}
	}
}