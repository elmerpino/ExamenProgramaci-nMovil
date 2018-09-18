using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;

namespace AppExamen
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Vendido : ContentPage
	{
        async private void Venta_Clicked(object sender, EventArgs e)
        {
            var detailPage = new Ventas();
            await Navigation.PushAsync(detailPage);
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            AbrirBase();
        }

        public void AbrirBase()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");

            var db = new SQLiteConnection(rutaDb);

            db.CreateTable<modelos.Venta>();
            db.CreateTable<modelos.Clientes>();
            db.CreateTable<modelos.Productos>();

            string query = "SELECT Venta.Id as Id, Clientes.Nombre AS Cliente,Productos.Nombre as Producto, Productos.PreciodeVenta as Costo, Venta.Fecha AS Fecha FROM Clientes " +
                "INNER JOIN Venta ON Clientes.Id = Venta.Cliente INNER JOIN Productos ON Productos.Id = Venta.Producto";

            var todoslosproductos = db.Query<modelos_aux.Venta>(query).ToList();

            lst.ItemsSource = null;
            lst.ItemsSource = todoslosproductos;
        }
        
        private async void ItemSeleccionado(object sender, ItemTappedEventArgs e)
        {
            var cliente = e.Item as modelos_aux.Venta;
            var EditarVenta = new VistaVenta();
            EditarVenta.id_venta(cliente.Id);
            EditarVenta.BindingContext = cliente;
            await Navigation.PushAsync(EditarVenta);
        }

        public Vendido()
        {
            InitializeComponent();
            AbrirBase();
            this.Appearing += MainPage_Appearing;
        }
    }
}