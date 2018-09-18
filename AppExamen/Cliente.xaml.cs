using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using System.Diagnostics;

namespace AppExamen
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cliente : ContentPage
	{
        public void AbrirBase()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");

            var db = new SQLiteConnection(rutaDb);
            db.CreateTable<modelos.Clientes>();

            var todoslosproductos = db.Table<modelos.Clientes>().ToList();

            lst.ItemsSource = null;
            lst.ItemsSource = todoslosproductos;
        }


        public Cliente(Ventas v)
        {
            InitializeComponent();
            this.v = v;
            AbrirBase();
            lst.IsRefreshing = false;
            this.Appearing += MainPage_Appearing; 
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {

            AbrirBase();
        }

        async private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            var detailPage = new AgregarCliente();
            await Navigation.PushAsync(detailPage);
            AbrirBase();
        }

        Ventas v;
        
        private async void ItemSeleccionado(object sender, ItemTappedEventArgs e)
        {
            var cliente = e.Item as modelos.Clientes;
            v.BindingContext = cliente;
            await Navigation.PopAsync();
        }
    }
}