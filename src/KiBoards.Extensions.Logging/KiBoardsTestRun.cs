[assembly: KiboardsTestStartup("KiBoards.Extensions.Logging.KiBoardsTestRun")]

namespace KiBoards.Extensions.Logging
{
    public class KiBoardsTestRun
    {
        public static KiBoardsTestRun Instance { get; private set; }
        
        public string Id { get; set; }

        public KiBoardsTestRun(string id) 
        { 
            Id = id;
            Instance = this;
        }
    }
}
