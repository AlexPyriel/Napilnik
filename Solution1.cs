namespace Solution1
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder pathfinder1 = new Pathfinder(new FileLogger());
            Pathfinder pathfinder2 = new Pathfinder(new ConsoleLogger());
            Pathfinder pathfinder3 = new Pathfinder(new SecureLogger(new FileLogger()));
            Pathfinder pathfinder4 = new Pathfinder(new SecureLogger(new ConsoleLogger()));
            Pathfinder pathfinder5 = new Pathfinder(new ConsoleLogger(), new SecureLogger(new FileLogger()));
        }
    }

    interface ILogger
    {
        public void WriteError(string message);
    }

    class ConsoleLogger : ILogger
    {
        public void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogger : ILogger
    {
        public void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class SecureLogger : ILogger
    {
        private ILogger _logger;

        public SecureLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                _logger.WriteError(message);
        }
    }

    class Pathfinder
    {
        private ILogger[] _loggers;

        public Pathfinder(params ILogger[] loggers)
        {
            _loggers = loggers;
        }

        public void Find(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.WriteError(message);
            }
        }
    }
}
