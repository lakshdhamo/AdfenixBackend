namespace Adfenix.Services.Service.Servers
{
    public abstract class ServerBase
    {
        protected Parameter[] parameters;

        public Parameter[] getParameters()
        { return (Parameter[])parameters.Clone(); }

        public abstract Task<string> FetchCountAsync();
    }
}
