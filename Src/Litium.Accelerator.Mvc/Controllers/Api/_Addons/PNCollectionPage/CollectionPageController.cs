using Litium.Runtime;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNCollectionPage;
using System;
using System.Threading.Tasks;

namespace Litium.Accelerator.Mvc.Controllers.Api._Addons.PNCollectionPage
{
    [Route("api/collectionpage")]
    public class CollectionPageController : ApiControllerBase
    {

        private readonly CollectionPageService _collectionPageService;
        private readonly ApplicationJsonConverter _applicationJsonConverter;

        public CollectionPageController(CollectionPageService collectionPageService, ApplicationJsonConverter applicationJsonConverter)
        {
            _collectionPageService = collectionPageService;
            _applicationJsonConverter = applicationJsonConverter;
        }

        [HttpGet]
        [Route("getGetCollectionPageData")]
        public async Task<IActionResult> GetCollectionPageData(Guid PageSystemId)
        {
            var collectionPageData = _collectionPageService.BuildCollectionPageData(PageSystemId);

            if (collectionPageData != null)
            {
                try
                {
                    var returnData = _applicationJsonConverter.ConvertObjectToJson(collectionPageData);
                    return Ok(returnData);
                }
                catch (Exception)
                {
                    return BadRequest();
                }

            }
            return BadRequest();
        }
    }
}