using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using JetBrains.Annotations;
using Litium.Accelerator.Builders;
using Litium.Accelerator.Extensions;
using PandoNexis.Accelerator.Extensions.ViewModels;
using PandoNexis.Accelerator.Extensions.Constants;

namespace PandoNexis.Accelerator.Extensions.Blocks.TextBlock
{
    public class TextBlockViewModel : IViewModel
    {
        public string Background { get; set; } = string.Empty;
        public List<TextBlockItemViewModel> Items { get; set; } = new List<TextBlockItemViewModel>();
       
    }
}

