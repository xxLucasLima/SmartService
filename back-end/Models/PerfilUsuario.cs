using System.Collections.Generic;

namespace back_end.Models
{
    public class PerfilUsuario
    {
        public long Id_PerfilUsuario { get; set; }
        public string Nome { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }

    public class PerfilUsuarioDDL
    {
        public long Value { get; set; }
        public string Text { get; set; }
    }
}