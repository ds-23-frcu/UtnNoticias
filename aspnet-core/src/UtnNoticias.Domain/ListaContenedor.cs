using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace UtnNoticias
{
	public class ListaContenedor : AuditedAggregateRoot<Guid>, Componente
	{
		public string Nombre { get; set; }
		public List<Componente> Componentes { get; set; }

	}
}
