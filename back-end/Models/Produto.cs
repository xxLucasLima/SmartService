using System;

namespace back_end.Models
{
    public class Produto
    {
        public long Id_Produto { get; set; }
        public string Tipo { get; set; }
        public string Observacao { get; set; }
        public string Descricao { get; set; }
        public string Ean13 { get; set; }
        public string Imei1 { get; set; }
        public string Imei2 { get; set; }
        public string Modelo { get; set; }
        public DateTime DataCriacao { get; set; }
        public long Id_Lote { get; set; }
        public long Id_Cliente { get; set; }
        public long Id_DefeitoProduto { get; set; }
        public Lote Lote { get; set; }
        public Cliente Cliente { get; set; }
        public DefeitoProduto DefeitoProduto { get; set; }
    }
    
}