using LY.Plugin.Widgets.ReturnForm.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using System.IO;

namespace LY.Plugin.Widgets.ReturnForm.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsReturnFormController : BasePluginController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IOrderService _orderService;
        private readonly IGenerateReturnFormService _generateReturnFormService;

        #endregion

        #region Ctor

        public WidgetsReturnFormController(IPermissionService permissionService,
            IOrderService orderService, 
            IGenerateReturnFormService generateReturnFormService)
        {
            this._permissionService = permissionService;
            this._orderService = orderService;
            this._generateReturnFormService = generateReturnFormService;
        }

        #endregion

        public IActionResult Generate(int orderId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var order = _orderService.GetOrderById(orderId);

            byte[] bytes;

            using (var stream = new MemoryStream())
            {
                _generateReturnFormService.CreateDocument(order, stream);
                bytes = stream.ToArray();
            }

            return File(bytes, MimeTypes.ApplicationPdf, $"order_{order.Id}.pdf");
        }
    }
}