using Discount.gRPC;
using Grpc.Core;

namespace Discount.gRPC.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<HiReply> SayHi(HiRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HiReply
            {
                Message = $"Hi {request.Name}, Your Message is: '{request.Message}'"
            });
        }
    }
}