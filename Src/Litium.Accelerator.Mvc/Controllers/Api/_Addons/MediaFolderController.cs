using Litium.Media;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.Accelerator.Extensions.Builders.Media;
using System;
using System.Threading.Tasks;

namespace Litium.Accelerator.Mvc.Controllers.Api.Addons
{
    [Route("api/mediacatalog")]
    public class MediaCatalogController : ApiControllerBase
    {
        private readonly PNMediaViewModelBuilder _PNMediaViewModelBuilder;

        public MediaCatalogController(PNMediaViewModelBuilder pNMediaViewModelBuilder)
        {
            _PNMediaViewModelBuilder = pNMediaViewModelBuilder;
        }

        [HttpGet]
        [Route("getMediaData")]
        public async Task<IActionResult> GetMediaData(Guid folderId)
        {   var filesAndFolders = _PNMediaViewModelBuilder.Build(folderId);
            if (filesAndFolders != null)
                return Ok(filesAndFolders);
            return BadRequest();
        }
    }
}