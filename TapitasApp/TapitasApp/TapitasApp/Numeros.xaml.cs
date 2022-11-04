using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TapitasApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Numeros : ContentPage
    {
        public Numeros(int numJugadores)
        {
            InitializeComponent();



            for(var i = 1; i <= numJugadores; i++)
            {   
                Label lbl = new Label();
                lbl.Text = "Jugador " + i.ToString();
                lbl.TextColor = Color.Black;
                lbl.FontAttributes = FontAttributes.Bold;

                Entry entry = new Entry();
                entry.Placeholder = "321 0000000";
                entry.Keyboard = Keyboard.Telephone;
                entry.MaxLength = 10;

                panelTelJugadores.Children.Add(lbl);
                panelTelJugadores.Children.Add(entry);

            }
        }
    }
}