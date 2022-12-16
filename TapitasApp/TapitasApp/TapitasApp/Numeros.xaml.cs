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
            Jugadores = numJugadores;
            Tapas = numTapas;
            generaCampos();
        }

        public static int Jugadores { get; set; }
        public int Tapas { get; set; }

        Entry[] campoTexto = new Entry[Jugadores];

        public void generaCampos()
        {

            for (var i = 0; i < Jugadores; i++)
            {
                campoTexto[i] = new Entry();
                campoTexto[i].Placeholder = $"Jugador {i}";

                panelTelJugadores.Children.Add(campoTexto[i]);
            }
        }

        public void loquesea(Entry[] campo)
        {
            for(var i = 0; i < campo.Length; i++)
            {
            DisplayAlert("Alerta", $"{campo[i].Text}", "OK");
            }
        }


        //public void AgregarLabel(StackLayout sl, string name)
        //{
        //    Label label = new Label() { Text = name };
        //    label.TextColor = Color.Black;
        //    label.FontAttributes = FontAttributes.Bold;

        //    sl.Children.Add(label);
        //}

        //public void LeerTexto()
        //{
        //    foreach(View v in panelTelJugadores.Children)
        //    {
        //        if(v.StyleId != null)
        //        {
        //            DisplayAlert("Alerta","No puedes estar vacios","Ok");
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
            loquesea(campoTexto);
        }
    }
}