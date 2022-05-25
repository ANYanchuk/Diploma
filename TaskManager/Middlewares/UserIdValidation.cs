using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

using static TaskManager.Data.Helpers.TokenHelper;

namespace TaskManager.Middlewares;

public class TokenMiddleware
{
    private readonly RequestDelegate _next;

    public TokenMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string token = context.Request.Headers.Authorization;
        string urlToken = context.Request.Query["userId"];
        IEnumerable<Claim> claims = DecodeToken(token);
        if (claims.FirstOrDefault(c => c.Type == "Id") is Claim idClaim && idClaim.Value == urlToken)
        {
            Console.WriteLine($"Welcome to the club, buddy");
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("КУДА ЛІЗЕШ????");
        }
    }
}