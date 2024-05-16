using System.Text;

namespace Server.Classes;

public partial class ConfigApp
{
    public CfgApplication Application { get; set; }

    public class CfgApplication
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string WebPage { get; set; }
        public string ApiKey { get; set; }
        public string Scheme { get; set; }
        public string DataBase { get; set; }
    }
}
