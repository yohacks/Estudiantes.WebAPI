namespace Utilitary.API.Controllers.v1
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Utilitary.API.Controllers.Common;
    using Utilitary.Core.Administracion.Commands;
    using Utilitary.Core.Administracion.Queries;
    using Utilitary.Core.Estudiantes.Commands;
    using Utilitary.Core.Estudiantes.Queries;

    /// <summary> 
    /// 
    /// </summary>
    public class AdministracionController : BaseController
    {
        /// <summary> 
        /// 
        /// </summary>
        /// <response code="200"> Retorna la información de los usuarios registrados </response>
        /// <remark></remark>
        [HttpGet(ApiRoutes.Administracion.GetInfoUsuario)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfoUsuario([FromRoute] GetInfoUsuarioQry request)
        {
            var result = await Mediador.Send(request);
            return Ok(result);
        }

        /// <summary> 
        /// Crear usuario
        /// </summary>
        /// <response code="200"> Guarda el usuario con su respectivo perfil </response>
        /// <remark></remark>
        [HttpPost(ApiRoutes.Administracion.PostUsuario)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostUsuarioNuevo([FromBody] PostUsuarioCmd data)
        {
            return Ok(await Mediador.Send(data));
        }

        /// <summary> 
        /// Crear usuario
        /// </summary>
        /// <response code="200"> Guarda el usuario con su respectivo perfil </response>
        /// <remark></remark>
        [HttpPost(ApiRoutes.Administracion.PostMateria)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostMateria([FromBody] PostMateriaCmd data)
        {
            return Ok(await Mediador.Send(data));
        }

        /// <summary> 
        ///  Consulta las materias registradas
        /// </summary>
        /// <response code="200"> Consulta las materias en estado 1</response>
        /// <remark></remark>
        [HttpGet(ApiRoutes.Administracion.GetMaterias)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMaterias([FromRoute] GetMateriasQry data)
        {
            return Ok(await Mediador.Send(data));
        }

        /// <summary> 
        ///  Consulta los cursos registrados
        /// </summary>
        /// <response code="200"> Consulta los cursos con toda la informacion que corresponde</response>
        /// <remark></remark>
        [HttpGet(ApiRoutes.Administracion.GetCursos)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCursos([FromRoute] GetCursosQry data)
        {
            return Ok(await Mediador.Send(data));
        }

        /// <summary> 
        /// Recuperar contraseña
        /// </summary>
        /// <response code="200"> Retorna el Password desencriptado del usuario </response>
        /// <remark></remark>
        [HttpGet(ApiRoutes.Administracion.GetUserPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPasswordForget([FromRoute] GetUserPasswordQry request)
        {
            var result = await Mediador.Send(request);
            return Ok(result);
        }

        /// <summary> 
        /// Eliminar Usuario
        /// </summary>
        /// <response code="200"> Elimina el usuario por idUsuario </response>
        /// <remark></remark>
        [HttpPost(ApiRoutes.Administracion.DeleteUsuario)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUsuario([FromRoute] DeleteUsuarioCmd data)
        {
            return Ok(await Mediador.Send(data));
        }

     

        /// <summary> 
        /// Listado de Perfiles
        /// </summary>
        /// <response code="200"> Retorna el listado de todos los perfiles activos </response>
        /// <remark></remark>
        [HttpGet(ApiRoutes.Administracion.GetPerfiles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPerfiles()
        {
            var result = await Mediador.Send(new GetPerfilesQry());
            return Ok(result);
        }

        /// <summary> 
        /// 
        /// </summary>
        /// <response code="200"> Retorna la información de los docentes activos </response>
        /// <remark></remark>
        [HttpGet(ApiRoutes.Administracion.GetInfoDocente)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfoDocente([FromRoute] GetInfoDocenteQry request)
        {
            var result = await Mediador.Send(request);
            return Ok(result);
        }

        /// <summary> 
        /// Crear curso
        /// </summary>
        /// <response code="200"> Guarda el curso asociando la materia al docente </response>
        /// <remark></remark>
        [HttpPost(ApiRoutes.Administracion.PostCurso)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostCurso([FromBody] PostCursoCmd data)
        {
            return Ok(await Mediador.Send(data));
        }

        /// <summary> 
        /// Insertar curso
        /// </summary>
        /// <response code="200"> Agrega un deposito al cierre</response>
        /// <remark></remark>
        [HttpPost(ApiRoutes.Administracion.PostCursoEstudiante)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostCursoEstudiante([FromBody] UpdateCursoCmd data)
        {
            return Ok(await Mediador.Send(data));
        }

        /// <summary> 
        /// Listado de depositos por rango de fechas
        /// </summary>
        /// <response code="200"> Retorna el listado de depositos si tiene </response>
        /// <remark></remark>
        [HttpPost(ApiRoutes.Administracion.GetCursosRegistrados)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepositosRegistrados([FromBody] GetCursosRegistradosQry data)
        {
            return Ok(await Mediador.Send(data));
        }


    }
}