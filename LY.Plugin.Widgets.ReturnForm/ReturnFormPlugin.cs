using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;

namespace LY.Plugin.Misc.ReturnForm
{
    /// <summary>
    /// Plugin
    /// </summary>
    public class ReturnFormPlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public ReturnFormPlugin(ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper)
        {
            this._localizationService = localizationService;
            this._settingService = settingService;
            this._webHelper = webHelper;
        }

        #endregion

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { AdminWidgetZones.OrderDetailsButtons };
        }



        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsReturnForm";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //locales
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Button", "Return form", "en-US");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Button", "Returseddel", "da-DK");

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.TitlePragragh", "Return form", "da-DK");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.TitleDetailsPragragh", "Firma adresse", "da-DK");

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Information.Name.", "Navn: ", "da-DK");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Information.Ordre.", "Ordre: ", "da-DK");

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Product.Title", "\nProdukter der skal sendes retur.", "da-DK");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Product.TableHeaderOne", "Produkter", "da-DK");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Product.TableHeaderTwo", "Byttes til str", "da-DK");

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Message.Title", "\nFeedback:", "da-DK");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Message.TableColumnOne", "Hvad ønskes fra kunden:\n\n\n", "da-DK");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Message.TableColumnTwo", "Hvad synes du om kundeoplevelsen:\n\n\n\n", "da-DK");

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Important.Title", "\nVIGTIGT:", "da - DK");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Important.Details", "Denne seddel skal være i pakken du sender retur.", "da - DK");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //locales
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Button");

            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.TitlePragragh");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.TitleDetailsPragragh");

            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Information.Name.");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Information.Ordre.");

            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Product.Title");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Product.TableHeaderOne");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Product.TableHeaderTwo");

            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Message.Title");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Message.TableColumnOne");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Message.TableColumnTwo");

            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Important.Title");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.ReturnForm.Document.Important.Details");

            base.Uninstall();
        }

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
    }
}