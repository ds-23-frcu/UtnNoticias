﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Localization;

namespace UtnNoticias
{
	public class Usuario
	{
		public int IdUsuario { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }

		public TipoLenguaje Lenguaje { get; set; }
	}
}
