namespace RecourceCloud
{
    public class Task
    {
        public Task(int priority, string label, string details)
        {
            Priority = priority;
            Label = label;
            ResourceName = details;
        }

        public int Priority{ get; set; }

        public string Label { get; set; }

        public string ResourceName { get; set; }
    }
}
