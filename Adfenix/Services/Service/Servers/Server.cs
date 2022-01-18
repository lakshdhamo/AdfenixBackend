using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.Services.Service.Servers
{
    public abstract class Server
    {
        public Server()
        { }

        protected Parameter[] parameters;

        public Parameter[] getParameters()
        { return (Parameter[])parameters.Clone(); }

        //public abstract void execute();
        public abstract Task<string> ExecuteAsync();
    }
}
