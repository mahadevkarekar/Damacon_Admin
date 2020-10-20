using System.IO;
using System.Reflection;

namespace Damacon.Console.Commands
{
    public class UpdateResources : ICommand
    {
        public void Execute()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\Damacon.DAL\i18n\Resources.cs"));
            DAL.i18n.Utility.ResourceBuilder.Create(filePath: newPath);
        }
    }
}
