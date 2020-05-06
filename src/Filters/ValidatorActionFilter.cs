namespace MovieWeb.WebApi.Filter
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using MovieWeb.WebApi.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ValidatorActionFilter : IActionFilter
    {   
        private readonly ILogger<ValidatorActionFilter> _logger;

        public ValidatorActionFilter(ILogger<ValidatorActionFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var message = new List<string>();
                message.AddRange(context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
               
                context.Result = new BadRequestObjectResult(
                    new BaseResponse 
                    { 
                        IsSuccessful = false, 
                        Message = message,  
                        HttpStatusCode = StatusCodes.Status400BadRequest
                    });

                _logger.LogWarning(CreateMessage(context, message));
            }
        }

        private string CreateMessage(ActionExecutingContext context, List<string> errorMessage)
        {
            var sb = new StringBuilder();

            errorMessage.ForEach(x => 
            {
                sb.AppendLine(x);
            });

            var message = $"Exception caught in global error handler, exception message: {sb}";

            return $"{message} RequestId: {context.HttpContext.TraceIdentifier}";
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}
