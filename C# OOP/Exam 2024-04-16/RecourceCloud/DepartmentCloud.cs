namespace RecourceCloud
{
    public class DepartmentCloud
    {
        private readonly List<Task> tasks;
        private readonly List<Resource> resources;

        public DepartmentCloud()
        {
            tasks = new List<Task>();
            resources = new List<Resource>();
        }

        public IReadOnlyCollection<Task> Tasks => tasks.AsReadOnly();

        public IReadOnlyCollection<Resource> Resources => resources.AsReadOnly();

        public string LogTask(string[] args)
        {
            if(args.Length != 3)
            {
                throw new ArgumentException("All arguments are required.");
            }

            if(args.Any(a => a == null))
            {
                throw new ArgumentException("Arguments values cannot be null.");
            }

            Task task = new Task(int.Parse(args[0]), args[1], args[2]);

            if(tasks.Any(t => t.ResourceName == task.ResourceName))
            {
                return $"{task.ResourceName} is already logged.";
            }

            tasks.Add(task);

            return $"Task logged successfully.";
        }

        public bool CreateResource()
        {
            var task = tasks.OrderBy(t => t.Priority).FirstOrDefault();

            if(task == null)
            {
                return false;
            }

            Resource resource = new Resource(task.ResourceName, task.Label);

            resources.Add(resource);
            tasks.Remove(task);
            return true;
        }

        public Resource? TestResource(string name)
        {
            var resource = resources.FirstOrDefault(t => t.Name == name);

            if(resource == null)
            {
                return null;
            }

            resource.IsTested = true;
            return resource;
        }
    }
}
