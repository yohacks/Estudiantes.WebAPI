namespace Utilitary.API.Controllers.Common
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary> 
    /// 
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;

        /// <summary> 
        /// 
        /// </summary>
        protected IMediator Mediador => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
