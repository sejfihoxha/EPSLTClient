using ClientEPSLTTask.UI;

namespace ClientEPSLTTask.Commands
{
    public class GenerateCommand : ICommandRequest
    {
        public ushort Count { get; set; }
        public byte Length { get; set; }
    }
}
