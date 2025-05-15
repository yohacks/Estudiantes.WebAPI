namespace Utilitary.API.Controllers.v1
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Utilitary.API.Controllers.Common;
    using Utilitary.Core.DashBoard.Queries;

    /// <summary> 
    /// 
    /// </summary>
    public class DashBoardController : BaseController
    {
        /// <summary> 
        /// Obtiene la cantidad de personas del curso
        /// </summary>
        /// <response code="200"> Retorna la información de los cursos  </response>
        /// <remark></remark>
        [HttpGet(ApiRoutes.DashBoard.GetPersonasCurso)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonasCurso([FromRoute] GetPersonasCursoQry request)
        {
            var result = await Mediador.Send(request);
            return Ok(result);
        }

        /// <summary> 
        /// Retorna la cantidad de personas en el curso
        /// </summary>
        /// <response code="200"> Retorna la informacion del curso </response>
        /// <remark></remark>
        [HttpGet(ApiRoutes.DashBoard.GetCantidadCurso)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCantidadCurso([FromRoute] GetCantidadCursoQry request)
        {
            var result = await Mediador.Send(request);
            return Ok(result);
        }

    }
}