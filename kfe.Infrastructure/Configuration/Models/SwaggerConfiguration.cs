namespace kfe.Infrastructure.Configuration.Models
{
    public class SwaggerConfiguration
    {
        public string Filename { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }
        public string MyProperty { get; set; }

        public SwaggerContactInformation Contact { get; set; }
    }

    public class SwaggerContactInformation
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
    }
}
