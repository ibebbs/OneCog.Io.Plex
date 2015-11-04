
namespace OneCog.Io.Plex.Demo.Message
{
    public class ServerConnectionAvailable
    {
        public ServerConnectionAvailable(IServer server)
        {
            Server = server;
        }

        public IServer Server { get; private set; }
    }
}
