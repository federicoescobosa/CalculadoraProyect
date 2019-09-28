using Calculadora.Firebase;
using Calculadora.Objetos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculadora
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        FirebaseCrud firebaseCrud = new FirebaseCrud();
       
        Operaciones operaciones = new Operaciones();
       
        public MainPage()
        {
            InitializeComponent();
            OnAppearing();
            
        }

        


        private async void goDataBase(object sender, EventArgs args)
        {

            await Navigation.PushAsync(new UIDataBase());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try {
                var botella = await firebaseCrud.getAllProducts(1);
                pickerBotella.ItemsSource = botella;
                var carton = await firebaseCrud.getAllProducts(2);
                pickerCarton.ItemsSource = carton;
                var terminado = await firebaseCrud.getAllProducts(3);
                pickerArticulo.ItemsSource = terminado;
                var silotainer = await firebaseCrud.getAllProducts(4);
                pickerTapon.ItemsSource = silotainer;
            }
            catch(Exception e)
            {
                DisplayAlert("Error", "" + e.Message, "OK");
            }

        }

        private async void Calculo_Clicked(object sender, EventArgs e)
        {
            

            if(litros.Text!=null && cajas.Text == null)  // Elige la rama por litros propuestos
            {
                try
                {
                    operacionesBotellas();

                }
                catch (NullReferenceException ex)
                {
                    mensageError();
                }



            }
            else if(litros.Text==null && cajas.Text!=null) // Elige la rama por cajas propuestas.
            {
                try
                {
                    operacionesCajas();

                }
                catch (NullReferenceException ex)
                {
                    mensageError();
                }

            }
            else // Si no se rellena ni los litros ni las cajas.
            {
                mensageError();
            }
            
            // Método que realiza todos cálculos.
             void operacionesBotellas()
            {

                DataBase articulo = pickerArticulo.SelectedItem as DataBase;
                DataBase botellanueva = pickerBotella.SelectedItem as DataBase;
                DataBase carton = pickerCarton.SelectedItem as DataBase;
                DataBase tapon = pickerTapon.SelectedItem as DataBase;

                // Total palet de botella nueva requerida.
                operaciones.litros = Convert.ToInt32(litros.Text);
                operaciones.cantidadNewBot = botellanueva.cantidad;
                operaciones.formato = botellanueva.formato;
                titulo.IsVisible = true;
                needbot.Text = operaciones.getNewPelletBot().ToString();

                // Total silostainer requeridos.
                operaciones.cantidadSilo = tapon.cantidad;
                operaciones.cantidadTotalBot = botellanueva.cantidad;
                titulo2.IsVisible = true;
                needtap.Text = operaciones.getSilostainer().ToString("N2");


                // Total palet terminado producidos.
                operaciones.pack = articulo.pack;
                operaciones.cantidadPalet = articulo.cantidad;
                titulo3.IsVisible = true;
                var paletTerminado = operaciones.getEndPellet();
                resutlterm.Text = paletTerminado.ToString("N2");

                // Total cartones requeridos.
                operaciones.cantidadCarton = carton.cantidad;  //Cantidad de cartones que tiene un palet.
                operaciones.cantidadPalet = articulo.cantidad; //Cantidad de cajas que tiene un palet
                operaciones.pack = articulo.pack;               //Pack que tiene una caja.
                operaciones.totalPaletTerminado = paletTerminado;
                titulo1.IsVisible = true;
                needcar.Text = operaciones.getCarton().ToString("N2");
            }

            void operacionesCajas()
            {
                DataBase articulo = pickerArticulo.SelectedItem as DataBase;
                DataBase botellanueva = pickerBotella.SelectedItem as DataBase;
                DataBase carton = pickerCarton.SelectedItem as DataBase;
                DataBase tapon = pickerTapon.SelectedItem as DataBase;

                //Total palet de carton requerido.
                operaciones.cajas = Convert.ToInt32(cajas.Text);
                operaciones.cantidadCarton = carton.cantidad;
                operaciones.pack = articulo.pack;
                titulo1.IsVisible = true;
                needcar.Text = operaciones.getPackaging().ToString("N2");

                // Palet terminado resultante de la producción.
                operaciones.cantidadPalet = articulo.cantidad;
                titulo3.IsVisible = true;
                resutlterm.Text = operaciones.getPaletTerminado().ToString("N2");

                //Total silostainer requeridos
                operaciones.cantidadSilo = tapon.cantidad;
                titulo2.IsVisible = true;
                needtap.Text = operaciones.getSilostainerCajas().ToString("N2");
                //Total palets de botella nueva requeridos.
                operaciones.cantidadNewBot = botellanueva.cantidad;
                titulo.IsVisible = true;
                needbot.Text = operaciones.getBotellaNueva().ToString("N2");


            }

            void mensageError()
            {
                DisplayAlert("Error", "No se pueden dejar elementos vacíos.", "Ok");

            }
           
                              
        }
    }

   
    

}
