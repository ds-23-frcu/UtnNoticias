using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtnNoticias
{
    internal class Alerta
    {
        public string Tema { get; set; }
        public EstadoAlerta Estado { get; set; }
        public Busqueda busqueda { get; set; }
        public ListaContenedor listacontenedor { get; set; }    


    }
}
