using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProxy.Core.Models
{
    public class Metadata
    {
        public string Host { get; set; }

        public IEnumerable<ControllerDefinition> Definitions { get; set; }

        public IEnumerable<ModelDefinition> Models { get; set; }

    }
}
