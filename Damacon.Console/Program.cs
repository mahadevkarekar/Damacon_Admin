using Damacon.Console.Commands;

namespace Damacon.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ICommand command = GetCommand(args);
            if (command != null)
            {
                command.Execute();
            }
        }

        private static ICommand GetCommand(string[] args) { 
        //{
        //    if (args[0] == "UpdateResources")
        //    {
        //        return new UpdateResources();
        //    }
        //    else if (args[0] == "ImportWorkedHour")
        //    {
        //        return new ImportWorkedHour();
        //    }
            return null;
        }
    }
}
