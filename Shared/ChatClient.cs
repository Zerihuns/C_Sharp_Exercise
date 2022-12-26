using Microsoft.AspNetCore.SignalR.Client;


namespace ChatApp.Shared
{

    public class ChatClient : IAsyncDisposable
    {
        public const string HUBURL = "/ChatHub";

        private readonly string _hubUrl;
        private HubConnection _hubConnection;

    
        public ChatClient(string username, string siteUrl)
        {
            // check inputs
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrWhiteSpace(siteUrl))
                throw new ArgumentNullException(nameof(siteUrl));
            // save username
            _username = username;
            // set the hub URL
            _hubUrl = siteUrl.TrimEnd('/') + HUBURL;
        }

        private readonly string _username;

       
        private bool _started = false;

        public async Task StartAsync()
        {
            if (!_started)
            {
                // create the connection using the .NET SignalR client
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_hubUrl)
                    .Build();
                Console.WriteLine("ChatClient: calling Start()");

                // add handler for receiving messages
                _hubConnection.On<string, string>(Messages.RECEIVE, (user, message) =>
                 {
                     HandleReceiveMessage(user, message);
                 });

                // start the connection
                await _hubConnection.StartAsync();

                Console.WriteLine("ChatClient: Start returned");
                _started = true;

                // register user on hub to let other clients know they've joined
                await _hubConnection.SendAsync(Messages.REGISTER, _username);
            }
        }

    
        private void HandleReceiveMessage(string username, string message)
        {
            // raise an event to subscribers
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(username, message));
        }

        public event MessageReceivedEventHandler MessageReceived;

        public async Task SendAsync(string message)
        {
            // check we are connected
            if (!_started)
                throw new InvalidOperationException("Client not started");
            // send the message
            await _hubConnection.SendAsync(Messages.SEND, _username, message);
        }

 
        public async Task StopAsync()
        {
            if (_started)
            {
                // disconnect the client
                await _hubConnection.StopAsync();

                await _hubConnection.DisposeAsync();
                _hubConnection = null;
                _started = false;
            }
        }

        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("ChatClient: Disposing");
            await StopAsync();
        }
    }

 
    /// Delegate for the message handler

    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);


    /// Message received argument class

    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(string username, string message)
        {
            Username = username;
            Message = message;
        }


   
        public string Username { get; set; }

        public string Message { get; set; }

    }

}

