using CyberSecurityDS.Core.Contracts;
using CyberSecurityDS.IO;
using CyberSecurityDS.IO.Contracts;

namespace CyberSecurityDS.Core;

public class Engine : IEngine
{
    private IReader reader;
    private IWriter writer;
    private IController controller;

    public Engine()
    {
        reader = new Reader();
        writer = new Writer();
        controller = new Controller();
    }
    public void Run()
    {
        while (true)
        {
            string[] input = reader.ReadLine().Split();
            if (input[0] == "Exit")
            {
                Environment.Exit(0);
            }
            try
            {
                string result = string.Empty;

                if (input[0] == "AddCyberAttack")
                {
                    string attackType = input[1];
                    string attackName = input[2];
                    int severityLevel = int.Parse(input[3]);
                    string extraParam = input[4];

                    result = controller.AddCyberAttack(attackType, attackName, severityLevel, extraParam);
                }
                else if (input[0] == "AddDefensiveSoftware")
                {
                    string softwareType = input[1];
                    string softwareName = input[2];
                    int effectiveness = int.Parse(input[3]);

                    result = controller.AddDefensiveSoftware(softwareType, softwareName, effectiveness);
                }
                else if (input[0] == "AssignDefense")
                {
                    string cyberAttackName = input[1];
                    string defensiveSoftwareName = input[2];

                    result = controller.AssignDefense(cyberAttackName, defensiveSoftwareName);
                }
                else if (input[0] == "MitigateAttack")
                {
                    string cyberAttackName = input[1];

                    result = controller.MitigateAttack(cyberAttackName);
                }
                else if (input[0] == "GenerateReport")
                {
                    result = controller.GenerateReport();
                }
                writer.WriteLine(result);
                writer.WriteText(result);
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex.Message);
                writer.WriteText(ex.Message);
            }
        }
    }

}
