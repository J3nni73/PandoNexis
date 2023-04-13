namespace PandoNexis.AddOns.Extensions.PNLoggedOnInfoLabel.ViewModels
{
    using Litium.Accelerator.Builders;
    using System;

    public class PersonInfo: IViewModel
    {
        public string OrgId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string OrganizationId { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;
    }
}
