namespace WZH.Infrastructure.DbContext
{
    public class DbConnectionOption
    {
        public string MasterConnection { get; set; }
        public IList<string> SlaveConnections { get; set; }
    }
}