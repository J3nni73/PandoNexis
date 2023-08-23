using Litium.Accelerator.Utilities;
using Litium.Customers;
using Litium.Customers.Queryable;
using Litium.Data;
using Litium.Data.Queryable;
using Litium.Runtime.DependencyInjection;
using Litium.Websites;
using PandoNexis.AddOns.PNPilot.Constants;
using PandoNexis.AddOns.PNPilot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNPilot.Services
{
    [Service(ServiceType = typeof(PilotUserService))]
    public class PilotUserService
    {
        private readonly PersonService _personService;
        private readonly PersonStorage _personStorage;
        private readonly DataService _dataService;
        private readonly GroupService _groupService;
        private readonly OrganizationService _organizationService;

        public PilotUserService(PersonService personService,
                                PersonStorage personStorage,
                                DataService dataService,
                                GroupService groupService)
        {
            _personService = personService;
            _personStorage = personStorage;
            _dataService = dataService;
            _groupService = groupService;
        }

        public List<Person> GetAvailablePersons(Website website)
        {
            var group = _groupService.Get<Group>(website.Fields.GetValue<Guid>(PilotFieldNameConstants.PilotAdminGroup));
            if (group == null)
                return new List<Person>();
            var persons = GetPersonsInGroup(group.SystemId);
            if (_personStorage?.CurrentSelectedOrganization != null)
            {
                var personsInOrgs = GetPersonsInOrganizationAndParent(_personStorage.CurrentSelectedOrganization.SystemId);
                foreach(var person in personsInOrgs)
                {
                    if (!persons.Contains(person))
                    {
                        persons.Add(person);
                    }
                }
            }
            return persons;
        }
        public Guid GetParentOrganizationSystemId(Guid organizationSystemId)
        {
            var result = Guid.Empty;
            using (var query = _dataService.CreateQuery<Organization>())
            {
                var q = query.Filter(filter => filter
                .Bool(boolFilter => boolFilter
                .Must(boolFilterMust => boolFilterMust
                .Field("Customer", "eq", organizationSystemId))));

                result = q.FirstOrDefault()?.SystemId??Guid.Empty;
            }
            return result;
        }
        private IList<Person> GetPersonsInOrganizationAndParent(Guid organizationSystemId)
        {
            var persons = _personService.GetByOrganization(organizationSystemId).ToList();
            var parentSystemId = GetParentOrganizationSystemId(organizationSystemId);
            if (parentSystemId != Guid.Empty)
            {
                var personsInParent = GetPersonsInOrganizationAndParent(parentSystemId);
                foreach (var person in personsInParent)
                {
                    if (persons.FirstOrDefault(i=>i.SystemId==person.SystemId)==null)
                    {
                        persons.Add(person);
                    }
                }
            }

            return persons;
        }

        public List<Person> GetPersonsInGroup(Guid groupSystemId)
        {
            var result = new List<Person>();
            var personSystemIds = GetPersonSystemIdsInGroup(groupSystemId);
            foreach (var personSystemId in personSystemIds)
            {
                var person = _personService.Get(personSystemId);
                if (person != null)
                    result.Add(person);
            }
            return result;
        }
        public List<Guid> GetPersonSystemIdsInGroup(Guid groupSystemId)
        {
            var result = new List<Guid>();
            using (var query = _dataService.CreateQuery<Person>())
            {
                query.Filter(i => i.ContainsInGroup(groupSystemId));
                var personSystemIds = new HashSet<Guid>(query.ToSystemIdList());
                result = personSystemIds.ToList();
            }
            return result;
        }

    }
}
