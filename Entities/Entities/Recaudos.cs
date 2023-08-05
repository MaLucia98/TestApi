using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Recaudos
    {
        public int Id { get; set; }
        public int Hora { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public int EstacionId { get; set; }
        public virtual Estaciones Estacion { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categorias Categoria { get; set; }
        public int SentidoId { get; set; }
        public virtual Sentidos Sentido { get; set; }

    }
}
