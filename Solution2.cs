namespace Solution2
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder pathfinder1 = new Pathfinder(new FileLogger(new NormalSecurePolicy()));
            Pathfinder pathfinder2 = new Pathfinder(new ConsoleLogger(new NormalSecurePolicy()));
            Pathfinder pathfinder3 = new Pathfinder(new FileLogger(new FridaySecurePolicy()));
            Pathfinder pathfinder4 = new Pathfinder(new ConsoleLogger(new FridaySecurePolicy()));
            Pathfinder pathfinder5 = new Pathfinder(new ConsoleLogger(new NormalSecurePolicy()), new FileLogger(new FridaySecurePolicy()));
        }
    }

    interface ILogger
    {
        public void WriteError(string message);
    }

    interface ISecuredPolicy
    {
        bool Secured();
    }

    class NormalSecurePolicy : ISecuredPolicy
    {
        public bool Secured() => true;
    }

    class FridaySecurePolicy : ISecuredPolicy
    {
        public bool Secured() => DateTime.Now.DayOfWeek == DayOfWeek.Friday;
    }

    class ConsoleLogger : ILogger
    {
        private ISecuredPolicy _securedPolicy;

        public ConsoleLogger(ISecuredPolicy securedPolicy)
        {
            _securedPolicy = securedPolicy;
        }

        public void WriteError(string message)
        {
            if (_securedPolicy.Secured())
                Console.WriteLine(message);
        }
    }

    class FileLogger : ILogger
    {
        private ISecuredPolicy _securedPolicy;

        public FileLogger(ISecuredPolicy securedPolicy)
        {
            _securedPolicy = securedPolicy;
        }

        public void WriteError(string message)
        {
            if (_securedPolicy.Secured())
                File.WriteAllText("log.txt", message);
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
