using System.Collections.Generic;

namespace Utilitary.Domine
{
    public class MenuModel
    {
        public int IdOpcion { get; set; }
        public string Descripcion { get; set; }
        public int? ParentId { get; set; }
        public string Url { get; set; }
        public int? Posicion { get; set; }
        public string Icon { get; set; }
        public string Titulo { get; set; }
        public string UrlManual { get; set; }
        public string UrlImagen { get; set; }
        public List<MenuModel> Items { get; set; }
    }
   
}
