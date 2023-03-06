using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colletta
{
    public class Persona
    {
        private string Id { get; set; }
        private string Nome { get; set; }

        public Persona(string nome)
        {
            Nome = nome;
            Id = $"{nome}-id";
        }

        public override string ToString()
        {
            return $"ID: {Id} - NOME: {Nome}";
        }

        public bool Equals(Persona p)
        {
            if (p == null) return false;

            if (this == p) return true;

            return (this.Id == p.Id);
        }

        public override int GetHashCode()
        {
            return (Id, Nome).GetHashCode();
        }
    }
}
