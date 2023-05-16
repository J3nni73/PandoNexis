using Litium.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Objects
{
    public class GenericButton
    {
        public string FieldId { get; set; } //FieldefinitionID
        public Guid EntitySystemId { get; set; }
        public string ButtonText { get; set; } = string.Empty;

        /// <summary>
        /// Set the button class name
        /// <para>Examples (These exist): </para>
        ///     <para>generic-data-view__btn-green</para> 
        ///     <para>generic-data-view__btn-orange</para>
        ///     <para>generic-data-view__btn-cyan</para>
        ///     <para>generic-data-view__btn-magenta</para>
        ///     <para>generic-data-view__btn-purple</para>
        ///     <para>generic-data-view__btn-red</para>
        /// </summary>
        public string ClassName { get; set; } = string.Empty;        
        public bool UseConfirmation { get; set; } = false;
        public string ConfirmationText { get; set; } = string.Empty;
        public bool ButtonOpenInModal { get; set; } = false;
        public Guid PageSystemId { get; set; }
        public string EndPointMethod { get; set; } = string.Empty;
        public string FieldTooltipMessage { get; set; } = string.Empty; // On hover field information

        /// <summary>
        /// The type (looks) of tooltip
        /// <para>Available options: </para>
        ///     <para>"dark" - Default</para> 
        ///     <para>"light"</para>
        ///     <para>"success"</para>
        ///     <para>"warning"</para>
        ///     <para>"error"</para>
        ///     <para>"info"</para>
        /// </summary>
        public string FieldTooltipType { get; set; } = "dark";
        public bool HideButton { get; set; } = false;

        // Downloading files
        /// <summary>
        /// Example: 
        /// <para>"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64"</para>
        /// </summary>
        public string DownloadMimeTypeString { get; set; } = string.Empty; 
        public string DownloadFileName { get; set; } = string.Empty;
        /// <summary>
        /// Example:
        /// <para>"xlsx" or "pdf" or...</para>
        /// </summary>
        public string DownloadFileType { get; set; } = string.Empty; 
    }
}
