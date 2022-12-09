using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using Litium.Blobs;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Media;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Web.Models;
using Litium.Web.Models.Products;
using System.Drawing;
using File = Litium.Media.File;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Services
{

    [Service(ServiceType = typeof(ProductImageService), Lifetime = DependencyLifetime.Singleton)]
    public class ProductImageService
    {
        public const string ModuleName = "media";
        private const string _folderName = "uploaded_images";

        private readonly VariantService _variantService;
        private readonly ProductModelBuilder _productModelBuilder;
        private readonly BaseProductService _baseProductService;
        private readonly SecurityContextService _securityContextService;
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly FolderService _folderService;
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly FileService _fileService;
        private readonly BlobService _blobService;
        private readonly FileMetadataExtractorService _fileMetadataExtractorService;
        private readonly GroupService _groupService;
        private readonly LanguageService _languageService;


        public ProductImageService(VariantService variantService,
            ProductModelBuilder productModelBuilder,
            BaseProductService baseProductService, SecurityContextService securityContextService, FolderService folderService, FieldDefinitionService fieldDefinitionService, FieldTemplateService fieldTemplateService, FileService fileService, BlobService blobService, GroupService groupService, LanguageService languageService)
        {
            _variantService = variantService;
            _productModelBuilder = productModelBuilder;
            _baseProductService = baseProductService;
            _securityContextService = securityContextService;
            _folderService = folderService;
            _fieldDefinitionService = fieldDefinitionService;
            _fieldTemplateService = fieldTemplateService;
            _fileService = fileService;
            _blobService = blobService;
            _groupService = groupService;
            _languageService = languageService;
        }

        public async Task<object> RemoveImageFromVariant(string entitySystemId, string imageId)
        {
            //var item = JsonConvert.DeserializeObject<JObject>(imageId);
            var variant = _variantService.Get(new Guid(entitySystemId));
            //var variant = _variantService.Get(new Guid(entitySystemId))?.MakeWritableClone();
            if (variant == null)
                return null;

            variant = variant.MakeWritableClone();
            // Start Get Images
            var imageIds = (variant?.Fields.GetValue<IList<Guid>>(SystemFieldDefinitionConstants.Images)).Where(x => x != new Guid(imageId));
            //var newList = new List<GenericGridViewFieldSimpleList>();
            if (imageIds != null)
            {
                using (_securityContextService.ActAsSystem("ProductImageService.RemoveImageFromVariant"))
                {
                    // image ids is a list of Guid that contains all the image ids that will be connected to the variant
                    variant.Fields.AddOrUpdateValue(SystemFieldDefinitionConstants.Images, imageIds);
                    _variantService.Update(variant);
                }
                return new object();
            }
            return null;
        }
        public async Task<GenericGridViewFieldSimpleList> AddImageFromVariant(string entitySystemId, string imageData, string imageName)
        {
            var folderTemplateId = _fieldTemplateService.GetAll().OfType<FolderFieldTemplate>().FirstOrDefault();
            var folderFieldTemplate = _fieldTemplateService.Get<FolderFieldTemplate>(folderTemplateId.SystemId);
            var parentFoldertest = _folderService.GetChildFolders(Guid.Empty);
            var folder = GetOrCreateFolder(_folderName)?.MakeWritableClone();

            if (folder != null)
            {
                var getImageIfExisting = _fileService.GetByFolder(folder.SystemId).FirstOrDefault(x => x.Name?.ToLower() == imageName.ToLower());
                if (getImageIfExisting == null)
                {
                    long fileSize = 0;
                    var match = System.Text.RegularExpressions.Regex.Match(imageData, @"data:image/(?<type>.+?),(?<data>.+)");
                    var base64Data = match.Groups["data"].Value;
                    var contentType = match.Groups["type"].Value;
                    var binData = Convert.FromBase64String(base64Data);
                    var template = _fieldTemplateService.FindFileTemplate(contentType.Split(";")[0]);
                    var blobContainer = _blobService.Create(File.BlobAuthority);
                    File file;
                    using (var stream = new MemoryStream(binData))
                    {
                        using (var blobStream = blobContainer.GetDefault().OpenWrite())
                        {
                            await stream.CopyToAsync(blobStream);
                            fileSize = stream.Length;

                            file = new File(template.SystemId, folder.SystemId, blobContainer.Uri, imageName)
                            {
                                FileSize = fileSize,
                                LastWriteTimeUtc = DateTimeOffset.UtcNow,
                            };
                            file.AccessControlList = folder.AccessControlList;

                        }
                        file = file.MakeWritableClone();
                        using (_securityContextService.ActAsSystem("ProductImageService.AddImageFromVariant"))
                        {
                            _fileService.Create(file);
                        }
                    }

                }
                var imageDataGuid = _fileService.GetByFolder(folder.SystemId).FirstOrDefault(x => x.Name.ToLower() == imageName.ToLower()).SystemId;

                var variant = _variantService.Get(new Guid(entitySystemId))?.MakeWritableClone();
                if (variant == null)
                    return null;

                // Start Get Images
                var imageIds = variant?.Fields.GetValue<IList<Guid>>(SystemFieldDefinitionConstants.Images);
                if (imageIds == null)
                {
                    imageIds = new List<Guid>();
                    imageIds.Add(imageDataGuid);
                }
                else
                {
                    imageIds.Add(imageDataGuid);
                }
                variant = variant.MakeWritableClone();
                using (_securityContextService.ActAsSystem("ProductImageService add image to Variant"))
                {
                    variant.Fields.AddOrUpdateValue(SystemFieldDefinitionConstants.Images, imageIds);
                    _variantService.Update(variant);
                }
                //.GetUrlToImage(Size.Empty, new Size(200, 120)).Url
                var getUrl = imageDataGuid.MapTo<ImageModel>();

                var imageInfo = new GenericGridViewFieldSimpleList { Text = $"{getUrl.Title}-id-{imageDataGuid}", Value = getUrl.GetUrlToImage(Size.Empty, new Size(200, 120)).Url };

                return imageInfo;

            }
            return null;
        }
        public static byte[] BytesFromBase64ImageString(string imageData)
        {
            var trunc = imageData.Split(',')[1];
            var padded = trunc.PadRight(trunc.Length + (4 - trunc.Length % 4) % 4, '=');
            return Convert.FromBase64String(padded);
        }

        public Folder GetOrCreateFolder(string id)
        {
            var folder = _folderService.Get(id);
            var folderTemplateSystemId = _fieldTemplateService.GetAll().OfType<FolderFieldTemplate>().FirstOrDefault();
            if (folder == null)
            {
                folder = _folderService.GetChildFolders(Guid.Empty).Where(x => x.FieldTemplateSystemId == folderTemplateSystemId.SystemId).FirstOrDefault(x => x.Name == _folderName);
            }

            if (folder != null)
            {
                return folder;
            }

            var newFolder = new Folder(folderTemplateSystemId.SystemId, id);
            newFolder.Id = id;
            //newFolder.ParentFolderSystemId = _folderService.GetChildFolders(Guid.Empty).First().SystemId;

            var visitorGroupSystemId = (_groupService.Get<Group>("Visitors") ?? _groupService.Get<Group>("Besökare"))?.SystemId ?? Guid.Empty;
            if (visitorGroupSystemId != Guid.Empty)
            {
                newFolder.AccessControlList.Add(new AccessControlEntry(Operations.Entity.Read, visitorGroupSystemId));
                //newFolder.AccessControlList.Add(new AccessControlEntry(Operations.Entity.Write, visitorGroupSystemId));
            }
            using (_securityContextService.ActAsSystem("ProductImageService.GetOrCreateFolder"))
            {
                _folderService.Create(newFolder);
            }
            folder = newFolder;

            //folder = _folderService.Get(id);
            return folder;
        }

        //private void GetOrCreateFile(string bitmapImage)
        //{
        //    var file = _fileService.Get(bitmapImage);
        //    if (file != null)
        //    {
        //        var blob = _blobService.Get(file.BlobUri);
        //        if (blob != null)
        //        {
        //            var blobData = blob.GetDefault();
        //            if (blobData != null)
        //            {
        //                using var stream = blobData.OpenRead();
        //                using (var image = new MagickImage(stream))
        //                {
        //                    //var geometry = new MagickGeometry
        //                    //{
        //                    //    Width = data.width,
        //                    //    Height = data.height,
        //                    //    X = data.x,
        //                    //    Y = data.y
        //                    //};
        //                    //image.Crop(geometry);
        //                    //image.Format = MagickFormat.Png;
        //                    //image.RePage();
        //                    var newBlob = _blobService.Create(File.BlobAuthority);

        //                    using (var blobStream = newBlob.GetDefault().OpenWrite())
        //                    {
        //                        image.Write(blobStream);
        //                    }
        //                    var imageSystemId = Guid.NewGuid();
        //                    var newFile = new File(imageFieldTemplate.SystemId, catalogFolder.SystemId, newBlob.Uri, file.Name);
        //                    newFile.SystemId = imageSystemId;
        //                    newFile.FileSize = image.ToByteArray().Length;
        //                    //newFile.SetImageDimension(new Size(data.width, data.height));
        //                    int suffixNumber = 1;
        //                    while (!_validationService.Validate(newFile, ValidationMode.Add).Succeeded && suffixNumber < 1000)
        //                    {
        //                        newFile.Name = suffixNumber + "_" + newFile.Name;
        //                        suffixNumber++;
        //                    }
        //                    _fileService.Create(newFile);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
