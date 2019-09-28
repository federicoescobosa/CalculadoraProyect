using Calculadora.Objetos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Calculadora.Firebase;
using System.Collections;

namespace Calculadora
{
    public partial class UIDataBase : ContentPage
    {

        DataBase dataBase = new DataBase();
        FirebaseCrud firebaseCrud = new FirebaseCrud();

        public UIDataBase()
        {
            InitializeComponent();
            lista();
            listaFormato();
            packlist();
        }

        protected async void OnAppearing()
        {
            base.OnAppearing();
            var getProduct = await firebaseCrud.getProduct((prod.SelectedIndex), Convert.ToInt32(cod.Text));
            listado.ItemsSource = getProduct.ToString();
        }

       private  void lista(){

            prod.Items.Add("");
            prod.Items.Add("Botella Nueva");
            prod.Items.Add("Cartón");
            prod.Items.Add("Terminado");
            prod.Items.Add("Tapón");
             
       }

        private void listaFormato()
        {
            cap.Items.Add("");
            cap.Items.Add("1/3");
            cap.Items.Add("1/4");
        }

        private void packlist()
        {
            pack.Items.Add("");
            pack.Items.Add("24 Botellas");
            pack.Items.Add("12 Botellas");
            pack.Items.Add("6 Botellas");
            pack.Items.Add("4 Botellas");

        }



        private async void Add_Clicked(object sender, EventArgs e) //Inserta todo en el objeto DataBase el nuevo producto.
        {
           
                int product = prod.SelectedIndex;
                int formato = cap.SelectedIndex;
                int packing = pack.SelectedIndex;
                await firebaseCrud.AddProduct(Convert.ToInt32(cod.Text), Convert.ToInt32(cant.Text), product, des.Text,formato,packing);

                await DisplayAlert("Entrada de Datos", "Los datos se han introducido correctamente", "OK");

                cod.Text = string.Empty;
                cant.Text = string.Empty;
                prod.SelectedIndex = 0;
                des.Text = string.Empty;
                cap.SelectedIndex = 0;
                pack.SelectedIndex = 0;
        }
        //Borra nodo en firebase
        private async void Del_Clicked(object sender, EventArgs e)
        {
            bool resp = await DisplayAlert("Borrar Datos.", "¿Estás seguro que quieres eliminar este registro?", "Si", "No");
            if (resp == true) {
                try {
                    await firebaseCrud.DeleteProduct(Convert.ToInt32(cod.Text),Convert.ToString(prod.SelectedIndex));
                    await DisplayAlert("Producto Eliminado", "Este producto se ha eliminado correctamente.", "OK");
                } catch(Exception ex)
                {
                    DisplayAlert("Error", "Este producto no se ha eliminado. "+ ex.Message, "OK");
                }
                
            }
        }

        // Oculta o aparece el picker prod segun su seleccion
        private void Prod_Unfocused(object sender, FocusEventArgs e)
        {

            if(prod.SelectedIndex==1 || prod.SelectedIndex==2 || prod.SelectedIndex == 3)
            {
                cap.IsVisible = true;

                if (prod.SelectedIndex == 2 || prod.SelectedIndex == 3)
                {
                    pack.IsVisible = true;
                }
                else
                {
                    pack.IsVisible = false;
                }
            }
            else
            {
                cap.IsVisible = false;
            }

        }

        private async void Search_Clicked(object sender, EventArgs e)
        {

            var producto = await firebaseCrud.getProduct(Convert.ToInt32(prod.SelectedIndex), Convert.ToInt32(cod.Text));
               
            if(producto != null)
            {
                //hacer codigo

            }
            else
            {
                await DisplayAlert("Error", "La búsqueda no ha producido ningún resultado", "OK");
            }
            
           
        }
    }
}
