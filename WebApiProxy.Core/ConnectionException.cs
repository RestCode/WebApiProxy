using System;

namespace WebApiProxy
{
    public class ConnectionException : Exception
    {

        private string uri;
        public ConnectionException(string uri)
        {
            this.uri = uri;
        }

        public override string Message
        {
            get
            {
                return "WebApiProxy: Could not connect to remote server - " + uri;
            }
        }
    }

    public class ConfigFileNotFoundException : Exception
    {
        private readonly string path;
        public ConfigFileNotFoundException(string path)
        {
            this.path = path;
        }
        public override string StackTrace
        {
            get
            {
                return String.Empty;
            }
        }

       

        public override string Message
        {
            get
            {
                return "WebApiProxy: Configuration file not found: " + path;
            }
        }
    }
}
