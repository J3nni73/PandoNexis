using Litium.Accelerator.ViewModels;
using Litium.Blocks;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Media;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Services
{
    [Service(ServiceType = typeof(PNMediaService))]
    [RequireServiceImplementation]
    public class PNMediaService
    {
        private readonly FieldDefinitionService _fieldDefinitionService;

        public PNMediaService(FieldDefinitionService fieldDefinitionService)
        {
            _fieldDefinitionService = fieldDefinitionService;
        }
        public string GetFileName(Guid fileId, CultureInfo culture)
        {
            var file = fileId.MapTo<Litium.Media.File>();
            if (file != null) { 
                return string.IsNullOrWhiteSpace(file.Fields.GetValue<string>(MediaFieldNameConstants.DisplayName, culture)) ? file.Name : file.Fields.GetValue<string>(MediaFieldNameConstants.DisplayName, culture);
            }
            return null;
        }

        public string GetFileName(Litium.Media.File file, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(file.Fields.GetValue<string>(MediaFieldNameConstants.DisplayName, culture)) ? file.Name : file.Fields.GetValue<string>(MediaFieldNameConstants.DisplayName);

        }
        public string GetFileName(Litium.Media.File file)
        {
            return string.IsNullOrWhiteSpace(file.Fields.GetValue<string>(MediaFieldNameConstants.DisplayName, CultureInfo.CurrentCulture)) ? file.Name : file.Fields.GetValue<string>(MediaFieldNameConstants.DisplayName);

        }

    }
}
