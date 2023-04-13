using Litium.Accelerator.Utilities;
using Litium.Media;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
//using PandoNexis.AddOns.Extensions.PNGenericDataView.ViewModels;

//using PandoNexis.AddOns.Extensions.PNGenericGridView.Processors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = Litium.Media.File;
using Newtonsoft.Json;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;

namespace Litium.Accelerator.Mvc.Controllers.Api._Addons.PNGenericDataView
{
    [Route("api/genericdataview")]
    public class GenericGDataViewController : ApiControllerBase
    {
        private readonly PersonStorage _personStorage;
        private readonly PageService _pageService;
        //private readonly HeaderInfoService _headerInfoService;
        //private readonly ProductImageService _productImageService;
        private readonly FolderService _folderService;
        private readonly FileService _fileService;


        public GenericGDataViewController(PersonStorage personStorage,
                                            FolderService folderService,
                                            FileService fileService,
                                            PageService pageService)
        {
            _personStorage = personStorage;
            _folderService = folderService;
            _fileService = fileService;
            _pageService = pageService;
        }
        [HttpPost]
        [Route("{type}")]
        public async Task<IActionResult> UploadFile(string type)
        {
            HttpContext.Request.EnableBuffering();
            Request.Body.Position = 0;
            using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
            {
                var task = stream
                    .ReadToEndAsync()
                    .ContinueWith(t =>
                    {
                        var res = t.Result;
                        return res;
                    });

                task.Wait();
                var test = task.Result;

            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getGenericDataView/{type}")]
        public async Task<IActionResult> GetGenericDataView(string type)
        {
            if (Guid.TryParse(type, out Guid pageSystemId))
            {
                var dataView = await (await GetProcessor(pageSystemId))?.GetDataView(pageSystemId, HttpContext.Request.QueryString.ToString());
                if (dataView != null)
                    return Ok(dataView);

            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getGenericDataViewMedia")]
        public async Task<IActionResult> GetGenericDataViewMedia(Guid folderId)
        {
            var topFolder = _folderService.Get(folderId);
            var filesAndFolders = CreateStructure(topFolder);
            if (filesAndFolders != null)
                return Ok(filesAndFolders);
            return BadRequest();
        }

        private FilesAndFolders CreateStructure(Folder folder)
        {
            var childFolders = _folderService.GetChildFolders(folder.SystemId);
            List<FilesAndFolders> filesAndFolders = null;
            if (childFolders != null && childFolders.Count() > 0)
            {
                filesAndFolders = new List<FilesAndFolders>();
                foreach (var folderItem in childFolders)
                {
                    filesAndFolders.Add(CreateStructure(folderItem));
                }
            }

            var folderFiles = _fileService.GetByFolder(folder.SystemId);
            return new FilesAndFolders
            {
                Files = folderFiles?.ToList(),
                FolderData = filesAndFolders,
            };

            return null;
        }

        [HttpPost]
        [Route("handleFormData/{type}")]
        public async Task<IActionResult> HandleFormData(string type)
        {

            HttpContext.Request.EnableBuffering();
            Request.Body.Position = 0;
            using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
            {
                var task = stream
                    .ReadToEndAsync()
                    .ContinueWith(t =>
                    {
                        var res = t.Result;
                        // TODO: Handle the post result!
                        return res;
                    });

                // await processing of the result
                task.Wait();
                var test = task.Result;
                var dataViewRow = await (await GetProcessor(type)).HandleFormData(test);
                if (dataViewRow != null)
                    return Ok(dataViewRow);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("getGenericDataForm/{type}")]
        public async Task<IActionResult> GetGenericDataForm(string type)
        {

            var dataRow = await (await GetProcessor(type)).GetDataForm(HttpContext.Request.QueryString.ToString());
            if (dataRow != null)
                return Ok(dataRow);
            return BadRequest();
        }

        //[HttpGet]
        //[Route("getGenericDataViewForExport/{type}")]
        //public async Task<IActionResult> GetGenericDataViewForExport(string type)
        //{
        //    // First we check if we are logged on correctly
        //    var currentOrganization = _personStorage.CurrentSelectedOrganization;
        //    if (currentOrganization == null /*|| _ewermanOrganizationService.Get(currentOrganization.SystemId) == null*/)
        //    {
        //        var obj = new
        //        {
        //            isNotLoggedOn = true
        //        };
        //        return Ok(obj);
        //    }

        //    var queryString = HttpContext.Request.QueryString.ToString();
        //    if (string.IsNullOrEmpty(queryString))
        //        queryString = Request.Headers["Referer"].ToString() ?? string.Empty;

        //    using (var stream = (MemoryStream)GetProcessor(type).Result.GetDataViewForExport(queryString).Result)
        //    {
        //        if (stream == null)
        //        {
        //            return Ok();
        //        }
        //        var bytes = stream.ToArray();
        //        return Ok(new { base64 = Convert.ToBase64String(bytes, 0, bytes.Length) });
        //    }
        //}
        //[HttpGet]
        //[Route("getHeaderInformation/{type}")]
        //public async Task<IActionResult> GetHeaderInformation(string type)
        //{
        //    // First we check if we are logged on correctly
        //    //var currentOrganization = _personStorage.CurrentSelectedOrganization;
        //    //if (currentOrganization != null)
        //    //{
        //    if (type == "void")
        //    {
        //        if (Request.Headers["Referer"].ToString()?.Contains("kassa") ?? false)
        //            type = "Checkout";
        //    }
        //    //    var htmlMarkup = _headerInfoService.GetHeaderInfo(type, currentOrganization);
        //    //    return Ok(htmlMarkup);
        //    //}
        //    var htmlMarkup = _headerInfoService.GetHeaderInfo(type);
        //    return Ok(htmlMarkup);
        //    //return BadRequest();
        //}

        [HttpPatch]
        [Route("{type}")]
        public async Task<IActionResult> UpdateRow(string type)
        {

            HttpContext.Request.EnableBuffering();
            Request.Body.Position = 0;
            using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
            {
                var task = stream
                    .ReadToEndAsync()
                    .ContinueWith(t =>
                    {
                        var res = t.Result;
                        // TODO: Handle the post result!
                        return res;
                    });

                // await processing of the result
                task.Wait();
                var result = task.Result;
                var dataViewRow = await (await GetProcessor(type)).UpdateRow(result);
                if (dataViewRow != null)
                    return Ok(dataViewRow);
            }

            return BadRequest();
        }


        //[HttpPost]
        //[Route("addImagesToArchiveAndVariant/{entitySystemId}/{type}")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddImagesToArchiveAndVariant(string entitySystemId, string type, List<ImgData> imageData)
        //{
        //    if (imageData != null)
        //    {
        //        var newImageList = new List<GenericDataViewFieldSimpleList>();
        //        foreach (var item in imageData)
        //        {
        //            var retUrl = item.ImageBase64;
        //            var retName = item.ImageName;

        //            var dataViewRow = await _productImageService.AddImageFromVariant(entitySystemId, retUrl, retName);
        //            if (dataViewRow != null)
        //                newImageList.Add(dataViewRow);
        //        }
        //        return Ok(newImageList);
        //    }


        //    return BadRequest();
        //}

        //[HttpPost]
        //[Route("removeImageFromVariant/{entitySystemId}/{type}/{imageId}")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> RemoveImagesFromArchiveAndVariant(string entitySystemId, string type, string imageId)
        //{
        //    var dataViewRow = await _productImageService.RemoveImageFromVariant(entitySystemId, imageId);
        //    if (dataViewRow != null)
        //        return Ok(dataViewRow);
        //    return BadRequest();
        //}

        // Special fields
        //[HttpPost]
        //[Route("getFieldData/{type}/{field}/{query}")]
        //public async Task<IActionResult> GetFieldData(string type, string field, string query)
        //{
        //    query = "q=" + query;
        //    var data = IoC.ResolvePlugin<IGridSpecialFieldData>(field, false)?.GetData(type, query);
        //    if (data != null)
        //    {
        //        return Ok(data);
        //    }

        //    return BadRequest();
        //}

        [HttpPut]
        [Route("buttonClick")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ButtonClick(object fieldData)
        {
            var field = JsonConvert.DeserializeObject<GenericDataField>(fieldData.ToString());

            if (Guid.TryParse(field.DataSource, out Guid pageSystemId))
            {
                var datacontainer = (await GetProcessor(pageSystemId)).ButtonClick(field)?.Result;
                if (datacontainer != null)
                    return Ok(datacontainer);
            }
            return BadRequest();

        }

        [HttpGet]
        [Route("getGenericDataViewByTab/{type}")]
        public async Task<IActionResult> GetGenericDataViewByTab(string type, string id)
        {
            //var dataView = await (await GetProcessor(type)).GetDataView(id);
            //if (dataView != null)
            //    return Ok(dataView);
            return BadRequest();
        }
        public async Task<IGenericDataViewProcessor> GetProcessor(Guid pageSystemId)
        {
            var page = _pageService.Get(pageSystemId);
            var source = page.Fields.GetValue<string>(PageFieldNameConstants.AreaSource);
            return await GetProcessor(source);


        }
        public async Task<IGenericDataViewProcessor> GetProcessor(string ServiceName)
        {
            var processor = IoC.ResolvePlugin<IGenericDataViewProcessor>(ServiceName, false);
            if (processor == null)
            {
                throw new Exception("Processor not supported " + ServiceName);
            }
            return processor;
        }

        public class ImgData
        {
            public string ImageName { get; set; }
            public string ImageBase64 { get; set; }
            public int ImageIndex { get; set; }
        }

        public class FilesAndFolders
        {
            public string FolderName { get; set; }
            public List<File> Files { get; set; }
            public List<FilesAndFolders> FolderData { get; set; }
        }

        public class FileData
        {
            public string FileName { get; set; }
            public string FileType { get; set; }
            public int FileHeight { get; set; }
            public int FileWidth { get; set; }
            public int FileSize { get; set; }
        }
    }
}