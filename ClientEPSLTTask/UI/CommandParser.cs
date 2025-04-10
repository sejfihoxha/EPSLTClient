using ClientEPSLTTask.Commands;
using ClientEPSLTTask.Models;

namespace ClientEPSLTTask.UI
{
    public interface ICommandRequest { }

    public static class CommandParser
    {
        public static bool TryParse(string input, out ICommandRequest? command, out string? error)
        {
            command = null;
            error = null;

            var parts = input?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts == null || parts.Length == 0)
            {
                error = "Empty command.";
                return false;
            }

            var keyword = parts[0].ToLower();

            switch (keyword)
            {
                case "generate":
                    if (parts.Length != 3)
                    {
                        error = "Usage: generate <count> <length>";
                        return false;
                    }

                    if (!ushort.TryParse(parts[1], out var count))
                    {
                        error = "Count must be a number.";
                        return false;
                    }

                    if (!byte.TryParse(parts[2], out var length) || (length != 7 && length != 8))
                    {
                        error = "Length must be 7 or 8.";
                        return false;
                    }

                    if (count > 2000)
                    {
                        error = "Maximum allowed count is 2000.";
                        return false;
                    }

                    command = new GenerateCommand { Count = count, Length = length };
                    return true;

                case "use":
                    if (parts.Length != 2 || (parts.Length != 7 && parts.Length != 8))
                    {
                        error = "Usage: use <7 or 8 char code>";
                        return false;
                    }

                    command = new UseCodeCommand { Code = parts[1] };
                    return true;

                default:
                    error = $"Unknown command: {keyword}";
                    return false;
            }
        }
    }
}