using EmailSender.Model;
using EmailSender.Repository.EmailSenderRepo;
using EmailSender.Service.EmailSenderService;
using EmailSender.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmailSender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailSenderController : ControllerBase
    {
        private readonly ILogger<EmailSenderController> _logger;
        private readonly IEmailSenderRepository _emailSender;
        private readonly IEmailSenderService _emailSenderService;

        public EmailSenderController(ILogger<EmailSenderController> logger, IEmailSenderRepository emailSender, IEmailSenderService emailSenderService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _emailSenderService = emailSenderService;

        }
        public EmailSenderResponse Get()
        {
            var response = new EmailSenderResponse();
            response.GetList = _emailSender.Get();
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<DefaultResponse>> PostAsync([FromBody] EmailSenderRequestModel data)
        {
            var response = new DefaultResponse();
            try
            {
                foreach (var d in data.EmailSenderInputs)
                {
                    await _emailSenderService.LogBlobAsync(d);
                    var senderData = new EmailSenderData();
                    var attributes = "";
                    var IsEmailValid = _emailSenderService.IsValidEmail(d.Email);
                    if (IsEmailValid == false)
                    {
                        response.StatusCode = 400;
                        response.StatusMessage = "Email is not valid. " + d.Email;
                        response.Success = false;
                        return response;
                    }
                    foreach (var a in d.Attributes)
                    {
                        var isDuplicate = _emailSenderService.FindDuplicate(a, d.Email);
                        if (isDuplicate == false)
                            attributes = attributes + "," + a;
                    }
                    senderData.Attributes = attributes;
                    senderData.Email = d.Email;
                    senderData.Key = d.Key;
                    senderData.AddedOn = DateTime.Now;

                    _emailSender.Insert(senderData);
                }
                response.StatusMessage = "Success";
                response.Success = true;
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.StatusMessage = ex.Message;
                response.Success = false;
                response.StatusCode = 505;
                return response;
            }
            return response;
        }
    }
}
