using Litium.Accelerator.Utilities;
using Litium.Media;
using Litium.Web.Administration.Media;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using NLog.Fluent;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using File = Litium.Media.File;

namespace Litium.Accelerator.Mvc.Controllers.Api._Addons
{
    [Route("api/genericgridview")]
    public class GenericGridViewController : ApiControllerBase
    {
        private readonly PersonStorage _personStorage;
        private readonly HeaderInfoService _headerInfoService;
        private readonly ProductImageService _productImageService;
        private readonly FolderService _folderService;
        private readonly FileService _fileService;


        public GenericGridViewController(PersonStorage personStorage, HeaderInfoService headerInfoService, ProductImageService productImageService, FolderService folderService, FileService fileService)
        {
            _personStorage = personStorage;
            _headerInfoService = headerInfoService;
            _productImageService = productImageService;
            _folderService = folderService;
            _fileService = fileService;
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
                        // TODO: Handle the post result!
                        return res;
                    });

                // await processing of the result
                task.Wait();
                var test = task.Result;
                //var gridViewRow = GetProcessor(type).UpdateRow(test);
                //if (gridViewRow != null)
                //    return Ok(gridViewRow);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getGenericGridView/{type}")]
        public async Task<IActionResult> GetGenericGridView(string type)
        {
            var gridView = await (await GetProcessor(type))?.GetGridView(HttpContext.Request.QueryString.ToString());
            if (gridView != null)
                return Ok(gridView);
            return BadRequest();
        }

        [HttpGet]
        [Route("getGenericGridViewMedia")]
        public async Task<IActionResult> GetGenericGridViewMedia(Guid folderId)
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
                var gridViewRow = await (await GetProcessor(type)).HandleFormData(test);
                if (gridViewRow != null)
                    return Ok(gridViewRow);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("getGenericGridForm/{type}")]
        public async Task<IActionResult> GetGenericGridForm(string type)
        {

            var gridRow = await (await GetProcessor(type)).GetGridForm(HttpContext.Request.QueryString.ToString());
            if (gridRow != null)
                return Ok(gridRow);
            return BadRequest();
        }

        [HttpGet]
        [Route("getGenericGridViewForExport/{type}")]
        public async Task<IActionResult> GetGenericGridViewForExport(string type)
        {
            // First we check if we are logged on correctly
            var currentOrganization = _personStorage.CurrentSelectedOrganization;
            if (currentOrganization == null /*|| _ewermanOrganizationService.Get(currentOrganization.SystemId) == null*/)
            {
                var obj = new
                {
                    isNotLoggedOn = true
                };
                return Ok(obj);
            }

            var queryString = HttpContext.Request.QueryString.ToString();
            if (string.IsNullOrEmpty(queryString))
                queryString = Request.Headers["Referer"].ToString() ?? string.Empty;

            using (var stream = (MemoryStream)GetProcessor(type).Result.GetGridViewForExport(queryString).Result)
            {
                if (stream == null)
                {
                    return Ok();
                }
                var bytes = stream.ToArray();
                return Ok(new { base64 = Convert.ToBase64String(bytes, 0, bytes.Length) });
            }
        }
        [HttpGet]
        [Route("getHeaderInformation/{type}")]
        public async Task<IActionResult> GetHeaderInformation(string type)
        {
            // First we check if we are logged on correctly
            //var currentOrganization = _personStorage.CurrentSelectedOrganization;
            //if (currentOrganization != null)
            //{
            if (type == "void")
            {
                if (Request.Headers["Referer"].ToString()?.Contains("kassa") ?? false)
                    type = "Checkout";
            }
            //    var htmlMarkup = _headerInfoService.GetHeaderInfo(type, currentOrganization);
            //    return Ok(htmlMarkup);
            //}
            var htmlMarkup = _headerInfoService.GetHeaderInfo(type);
            return Ok(htmlMarkup);
            //return BadRequest();
        }

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
                var gridViewRow = await (await GetProcessor(type)).UpdateRow(result);
                if (gridViewRow != null)
                    return Ok(gridViewRow);
            }

            //string data = new StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd();
            //var gridViewRow = GetProcessor(type).UpdateRow(data);
            //if (gridViewRow != null)
            //    return Ok(gridViewRow);
            return BadRequest();
        }


        [HttpPost]
        [Route("addImagesToArchiveAndVariant/{entitySystemId}/{type}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImagesToArchiveAndVariant(string entitySystemId, string type, List<ImgData> imageData)
        {
            if (imageData != null)
            {
                var newImageList = new List<GenericGridViewFieldSimpleList>();
                foreach (var item in imageData)
                {
                    var retUrl = item.ImageBase64;
                    var retName = item.ImageName;

                    var gridViewRow = await _productImageService.AddImageFromVariant(entitySystemId, retUrl, retName);
                    if (gridViewRow != null)
                        newImageList.Add(gridViewRow);
                }
                return Ok(newImageList);
            }


            return BadRequest();
        }

        [HttpPost]
        [Route("removeImageFromVariant/{entitySystemId}/{type}/{imageId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveImagesFromArchiveAndVariant(string entitySystemId, string type, string imageId)
        {
            var gridViewRow = await _productImageService.RemoveImageFromVariant(entitySystemId, imageId);
            if (gridViewRow != null)
                return Ok(gridViewRow);
            return BadRequest();
        }

        // Special fields
        [HttpPost]
        [Route("getFieldData/{type}/{field}/{query}")]
        public async Task<IActionResult> GetFieldData(string type, string field, string query)
        {
            //query = "q=" + query;
            //var data = IoC.ResolvePlugin<IGridSpecialFieldData>(field, false)?.GetData(type, query);
            //if (data != null)
            //{
            //    return Ok(data);
            //}

            return BadRequest();
        }

        [HttpPut]
        [Route("setFieldData")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetFieldData(string fieldData)
        {
            //var data = IoC.ResolvePlugin<IGridSpecialFieldData>(fieldData.FieldId, false)?.SetData(fieldData.DataSource, fieldData);
            //if (data != null)
            //{
            //    return Ok(data);
            //}

            return BadRequest();
        }

        [HttpGet]
        [Route("getGenericGridViewByTab/{type}")]
        public async Task<IActionResult> GetGenericGridViewByTab(string type, string id)
        {
            var gridView = await (await GetProcessor(type)).GetGridView(id);
            if (gridView != null)
                return Ok(gridView);
            return BadRequest();
        }

        public async Task<IGridViewDataProcessor> GetProcessor(string ServiceName)
        {
            var processor = IoC.ResolvePlugin<IGridViewDataProcessor>(ServiceName, false);
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