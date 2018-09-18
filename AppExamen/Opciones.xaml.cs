using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppExamen
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Opciones : ContentPage
	{
		public Opciones ()
		{
			InitializeComponent ();
		}

        async private void cliente_clicked(object sender, EventArgs e)
        {
            var detailPage = new VistaCliente();
            await Navigation.PushAsync(detailPage);
        }

        async private void producto_clicked(object sender, EventArgs e)
        {
            var detailPage = new VistaProducto();
            await Navigation.PushAsync(detailPage);
        }
    }
}