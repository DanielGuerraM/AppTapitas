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
            Jugadores = numJugadores;
            Tapas = numTapas;
            InitializeComponent();
            generaCampos();
        }

        public static int Jugadores { get; set; }
        public int Tapas { get; set; }

        Entry[] campoTexto = new Entry[Jugadores];

        public void generaCampos()
        {
            campoTexto = new Entry[Jugadores];
            for (var i = 0; i < Jugadores; i++)
            {
                campoTexto[i] = new Entry();
                campoTexto[i].Placeholder = $"Jugador {i}";
                campoTexto[i].MaxLength = 10;
                campoTexto[i].Keyboard = Keyboard.Telephone;

                Label label = new Label();
                label.Text = "Jugador " + (i+1);
                label.TextColor = Color.Black;

                panelTelJugadores.Children.Add(label);
                panelTelJugadores.Children.Add(campoTexto[i]);
            }
        }

        public void LeerNumeros(Entry[] campo)
        {
            long[] telefonosJugadores = new long[Jugadores];

            for(var i = 0; i < campo.Length; i++)
            {
                telefonosJugadores[i] = Convert.ToInt64(campoTexto[i].Text);

                DisplayAlert("Alerta", $"{telefonosJugadores[i]}","Ok");
            }
        }

        //public void estanVaciosCamposTelefonos()
        //{
        //    for(var i = 0; i < campoTexto.Length; i++)
        //    {
        //        if(campoTexto[i].ToString() == "")
        //        {
        //            DisplayAlert("Alerta","Los campos de telefonos son obligatorios","Ok");
        //        }
        //    }
        //}

        //public int[] numerosTapas(int tapas, int jugadores)
        //{
        //    int[] numeroTapa = new int[tapas * jugadores];

        //    Random rdm = new Random();
        //    bool repetido;
        //    int numero;
        //    int indice = 0;

        //    while (indice < numeroTapa.Length)
        //    {
        //        repetido = false;
        //        numero = rdm.Next(1, 16);

        //        for (int i = 0; i < indice; i++)
        //        {
        //            if (numeroTapa[i] == numero)
        //            {
        //                repetido = true;
        //            }
        //        }

        //        if (!repetido)
        //        {
        //            numeroTapa[indice] = numero;
        //            indice++;
        //        }
        //    }
        //    return numeroTapa;
        //}

        private void Button_Clicked(object sender, EventArgs e)
        {
            LeerNumeros(campoTexto);
        }
    }
}