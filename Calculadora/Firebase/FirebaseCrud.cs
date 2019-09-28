using Android.OS;
using Calculadora.Objetos;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calculadora.Firebase
{
    public class FirebaseCrud
    {

        FirebaseClient firebase;
        DataBase data;
        int codigo;
        
        public FirebaseCrud()
        {
            firebase = new FirebaseClient("https://calculadora-aac95.firebaseio.com/"); //Url del proyecto de firebase.
            data = new DataBase();
            
        }
        
        
       

        //Añade productos a firebase
        public async Task AddProduct(int producto, int cant, int type, string desc,int cap,int packing)
        {

            await firebase.Child(Convert.ToString(type)).PostAsync(new DataBase()
                {

                    cod_producto = producto,
                    cantidad = cant,
                    tipo = type,
                    descripcion = desc,
                    formato = cap,
                    pack = packing
            });

         }


        //Hace una búsqueda por tipo de producto.
        public async Task<DataBase> getProduct(int type, int code)
        {
            var allProducts = await getAllProducts(type);
            await firebase.Child(Convert.ToString(type)).OnceAsync<DataBase>();
            return allProducts.Where(a => a.cod_producto == code).FirstOrDefault();
        }

       
        



        public async Task<List<DataBase>> getAllProducts(int type)
        {
            return (await firebase.Child(Convert.ToString(type)).OnceAsync<DataBase>()).Select(item => new DataBase
            {

                descripcion = item.Object.descripcion,
                tipo = item.Object.tipo,
                cantidad = item.Object.cantidad,
                pack = item.Object.pack,
                formato = item.Object.formato,
                cod_producto = item.Object.cod_producto

            }).ToList();
        }


        //Borrar
        public async Task DeleteProduct(int cod,string tipo)
        {
            var toDeleteProduct = (await firebase
              .Child(tipo)
              .OnceAsync<DataBase>()).Where(a => a.Object.cod_producto == cod).FirstOrDefault();
            await firebase.Child(tipo).Child(toDeleteProduct.Key).DeleteAsync();
        }
    }
}
