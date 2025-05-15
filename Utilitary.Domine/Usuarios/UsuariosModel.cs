namespace Utilitary.Domine
{
    using System;
    using System.Collections.Generic;

    public class UsuariosModel
    {
        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaIngreso { get; set; }
    }
}
