
using Amazon.SQS;
using Amazon.SQS.Model;
using SqsPublisher;
using System.Text.Json;
using System.Text.Json.Serialization;

var sqsClient = new AmazonSQSClient();

var customer = new CustomerCreated
{
    Id = Guid.NewGuid(),
    Email = "email@email.com",
    FullName = "Name Test",
    DateOfBirth = new DateTime(2000, 1, 1),
    GitHubUsername = "github-test"
};

var queryUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queryUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(CustomerCreated)
            }
        }
    }
};

var response = await sqsClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine();