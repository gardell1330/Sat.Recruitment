namespace Sat.Recruitment.Common;

public interface IUsersFile
{
    public StreamReader ReadUsersFromFile();
    public StreamWriter WriterUserFromFile();
}