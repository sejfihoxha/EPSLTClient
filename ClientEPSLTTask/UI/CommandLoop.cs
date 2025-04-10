using ClientEPSLTTask.Commands;
using ClientEPSLTTask.Models;
using ClientEPSLTTask.Services;

namespace ClientEPSLTTask.UI
{
    public class CommandLoop
    {
        private readonly DiscountClientService _client;

        public CommandLoop(DiscountClientService client)
        {
            _client = client;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Type: generate <count> <length> or use <7 or 8 char-code>. Type 'exit' to quit.");

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine()?.Trim();

                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (!CommandParser.TryParse(input, out var command, out var error))
                {
                    Console.WriteLine($"{error}");
                    continue;
                }

                try
                {
                    switch (command)
                    {
                        case GenerateCommand generate:
                            await _client.SendGenerateAsync(generate.Count, generate.Length);
                            var genResult = await _client.ReadBoolAsync();

                            Console.WriteLine(genResult ? $"{generate.Count} discount codes generated." : "Failed to generate discount codes.");

                            break;

                        case UseCodeCommand use:
                            await _client.SendUseCodeAsync(use.Code);
                            var useResult = await _client.ReadByteAsync();

                            Console.WriteLine(useResult == 1 ? $"Code '{use.Code}' used successfully" : "Failed to use this code");

                            break;

                        default:
                            Console.WriteLine("Unrecognized command type.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while processing command: {ex.Message}");
                }
            }

            _client.Dispose();
        }
    }
}