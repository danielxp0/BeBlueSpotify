using System;
using System.Collections.Generic;

namespace BeBlueSpotify.Model
{
    public partial class Usuario
    {
        public Usuario()
        {
            Venda = new HashSet<Venda>();
        }

        public int Idusuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public decimal CashAcumulado { get; set; }

        public virtual ICollection<Venda> Venda { get; set; }
    }
}
