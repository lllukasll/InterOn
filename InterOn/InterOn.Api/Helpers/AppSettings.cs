namespace InterOn.Api.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string AccessExpireMinutes { get; set; }

        public string RefreshExpireMinutes { get; set; }
    }
}
