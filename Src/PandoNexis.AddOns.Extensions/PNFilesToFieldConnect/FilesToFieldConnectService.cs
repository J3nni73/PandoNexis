using Litium.Media;
using Litium.Runtime.DependencyInjection;
using Litium.FieldFramework;
using Litium.Products;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Security;
using Litium.Web.Administration.WebApi.Media.ViewModels;
using FieldTemplateBase = Litium.Media.FieldTemplateBase;
using Litium.Customers;

namespace PandoNexis.AddOns.Extensions.PNFilesToFieldConnect
{
    [Service(ServiceType = typeof(FilesToFieldConnectService))]
    public class FilesToFieldConnectService
    {
        private readonly FolderService _folderService;
        private readonly FileService _filService;
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly LanguageService _languageService;
        private readonly SecurityContextService _securityContextService;
        private readonly GroupService _groupService;

        private Guid _visitorGroupSystemId;


        public FilesToFieldConnectService(FolderService folderService,
                                        FileService filService,
                                        FieldDefinitionService fieldDefinitionService,
                                        LanguageService languageService,
                                        SecurityContextService securityContextService,
                                        FieldTemplateService fieldTemplateService,
                                        GroupService groupService)
        {
            _folderService = folderService;
            _filService = filService;
            _fieldDefinitionService = fieldDefinitionService;
            _languageService = languageService;
            _securityContextService = securityContextService;
            _fieldTemplateService = fieldTemplateService;
            _groupService = groupService;
            _visitorGroupSystemId = (_groupService.Get<Group>("Visitors") ?? _groupService.Get<Group>("Besökare"))?.SystemId ?? Guid.Empty;
            _groupService = groupService;
        }


        public void InitiateProductCertificateService(string id)
        {
            if (CreateCertificateMediaFolder(id))
            {
                RefreshTextOptionField(id);

            }
        }
        private void RefreshTextOptionField(string folderId)
        {
            var folder = _folderService.Get(folderId);
            var files = _filService.GetByFolder(folder.SystemId);
            if (files.Any())
            {
                var field = _fieldDefinitionService.Get<ProductArea>(folderId).MakeWritableClone();
                if (field != null && field.FieldType == SystemFieldTypeConstants.TextOption)
                {
                    var options = new TextOption();

                    foreach (var file in files)
                    {
                        if (options.Items.FirstOrDefault(i => i.Value == file.SystemId.ToString()) == null)
                        {
                            var textOptionItem = new TextOption.Item();
                            textOptionItem.Value = file.SystemId.ToString();

                            var languages = _languageService.GetAll();

                            foreach (var language in languages)
                            {
                                var fileName = file.Name;
                                textOptionItem.Name.Add(language.Id, fileName);

                            }
                            options.Items.Add(textOptionItem);
                        }
                    }
                    field.Option = options;


                    using (_securityContextService.ActAsSystem("ProductCertificate task"))
                    {
                        _fieldDefinitionService.Update(field);
                    }
                }

            }
        }


        private bool CreateCertificateMediaFolder(string folderId)
        {
            var mainFoler = GetMainFolder();

            var folder = _folderService.Get(folderId);
            if (folder == null)
            {
                var templateId = _fieldTemplateService.Get<FieldTemplateBase>("DefaultFolderTemplate");

                var folderSystemId = Guid.NewGuid();
                folder = new Folder(templateId.SystemId, folderId);
                folder.Id = folderId;
                folder.ParentFolderSystemId = mainFoler.SystemId;
                folder.Name = folderId;

                using (_securityContextService.ActAsSystem("ProductCertificate task"))
                {
                    _folderService.Create(folder);
                }
            }
         
            return folder != null;
        }

        private Folder GetMainFolder()
        {
            var folder = _folderService.Get(FilesToFieldConnectConstants.FilesToFieldConnectMainFolder);
            if (folder == null)
            {
                var templateId = _fieldTemplateService.Get<FieldTemplateBase>("DefaultFolderTemplate");

                folder = new Folder(templateId.SystemId, FilesToFieldConnectConstants.FilesToFieldConnectMainFolder);
                folder.Id = FilesToFieldConnectConstants.FilesToFieldConnectMainFolder;
                folder.Name = FilesToFieldConnectConstants.FilesToFieldConnectMainFolder;
                if (_visitorGroupSystemId != Guid.Empty && folder.AccessControlList.All(i => i.GroupSystemId != _visitorGroupSystemId))
                {
                    folder.AccessControlList.Add(new AccessControlEntry(Operations.Entity.Read, _visitorGroupSystemId));
                }
                using (_securityContextService.ActAsSystem("ProductCertificate task"))
                {
                    _folderService.Create(folder);
                }
            }
            
            return folder;
        }
    }
}
