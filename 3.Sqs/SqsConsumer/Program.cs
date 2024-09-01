
using Amazon.SQS;
using Amazon.SQS.Model;

var sqsClient = new AmazonSQSClient();

var queryUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var cts = new CancellationTokenSource();

var receiveMessageRequest = new ReceiveMessageRequest
{
   QueueUrl = queryUrlResponse.QueueUrl,
   MessageSystemAttributeNames = new List<string> { "All" },
   MessageAttributeNames = new List<string> { "All" },
};

while (!cts.IsCancellationRequested)
{
   var receiveMessageResponse = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, cts.Token);
   

   foreach (var message in receiveMessageResponse.Messages)
   {
      Console.WriteLine($"MESSAGE RECEIVED AT: {DateTime.Now}");
      Console.WriteLine();

      Console.WriteLine($"MessageId: {message.MessageId}");
      Console.WriteLine($"Body: {message.Body}");
      Console.WriteLine();

      await sqsClient.DeleteMessageAsync(queryUrlResponse.QueueUrl, message.ReceiptHandle, cts.Token);
   }

   await Task.Delay(3000);
}