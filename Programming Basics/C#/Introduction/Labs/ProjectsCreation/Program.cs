string architectName = Console.ReadLine();
int projectsCount = int.Parse(Console.ReadLine());

int requiredHours = projectsCount * 3;
Console.WriteLine($"The architect {architectName} will need {requiredHours} hours to complete {projectsCount} project/s.");