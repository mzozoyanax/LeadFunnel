using LeadFunnel.Domain;
using LeadFunnel.Domain.Models;
using LeadFunnel.Domain.ViewModels;
using LeadFunnel.Interface.Repositories;
using LeadFunnel.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Studio.V2.Flow;
using Twilio.Types;
using static System.Net.Mime.MediaTypeNames;

namespace LeadFunnel.Repository
{
    public class TwilioRepository : ITwilioRepository
    {
        private readonly IEntityRepository<Contacts> _contacts;
        private readonly IEntityRepository<TwilioCredential> _credentials;
        private readonly IEntityRepository<TwilioWorkflow> _workflow;
        private readonly IEntityRepository<TwilioWorkflowGroups> _workflowGroups;
        private readonly TwilioRepository _twilioRepository;

        public TwilioRepository(IEntityRepository<TwilioCredential> credentials, IEntityRepository<Contacts> contacts)
        {
            _credentials = credentials;
            _contacts = contacts;
        }

        public bool ForwardTextMessages()
        {
            try
            {
                var cred = _credentials.GetAll().FirstOrDefault();

                // Twilio Credentials
                string accountSid = cred.AccountId;
                string apiSecret = cred.ApiSecret;

                // Initialize Twilio client
                TwilioClient.Init(accountSid, apiSecret);

                // Your Twilio phone number and personal number
                string twilioPhoneNumber = cred.VirtualPhone;
                string personalNumber = cred.PersonalPhone;

                // Fetch the latest incoming SMS message
                var message = MessageResource.Read(
                    to: new PhoneNumber(twilioPhoneNumber),
                    limit: 1 // Limit to 1 message
                ).FirstOrDefault(); // Get the first (latest) message or default to null

                if (message != null)
                {
                    // Forward the latest SMS message to your personal number
                    MessageResource.Create(
                        body: message.Body,
                        from: new PhoneNumber(twilioPhoneNumber),
                        to: new PhoneNumber(personalNumber)
                    );
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;    
        }

        public bool SendTextToAllContact(MessageViewModel messageViewModel)
        {
            try
            {
                var cred = _credentials.GetAll().FirstOrDefault();

                var accountSid = cred.AccountId;
                var authToken = cred.ApiSecret;

                TwilioClient.Init(accountSid, authToken);

                var query = _contacts.GetAll();

                foreach (var item in query)
                {
                    var body = messageViewModel.Body.Replace("[name]", item.Name)
                        .Replace("[company]", item.Company)
                        .Replace("[phone]", item.Phone)
                        .Replace("[email]", item.Email);

                    var messageOptions = new CreateMessageOptions(
                      new PhoneNumber(item.Phone));
                    messageOptions.From = new PhoneNumber(cred.VirtualPhone);
                    messageOptions.Body = body;

                    var message = MessageResource.Create(messageOptions);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool SendTextToIndividualContact(MessageViewModel messageViewModel)
        {
            try
            {
                var cred = _credentials.GetAll().FirstOrDefault();

                var accountSid = cred.AccountId;
                var authToken = cred.ApiSecret;

                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                  new PhoneNumber(messageViewModel.To));
                messageOptions.From = new PhoneNumber(cred.VirtualPhone);
                messageOptions.Body = messageViewModel.Body;

                var message = MessageResource.Create(messageOptions);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> TriggerStudioFlow(RegisterViewModel registerViewModel, string flowSid)
        {
            try
            {
                var cred = _credentials.GetAll().FirstOrDefault();

                // Twilio Account SID and Auth Token
                string accountSid = cred.AccountId;
                string authToken = cred.ApiSecret;
                string virtualPhone = cred.VirtualPhone;
                string realPhone = cred.PersonalPhone;

                TwilioClient.Init(accountSid, authToken);
                
                string studioFlowSid = flowSid;

                var message = MessageResource.Create(
                    to: new PhoneNumber(virtualPhone),
                    from: new PhoneNumber(virtualPhone),
                    body: "Hello from C#");
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<MessageViewModel> TwilioTextMessages()
        {
            List<MessageViewModel> messageViewModels = new List<MessageViewModel>();

            try
            {
                var cred = _credentials.GetAll().FirstOrDefault();

                // Twilio Credentials
                string accountSid = cred.AccountId;
                string apiSecret = cred.ApiSecret;

                // Initialize Twilio client
                TwilioClient.Init(accountSid, apiSecret);

                // Your Twilio phone number
                string twilioPhoneNumber = cred.VirtualPhone;

                // Fetch all the messages received on your Twilio number
                var messages = MessageResource.Read(
                    to: new PhoneNumber(twilioPhoneNumber)
                );

                foreach (var message in messages)
                {
                    MessageViewModel messageView = new MessageViewModel()
                    {
                        Sid = message.Sid.ToString(),
                        From = message.From.ToString(),
                        To = message.To.ToString(),
                        Body = message.Body.ToString(),
                        DateSent = message.DateSent,
                    };

                    messageViewModels.Add(messageView);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return messageViewModels;
        }


        public bool ActiveReply()
        {
            var texts = _twilioRepository.TwilioTextMessages().FirstOrDefault();
            var contact = _contacts.GetAll().LastOrDefault();

            var result = (texts.From == contact.Phone);

            return result;
        }

        public bool RunWorkflow(int Id)
        {
            try
            {
                var workflowGroups = _workflowGroups.GetAll().Where(x => x.GroupId == Id).FirstOrDefault();

                if (workflowGroups != null)
                {
                    var workflow = _workflow.GetAll().Where(x => x.GroupId == workflowGroups.GroupId);

                    foreach (var w in workflow)
                    {
                        var replied = ActiveReply();

                        if (w.Reply == replied)
                        {
                            SendSimpleTextMessage(w.InitialMessage);
                        }
                        else
                        {
                            SendSimpleTextMessage(w.FollowupMessage);

                            Thread.Sleep(w.Delay);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool SendSimpleTextMessage(string Message)
        {
            try
            {
                var contact = _contacts.GetAll().LastOrDefault();

                MessageViewModel messageViewModel = new MessageViewModel()
                {
                    Sid = "",
                    From = _credentials.GetAll().FirstOrDefault().VirtualPhone,
                    To = "",
                    Body = Message.Replace("[name]", contact.Name)
                                .Replace("[company", contact.Company)
                                .Replace("[phone]", contact.Phone)
                                .Replace("[email]", contact.Email)
                };

                _twilioRepository.SendTextToAllContact(messageViewModel);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
