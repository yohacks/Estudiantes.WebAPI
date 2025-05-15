namespace Utilitary.Domine
{
    using System;

    public class InfoUsuarioModel
    {
        public string Usuario { get; set; }
    }
    public class ListadoInfoUsuarioModel
    {
        public int IdUsuario { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Activo { get; set; }
        public int IdPerfil { get; set; }
        public string Descripcion { get; set; }
    }
}
