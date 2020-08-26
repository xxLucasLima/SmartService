using System;
using System.Collections.Generic;

namespace back_end.Models
{
    public class Lote
    {
        public long Id_Lote { get; set; }
        public string CodEntrada { get; set; }
        public string Descricao { get; set; }

        public string CodSaida { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<Produto> Produtos { get; set; }

    }

    public class LoteDDL
    {
        public long Value { get; set; }
        public string Text { get; set; }
    }
}