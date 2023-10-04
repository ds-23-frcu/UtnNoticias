using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtnNoticias
{
    internal class Noticia
    {
        public string Tema {  get; set; }
        public string Autor {  get; set; }
        public string Titulo {  get; set; }
        public string Link { get; set; }
        public string Descripcion { get; set; }
        public DateOnly FechaPublicacion { get; set; }
        public string Contenido {  get; set; }  
        public DateTime fechaLectura { get; set; }  

    }
}
