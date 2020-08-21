using System;
using System.Collections.Generic;

namespace BeBlueSpotify.Model
{
    public partial class CashSemanal
    {
        public int IdcashSemanal { get; set; }
        public int Idgenero { get; set; }
        public int DiaSemana { get; set; }
        public decimal Cash { get; set; }

        public virtual Genero IdgeneroNavigation { get; set; }
    }
}
