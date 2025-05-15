namespace Utilitary.API.Controllers.v1
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Utilitary.API.Controllers.Common;
    using Utilitary.Core.Estudiantes.Commands;
    using Utilitary.Core.Estudiantes.Queries;

    /// <summary> 
    /// 
    /// </summary>
    public class EstudiantesController : BaseController
    {
        /// <summary> 
        /// login del usuario
        /// </summary>
        /// <response code="200"> Retorna la información del usuario logueado </response>
        /// <remark></remark>
        [HttpPost(ApiRoutes.Login.GetLoginUser)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLoginUser([FromBody] GetLoginUserQry request)
        {
            var result = await Mediador.Send(request);
            return Ok(result);
        }

        /// <summary> 
        /// Cambiar contraseña
        /// </summary>
        /// <response code="200"> Actualiza la contraseña al usuario </response>
        /// <remark></remark>
        [HttpPost(ApiRoutes.Login.UpdatePassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePasswordUser([FromBody] UpdatePasswordCmd request)
        {
            var result = await Mediador.Send(request);
            return Ok(result);
        }

        /// <summary> 
        /// Recuperar contraseña
        /// </summary>
        /// <response code="200"> Retorna el Password desencriptado y envía correo de recuperación al usuario </response>
        /// <remark></remark>
        [HttpGet(ApiRoutes.Login.GetPasswordForget)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPasswordForget([FromRoute] GetPasswordForgetQry request)
        {
            var result = await Mediador.Send(request);
            return Ok(result);
        }

        /// <summary> 
        /// Consultar menu
        /// </summary>
        /// <response code="200"> Consulta menu y submenu por usuario</response>
        /// <remark></remark>
        [HttpGet(ApiRoutes.Login.GetMenu)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMenu([FromRoute] GetMenuQry data)
        {
            return Ok(await Mediador.Send(data));
        }

        
    }
}