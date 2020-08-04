using System;

namespace back_end.Models
{
    public class Usuario
    {
        
        public long Id_Usuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public long Id_Empresa { get; set; }
        public long Id_PerfilUsuario { get; set; }
        public PerfilUsuario PerfilUsuario { get; set; }
        public Empresa Empresa { get; set; }
    }
}