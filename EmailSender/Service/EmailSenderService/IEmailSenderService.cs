using EmailSender.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailSender.Service.EmailSenderService
{
    public interface IEmailSenderService
    {
        bool IsValidEmail(string email);
        bool FindDuplicate(string attr, string email);
        Task LogBlobAsync(EmailSenderInput data);
        void PrepareEmailsForUsers();
        string FindUniqueAttributes(string email);
        List<string> GetUserEmails();
    }
}
