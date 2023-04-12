using AutoMapper;
using Litium.Accelerator.ViewModels.Framework;
using Litium.Accelerator.ViewModels.Product;
using Litium.Runtime.AutoMapper;
using PandoNexis.Accelerator.Extensions.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.ViewModel.Product
{
    public class PNProductItemViewModel : ProductItemViewModel, IAutoMapperConfiguration
    {
        //PandoExtensions: begin
        public string ExtendedDescription { get; set; }
        //PandoExtensions: end


        //PandoNexis: BEGIN MODEL

        //PandoNexis Visibility begin
        public bool EcomEnabled { get; set; }
        //PandoNexis Visibility end

        //PandoNexis: END MODEL


        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductItemViewModel, PNProductItemViewModel>();
        }
    }
}
