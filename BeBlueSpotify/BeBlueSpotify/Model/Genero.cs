using System;
using System.Collections.Generic;

namespace BeBlueSpotify.Model
{
    public partial class Genero
    {
        public Genero()
        {
            Album = new HashSet<Album>();
            CashSemanal = new HashSet<CashSemanal>();
        }

        public int Idgenero { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Album> Album { get; set; }
        public virtual ICollection<CashSemanal> CashSemanal { get; set; }
    }
}
