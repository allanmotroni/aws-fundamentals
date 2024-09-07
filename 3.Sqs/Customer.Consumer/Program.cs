using Amazon.SQS;
using Customer.Consumer;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAmazonSQS,AmazonSQSClient>();
builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection(QueueSettings.Key));
builder.Services.AddHostedService<QueueConsumerService>();
builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

app.Run();
