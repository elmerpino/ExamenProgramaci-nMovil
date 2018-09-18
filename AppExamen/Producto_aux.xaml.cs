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
	public partial class Producto_aux : ContentPage
	{
        public void AbrirBase()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");

            var db = new SQLiteConnection(rutaDb);
            db.CreateTable<modelos.Productos>();
            var todoslosproductos = db.Table<modelos.Productos>().ToList();

            lst.ItemsSource = null;
            lst.ItemsSource = todoslosproductos;
        }


        public Producto_aux(VistaVenta v)
        {
            InitializeComponent();
            this.v = v;
            AbrirBase();
            lst.IsRefreshing = false;
            this.Appearing += MainPage_Appearing; ;
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            AbrirBase();
        }

        async private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            var detailPage = new AgregarProducto();
            await Navigation.PushAsync(detailPage);
            AbrirBase();
        }

        VistaVenta v;
        private async Task ItemSeleccionado(object sender, ItemTappedEventArgs e)
        {
            var producto = e.Item as modelos.Productos;
            v.BindingContext = producto;
            await Navigation.PopAsync();
        }
    }
}