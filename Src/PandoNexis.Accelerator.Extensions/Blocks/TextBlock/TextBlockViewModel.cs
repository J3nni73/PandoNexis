using Litium.Accelerator.Builders;

namespace PandoNexis.Accelerator.Extensions.Blocks.TextBlock
{
    public class TextBlockViewModel : IViewModel
    {
        public string Background { get; set; } = string.Empty;
        public List<TextBlockItemViewModel> Items { get; set; } = new List<TextBlockItemViewModel>();
       
    }
}

