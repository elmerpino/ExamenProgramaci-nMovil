using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Microcharts;
using SkiaSharp;
using Microcharts.Forms;

namespace AppExamen
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Grafica : ContentPage
	{

        public void abrirBase(object sender, EventArgs e)
        {

            //Crear la ruta donde se almacenara la base de datos
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");

            //Abrir la base de datos y en caso de que no exista se creara
            var db = new SQLiteConnection(rutaDb);

            //Abrir la tabla de productos y en cso de que no exista se creara
            db.CreateTable<modelos.Venta>();
            db.CreateTable<modelos.Productos>();


            //Cargar la lista de todos los productos a una arreglo
            string query = "SELECT COUNT(Venta.Id) as Cantidad, Productos.Nombre as Producto FROM Venta JOIN Productos ON Venta.Producto=Productos.Id GROUP BY Productos.Id ORDER BY 1 DESC LIMIT 5";

            List<modelos_aux.GraficaProductos> p = null;
            p = db.Query<modelos_aux.GraficaProductos>(query).ToList();

            //Cargar la lista de todos los clientes a una arreglo
            query = "SELECT COUNT(Venta.Id) as Cantidad, Clientes.Nombre as Cliente FROM Venta JOIN Clientes ON Venta.Cliente=Clientes.Id GROUP BY Clientes.Id ORDER BY 1 DESC LIMIT 5";
            List<modelos_aux.GraficaClientes> c = null;
            c = db.Query<modelos_aux.GraficaClientes>(query).ToList();

            mostrarGrafico(p, c);
        }

        List<Microcharts.Entry> _entriesProducto;
        List<Microcharts.Entry> _entriesCliente;

        private void mostrarGrafico(List<modelos_aux.GraficaProductos> productos, List<modelos_aux.GraficaClientes> clientes)
        {
            _entriesProducto = null;
            _entriesProducto = new List<Microcharts.Entry>();
            foreach (var i in productos)
            {
                var pro = new Microcharts.Entry(i.Cantidad)
                {
                    Label = i.Producto,
                    ValueLabel = i.Cantidad.ToString(),
                    Color = SKColor.Parse("#00CC00")
                };
                _entriesProducto.Add(pro);
            }

            _entriesCliente = null;
            _entriesCliente = new List<Microcharts.Entry>();

            foreach (var j in clientes)
            {
                var clie = new Microcharts.Entry(j.Cantidad)
                {
                    Label = j.Cliente,
                    ValueLabel = j.Cantidad.ToString(),
                    Color = SKColor.Parse("#3399FF")
                };
                _entriesCliente.Add(clie);
            }

            GraficoProducto.Chart = new BarChart { Entries = _entriesProducto };
            GraficoCliente.Chart = new BarChart { Entries = _entriesCliente };
        }


        public Grafica()
        {
            InitializeComponent();
            this.Appearing += abrirBase;
        }
    }
}