using System;
using System.Collections.Generic;

namespace back_end.Models
{
    public class Empresa
    {
        public long Id_Empresa { get; set; }
        public bool Ativo { get; set; }
        public string Cnpj { get; set; }
        public DateTime DataCriacao { get; set; }
        public string NomeFantasia { get; set; }
        public string razaoSocial { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }

    public class EmpresaDDL
    {
        public long Value { get; set; }
        public string Text { get; set; }
    }
}