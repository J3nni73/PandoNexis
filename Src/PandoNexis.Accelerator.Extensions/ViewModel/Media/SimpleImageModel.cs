using Litium.Accelerator.Builders;

namespace PandoNexis.Accelerator.Extensions.ViewModel.Media
{
    public class SimpleImageModel
    {
        public string Title { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string IconCssClass { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}