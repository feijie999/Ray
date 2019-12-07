﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Ray.Core;
using Ray.Core.Services;
using Ray.EventBus.Kafka;
using Ray.Storage.PostgreSQL;
using System;
using System.Net;
using System.Threading.Tasks;
using Transfer.Grains;
using Transfer.Grains.Grains;

namespace Transfer.Server
{
    class Program
    {
        static Task Main(string[] args)
        {
            var host = CreateHost();
            return host.RunAsync();
        }
        private static IHost CreateHost()
        {
            return new HostBuilder()
                .UseOrleans((context, siloBuilder) =>
                {
                    siloBuilder
                        .UseLocalhostClustering()
                        .UseDashboard()
                        .AddRay<TransferConfig>()
                        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                        .ConfigureApplicationParts(parts =>
                        {
                            parts.AddApplicationPart(typeof(Account).Assembly).WithReferences();
                            parts.AddApplicationPart(typeof(UtcUIDGrain).Assembly).WithReferences();
                        });
                })
                .ConfigureServices(servicecollection =>
                {
                    //注册postgresql为事件存储库
                    servicecollection.AddPostgreSQLStorage(config =>
                    {
                        config.ConnectionDict.Add("core_event", "Server=127.0.0.1;Port=5432;Database=Ray;User Id=postgres;Password=luohuazhiyu;Pooling=true;MaxPoolSize=20;");
                    });
                    servicecollection.AddPostgreSQLTxStorage(options =>
                    {
                        options.ConnectionKey = "core_event";
                        options.TableName = "Transaction_Record";
                    });
                    servicecollection.AddKafkaMQ(
                    config => { },
                    config =>
                    {
                        config.BootstrapServers = "192.168.1.3:9092";
                    }, config =>
                    {
                        config.BootstrapServers = "192.168.1.3:9092";
                    });
                    servicecollection.Configure<GrainCollectionOptions>(options =>
                    {
                        options.CollectionAge = TimeSpan.FromMinutes(5);
                    });
                })
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Information);
                    logging.AddConsole(options => options.IncludeScopes = true);
                }).Build();
        }
    }
}
