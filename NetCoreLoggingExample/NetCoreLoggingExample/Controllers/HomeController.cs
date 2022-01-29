using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreLoggingExample.Logging;
using System;

namespace NetCoreLoggingExample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILogger _specificLogger;

        public HomeController(ILogger<HomeController> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _specificLogger = loggerFactory.CreateLogger("XYZ");
        }

        [HttpGet, Route("Get")]
        public string Get()
        {
            var response = $"GET: Controller => {nameof(HomeController)}; Action => {nameof(Get)}.";
            _logger.LogInformation(response);
            return response;
        }

        [HttpGet, Route("All")]
        public string GetAllLogs()
        {
            var response = $"GET: Controller => {nameof(HomeController)}; Action => {nameof(GetAllLogs)}.";

            _logger.LogCritical(response);
            _logger.LogTrace(response);
            _logger.LogDebug(response);
            _logger.LogInformation(response);        
            _logger.LogWarning(response);
            _logger.LogError(response);
            _logger.LogCritical(response);

            _specificLogger.LogCritical(response);
            _specificLogger.LogTrace(response);
            _specificLogger.LogDebug(response);
            _specificLogger.LogInformation(response);
            _specificLogger.LogWarning(response);
            _specificLogger.LogError(response);
            _specificLogger.LogCritical(response);
            return response;
        }

        [HttpGet, Route("WithMessageTemplating")]
        public string GetAllLogsWithLogMessageTemplating()
        {
            var messageTemplate = "GET: Value 1 = {test}; value 2 = {test2}";
            var test = "A";
            var test2 = "B";

            _logger.LogCritical(messageTemplate, test, test2);
            _logger.LogTrace(messageTemplate, test, test2);
            _logger.LogDebug(messageTemplate, test, test2);
            _logger.LogInformation(messageTemplate, test, test2);
            _logger.LogWarning(messageTemplate, test, test2);
            _logger.LogError(messageTemplate, test, test2);
            _logger.LogCritical(messageTemplate, test, test2);

            _specificLogger.LogCritical(messageTemplate, test2, test);
            _specificLogger.LogTrace(messageTemplate, test2, test);
            _specificLogger.LogDebug(messageTemplate, test2, test);
            _specificLogger.LogInformation(messageTemplate, test2, test);
            _specificLogger.LogWarning(messageTemplate, test2, test);
            _specificLogger.LogError(messageTemplate, test2, test);
            _specificLogger.LogCritical(messageTemplate, test2, test);

            return $"GET: Controller => {nameof(HomeController)}; Action => {nameof(GetAllLogsWithLogMessageTemplating)}."; 
        }

        [HttpGet, Route("WithEventLogId")]
        public string GetAllLogsWithEventId()
        {
            var response = $"GET: Controller => {nameof(HomeController)}; Action => {nameof(GetAllLogsWithEventId)}.";

            _logger.LogCritical(RepoLogEvents.AddItem, response);
            _logger.LogTrace(RepoLogEvents.UpdateItem, response);
            _logger.LogDebug(RepoLogEvents.RemoveItem, response);
            _logger.LogInformation(RepoLogEvents.GetItem, response);
            _logger.LogWarning(RepoLogEvents.GetItems, response);
            _logger.LogError(RepoLogEvents.AddItem, response);
            _logger.LogCritical(RepoLogEvents.UpdateItem, response);

            _specificLogger.LogCritical(RepoLogEvents.AddItem, response);
            _specificLogger.LogTrace(RepoLogEvents.UpdateItem, response);
            _specificLogger.LogDebug(RepoLogEvents.RemoveItem, response);
            _specificLogger.LogInformation(RepoLogEvents.GetItem, response);
            _specificLogger.LogWarning(RepoLogEvents.GetItems, response);
            _specificLogger.LogError(RepoLogEvents.AddItem, response);
            _specificLogger.LogCritical(RepoLogEvents.UpdateItem, response);
            return response;
        }

        [HttpGet, Route("WithException")]
        public string GetAllLogsWithException()
        {
            try
            {
                throw new Exception("Oops! I did it again.");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An exception is caught.");
                _specificLogger.LogCritical(ex, "An exception is caught.");
                return $"GET: Controller => {nameof(HomeController)}; Action => {nameof(GetAllLogsWithException)}.";
            }
        }

        [HttpGet, Route("WithLogScope")]
        public string GetAllLogsWithLogScope()
        {
            var response = $"GET: Controller => {nameof(HomeController)}; Action => {nameof(GetAllLogsWithLogScope)}.";

            using (_logger.BeginScope("1234567890"))
            {
                _logger.LogInformation("Первое значение");
                _logger.LogInformation("Второе значение");
                _logger.LogInformation("Третье значение");
            }

            _logger.LogInformation("Четвертое значение");

            return response;
        }

        [HttpGet, Route("Index")]
        public string Index()
        {
            var response = $"GET: Controller => {nameof(HomeController)}; Action => {nameof(Index)}";
            _logger.LogInformation(response);
            return response;
        }
    }
}
