using MiniORM.App;
using MiniORM.App.Entities;

const string connectionString = "Server=.;Database=MiniORM;User ID=sa;Password=123456-Aa;TrustServerCertificate=True";
var dbContext = new SoftUniDbContext(connectionString);

// Add
// dbContext.Projects.Add(new Project { Name = "MiniORM" });

// Remove
var projectToDelete = dbContext.Projects.FirstOrDefault(x => x.Name == "MiniORM");
if (projectToDelete is not null) dbContext.Projects.Remove(projectToDelete);

// Update
var employeesToUpdate = dbContext.Employees.Where(e => e.LastName == "Taylor" || !e.IsEmployed).ToList();
foreach (var employee in employeesToUpdate) employee.LastName = "Smith";

dbContext.SaveChanges();
