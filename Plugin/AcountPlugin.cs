using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
	[CrmPluginRegistration(MessageNameEnum.Update, "account", StageEnum.PreOperation,
		ExecutionModeEnum.Synchronous, "name", "Pre-Update Account", 1000,
		IsolationModeEnum.Sandbox)]
	public class AcountPlugin : IPlugin
	{
		public void Execute(IServiceProvider serviceProvider)
		{
			IPluginExecutionContext context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
			ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

			Entity entity = context.InputParameters["Target"] as Entity;

			if (entity.Contains("account"))
			{
				IOrganizationServiceFactory serviceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
				IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

				Entity ac = new Entity(entity.LogicalName);
				ac["accountid"] = entity.Id;
				ac["telephone1"] = "08138549501";

				service.Update(ac);
			}
		}
	}
}
