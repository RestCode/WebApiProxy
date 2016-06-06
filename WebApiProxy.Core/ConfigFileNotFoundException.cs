using System;

namespace WebApiProxy
{
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
