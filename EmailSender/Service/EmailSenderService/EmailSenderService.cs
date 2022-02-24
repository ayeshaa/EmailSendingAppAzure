using EmailSender.Model;
using EmailSender.Repository.EmailSenderRepo;
using EmailSender.Repository.EmailTemplateRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EmailSender.Service.EmailSenderService
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration configuration;
        private readonly IEmailSenderRepository _emailSender;
        private readonly IEmailTemplateRepository _emailTemplateRepo;
        public EmailSenderService(IConfiguration configuration, IEmailSenderRepository emailSender, IEmailTemplateRepository emailTemplateRepo)
        {
            this.configuration = configuration;
            this._emailSender = emailSender;
            this._emailTemplateRepo = emailTemplateRepo;
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool FindDuplicate(string attr, string email)
        {
            var emailData = _emailSender.Get();
            foreach (var e in emailData)
            {
                if (e.Attributes.Contains(attr) && e.AddedOn.Value.Date == System.DateTime.Now.Date && e.Email == email)
                    return true;
            }
            return false;
        }

        public async Task LogBlobAsync(EmailSenderInput data)
        {
            var dirPath = Directory.GetCurrentDirectory();
            var localPath = dirPath + "/data";
            string fileName = "emailBlobFile" + Guid.NewGuid().ToString() + ".txt";
            string localFilePath = Path.Combine(localPath, fileName);
            var storageConnectionString = this.configuration["AzureStorageConnectionString"];
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'quickstartblobs' and 
                // append a GUID value to it to make the name unique.
                CloudBlobContainer cloudBlobContainer =
                    cloudBlobClient.GetContainerReference("quickstartblobs" +
                        Guid.NewGuid().ToString());
                await cloudBlobContainer.CreateAsync();

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await cloudBlobContainer.SetPermissionsAsync(permissions);

                // Create a file in your local MyDocuments folder to upload to a blob.
                string localFileName = "EmailSender_" + Guid.NewGuid().ToString() + ".txt";
                string sourceFile = Path.Combine(localPath, localFileName);
                // Write text to the file.
                var DataToWrite = "Key: " + data.Key + "\n";
                DataToWrite = DataToWrite + "Email: " + data.Email + "\n";
                foreach (var a in data.Attributes)
                {
                    DataToWrite = DataToWrite + "Attribute: " + a + "\n";
                }
                DataToWrite = DataToWrite + "\n";
                File.WriteAllText(sourceFile, DataToWrite);

                // Get a reference to the blob address, then upload the file to the blob.
                // Use the value of localFileName for the blob name.
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
                await cloudBlockBlob.UploadFromFileAsync(sourceFile);
            }
            else
            {
                // Otherwise, let the user know that they need to define the environment variable.
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add an environment variable named 'AZURE_STORAGE_CONNECTION_STRING' with your storage " +
                    "connection string as a value.");
                Console.WriteLine("Press any key to exit the application.");
                Console.ReadLine();
            }
        }
        public string FindUniqueAttributes(string email)
        {
            var uAttr = "";
            int count = 0;
            var emailData = _emailSender.Get();
            foreach (var e in emailData)
            {
                if (e.AddedOn.Value.Date == System.DateTime.Now.Date && e.Email == email)
                {
                    var attributes = e.Attributes.Split(",");
                    foreach (var a in attributes)
                    {
                        if (!uAttr.Contains(a))
                        {
                            uAttr = uAttr + ", " + a;
                            count++;
                        }
                    }
                }
            }
            if (count >= 10)
                return uAttr;
            else return "";
        }
        public void PrepareEmailsForUsers()
        {
            var usersEmail = GetUserEmails();

            foreach (var u in usersEmail)
            {
                var emailTemplate = new EmailTemplates();
                var UniqueAttributesbyUsers = FindUniqueAttributes(u);
                if (UniqueAttributesbyUsers != null && UniqueAttributesbyUsers != "")
                {
                    emailTemplate.SentBy = u;
                    emailTemplate.SentOn = DateTime.Now;
                    emailTemplate.Body = "Congratulate!\n We have received following unique attributes from you: " + UniqueAttributesbyUsers +
                     "\n" + "Best regards, Millisecond";
                    _emailTemplateRepo.Insert(emailTemplate);
                }
            }
        }
        public List<string> GetUserEmails()
        {
            var emailData = _emailSender.Get();
            List<string> Users = new List<string>();
            foreach (var u in emailData)
            {
                if (!Users.Contains(u.Email))
                    Users.Add(u.Email);
            }
            return Users;
        }

    }
}
