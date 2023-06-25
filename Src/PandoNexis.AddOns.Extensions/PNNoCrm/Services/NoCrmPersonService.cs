using Litium.Accelerator.Constants;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;
using System;
using System.Collections.Generic;
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

        public NoCrmPersonService(FieldTemplateService fieldTemplateService, 
                                    SecurityContextService securityContextService, 
                                    PersonService personService)
        {
            _fieldTemplateService = fieldTemplateService;
            _securityContextService = securityContextService;
            _personService = personService;
        }

        public bool CreatePerson(GenericDataViewResponse dataViewResponse)
        {
            var template = _fieldTemplateService.Get<PersonFieldTemplate>(typeof(CustomerArea), DefaultWebsiteFieldValueConstants.CustomerTemplateId);
            if (template==null)return false;
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

        }
        public void CreateLogin(string entitySystemId)
        {
            if (Guid.TryParse(entitySystemId, out Guid id))
                CreateLogin(id);

            
        }
    }
}
