namespace TheContentDepartment.Core;

using TheContentDepartment.Core.Contracts;
using TheContentDepartment.IO;
using TheContentDepartment.IO.Contracts;

public class Engine : IEngine
{
    private readonly IReader _reader = new Reader();
    private readonly IWriter _writer = new Writer();
    private readonly IController _controller = new Controller();

    public void Run()
    {
        while (true)
        {
            var input = this._reader.ReadLine().Split();
            if (input[0] == "Exit")
            {
                Environment.Exit(0);
            }
            try
            {
                var result = string.Empty;

                if (input[0] == "JoinTeam")
                {
                    result = this._controller.JoinTeam(input[1], input[2], input[3]);
                }
                else if (input[0] == "CreateResource")
                {
                    result = this._controller.CreateResource(input[1], input[2], input[3]);
                }
                else if (input[0] == "LogTesting")
                {
                    result = this._controller.LogTesting(input[1]);
                }
                else if (input[0] == "ApproveResource")
                {
                    result = this._controller.ApproveResource(input[1], bool.Parse(input[2]));
                }
                else if (input[0] == "DepartmentReport")
                {
                    result = this._controller.DepartmentReport();
                }
                this._writer.WriteLine(result);
                this._writer.WriteText(result);
            }
            catch (Exception ex)
            {
                this._writer.WriteLine(ex.Message);
                this._writer.WriteText(ex.Message);
            }
        }
    }
}