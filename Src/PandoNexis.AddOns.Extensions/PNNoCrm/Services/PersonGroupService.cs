using Litium.Accelerator.Routing;
using Litium.Customers;
using Litium.Customers.Queryable;
using Litium.Data;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Services
{
    [Service(ServiceType = typeof(PersonGroupService))]
    public class PersonGroupService
    {
        private readonly DataService _dataService;
        private readonly PersonService _personService;
        private readonly GroupService _groupService;

        public PersonGroupService(DataService dataService,
            PersonService personService,
            GroupService groupService)
        {
            _dataService = dataService;
            _personService = personService;
            _groupService = groupService;
        }

        public List<Group> GetGroups()
        {
            var result = new List<Group>();
            using (var query = _dataService.CreateQuery<Group>())
            {
                foreach (var systemId in query.ToSystemIdList())
                {
                    result.Add(_groupService.Get<Group>(systemId));
                }
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
        public int CountPersonsInGroup(Guid groupSystemId)
        {
            return GetPersonSystemIdsInGroup(groupSystemId)?.Count() ?? 0;
        }
        public List<Person> GetPersonsInGroup(Guid groupSystemId)
        {
            var result = new List<Person>();
            var personSystemIds = GetPersonSystemIdsInGroup(groupSystemId);
            foreach(var personSystemId in personSystemIds)
            {
                var person = _personService.Get(personSystemId);
                if (person !=null)
                    result.Add(person);
            }
            return result;
        }
    }
}
