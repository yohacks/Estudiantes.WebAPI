namespace Utilitary.API.Controllers.v1
{
    /// <summary> 
    /// 
    /// </summary>
    public static class ApiRoutes
    {
        /// <summary> 
        /// 
        /// </summary>
        public const string Root = "estudiantes";
        /// <summary> 
        /// 
        /// </summary>
        public const string Version = "v1";
        /// <summary> 
        /// 
        /// </summary>
        public const string Base = Root + "/" + Version;


        /// <summary> 
        /// Métodos para consultar la información del cierre de actividades diarias
        /// </summary>
        public static class Administracion
        {
            /// <summary> 
            /// 
            /// </summary>
            public const string GetInfoUsuario = Base + "/Administracion/GetInfoUsuario";

            /// <summary> 
            /// 
            /// </summary>
            public const string GetPerfiles = Base + "/Administracion/GetPerfiles";

            /// <summary> 
            /// Referencia URL que crea o edita un usuario con su perfil correspondiente
            /// </summary>
            public const string PostUsuario = Base + "/Administracion/PostUsuario";

            /// <summary> 
            /// Referencia URL que crea una materia
            /// </summary>
            public const string PostMateria = Base + "/Administracion/PostMateria";

            /// <summary> 
            /// Referencia URL que crea un curso
            /// </summary>
            public const string PostCurso = Base + "/Administracion/PostCurso";

            /// <summary> 
            /// Referencia URL que crea un curso
            /// </summary>
            public const string PostCursoEstudiante = Base + "/Administracion/PostCursoEstudiante";


            /// <summary> 
            /// 
            /// </summary>
            public const string GetCursosRegistrados = Base + "/Administracion/GetCursosRegistrados";

            /// <summary> 
            /// 
            /// </summary>
            public const string GetMaterias = Base + "/Administracion/GetMaterias";

            /// <summary> 
            /// 
            /// </summary>
            public const string GetCursos = Base + "/Administracion/GetCursos";

            /// <summary> 
            /// 
            /// </summary>
            public const string DeleteUsuario = Base + "/Administracion/DeleteUsuario/{IdUsuario}";

            /// <summary> 
            /// 
            /// </summary>
            public const string GetUserPassword = Base + "/Administracion/GetUserPassword/{Username}";

            /// <summary> 
            /// 
            /// </summary>
            public const string GetInfoDocente = Base + "/Administracion/GetInfoDocente";

        }

       

        /// <summary> 
        /// Métodos para consultar la información del usuario
        /// </summary>
        public static class Login
        {
            /// <summary> 
            /// 
            /// </summary>
            public const string GetLoginUser = Base + "/Login/GetLoginUser/";

            /// <summary> 
            /// 
            /// </summary>
            public const string UpdatePassword = Base + "/Login/UpdatePassword/";

            /// <summary> 
            /// 
            /// </summary>
            public const string GetPasswordForget = Base + "/Login/GetPasswordForget/{Username}";

            /// <summary> 
            /// 
            /// </summary>
            public const string GetMenu = Base + "/Menu/GetMenu/{IdPerfil}/{Referencia}";
        }

        /// <summary> 
        /// Métodos para consultar la información del dashboard al momento de loguearse en el aplicativo
        /// </summary>
        public static class DashBoard
        {
            /// <summary> 
            /// 
            /// </summary>
            public const string GetPersonasCurso = Base + "/DashBoard/GetPersonasCurso/{Referencia}/{IdUsuario}";

            /// <summary> 
            /// 
            /// </summary>
            public const string GetCantidadCurso = Base + "/DashBoard/GetCantidadCurso/{Referencia}";


        }



    }
}