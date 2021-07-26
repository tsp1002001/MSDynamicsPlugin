using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Plugins
{
    public class DefaultOpeningHourPlugin : IPlugin
    {
        #region Secure/Unsecure Configuration Setup
        private string _secureConfig = null;
        private string _unsecureConfig = null;

        public DefaultOpeningHourPlugin(string unsecureConfig, string secureConfig)
        {
            _secureConfig = secureConfig;
            _unsecureConfig = unsecureConfig;
        }
        #endregion
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            try
            {
                Entity target = (Entity)context.InputParameters["Target"];
                tracer.Trace("Custom Plugin for Aldi Warehouse");
                tracer.Trace($"Target Name is {target.Attributes["cr299_name"]}");
                target.Attributes.Add("cr299_openinghours", "8:00 AM - 5:00 PM");
                tracer.Trace($"cr299_openinghours is {target.Attributes["cr299_openinghours"]}");
                service.Update(target);
            }
            catch (Exception e)
            {
                tracer.Trace("inside exception Custom Plugin for Aldi Warehouse");
                throw new InvalidPluginExecutionException(e.Message);
            }
        }
    }
}