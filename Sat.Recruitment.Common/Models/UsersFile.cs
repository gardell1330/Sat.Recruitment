using System.IO;
using Sat.Recruitment.Common;

namespace Sat.Recruitment.Api.Utilities
{
    public class UsersFile : IUsersFile
    {
        public StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
        
        public StreamWriter WriterUserFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Append);

            StreamWriter reader = new StreamWriter(fileStream);
            return reader;
        }
    }
}