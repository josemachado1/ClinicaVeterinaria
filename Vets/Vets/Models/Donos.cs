using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vets.Models
{
    public class Donos
    {
        public Donos()
        {
            //construtor da Classe, que vai ser utilizado
            //para inicializar o atributo 'ListaDeAnimais'
            ListaDeAnimais = new HashSet<Animais>();
        }

        public int DonosID { get; set; }

        public string Nome { get; set; }


        public string NIF { get; set; }

        //relacionar os 'Donos' com os 'Animais'
        //1 Dono tem muitos Animais
        public virtual ICollection<Animais> ListaDeAnimais { get; set; }

    }
}