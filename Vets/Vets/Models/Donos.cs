﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vets.Models
{
    public class Donos
    {
        // vai representar os dados da tabela dos DONOS

        // criar o construtor desta classe
        // e carregar a lista de Animais
        public Donos()
        {
            ListaDeAnimais = new HashSet<Animais>();
        }


        [Key] // indica que estamos perante uma PK
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // quando usado, inibe o atributo de ser Auto Number
        [Display(Name = "Identificador do Cliente")]
        public int DonoID { get; set; }

        [Required(ErrorMessage = "o {0} é de preenchimento obrgatório...")]
        [Display(Name = "Nome do cliente")]
        [RegularExpression("[A-Z][a-záéíóúàèìòù]+((( )|( de|da|dos )|( d'))?",
                ErrorMessage = "Deve escrever o {0} só com letras. Pode usar um espaço em branco entre palavras. Deve começar cada nome com uma maiúscula")]
        public string Nome { set; get; }

        [Required(ErrorMessage = "Não se esqueça de preencher o Nº de contribuinte")]
        [RegularExpression("[0-9]{9}",
                ErrorMessage ="O {0} só aceita 9 algarismos.")]
        public string NIF { get; set; }

        // especificar que um DONO tem muitos ANIMAIS
        public ICollection<Animais> ListaDeAnimais { get; set; }

    }
}