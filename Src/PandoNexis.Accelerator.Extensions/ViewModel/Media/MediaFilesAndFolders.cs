namespace PandoNexis.Accelerator.Extensions.ViewModel.Media
{
    public class MediaFilesAndFolders
    {   
        public string FolderName { get; set; }
        // public List<File> Files { get; set; }
        public IList<MediaCatalogFileData> Files { get; set; }
        public List<MediaFilesAndFolders> FolderData { get; set; }
    }

    public class MediaCatalogFileData
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public int FrameHeight { get; set; }
        public int FrameWidth { get; set; }
        public long FileSize { get; set; }
        public string IconCssClass { get; set; }
        public string DownloadUrl { get; set; }
        public string LargeThumbnailUrl { get; set; }
        public string MediumThumbnailUrl { get; set; }
        public string SmallThumbnailUrl { get; set; }
    }
}