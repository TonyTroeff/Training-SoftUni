Dictionary<string, List<decimal>> gradesByStudent = new Dictionary<string, List<decimal>>();

int n = int.Parse(Console.ReadLine());
for (int i = 0; i < n; i++)
{
    string[] data = Console.ReadLine().Split();
    string name = data[0];
    decimal grade = decimal.Parse(data[1]);

    if (!gradesByStudent.ContainsKey(name)) gradesByStudent[name] = new List<decimal>();
    gradesByStudent[name].Add(grade);
}

foreach (var (student, grades) in gradesByStudent)
{
    string gradesInfo = string.Join(' ', grades.Select(g => g.ToString("f2")));
    Console.WriteLine($"{student} -> {gradesInfo} (avg: {grades.Average():f2})");
}