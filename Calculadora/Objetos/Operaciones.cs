

namespace Calculadora.Objetos
{
    public class Operaciones
    {
       public int pack { get; set; } // Tipo pack caja
       public int formato { get; set; } // Tipo de formato
       public int litros { get; set; } // Litros demandados por la produccion
       public int cantidadTotalBot { get; set; } 
       public int cantidadNewBot { get; set; }
       public int cantidadSilo { get; set; }
       public int cantidadPalet { get; set; }
       public int cantidadCarton { get; set; }
       public double totalPaletTerminado { get; set; }
       public int cajas { set; get; } //Cajas demandadas por la produccion

        //Devuelve en botellas el pack en que está hecha la caja.
        private int getPack()
        {
            switch (pack)
            {
                case 1:
                    return 1;
                    break;
                case 2:
                    return 2;
                    break;
                case 3:
                    return 4;// Es la cantidad de carton pack
                    break;
                case 4:
                    return 6;// Es la cantidad de carton pack
                    break;
                default:
                    return 0; //Escribir más adelante mensage de error.

                    
            }
        }

        //Devuelve el formato en litros.
        private double getFormat()
        {
            if(formato == 1)
            {
                return 0.33;
            }
            else
            {
                return 0.25;
            }
        }
        // Devuleve si la caja es de 24 botellas o de 12 botellas.
        private int getBotellasCaja()
        {
            int packing;
            if (pack != 2)
            {
                return packing = 24;
            }
            else
            {
                return packing = 12;
            }
        }



        public int getNewPelletBot()   //Cálculo de botella nueva requerida.
        {
            double totalLitro = cantidadNewBot * getFormat(); // Cantidad de litros que tiene un palets de botella nueva.

           return (int)(litros / totalLitro);


        }

        public double getSilostainer() // Cálculo de tapones requeridos.
        {
            return (cantidadNewBot * getNewPelletBot()) / (double)cantidadSilo;
        }

        public double getEndPellet()
        {
            

           double cantidadBotellasPalet =  cantidadPalet * getBotellasCaja();

            return litros/(cantidadBotellasPalet * getFormat());


        }

        public double getCarton()
        {
            int totalPackPalet = (cantidadPalet * getPack())* (int)getEndPellet();
            return   totalPackPalet / cantidadCarton;
        }

        //Devuelve el numero de palets de Carton requerido por la vía de cajas
        public double getPackaging()
        {
            var aux = getPack();
            return (getPack() * cajas) / cantidadCarton;
        }
        // Devuelve el número de palets de botella nueva requerida por la vía de cajas.
        public double getPaletTerminado()
        {
            return cajas / cantidadPalet;
        }

        // Devuelve el número de silostainer requerido por la via de cajas.
        public double getSilostainerCajas()
        {
            return cantidadSilo / (cajas * getBotellasCaja());
        }

        // Devuelve el número de palets de botella nueva por la via de cajas.
        public double getBotellaNueva()
        {
            return (cajas * getBotellasCaja()) / cantidadNewBot;
        }

        // Metodo que devuelve el tipo de pack que tiene palet
        private int getPack_()
        {
            switch (pack)
            {
                case 1:
                    return 24;
                    break;
                case 2:
                    return 12;
                    break;
                case 3:
                    return 4;// Es la cantidad de carton pack
                    break;
                case 4:
                    return 6;// Es la cantidad de carton pack
                    break;
                default:
                    return 0; //Escribir más adelante mensage de error.


            }

        }

                
    }
}
