using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TapitasApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnRepartir_Clicked(object sender, EventArgs e)
        {

            if(!string.IsNullOrEmpty(txtNumJugadores.Text) && !string.IsNullOrEmpty(txtNumTapas.Text))
            {
                var numJugadores = int.Parse(txtNumJugadores.Text);
                var numTapas = int.Parse(txtNumTapas.Text);

                if(numJugadores <= 1 || numJugadores > 4)
                {
                    DisplayAlert("Alerta", "Número de jugadores no valido", "oK");
                    return;
                }
                    Navigation.PushAsync(new Numeros(numJugadores));
                    txtNumJugadores.Text = "";
                    txtNumTapas.Text = "";
            }
            else
            {
                DisplayAlert("Alerta", "Todos los campos son requeridos", "Ok");
            }
        }
    }
}
