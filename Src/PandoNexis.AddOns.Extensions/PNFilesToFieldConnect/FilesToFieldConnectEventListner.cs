using Litium.Accelerator.Caching;
using Litium.Events;
using Litium.Media;
using Litium.Media.Events;
using Litium.Runtime;
using Litium.Runtime.DependencyInjection;

namespace PandoNexis.AddOns.Extensions.PNFilesToFieldConnect
{
    [Autostart]
    [Service(ServiceType = typeof(ProductCertificateEventListner), Lifetime = DependencyLifetime.Singleton)]
    public  class ProductCertificateEventListner
    {
        public readonly FilesToFieldConnectService _filesToFieldConnectService;
        public readonly FolderService _folderService;
        public ProductCertificateEventListner(EventBroker eventBroker,
            FolderService folderService,
            FilesToFieldConnectService filesToFieldConnectService)
        {
            eventBroker.Subscribe<FileCreated>(x => SyncFilesAndFieldOptions(x.Item));
            eventBroker.Subscribe<FileUpdated>(x => SyncFilesAndFieldOptions(x.Item));
            eventBroker.Subscribe<FileDeleted>(x => SyncFilesAndFieldOptions(x.Item));
            _folderService = folderService;
            _filesToFieldConnectService = filesToFieldConnectService;
        }

        private void SyncFilesAndFieldOptions(Litium.Media.File file)
        {
            var folder = _folderService.Get(file.FolderSystemId);
            if (folder == null)
                return;
            var mainFolder = _folderService.Get(FilesToFieldConnectConstants.FilesToFieldConnectMainFolder);
            if (mainFolder==null || folder.ParentFolderSystemId != mainFolder.SystemId)
                return;

           _filesToFieldConnectService.InitiateProductCertificateService(folder.Name);


        }
    }
}
