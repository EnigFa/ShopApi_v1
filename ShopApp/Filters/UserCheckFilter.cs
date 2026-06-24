using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Domain.Models;

namespace Shop.App.Filters;

public class UserCheckFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("UserCheckFilter running"); //...
        var user = context.ActionArguments["user"] as User;

        if (user == null || user.Login != "admin" || user.Id != 1)
        {
            context.Result = new JsonResult(new
            {
                message = "No authorization"
            })
            {
                StatusCode = 401
            };
        }
    }
}