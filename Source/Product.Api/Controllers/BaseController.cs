using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Product.Api.Controllers
{
    
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly ILogger<ProductController> Logger;
        protected readonly T Service;
        public BaseController(ILogger<ProductController> logger, T service) {
            Logger = logger;
            Service = service;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public NotFoundResult NotFound(string logMessage)
        {
            Logger.LogWarning($"Not Found: {logMessage}");
            return this.NotFound();
        }
    }
}
