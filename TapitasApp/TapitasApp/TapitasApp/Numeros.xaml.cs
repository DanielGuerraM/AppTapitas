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
            }
            estaVacio(campo, telefonosJugadores);
        }

        public void estaVacio(Entry[] camposTelefonos, long[] telefonosJugadores)
        {
            for(var i = 0; i < camposTelefonos.Length; i++)
            {
                if (Convert.ToInt64(camposTelefonos[i].Text) == 0)
                {
                    DisplayAlert("Alerta","Los campos no deben ir vacios","Ok");
                    return;
                }
            }
            sonRepetidos(telefonosJugadores);
        }

        public void sonRepetidos(long[] telefonos)
        {
            for (var i = 0; i < telefonos.Length; i++)
            {
                for (var j = 0; j < telefonos.Length; j++)
                {
                    if (telefonos[i] == telefonos[j] && i != j)
                    {
                        DisplayAlert("Alerta", "Los numeros no se deben de repetir", "Ok");
                        return;
                    }
                }                 
            }
            GenerarNumeros();
        }

        public void GenerarNumeros()
        {
            int[] numerosTapas = new int[(Tapas * Jugadores)];

            Random rdn = new Random();

            for(var i = 0; i < numerosTapas.Length; i++)
            {
                numerosTapas[i] = rdn.Next(1, 16);
            }
            enviarMensaje(numerosTapas);
        }

        public void enviarMensaje(int[] numerosTapas)
        {
            try 
            {
                var accountSid = "AC365fe6358f7166e9f528408d6bc3f702";
                var authToken = "14a11f663f5c83eac714e47be6754b01";
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                    new PhoneNumber("whatsapp:+573213070026"));
                messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
                string[] mensaje = new string[numerosTapas.Length];

                for (var i = 0; i < numerosTapas.Length; i++)
                {
                    mensaje[i] = numerosTapas[i].ToString();
                    int posCero =  (mensaje.Length - 1) - (mensaje.Length - 1);
                    messageOptions.Body = $"Los numeros con los que jugaras son: *{mensaje[posCero]} {mensaje[posCero + 1]}*";
                }

                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);

                DisplayAlert("Exito", "Los numeros fueron enviados correctamente", "Continuar");
                return;
            }
            catch
            {
                DisplayAlert("Error","No se pudo completar la accion","Volver a intentar");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            LeerNumeros(campoTexto);     
        }
    }
}