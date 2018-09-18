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
	public partial class DetalleCliente : ContentPage
	{
		public DetalleCliente ()
		{
			InitializeComponent ();
		}

        private async void ActualizarRegistro(object sender, EventArgs e)
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            var db = new SQLiteConnection(rutaDb);

            var registro = new modelos.Clientes
            {
                Id = int.Parse(Id.Text),
                Nombre = nombre.Text,
                Telefono = telefono.Text,
                Email = email.Text,
                Comentarios = comentarios.Text,
                Foto = foto.Text
            };

            db.Table<modelos.Clientes>();
            db.Update(registro);
            await DisplayAlert("", "Cliente actualizado", "Aceptar");
            await Navigation.PopAsync();
        }

        async private void EliminarCliente(object sender, EventArgs e)
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            var db = new SQLiteConnection(rutaDb);

            int MiId = int.Parse(Id.Text);

            var respuesta = await DisplayAlert("", "¿Esta seguro de que lo desea eliminar?", "Si", "No");

            if (respuesta == true)
            {
                db.Delete<modelos.Clientes>(MiId);
                await DisplayAlert("", "Cliente eliminado", "Aceptar");
                await Navigation.PopAsync();
            }
        }
    }
}