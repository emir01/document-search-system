using System.Configuration;
using System.Web.Mvc;

namespace DSS.Presentation.Web.Controllers
{
    /// <summary>
    /// The logging controller is used to handle the client side configuration requests as well
    /// as log requests that should be handled by an apropriate logging service.
    /// </summary>
    public class LoggingController : Controller
    {
        #region Properties

        /// <summary>
        /// The key for the application configuration value determining if console logging should be enabled
        /// </summary>
        private const string AppConfigConsoleLoggingKey = "AppConfigurationConsoleLoggingEnabledKey";

        /// <summary>
        /// The key for the application configuration value determining if Server logging should be enabled
        /// </summary>
        private const string AppConfigServerLoggingKey = "AppConfigurationServerLoggingEnabledKey";

        /// <summary>
        /// The server logging url address where logging data should be sent.
        /// </summary>
        private const string AppConfigServerLoggingUrlKey = "AppConfigurationServerLoggingUrlKey";

        #endregion

        #region Configuration Requests

        /// <summary>
        /// The action method used to get the client logging configuration from the web config
        /// </summary>
        /// <returns>Json result to the client containing the logging setup informatoin</returns>
        public JsonResult GetLoggingConfiguration()
        {
            // get the configuratoin from the app settings in web config
            var consoleLoggin = ConfigurationManager.AppSettings[AppConfigConsoleLoggingKey];
            var serverLogging = ConfigurationManager.AppSettings[AppConfigServerLoggingKey];
            var serverLoggingUrl = ConfigurationManager.AppSettings[AppConfigServerLoggingUrlKey];

            // create an anonymous object that will be returned as json to the client side logging module
            var loggingConfiguration = new
                                           {
                                               ConsoleEnabled = consoleLoggin,
                                               ServerEnabled = serverLogging,
                                               ServerLoggingUrl = serverLoggingUrl
                                           };

            // return the json configuratoin object
            return Json(loggingConfiguration, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Log Requests

        /// <summary>
        /// Saves a client side log message.
        /// </summary>
        /// <param name="timestamp">The timestamp string for the log message</param>
        /// <param name="message">The actual message that should be saved</param>
        /// <param name="data">The data that should be logged with the message.</param>
        /// <returns>Short json message for the </returns>
        public ActionResult Log(string message, object data,string timestamp)
        {
            // Call a logging server you actually want to save the loging data

            // return an empty result
            return new EmptyResult();
        }

        #endregion
    }
}
