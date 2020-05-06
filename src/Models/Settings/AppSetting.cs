namespace MovieWeb.WebApi.Model
{
    using System.Collections.Generic;

    public partial class TokenSetting : ITokenSetting
    {
        public string AudienceId { get; set; }

        public string AudienceSecret { get; set; }

        public string AudiencyIssuer { get; set; }

        public string ExpireTime { get; set; }
    }

    public partial class EmailSetting : IEmailSetting
    {
        public string NameCompany { get; set; }

        public List<Contacts> ContactCompany { get; set; }

        public string NameServer { get; set; }

        public string MailServer { get; set; }

        public int MailServerPort { get; set; }
       
        public string Password { get; set; }

        public string SmtpClient { get; set; }
    }

    public partial class Contacts
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }

    public interface ITokenSetting
    {
        string AudienceId { get; set; }

        string AudienceSecret { get; set; }

        string AudiencyIssuer { get; set; }

        string ExpireTime { get; set; }
    }

    public interface IEmailSetting
    {
        string NameCompany { get; }

        List<Contacts> ContactCompany { get; set; }

        string NameServer { get; }

        string MailServer { get; }

        int MailServerPort { get; }

        string Password { get; }

        string SmtpClient { get; }
    }
}
