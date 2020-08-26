using System;
using System.Collections.Generic;

namespace back_end.Models
{
    public class Cliente
    {
        public long Id_Cliente { get; set; }
        public string CnpjCpf { get; set; }
        public string InscEstadual { get; set; }
        public string InscMunicipal { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string TipoPessoa { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
    
    }

    
    public class ClienteDDL
    {
        public long Value { get; set; }
        public string Text { get; set; }
    }
}