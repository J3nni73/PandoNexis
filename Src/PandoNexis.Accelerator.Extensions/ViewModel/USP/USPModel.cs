using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;
using AutoMapper;
using Litium.Web.Models;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Usp
{
    public class USPModel 
    {
        public string UspTitle { get; set; } = string.Empty;
        public string UspSubTitle { get; set; } = string.Empty;
        public string UspText { get; set; } = string.Empty;
        public string UspThemeCssClass { get; set; } = string.Empty;
        public ImageModel UspIcon { get; set; } = new ImageModel();
        public string UspLink { get; set; } = string.Empty;
    }
}