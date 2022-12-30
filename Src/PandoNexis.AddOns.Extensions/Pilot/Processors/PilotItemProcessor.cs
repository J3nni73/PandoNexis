using DocumentFormat.OpenXml.Wordprocessing;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.Pilot.Definitions;
using PandoNexis.AddOns.Extensions.Pilot.Services;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Processors;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.Pilot.Processors
{
    [Service(Name = "ItemProcessor")]
    public class PilotItemProcessor : UnboundGenericGridViewDataProcessorBase
    {
        private readonly PilotItemService _pilotItemService;
        private readonly ItemProcessorService _itemProcessorService;

        public PilotItemProcessor(PilotItemService pilotItemService, 
                                    ItemProcessorService itemProcessorService)
        {
            _pilotItemService = pilotItemService;
            _itemProcessorService = itemProcessorService;
        }

        private const string _processorName = PilotConstants.ItemProcessor;
        public override async Task<object> GetGridForm(string data)
        {
            var fields = _itemProcessorService.GetFieldsToForm();
            return fields;
        }

        public override async Task<object> GetGridView(string data)
        {
            var gridView = new GenericGridView
            {
                DataRows = new List<GenericGridViewRow>(),
                Settings = new GenericGridViewSettings(50, 50)
            };

            var items = _pilotItemService.GetItems();
            
           
            gridView.DataRows.AddRange(_itemProcessorService.BuildItemRows(items));


            return gridView;
        }

        public override Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }

        public override Task<object> HandleFormData(string data)
        {
            throw new NotImplementedException();
        }
    }
}
