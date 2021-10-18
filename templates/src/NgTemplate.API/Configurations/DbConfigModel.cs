namespace NgTemplate.API.Configurations
{
     public class DbConfigModel
    {
        public string ConnectionString { get; set; }

        public int MajorVersion { get; set; }

        public int MinorVersion { get; set; }

        public int Build { get; set; }
    }
}