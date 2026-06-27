using Sigillum.Arcavis.Core.Application;
using Sigillum.Arcavis.Infrastructure;
using Sigillum.Arcavis.Presentation.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<OutboxWorker>();

builder.Services.AddInfrastructure(builder.Configuration)
                .AddApplication();

var host = builder.Build();
host.Run();
