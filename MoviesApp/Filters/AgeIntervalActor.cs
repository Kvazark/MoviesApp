using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesApp.Filters
{
    public class AgeIntervalActor : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var DateOfBirth = DateTime.Parse(context.HttpContext.Request.Form["DateOfBirth"]);
            if (Math.Abs(DateOfBirth.Year - DateTime.Now.Year) < 7 || Math.Abs(DateOfBirth.Year - DateTime.Now.Year) > 99)
            {
                context.Result = new BadRequestResult();
            }
        }
        

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }
    }
}