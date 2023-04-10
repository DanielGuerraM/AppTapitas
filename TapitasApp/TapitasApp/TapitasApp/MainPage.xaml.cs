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
            string numJugadoresSinPuntos = txtNumJugadores.Text.Replace(".", "");
            string numTapasSinPuntos = txtNumTapas.Text.Replace(".", "");

            if (!string.IsNullOrEmpty(numJugadoresSinPuntos) && !string.IsNullOrEmpty(numTapasSinPuntos))
            {
                var numJugadores = int.Parse(numJugadoresSinPuntos);
                var numTapas = int.Parse(numTapasSinPuntos);

                if (numJugadores <= 1 || numJugadores > 4 || numTapas == 0)
                {
                    DisplayAlert("Alerta", "Número de jugadores o tapas no valido", "oK");
                    return;
                }
                else if(numJugadores == 4 && numTapas >= 4 || numTapas == 0)
                {
                    DisplayAlert("Alerta", "Número de tapas o tapas no valido", "oK");
                    return;
                }
                else if (numJugadores == 3 && numTapas > 5 || numTapas == 0)
                {
                    DisplayAlert("Alerta", "Número de tapas o tapas no valido", "oK");
                    return;
                }
                else if (numJugadores == 2 && numTapas > 6 || numTapas == 0)
                {
                    DisplayAlert("Alerta", "Número de tapas o tapas no valido", "oK");
                    return;
                }

                Navigation.PushAsync(new Numeros(numJugadores, numTapas));
                    txtNumJugadores.Text = "";
                    txtNumTapas.Text = "";
            }
            else
            {
                DisplayAlert("Alerta", "Los campos no pueden estar vacios ni tener puntos", "Ok");
            }
        }
    }
}
