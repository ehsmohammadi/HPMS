namespace MITD.PMS.Common.Utilities
{
    public interface IEmailManager
    {
        void SendEmail(string subject,string emailAddress, string emailContent);
        void SendVerificationEmail(string lastName, string emailAddress, string verificationCode);
    }
}
