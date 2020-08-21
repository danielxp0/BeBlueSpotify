using System;
using System.Collections.Generic;

namespace BeBlueSpotify.Model
{
    public partial class VendaItem
    {
        public int IdvendaItem { get; set; }
        public int Idvenda { get; set; }
        public int Idalbum { get; set; }
        public int Qtd { get; set; }
        public int ValorUnitario { get; set; }
        public decimal Cash { get; set; }

        public virtual Album IdalbumNavigation { get; set; }
        public virtual Venda IdvendaNavigation { get; set; }
    }
}
