using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using System;

namespace LY.Plugin.Widgets.ReturnForm.Components
{
    [ViewComponent(Name = "WidgetsReturnForm")]
    public class WidgetsReturnFormViewComponent : NopViewComponent
    {
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            int model = Convert.ToInt32(additionalData);
            if (widgetZone == AdminWidgetZones.OrderDetailsButtons)
            {
                return View("~/Plugins/Widgets.ReturnForm/Views/AdminButton.cshtml", model);
            }

            return Content("");
        }
    }
}
