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
	public partial class AgregarCliente : ContentPage
	{

        private void AgregarClientes()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio1.db");
            // DisplayAlert("Ruta de la base de datos", rutaDb, "ok");
            // Crea la base de datos si no existe, y crea una conexión
            var db = new SQLiteConnection(rutaDb);

            // Crea la tabla si no existe

            db.CreateTable<modelos.Clientes>();


            var registro = new modelos.Clientes
            {
                Nombre = nombre.Text,
                Telefono = telefono.Text,
                Email = email.Text,
                Comentarios = comentarios.Text,
                Foto = foto.Text
            };

            db.Insert(registro);
            DisplayAlert("Agregar", "El registro fue agregado con exito!", "ok");
        }

        public AgregarCliente()
        {
            InitializeComponent();
        }

        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            AgregarClientes();
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}