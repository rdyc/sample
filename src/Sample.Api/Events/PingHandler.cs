using System.Diagnostics;
using MediatR;

namespace Sample.Api.Events
{
    public class Ping : IRequest<string> { }
    
    public class PingHandler : IRequestHandler<Ping, string> {
        public string Handle(Ping request) {
            return "Pong";
        }
    }
    
    public class OneWay : IRequest { }
    public class OneWayHandlerWithBaseClass : IRequestHandler<OneWay> {
        public void Handle(OneWay request) {
            // Twiddle thumbs
            Debug.WriteLine(request); 
        }
    }
}