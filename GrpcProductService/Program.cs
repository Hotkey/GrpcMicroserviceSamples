using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Grpc.Net.Client;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Runtime.CompilerServices;

namespace GrpcProductService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //var serverAddress = "https://localhost:5001";
            //var channel = GrpcChannel.ForAddress(serverAddress);
            //var client = new OrderService.Order.OrderClient(channel);
            //_orderClient = new Order.OrderClient();

            //var creditRequest = new HelloRequest { Name = "AHMET" };
            //var reply = await _orderClient.SayHelloAsync(creditRequest);

            //Console.WriteLine($"Credit for customer {creditRequest.Name} {reply.Message }");
            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();

            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                  {
                      webBuilder.ConfigureKestrel(options =>
                      {
                            // Setup a HTTP/2 endpoint without TLS.
                            options.ListenLocalhost(5000, o => o.Protocols =
                                HttpProtocols.Http2);
                      });
                  }
                  webBuilder.UseStartup<Startup>();
              });
    }
}
