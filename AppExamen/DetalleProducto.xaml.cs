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
	public partial class DetalleProducto : ContentPage
	{
		public DetalleProducto ()
		{
			InitializeComponent ();
		}

        private async void ActualizarRegistro(object sender, EventArgs e)
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            var db = new SQLiteConnection(rutaDb);

            var registro = new modelos.Productos
            {
                Id = int.Parse(Id.Text),
                Nombre = nombre.Text,
                PreciodeCosto = double.Parse(preciodecosto.Text),
                Cantidad = int.Parse(cantidad.Text),
                PreciodeVenta = double.Parse(preciodeventa.Text),
                Descripcion = descipcion.Text,
                Foto = foto.Text
            };

            db.Table<modelos.Productos>();
            db.Update(registro);
            await DisplayAlert("", "Producto actualizado", "Aceptar");
            await Navigation.PopAsync();
        }

        async private void EliminarProducto(object sender, EventArgs e)
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            var db = new SQLiteConnection(rutaDb);

            int MiId = int.Parse(Id.Text);

            var respuesta = await DisplayAlert("", "¿Esta seguro de que lo desea eliminar?", "Si", "No");

            if (respuesta == true)
            {
                db.Delete<modelos.Productos>(MiId);
                await DisplayAlert("", "Producto eliminado", "Aceptar");
                await Navigation.PopAsync();
            }
        }
    }
}