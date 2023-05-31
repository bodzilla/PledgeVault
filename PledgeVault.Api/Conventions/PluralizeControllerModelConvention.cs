using Humanizer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace PledgeVault.Api.Conventions;

internal sealed class PluralizeControllerModelConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller) => controller.ControllerName = controller.ControllerName.Pluralize();
}