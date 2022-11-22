using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TapitasApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Numeros : ContentPage
    {
        public Numeros(int numJugadores, int numTapas)
        {
            InitializeComponent();
            generaCampos(numJugadores);

            Jugadores = numJugadores;
            Tapas = numTapas;
        }

        public int Jugadores { get; set; }
        public int Tapas { get; set; }

        public void generaCampos(int jugadores)
        {
            for (var i = 1; i <= jugadores; i++)
            {
                Label lbl = new Label();
                lbl.Text = "Jugador " + i.ToString();
                lbl.TextColor = Color.Black;
                lbl.FontAttributes = FontAttributes.Bold;

                Entry entry = new Entry();
                entry.Placeholder = "321 0000000";
                entry.Keyboard = Keyboard.Telephone;
                entry.HorizontalTextAlignment = TextAlignment.Center;
                entry.MaxLength = 10;

                panelTelJugadores.Children.Add(lbl);
                panelTelJugadores.Children.Add(entry);
            }
        }

        public int[] numerosTapas(int tapas, int jugadores)
        {
            int[] numeroTapa = new int[tapas * jugadores];

            int n;
            Random rdm = new Random();

            for(var i = 0; i < numeroTapa.Length; i++)
            {
                numeroTapa[i] = rdm.Next(1, 16);
            }

            return numeroTapa;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            numerosTapas(this.Tapas, this.Jugadores);
            Hola mijo;
        }
    }
}