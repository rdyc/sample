using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;

namespace Sample.Api.Notifications
{
    public class PingNotification : INotification
    {
        
    }
    
    public class Pong1 : INotificationHandler<PingNotification> {
        public void Handle(PingNotification notification) {
            Debug.WriteLine("Pong 1");
        }
    }
    
    public class Pong2 : INotificationHandler<PingNotification> {
        public void Handle(PingNotification notification) {
            Debug.WriteLine("Pong 2");
        }
    }

    public class Ping : IRequest<Pong>
    {
        public string Message { get; set; }
    }
    
    public class Pong
    {
        public string Message { get; set; }

        
    }
    
    public class PingHandlerAsync : IAsyncRequestHandler<Ping, Pong> {
        public async Task<Pong> Handle(Ping request) {
            return await DoPong(); // Whatever DoPong does
        }
        
        public async Task<Pong> DoPong()
        {
            //return await new Task<string>(@dsadsadsa).GetAwaiter();
            return await Task.FromResult(new Pong { Message = "dsadasd " + " Pong" });
        }
    }
}