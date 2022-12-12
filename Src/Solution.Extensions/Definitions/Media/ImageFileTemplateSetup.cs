using Litium.FieldFramework;
using Litium.Accelerator.Definitions;
using PN = PandoNexis.Accelerator.Extensions.Constants;
using Litium.Media;

namespace Solution.Extensions.Definitions
{
    internal class ImageFileTemplateSetup : FieldTemplateSetup
    {
        public override IEnumerable<FieldTemplate> GetTemplates()
        {
            var templates = new List<FieldTemplate>
            {
                new FileFieldTemplate("Image")
                {
                    FileExtensions = new HashSet<string>("bmp,gif,jpg,jpeg,png,pngx,tif,tiff,ico,icon,svg".Split(','),StringComparer.OrdinalIgnoreCase),
                    TemplateType = FileTemplateType.Image,
                    FieldGroups = new []
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                            Fields =
                            {
                                SystemFieldDefinitionConstants.NameInvariantCulture,
                                PN.MediaFieldNameConstants.DisplayName,
                                SystemFieldDefinitionConstants.Description,
                                SystemFieldDefinitionConstants.LastWriteTimeUtc,
                                SystemFieldDefinitionConstants.FileSize
                            }
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "ImageProperties",
                            Collapsed = false,
                            Fields =
                            {
                                "exif_Make",
                                "exif_Model",
                                "exif_Orientation",
                                "exif_Software",
                                "exif_Date_and_Time",
                                "exif_YCbCr_positioning",
                                "exif_Compression",
                                "exif_X_resolution",
                                "exif_Y_resolution",
                                "exif_Resolution_unit",
                                "exif_Exposure_time",
                                "exif_F-number",
                                "exif_Exposure_program",
                                "exif_Exif_version",
                                "exif_DateTime_Original",
                                "exif_DateTime_Digitized",
                                "exif_Components_configuration",
                                "exif_Compressed_bits_per_pixel",
                                "exif_Shutter_Speed_Value",
                                "exif_Exposure_Bias_Value",
                                "exif_Aperture_Value",
                                "exif_Max_Aperture_Value",
                                "exif_Metering_mode",
                                "exif_Flash",
                                "exif_Focal_length",
                                "exif_MakerNote",
                                "exif_FlashPix_version",
                                "exif_Color_space",
                                "exif_Focal_Plane_X_Resolution",
                                "exif_Focal_Plane_Y_Resolution",
                                "exif_Pixel_X_dimension",
                                "exif_Pixel_Y_dimension",
                                "exif_File_source",
                                "exif_Interoperability_index",
                                "exif_Interoperability_version",
                                "exif_Exposure_Mode",
                                "exif_White_Balance_Mode",
                                "exif_Digital_Zoom_Ratio",
                                "exif_Compression_Type",
                                "exif_Data_Precision",
                                "exif_Number_of_Components"
                            }
                        },
                    }
                },
                 new FileFieldTemplate("Other")
                {
                   TemplateType = FileTemplateType.Other,
                    FieldGroups = new []
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                            Fields =
                            {
                                SystemFieldDefinitionConstants.NameInvariantCulture,
                                PN.MediaFieldNameConstants.DisplayName,
                                SystemFieldDefinitionConstants.Description
                            }
                        },
                       
                    }
                },
           };
            return templates;
        } 
    } 
}