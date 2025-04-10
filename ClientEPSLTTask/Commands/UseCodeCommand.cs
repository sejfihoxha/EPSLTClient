using ClientEPSLTTask.UI;

namespace ClientEPSLTTask.Models
{
    public class UseCodeCommand : ICommandRequest
    {
        public string Code { get; set; }
    }
}
