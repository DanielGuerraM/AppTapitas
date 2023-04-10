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
                campoTexto[i].Keyboard = Keyboard.Numeric;

                Label label = new Label();
                label.Text = "Jugador " + (i+1);
                label.TextColor = Color.Black;

                panelTelJugadores.Children.Add(label);
                panelTelJugadores.Children.Add(campoTexto[i]);

            }
        }

        public void LeerNumeros(Entry[] camposTelefonos)
        {
            long[] telefonosJugadores = new long[Jugadores];

            /*for(var i = 0; i < camposTelefonos.Length; i++)
            {
                telefonosJugadores[i] = Convert.ToInt64(campoTexto[i].Text);
            }*/

            for (var i = 0; i < camposTelefonos.Length; i++)
            {
                string telefonoSinPunto = camposTelefonos[i].Text.Replace(".", "");

                if (Convert.ToInt64(telefonoSinPunto.Length) == 0)
                {
                    DisplayAlert("Alerta", "Los campos no deben ir vacios", "Ok");
                    return;
                }
                if (Convert.ToInt64(telefonoSinPunto.Length) < 10)
                {
                    DisplayAlert("Alerta", "Los telefonos no pueden ser menores a 10 dígitos ni tener puntos", "Ok");
                    return;
                }
            }

            for (var i = 0; i < telefonosJugadores.Length; i++)
            {
                for (var j = 0; j < telefonosJugadores.Length; j++)
                {
                    if (telefonosJugadores[i] == telefonosJugadores[j] && i != j)
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
            
            Random rdn = new Random();
            int[] numerosTapas = new int[(Tapas * Jugadores)];
            HashSet<int> set = new HashSet<int>();

            for (int i = 0; i < numerosTapas.Length; i++)
            {
                int num;
                do
                {
                    // Generar un número aleatorio
                    num = rdn.Next(1, 16);
                } while (set.Contains(num)); // Verificar si el número ya está en el conjunto

                // Agregar el número al arreglo y al conjunto
                numerosTapas[i] = num;
                set.Add(num);
            }

            enviarMensaje(numerosTapas);

        }

        public void enviarMensaje(int[] numerosTapas)
        {
            try 
            {
                var accountSid = "AC365fe6358f7166e9f528408d6bc3f702";
                var authToken = "28f528e32a9a24f8ad0f887fa8e74a24";
                TwilioClient.Init(accountSid, authToken);

                //--------------------------------------------------

                for (var j = 0; j < Jugadores; j++) {

                    var messageOptions = new CreateMessageOptions(
                    new PhoneNumber($"whatsapp:+573213070026"));
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
                }

                
             //---------------------------------------------------------------------------------------------------------------------
                return;
            }
            catch (Exception e)
            {
                DisplayAlert("Error","No se pudo completar la accion","Volver a intentar" + e);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            LeerNumeros(campoTexto);     
        }
    }
}