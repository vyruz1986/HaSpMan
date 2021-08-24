namespace Web.Configuration
{
    public class OidcConfig
    {
        public const string SectionName = "Oidc";
        public string? Authority { get; set; }
        public string? Audience { get; set; }
        public string? ClientSecret { get; set; }
    }
}