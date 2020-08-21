using System;
using System.Collections.Generic;

namespace BeBlueSpotify.Model
{
    public partial class Album
    {
        public Album()
        {
            VendaItem = new HashSet<VendaItem>();
        }

        public int Idalbum { get; set; }
        public int Idgenero { get; set; }
        public string Nome { get; set; }
        public string Artista { get; set; }
        public decimal Valor { get; set; }

        public virtual Genero IdgeneroNavigation { get; set; }
        public virtual ICollection<VendaItem> VendaItem { get; set; }
    }
}
