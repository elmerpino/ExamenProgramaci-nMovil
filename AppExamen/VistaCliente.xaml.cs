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
	public partial class VistaCliente : ContentPage
	{
        public void AbrirBase()
        {
            //Crear la ruta donde se almacenara la base de datos
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");

            //Abrir la base de datos y en caso de que no exista se creara
            var db = new SQLiteConnection(rutaDb);

            //Abrir la tabla de productos y en cso de que no exista se creara
            db.CreateTable<modelos.Clientes>();

            //Cargar la lista de todos los productos a una arreglo
            var todoslosproductos = db.Table<modelos.Clientes>().ToList();

            //mostrarla en el lisview perzonalizado para ver los datos de los productos
            lst.ItemsSource = null;
            lst.ItemsSource = todoslosproductos;
        }

        private void seleccionarCliente_Appearing(object sender, EventArgs e)
        {
            AbrirBase();
        }

        public VistaCliente()
        {
            InitializeComponent();
            this.Appearing += seleccionarCliente_Appearing;
        }

        public async void MenuItem1_Clicked()
        {
            await Navigation.PushAsync(new AgregarCliente());
            AbrirBase();
        }

        private async void ItemSeleccionado(object sender, ItemTappedEventArgs e)
        {
            var cliente = e.Item as modelos.Clientes;
            var editarClientes = new DetalleCliente();
            editarClientes.BindingContext = cliente;
            await Navigation.PushAsync(editarClientes);
        }
    }
}