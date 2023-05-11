namespace BlogMVCWebApp.Web.Models.ViewModels
{
    public class UpdateTagRequest
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
