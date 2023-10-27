[assembly: KiboardsTestStartup("KiBoards.Extensions.Logging.Startup")]

namespace KiBoards.Extensions.Logging
{
    public class Startup
    {
        public static Startup Instance { get; private set; }
        
        public string RunId { get; set; }

        public Startup(string id) 
        { 
            RunId = id;
            Instance = this;
        }
    }
}
