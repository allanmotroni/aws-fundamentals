using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Customers.Api.Messaging
{
   public class SqsMessenger : ISqsMessenger
   {
      private readonly IAmazonSQS _sqs;
      private readonly IOptions<QueueSettings> _queueSettings;
      private string? _queueName;

      public SqsMessenger(
         IAmazonSQS sqs,
         IOptions<QueueSettings> queueSettings)
      {
         _sqs = sqs;
         _queueSettings = queueSettings;
      }

      private async Task<string> GetQueueUrlAsync()
      {
         if (_queueName is not null)
         {
            return _queueName;
         }

         var queueUrlResponse = await _sqs.GetQueueUrlAsync(_queueSettings.Value.Name);
         _queueName = queueUrlResponse.QueueUrl;

         return _queueName;
      }

      public async Task<SendMessageResponse> SendMessageAsync<T>(T message)
      {
         var queueUrl = await GetQueueUrlAsync();

         var sendMessageRequest = new SendMessageRequest
         {
            QueueUrl = queueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue> {
               {
                  "MessageType", new MessageAttributeValue
                  {
                     DataType = "String",
                     StringValue = typeof(T).Name
                  }
               }
            }
         };

         return await _sqs.SendMessageAsync(sendMessageRequest);
      }
   }
}
