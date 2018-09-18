using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace AppExamen.modelos
{
    class Venta
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Fecha { get; set; }
        public int Cliente { get; set; }
        public int Producto { get; set; }
        public string Pagado { get; set; }
    }
}
