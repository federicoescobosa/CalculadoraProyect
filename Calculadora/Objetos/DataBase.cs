
namespace Calculadora.Objetos
{

    //Objeto que recoge y pasa parámetros  a firebase.
    public class DataBase 
    {
        public int cod_producto { get; set; } 
        public int cantidad { get; set; }
        public int tipo { get; set; }
        public string descripcion { get; set; }
        public int formato { get; set; }
        public int pack { get; set; }
        public string concat { get { return "(" + cod_producto + ")"+"    " + descripcion; } }      

    }
}
