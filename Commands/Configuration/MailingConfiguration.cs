using System.ComponentModel.DataAnnotations;

namespace Commands.Configuration;
public class MailingConfiguration
{
    public const string SectionName = "Mailing";

    [Required]
    public MailServerConfiguration MailServer { get; set; } = default!;

    [Required]
    public SenderConfiguration Sender { get; set; } = default!;

    public class MailServerConfiguration
    {
        [Required(AllowEmptyStrings = false)]
        public string Host { get; set; } = default!;

        [Range(1, 63535)]

        public int Port { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public bool HaveCredentials => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);

        public bool EnableSsl { get; set; } = false;
    }

    public class SenderConfiguration
    {
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; } = default!;

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = default!;
    }
}
