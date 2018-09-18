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
	public partial class Calendario : ContentPage
	{
		public Calendario (Ventas v)
		{
			InitializeComponent ();
            this.v = v;
        }

        string fecha = "18/9/2018";
        Ventas v;

        async private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {           
                string[] calendarioSeparar;
                string CalendarioSinAcomodar = e.NewDate.ToString();
                calendarioSeparar = CalendarioSinAcomodar.Split(' ');
                string[] fechaSeparar;
                string fechaSinSeparar = calendarioSeparar[0];
                fechaSeparar = fechaSinSeparar.Split('/');
                fecha = fechaSeparar[1] + "/" + fechaSeparar[0] + "/" + fechaSeparar[2];


            v.Fecha_Calendario(fecha);
            await Navigation.PopAsync();
        }

        async private void Fecha_Clicked(object sender, EventArgs e)
        {
            v.Fecha_Calendario(fecha);
            await Navigation.PopAsync();
        }
    }
}