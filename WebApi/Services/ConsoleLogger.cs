using System;

namespace WebApi.Services
{
    public class ConsoleLogger : ILoggerService
    {
        // ConsoleLogger service writes logs to the console
        public void Write(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}