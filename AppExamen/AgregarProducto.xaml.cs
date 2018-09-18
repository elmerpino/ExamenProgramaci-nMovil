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
    public partial class AgregarProducto : ContentPage
    {
        private void AgregarProductos()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            // DisplayAlert("Ruta de la base de datos", rutaDb, "ok");
            // Crea la base de datos si no existe, y crea una conexión
            var db = new SQLiteConnection(rutaDb);

            // Crea la tabla si no existe

            db.CreateTable<modelos.Productos>();


            var registro = new modelos.Productos
            {
                Nombre = nombre.Text,
                PreciodeCosto = double.Parse(preciodecosto.Text),
                Cantidad = int.Parse(cantidad.Text),
                PreciodeVenta = double.Parse(preciodeventa.Text),
                Descripcion = descipcion.Text,
                Foto = foto.Text
            };

            db.Insert(registro);
            DisplayAlert("Agregar", "El registro fue agregado con exito!", "ok");
        }

        public AgregarProducto()
        {
            InitializeComponent();
        }

        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            AgregarProductos();
            var detailPage = new AgregarProducto();
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}