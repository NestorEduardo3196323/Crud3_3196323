using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Crud3_3196323
{
    [Table("trabajador")]
    public class Trabajador
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre")]
        public string? Nombre { get; set; }
        [Column("horas")]
        public double Horas { get; set; }
        [Column("pagohora")]
        public double PagoHora { get; set; }
        [Column("sueldo")]
        public double Sueldo { get; set; }
    }
}
