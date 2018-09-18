using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace AppExamen.modelos
{
    class Clientes
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Comentarios { get; set; }
        public string Foto { get; set; }
    }
}
