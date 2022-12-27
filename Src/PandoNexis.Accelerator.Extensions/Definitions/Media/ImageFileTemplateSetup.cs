using Litium.FieldFramework;
using Litium.Media;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PN = PandoNexis.Accelerator.Extensions.Constants;


namespace PandoNexis.Accelerator.Extensions.Definitions.Media
{
    internal class ImageFileTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>()
            {
                GetFileField("Image", "General", SystemFieldDefinitionConstants.NameInvariantCulture),
                GetFileField("Image", "General", PN.MediaFieldNameConstants.DisplayName),
                GetFileField("Image", "General", SystemFieldDefinitionConstants.Description),
                GetFileField("Image", "General", SystemFieldDefinitionConstants.LastWriteTimeUtc),
                GetFileField("Image", "General", SystemFieldDefinitionConstants.FileSize),
                GetFileField("Image", "ImageProperties", "exif_Make"),
                GetFileField("Image", "ImageProperties", "exif_Model"),
                GetFileField("Image", "ImageProperties", "exif_Orientation"),
                GetFileField("Image", "ImageProperties", "exif_Software"),
                GetFileField("Image", "ImageProperties", "exif_Date_and_Time"),
                GetFileField("Image", "ImageProperties", "exif_YCbCr_positioning"),
                GetFileField("Image", "ImageProperties", "exif_Compression"),
                GetFileField("Image", "ImageProperties", "exif_X_resolution"),
                GetFileField("Image", "ImageProperties", "exif_Y_resolution"),
                GetFileField("Image", "ImageProperties", "exif_Resolution_unit"),
                GetFileField("Image", "ImageProperties", "exif_Exposure_time"),
                GetFileField("Image", "ImageProperties", "exif_F-number"),
                GetFileField("Image", "ImageProperties", "exif_Exposure_program"),
                GetFileField("Image", "ImageProperties", "exif_Exif_version"),
                GetFileField("Image", "ImageProperties", "exif_DateTime_Original"),
                GetFileField("Image", "ImageProperties", "exif_DateTime_Digitized"),
                GetFileField("Image", "ImageProperties", "exif_Components_configuration"),
                GetFileField("Image", "ImageProperties", "exif_Compressed_bits_per_pixel"),
                GetFileField("Image", "ImageProperties", "exif_Shutter_Speed_Value"),
                GetFileField("Image", "ImageProperties", "exif_Exposure_Bias_Value"),
                GetFileField("Image", "ImageProperties", "exif_Aperture_Value"),
                GetFileField("Image", "ImageProperties", "exif_Max_Aperture_Value"),
                GetFileField("Image", "ImageProperties", "exif_Metering_mode"),
                GetFileField("Image", "ImageProperties", "exif_Flash"),
                GetFileField("Image", "ImageProperties", "exif_Focal_length"),
                GetFileField("Image", "ImageProperties", "exif_MakerNote"),
                GetFileField("Image", "ImageProperties", "exif_FlashPix_version"),
                GetFileField("Image", "ImageProperties", "exif_Color_space"),
                GetFileField("Image", "ImageProperties", "exif_Focal_Plane_X_Resolution"),
                GetFileField("Image", "ImageProperties", "exif_Focal_Plane_Y_Resolution"),
                GetFileField("Image", "ImageProperties", "exif_Pixel_X_dimension"),
                GetFileField("Image", "ImageProperties", "exif_Pixel_Y_dimension"),
                GetFileField("Image", "ImageProperties", "exif_File_source"),
                GetFileField("Image", "ImageProperties", "exif_Interoperability_index"),
                GetFileField("Image", "ImageProperties", "exif_Interoperability_version"),
                GetFileField("Image", "ImageProperties", "exif_Exposure_Mode"),
                GetFileField("Image", "ImageProperties", "exif_White_Balance_Mode"),
                GetFileField("Image", "ImageProperties", "exif_Digital_Zoom_Ratio"),
                GetFileField("Image", "ImageProperties", "exif_Compression_Type"),
                GetFileField("Image", "ImageProperties", "exif_Data_Precision"),
                GetFileField("Image", "ImageProperties", "exif_Number_of_Components"),
                };
            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new FileFieldTemplate("Image")
                {
                    FileExtensions = new HashSet<string>("bmp,gif,jpg,jpeg,png,pngx,tif,tiff,ico,icon,svg".Split(','),StringComparer.OrdinalIgnoreCase),
                    TemplateType = FileTemplateType.Image,
                    FieldGroups = new []
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "ImageProperties",
                            Collapsed = false,
                        },
                    }
            };
            return template;
        }
    }
}
