
using ClientEPSLTTask.Services;
using ClientEPSLTTask.UI;

class Program
{
    static async Task Main()
    {
        var clientService = new DiscountClientService("127.0.0.1", 5000);
        var commandLoop = new CommandLoop(clientService);
        await commandLoop.RunAsync();
    }
}