using System;
using System.Collections.Generic;

namespace BeBlueSpotify.Model
{
    public partial class Venda
    {
        public Venda()
        {
            VendaItem = new HashSet<VendaItem>();
        }

        public int Idvenda { get; set; }
        public int Idusuario { get; set; }
        public DateTime Data { get; set; }

        public virtual Usuario IdusuarioNavigation { get; set; }
        public virtual ICollection<VendaItem> VendaItem { get; set; }
    }
}
