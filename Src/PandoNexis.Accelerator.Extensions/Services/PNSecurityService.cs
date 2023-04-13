using Litium.Customers;
using Litium.Media;
using Litium.Runtime.DependencyInjection;
using Litium.Security;

namespace PandoNexis.Accelerator.Extensions.Services
{
    [Service(ServiceType = typeof(PNSecurityService), Lifetime = DependencyLifetime.Scoped)]
    [RequireServiceImplementation]
    public class PNSecurityService
    {
        private readonly FileService _fileService;
        private readonly PersonService _personService;

        public PNSecurityService(FileService fileService, PersonService personService)
        {
            _fileService = fileService;
            _personService = personService;
        }

        public bool HasFileAccess(Person person = null, object value = null)
        {
            if (value == null || !(value is Guid))
            {
                return false;
            }
            var file = _fileService.Get((Guid)value);
            var everyone = _personService.Get(SecurityContextService.Everyone.Id);

            if (file.AccessControlList.Any(x => x.Operation == Operations.Entity.Read && x.GroupSystemId == everyone.SystemId))
            {
                return true;
            }

            var groupLinks = person.GroupLinks;

            foreach (var group in groupLinks)
            {
                if (file.AccessControlList.Any(x => x.Operation == Operations.Entity.Read && x.GroupSystemId == group.GroupSystemId))
                    return true;
            }

            return false;
        }
    }
}
