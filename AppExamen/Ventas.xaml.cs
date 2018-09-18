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
    public partial class Ventas : ContentPage
    {
    public Ventas()
        {
            InitializeComponent();
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
        

        async private void Ventas_Clicked(object sender, EventArgs e)
        {
            var detailPage = new MainPage();
            AgregarVenta();
            await Navigation.PushAsync(detailPage);
        }

        async private void Fecha_Clicked(object sender, EventArgs e)
        {
            var detailPage = new Calendario(this);
            await Navigation.PushAsync(detailPage);
        }

        async private void Cliente_Clicked(object sender, EventArgs e)
        {
            var detailPage = new Cliente(this);
            await Navigation.PushAsync(detailPage);
        }

        async private void Producto_Clicked(object sender, EventArgs e)
        {
            var detailPage = new Producto(this);
            await Navigation.PushAsync(detailPage);
        }

        string fecha;

        public void Fecha_Calendario(string fecha_venta)
        {
            Fecha.Text = fecha_venta;
            fecha = fecha_venta;
        }

        private void AgregarVenta()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            // DisplayAlert("Ruta de la base de datos", rutaDb, "ok");
            // Crea la base de datos si no existe, y crea una conexión
            var db = new SQLiteConnection(rutaDb);

            // Crea la tabla si no existe

            db.CreateTable<modelos.Venta>();

            string pagado;
            if (recordar.IsToggled)
            {
                pagado = "Si";
            }
            else {
                pagado = "No";
            }

            var registro = new modelos.Venta
            {
                Fecha = fecha,
                Cliente = c.Id,
                Producto = p.Id,
                Pagado = pagado,
            };

            db.Insert(registro);
            DisplayAlert("Agregar", "El registro fue agregado con exito!", "ok");
        }
    }
}