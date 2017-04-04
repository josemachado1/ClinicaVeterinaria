using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Vets.Models
{
    public class VetsDB : DbContext
    {
        //representar a Base de Dados
        //descrevendo as tabelas que la estao contidas

        //representar o 'construtor' desta classe
        //identificar onde se encontra a Base de Dados
        public VetsDB() : base("VetsDBConnection") { }

        //decrever as 'tabelas' que estao na BD
        public virtual DbSet<Donos > Donos { get; set; }
        public virtual DbSet<Animais> Animais { get; set; }
    }
}