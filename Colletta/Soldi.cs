﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colletta
{
    public class Soldi
    {
        private string Id { get; set; }
        private double Importo { get; set; }
        private string Valuta { get; set; }

        public Soldi(double importo, string valuta)
        {
            Importo = importo;
            Id = $"{Importo}-id";
            Valuta = valuta;
        }

        public override string ToString()
        {
            return $"ID: {Id} - IMPORTO: {Importo}{Valuta}";
        }

        public bool Equals(Soldi p)
        {
            if (p == null) return false;

            if (this == p) return true;

            return (this.Id == p.Id);
        }

        public override int GetHashCode()
        {
            return (Id, Importo, Valuta).GetHashCode();
        }
    }
}