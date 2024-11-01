using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.Services.CacheService;
using Store.Service.Services.CachService;
using System.Text;

namespace Store.Web.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private int _timeToLiveInSeconds;

        public CacheAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cacheServices = context.HttpContext.RequestServices.GetRequiredService<ICachService>();

            var cacheKey = GenerateCachKeyFromRequest(context.HttpContext.Request);

            var cacheResponse = await _cacheServices.GetCacheResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))
                {
                var ContentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = ContentResult;
                return;
                }
            var excutedContext = await next();

            if (excutedContext.Result is OkObjectResult response)
            {
                await _cacheServices.SetCacheResponseAsync(cacheKey, response.Value,TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
        }

        private string GenerateCachKeyFromRequest(HttpRequest request)
        {
            StringBuilder cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}");
            foreach (var (key, Value) in request.Query.OrderBy(x => x.Key))
            {
                cacheKey.Append($"{key}-{Value}");
            }
            return cacheKey.ToString();
        }
    }
}
