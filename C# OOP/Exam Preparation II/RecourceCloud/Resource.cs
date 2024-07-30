namespace RecourceCloud
{
    public class Resource
    {
        public Resource(string name, string resourceType)
        {
            Name = name;
            ResourceType = resourceType;
        }

        public string Name { get; set; }

        public string ResourceType { get; set; }

        public bool IsTested { get; set; }
    }
}
