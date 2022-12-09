
using Litium.Media;
using Litium.Runtime.AutoMapper;
using PandoNexis.Accelerator.Extensions.ViewModel.Media;
using Litium.Web.Administration.WebApi.Media.ViewModels;
using JetBrains.Annotations;
using Litium.Accelerator.Builders.Product;
using Litium.Runtime.DependencyInjection;

namespace PandoNexis.Accelerator.Extensions.Builders.Media
{
    [UsedImplicitly]
    [Service(ServiceType = typeof(PNMediaViewModelBuilder), Lifetime = DependencyLifetime.Scoped)]
    public class PNMediaViewModelBuilder
    {
        private readonly FolderService _folderService;
        private readonly FileService _fileService;
        public PNMediaViewModelBuilder(FolderService folderService, FileService fileService)
        {
            _folderService = folderService;
            _fileService = fileService;
        }
        public MediaFilesAndFolders Build(Guid folderId)
        {
            var topFolder = _folderService.Get(folderId);
            return CreateStructure(topFolder);
        }

        private MediaFilesAndFolders CreateStructure(Folder folder)
        {
            var childFolders = _folderService.GetChildFolders(folder.SystemId);
            List<MediaFilesAndFolders> filesAndFolders = null;
            if (childFolders != null && childFolders.Count() > 0)
            {
                filesAndFolders = new List<MediaFilesAndFolders>();
                foreach (var folderItem in childFolders)
                {
                    filesAndFolders.Add(CreateStructure(folderItem));
                }
            }

            var folderFiles = _fileService.GetByFolder(folder.SystemId);
            var fileModels = folderFiles?.Select(x => x.MapTo<FileModel>()).Where(x => x.Readable == true).ToList();

            var files = fileModels.ConvertAll(x => new MediaCatalogFileData
            {
                Name = x.Name,
                Extension = x.Extension,
                FrameHeight = x.FrameHeight,
                FrameWidth = x.FrameWidth,
                FileSize = x.FileSize,
                IconCssClass = x.IconCssClass,
                DownloadUrl = x.DownloadUrl,
                LargeThumbnailUrl = x.LargeThumbnailUrl,
                MediumThumbnailUrl = x.MediumThumbnailUrl,
                SmallThumbnailUrl = x.SmallThumbnailUrl,
            });

            return new MediaFilesAndFolders
            {
                Files = files,
                FolderData = filesAndFolders,
                FolderName = folder.Name,
            };

            return null;
        }

    }
}
