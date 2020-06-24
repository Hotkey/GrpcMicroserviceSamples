using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using GrpcProductService.Protos.Greeter;

namespace GrpcProductService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly OrderService.Order.OrderClient _orderClient;

        public GreeterService(ILogger<GreeterService> logger, OrderService.Order.OrderClient orderClient)
        {
            _logger = logger;
            _orderClient = orderClient;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var creditRequest = new GrpcProductService.OrderService.HelloRequest { Name = "AHMET" };
            var reply = await _orderClient.SayHelloAsync(creditRequest);
            var strMsg =  $"Credit for customer {creditRequest.Name} {reply.Message }";
            return await Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
