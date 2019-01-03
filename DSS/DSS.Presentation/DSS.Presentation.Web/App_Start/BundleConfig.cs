using System.Web.Optimization;
using BundleTransformer.Core.Transformers;

namespace DSS.Presentation.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            CssGeneral(bundles);

            FontAwesome(bundles);

            CssDataTables(bundles);

            JsLib(bundles);

            JsTools(bundles);

            JsDataTables(bundles);

            JsApp(bundles);

            JsDataQueryBundle(bundles);

            JsBootstrapBundle(bundles);
        }

        #region CSS

        private static void CssGeneral(BundleCollection bundles)
        {
            // Css Common Bundles
            var styleBunde = new StyleBundle("~/Content/common/css");
            styleBunde.Include(
                "~/Content/common/*.css",
                "~/Content/chosen/*.css",
                "~/Content/select2/*.css",
                "~/Content/tagedit/*.css"
                );

            bundles.Add(styleBunde);

            // Css Jquery UI bundles
            var jqUiBunde = new StyleBundle("~/Content/themes/humanity/css");
            jqUiBunde.Include("~/Content/themes/humanity/jquery-ui-1.8.23.custom.css");

            bundles.Add(jqUiBunde);

            var lessBundle = new StyleBundle("~/Content/less/less").Include(
                "~/Content/less/site.less",

                // Add the data.query styles
                "~/Content/less/DataQuery/data.query.table.less"
            );

            lessBundle.Transforms.Add(new CssTransformer());
            lessBundle.Transforms.Add(new CssMinify());

            bundles.Add(lessBundle);
        }

        private static void FontAwesome(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/fontawesome/css").Include("~/Content/fontawesome/css/*.css"));
        }

        private static void CssDataTables(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundle/Content/DataTables-1.9.4").Include(
                "~/Content/DataTables-1.9.4/extras/TableTools/media/css/TableTools.css",
                "~/Content/DataTables-1.9.4/media/css/BootstrapTables.css"
            ));
        }

        #endregion

        #region Js

        #region Common

        private static void JsLib(BundleCollection bundles)
        {
            // Add the basic javascript bundles including all the javascript third
            //party libraries
            bundles.Add(new ScriptBundle("~/bundle/Scripts/js").Include(
                "~/Scripts/core.console.js",
                "~/Scripts/jquery-2.0.3.js",
                "~/Scripts/jquery-ui-1.10.3.js",
                "~/Scripts/knockout-3.0.0.js",
                "~/Scripts/jQuery.fileinput.js",
                "~/Scripts/json2.js",
                "~/Scripts/underscore.js",
                "~/Scripts/jquery.pnotify.js",
                "~/Scripts/jquery.loadmask.js",
                "~/Scripts/amplify.js",
                "~/Scripts/ICanHaz.min.js",
                "~/Scripts/jquery.fileDownload.js",
                "~/Scripts/jquery.sticky.js",

                // add the chosen scripts
                "~/Scripts/chosen.jquery.js",

                // add the select 2 scripts
                "~/Scripts/select2.js",

                // add the tag edit scripts
                "~/Scripts/jquery.autoGrowInput.js",
                "~/Scripts/jquery.tagedit.js"
            ));
        }

        #endregion

        #region Data Tables

        private static void JsDataTables(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundle/Scripts/DataTables-1.9.4").Include(
                "~/Scripts/DataTables-1.9.4/media/js/jquery.dataTables.js",
                "~/Scripts/DataTables-1.9.4/extras/TableTools/media/js/TableTools.js"
                            ));
        }

        #endregion

        #region Custom Tools Tools

        private static void JsTools(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundle/jsTools").Include(
            // FRAMEWORK
            // --------------------------------------------


            // Basic framework utilities
            "~/Scripts/Common/Framework/Params.js",
            "~/Scripts/Common/Framework/BaseRepository.js",

            // Message logging
            "~/Scripts/Common/Framework/GlobalMessageLogger.js",
            "~/Scripts/Common/Framework/FilteredConsoleLog.js",

            // Global Error processing
            "~/Scripts/Common/Framework/ErrorProcessor.js",

            // Url utilities
            "~/Scripts/Common/Framework/Url.js",
            "~/Scripts/Common/Framework/UrlSearchUtility.js",

            // KO
            // --------------------------------------------
            "~/Scripts/Common/KO/ObservableUtilities.js",

            // UX
            // --------------------------------------------

            "~/Scripts/Common/UX/LoadmaskWrapper.js",
            "~/Scripts/Common/UX/PinesWrapper.js",

            // AJAX
            // --------------------------------------------

            "~/Scripts/Common/Ajax/AjaxCallback.js",
            "~/Scripts/Common/Ajax/Ajax.js",
            "~/Scripts/Common/Ajax/CommonJsonModelProcessor.js",


            // Knockout framework bindings
            // --------------------------------------------


            "~/Scripts/Common/KnockoutBindings/Widget/ChosenSelect.js",
            "~/Scripts/Common/KnockoutBindings/Widget/jQueryUIAutoCompleteBinding.js",
            "~/Scripts/Common/KnockoutBindings/Widget/TagEdit.js",
            "~/Scripts/Common/KnockoutBindings/Widget/Select2Bindings.js",
            "~/Scripts/Common/KnockoutBindings/Common/OnChangeBindings.js"
            ));
        }

        #endregion

        #region App

        /// <summary>
        /// Configure application specific bundles
        /// </summary>
        /// <param name="bundles"></param>
        private static void JsApp(BundleCollection bundles)
        {
            // Add the basic Ko View Model Bundles
            bundles.Add(new ScriptBundle("~/bundle/koBasicViewModels").Include(
               "~/Scripts/src/Ko/ViewModels/Category/CategoryViewModel.js",
               "~/Scripts/src/Ko/ViewModels/Document/DocumentViewModel.js",
               "~/Scripts/src/Ko/ViewModels/Document/DisplayDocumentViewModel.js",
               "~/Scripts/src/Ko/ViewModels/Keyword/KeywordViewModel.js",
               "~/Scripts/src/Utilities/*.js",
               "~/Scripts/src/Ko/ViewModels/Action/MainActions/*.js",
               "~/Scripts/src/Ko/ViewModels/Action/DocumentDetailsViewModel.js"
               ));

            bundles.Add(new ScriptBundle("~/bundle/myjavascript").Include(
                "~/Scripts/src/namespace.js",
                "~/Scripts/src/Layout.js",
                "~/Scripts/src/App.js"
            ));
        }

        #endregion

        #region Data Query

        private static void JsDataQueryBundle(BundleCollection bundles)
        {
            // Register the Data Query Client side extension scripts 

            bundles.Add(new ScriptBundle("~/Scripts/DataQuery").Include(

                // General exception functioanlity for the client side Data Query Module
                "~/Scripts/DataQuery/Exceptions/DataQueryExceptions.js",

                // Specific Data Table Client side functionality for the Data Query Module
                "~/Scripts/DataQuery/Tables/Build/Objects/BuildObjects.js",
                "~/Scripts/DataQuery/Tables/Build/DatatableServerComm.js",
                "~/Scripts/DataQuery/Tables/Build/DatatableActionExtensions.js",
                "~/Scripts/DataQuery/Tables/Build/DatatableExtensions.js",

                "~/Scripts/DataQuery/Tables/DatatableSetup.js",

                // Add the filtering functionality scripts
                "~/Scripts/DataQuery/Tables/Filter/Objects/FilterObjects.js",
                "~/Scripts/DataQuery/Tables/Filter/QueryFilter.js"
            ));
        }

        #endregion

        #region Boostrap

        private static void JsBootstrapBundle(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundle/bootstrapjs").Include(
                "~/Scripts/bootstrap/bootstrap-alert.js",
                "~/Scripts/bootstrap/bootstrap-carousel.js",
                "~/Scripts/bootstrap/bootstrap-collapse.js",
                "~/Scripts/bootstrap/bootstrap-dropdown.js",
                "~/Scripts/bootstrap/bootstrap-modal.js",
                "~/Scripts/bootstrap/bootstrap-scrollspy.js",
                "~/Scripts/bootstrap/bootstrap-tab.js",
                "~/Scripts/bootstrap/bootstrap-tooltip.js",
                "~/Scripts/bootstrap/bootstrap-transition.js",
                "~/Scripts/bootstrap/bootstrap-typehead.js"
            ));
        }

        #endregion

        #endregion
    }
}