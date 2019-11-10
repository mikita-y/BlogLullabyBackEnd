using System;
using System.IO;
using BlogLullaby.WEB_API.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BlogLullaby.WEB_API.Filters
{
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        ILogger _logger;
        public CustomExceptionFilterAttribute(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            _logger = loggerFactory.CreateLogger("FileLogger");
        }

        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string exceptionStack = context.Exception.StackTrace;
            string exceptionMessage = context.Exception.Message;
            /*context.Result = new ContentResult
            {
                Content = $"В методе {actionName} возникло исключение: \n {exceptionMessage} \n {exceptionStack}"
            };*/
            _logger.LogError($"{DateTime.Now} \r\n В методе {actionName} возникло исключение: \r\n {exceptionMessage} \r\n {exceptionStack} \r\n");
            context.ExceptionHandled = true;
        }
    }
}
