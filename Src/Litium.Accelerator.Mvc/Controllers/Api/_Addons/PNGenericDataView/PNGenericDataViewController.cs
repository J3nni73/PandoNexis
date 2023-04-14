//using Litium.Media;
//using Litium.Runtime;
//using Microsoft.AspNetCore.Mvc;
//using PandoNexis.Accelerator.Extensions.Builders.Media;
//using PandoNexis.AddOns.Extensions.PNGenericDataView.Builders;
//using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
////using PandoNexis.AddOns.Extensions.PNGenericDataViewPage;
//using System;
//using System.Threading.Tasks;

//namespace Litium.Accelerator.Mvc.Controllers.Api._Addons.PNGenericDataView
//{
//    [Route("api/pngenericdataview")]
//    public class PNGenericDataViewController : ApiControllerBase
//    {
//        private readonly PNGenericDataViewService _pnGenericDataViewService;
//        private readonly PNGenericDataViewViewModelBuilder _pnGenericDataViewBuilder;
//        private readonly ApplicationJsonConverter _applicationJsonConverter;

//        public PNGenericDataViewController(PNGenericDataViewService pnGenericDataViewService, PNGenericDataViewViewModelBuilder pnGenericDataViewBuilder, ApplicationJsonConverter applicationJsonConverter)
//        {
//            _pnGenericDataViewService = pnGenericDataViewService;
//            _pnGenericDataViewBuilder = pnGenericDataViewBuilder;
//            _applicationJsonConverter = applicationJsonConverter;
//        }

//        [HttpGet]
//        [Route("getPNGenericDataViewData")]
//        public async Task<IActionResult> GetPNGenericDataViewData(Guid id)
//        {
//            var pnGenericDataViewData = await _pnGenericDataViewBuilder.BuildAsync(id);

//            if (pnGenericDataViewData != null)
//            {
//                try
//                {
//                    var returnData = _applicationJsonConverter.ConvertObjectToJson(pnGenericDataViewData);
//                    return Ok(returnData);
//                }
//                catch (Exception)
//                {
//                    return BadRequest();
//                }
//            }
//            return BadRequest();
//        }
//    }
//}