using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace AppExamen.modelos
{
    class Productos
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double PreciodeVenta { get; set; }
        public int Cantidad { get; set; }
        public double PreciodeCosto { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
    }
}
