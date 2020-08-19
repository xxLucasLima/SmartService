using System.Collections.Generic;

namespace back_end.Models
{
    public class DefeitoProduto
    {
        public long Id_DefeitoProduto { get; set; }
        public string Descricao { get; set; }
        public ICollection<Produto> Produtos { get; set; }

    }

    public class DefeitoProdutoDDL
    {
        public long Value { get; set; }
        public string Text { get; set; }
    }
}