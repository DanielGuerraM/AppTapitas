using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using OpenTK;

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
        public static int Tapas { get; set; }

        Entry[] numerosTelefonosJugadores = new Entry[Jugadores];
        int[] numerosTapasJugador = new int[Jugadores * Tapas];
        List<long> numerosJugadores = new List<long>();
        bool estaCorrecto;

        public void generaCampos()
        {
            numerosTelefonosJugadores = new Entry[Jugadores];
            for (var i = 0; i < Jugadores; i++)
            {
                numerosTelefonosJugadores[i] = new Entry();
                numerosTelefonosJugadores[i].Placeholder = $"Jugador {i}";
                numerosTelefonosJugadores[i].MaxLength = 10;
                numerosTelefonosJugadores[i].Keyboard = Keyboard.Numeric;

                Label label = new Label();
                label.Text = "Jugador " + (i+1);
                label.TextColor = Color.Black;

                panelTelJugadores.Children.Add(label);
                panelTelJugadores.Children.Add(numerosTelefonosJugadores[i]);
            }
        }

        public bool sonCorrectos(Entry[] camposTelJuegadores)
        {
            bool existe = false;

            for(var i = 0; i < camposTelJuegadores.Length; i++) 
            {
                if(string.IsNullOrEmpty(camposTelJuegadores[i].Text))
                {
                    DisplayAlert("Alerta!", "Los campos no pueden ir vacios", "Continuar");
                    return estaCorrecto = false;
                }

                if (camposTelJuegadores[i].Text.Contains(".") || camposTelJuegadores[i].Text.Contains("-"))
                {
                    DisplayAlert("Alerta!", "Formato telefonico no valido", "Continuar");
                    return estaCorrecto = false;
                }

                if(camposTelJuegadores[i].Text.Length < 10)
                {
                    DisplayAlert("Alerta!", "Numeros telefonicos no validos", "Continuar");
                    return estaCorrecto = false;
                }

                if (numerosJugadores.Contains(Convert.ToInt64(camposTelJuegadores[i].Text)))
                {
                    existe = true;
                }

                if (existe)
                {
                    DisplayAlert("Alerta!", "Los numeros telefonicos no se pueden repetir", "Continuar");
                    numerosJugadores.Clear();
                    return estaCorrecto = false;
                }

                numerosJugadores.Add(Convert.ToInt64(camposTelJuegadores[i].Text));
            }

            estaCorrecto = true;

            return estaCorrecto;
        }

        public int[] generarNumeros()
        {
            Random rdn = new Random();
            numerosTapasJugador = new int[Jugadores * Tapas];
            HashSet<int> set = new HashSet<int>();

            for (int i = 0; i < numerosTapasJugador.Length; i++)
            {
                int num;
                do
                {
                    // Generar un número aleatorio
                    num = rdn.Next(1, 16);
                }
                // Verificar si el número ya está en el conjunto
                while (set.Contains(num));

                // Agregar el número al arreglo y al conjunto
                numerosTapasJugador[i] = num;
                set.Add(num);
            }

            return numerosTapasJugador;
        }

        public void enviarMensaje(int[] numerosTapas)
        {
            try 
            {
                var accountSid = "AC365fe6358f7166e9f528408d6bc3f702";
                var authToken = "ac8cef3cd17fc48cdf25a114d689fab2";
                TwilioClient.Init(accountSid, authToken);

              

                var messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:+573213070026"));
                messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
                string[] mensaje = new string[numerosTapas.Length];

                for (var i = 0; i < numerosTapas.Length; i++)
                {
                    mensaje[i] = numerosTapas[i].ToString();
                    int posCero = (mensaje.Length - 1) - (mensaje.Length - 1);
                    if (Tapas == 1)
                    {
                        messageOptions.Body = $"Los numeros con los que jugaras son: \n*{mensaje[posCero]}*";
                    }
                    else if (Tapas == 2)
                    {
                        messageOptions.Body = $"Los numeros con los que jugaras son: \n*{mensaje[posCero]} - {mensaje[posCero + 1]}*";
                    }
                    else if (Tapas == 3)
                    {
                        messageOptions.Body = $"Los numeros con los que jugaras son: \n*{mensaje[posCero]} - {mensaje[posCero + 1]} - {mensaje[posCero + 2]}*";
                    }
                    else if (Tapas == 4)
                    {
                        messageOptions.Body = $"Los numeros con los que jugaras son: \n*{mensaje[posCero]} - {mensaje[posCero + 1]} - {mensaje[posCero + 2]} - {mensaje[posCero + 3]}*";
                    }
                }

                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);

                DisplayAlert("Exito", "Los numeros fueron enviados correctamente", "Continuar");

                return;
            }
            catch (Exception e)
            {
                DisplayAlert("Error","No se pudo completar la accion","Volver a intentar" + e);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            sonCorrectos(numerosTelefonosJugadores);

            if (!estaCorrecto)
            {
                return;
            }
            generarNumeros();
            enviarMensaje(numerosTapasJugador);
        }
    }
}