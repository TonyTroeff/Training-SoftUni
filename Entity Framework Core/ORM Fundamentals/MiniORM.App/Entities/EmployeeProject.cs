namespace MiniORM.App.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EmployeeProject
{
    [Key, ForeignKey(nameof(Employee))] public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    [Key, ForeignKey(nameof(Project))] public int ProjectId { get; set; }
    public Project Project { get; set; }
}