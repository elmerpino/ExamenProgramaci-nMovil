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
	public partial class VistaVenta : ContentPage
	{
		public VistaVenta ()
		{
			InitializeComponent ();
            this.Appearing += Actualizar;
        }

        modelos.Clientes c;
        modelos.Productos p;

        private void Actualizar(object sender, EventArgs e)
        {
            if (BindingContext is modelos.Clientes)
            {
                c = (modelos.Clientes)BindingContext;
                Cliente.Text = c.Nombre;
            }

            if (BindingContext is modelos.Productos)
            {
                p = (modelos.Productos)BindingContext;
                Producto.Text = p.Nombre;
            }
        }

        async private void Fecha_Clicked(object sender, EventArgs e)
        {
            var detailPage = new Calendario_aux(this);
            await Navigation.PushAsync(detailPage);
        }

        async private void Cliente_Clicked(object sender, EventArgs e)
        {
            var detailPage = new Cliente_aux(this);
            await Navigation.PushAsync(detailPage);
        }

        async private void Producto_Clicked(object sender, EventArgs e)
        {
            var detailPage = new Producto_aux(this);
            await Navigation.PushAsync(detailPage);
        }

        string fecha;

        public void Fecha_Calendario(string fecha_venta)
        {
            Fecha.Text = fecha_venta;
            fecha = fecha_venta;
        }

        int id;

        public void id_venta(int id)
        {
            this.id = id;
        }

        private async void ActualizarRegistro(object sender, EventArgs e)
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            var db = new SQLiteConnection(rutaDb);

            string pago;

            if (recordar.IsToggled)
            {
                pago = "si";
            }
            else
            {
                pago = "no";
            }

            var registro = new modelos.Venta
            {
                Id = id,
                Fecha = Fecha.Text,
                Cliente = c.Id,
                Producto = p.Id,
                Pagado = pago,
            };

            db.Table<modelos.Venta>();
            db.Update(registro);
            await DisplayAlert("", "Venta actualizado", "Aceptar");
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
                db.Delete<modelos.Venta>(MiId);
                await DisplayAlert("", "Venta eliminado", "Aceptar");
                await Navigation.PopAsync();
            }
        }
    }
}