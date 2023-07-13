using DocumentFormat.OpenXml.Office2010.CustomUI;
using DocumentFormat.OpenXml.Presentation;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Mailing;
using Litium.Accelerator.Services;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using PandoNexis.AddOns.Extensions.PNNoCrm.Definitions;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNNoCrm.Services
{
    [Service(ServiceType = typeof(NoCrmPersonService))]
    public class NoCrmPersonService
    {
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly SecurityContextService _securityContextService;
        private readonly PersonService _personService;
        private readonly GenericButtonService _genericButtonService;
        private readonly MailService _mailService;
        private readonly NoCrmPersonGroupService _noCrmPersonGroupService;

        public NoCrmPersonService(FieldTemplateService fieldTemplateService,
                                    SecurityContextService securityContextService,
                                    PersonService personService,
                                    GenericButtonService genericButtonService,
                                    MailService mailService,
                                    NoCrmPersonGroupService noCrmPersonGroupService)
        {
            _fieldTemplateService = fieldTemplateService;
            _securityContextService = securityContextService;
            _personService = personService;
            _genericButtonService = genericButtonService;
            _mailService = mailService;
            _noCrmPersonGroupService = noCrmPersonGroupService;
        }

        public bool CreatePerson(GenericDataViewResponse dataViewResponse)
        {
            var template = _fieldTemplateService.Get<PersonFieldTemplate>(typeof(CustomerArea), DefaultWebsiteFieldValueConstants.CustomerTemplateId);
            if (template == null) return false;
            var person = new Person(template.SystemId);
            person.SystemId = Guid.NewGuid();
            foreach (var field in dataViewResponse.Form)
            {
                person.Fields.AddOrUpdateValue(field.Key, field.Value);
            }
            person.Fields.AddOrUpdateValue(RegisterMeConstants.AddedByRegisterMeForm, true);

            using (_securityContextService.ActAsSystem())
            {
                _personService.Create(person);
                return true;
            }
        }

        public void CreateLogin(Guid systemId)
        {
            var person = _personService.Get(systemId).MakeWritableClone();
            var userName = person.Email;
            person.LoginCredential.Username = userName;

            using (_securityContextService.ActAsSystem())
            {
                _personService.Update(person);
            }
            ResetPassword(person, true);
        }
        public void CreateLogin(string entitySystemId)
        {
            if (Guid.TryParse(entitySystemId, out Guid id))
                CreateLogin(id);
        }
        public void ResetPassword(Person person, bool isNewPerson = false)
        {
            person = person.MakeWritableClone();
            if (person == null) return;

            var password = "ChangePasswordNow2023";

            person.LoginCredential.NewPassword = password;
            person.LoginCredential.PasswordExpirationDate = DateTime.Now;
            using (_securityContextService.ActAsSystem())
            {
                _personService.Update(person);
            }

            var message = string.Empty;
            var subject = string.Empty;
            if (isNewPerson)
            {
                subject = "Jennifers 50års fest - Nu börjar det närma sig";
                message = $"<html><body><p>Du har fått ett inlogg till <a href=\"https://www.jennifer50.se/Logga-in?RedirectUrl=fest0707\">www.jennifer50.se</a> <p>Användarnamn = {person.LoginCredential.Username}</p> <p>Lösenord = {password}</p> <p>Du kommer bli ombedd att byta lösenord när du loggar in första gången du loggar in.</p><p>Detaljer om festen kommer att publiceras bakom inlogg på siten.</p><h3>Med vänliga hälsningar</h3><h1>Jennifer Sköld</h1></body></html>";
            }
            else
            {
                subject = "Jennifers 50års fest - Ditt lösenord är återställt";
                message = $"<html><body><p>Ditt nya lösenord är lösenord = {password} </p><p>Du måste byta lösenord när du loggar in.</p><h3>Med vänliga hälsningar</h3><h1>Jennifer Sköld</h1></body></html>";
            }

            var mailDefinition = new NoCrmMailDefinition(person.Email, subject, message, Guid.Parse("BCA2AF71-8EDE-45A4-9D6E-F153A3B597CC"));

            _mailService.SendEmail(mailDefinition, false);
        }
        public void ResetPassword(Guid systemId)
        {
            var person = _personService.Get(systemId);
            ResetPassword(person);
        }
        public void ResetPassword(string entitySystemId)
        {
            if (Guid.TryParse(entitySystemId, out Guid id))
                ResetPassword(id);
        }
        public GenericButton GetAddLoginButton(Website website, Person person)
        {
            if (_personService.Get(_securityContextService.GetIdentityUserSystemId().GetValueOrDefault()) == null || string.IsNullOrEmpty(person.LoginCredential.Username))
                return _genericButtonService.GetButton(website, NoCrmProcessorConstants.NoCrmButtonLinks, NoCrmProcessorConstants.AddLogin, NoCrmProcessorConstants.NoCrmButtonNames, person.SystemId);
            else
                return _genericButtonService.GetButton(website, NoCrmProcessorConstants.NoCrmButtonLinks, NoCrmProcessorConstants.ResetPassword, NoCrmProcessorConstants.NoCrmButtonNames, person.SystemId);

        }
        public List<string> GetEmailAddresses(Guid systemId, bool useUserName = false)
        {
            var result = new List<string>();
            var persons = new List<Person>();
            var p = _personService.Get(systemId);
            if (p != null) persons.Add(p);
            if (!persons.Any())
            {
                var personsInGroup = _noCrmPersonGroupService.GetPersonsInGroup(systemId);
                if (personsInGroup != null && personsInGroup.Any())
                    persons.AddRange(personsInGroup);
            }
            //ToDo: add for persons in organization

            if (persons.Any())
            {
                foreach (var person in persons)
                {
                    if (useUserName)
                    {
                        if (!result.Contains(person.LoginCredential.Username) && IsValidEmail(person.LoginCredential.Username))
                        {
                            result.Add(person.LoginCredential.Username);
                        }
                    }
                    else
                    {
                        if (!result.Contains(person.Email) && IsValidEmail(person.LoginCredential.Username))
                        {
                            result.Add(person.Email);
                        }
                    }
                }
            }

            return result;
        }
        public bool IsValidEmail(string email) 
        {
            if (string.IsNullOrEmpty(email)) return false;
            if(new EmailAddressAttribute().IsValid(email))return true;
            


            return false;
        }

    }
}
