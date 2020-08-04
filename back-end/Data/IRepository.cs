using System.Threading.Tasks;
using back_end.Models;

namespace back_end.Data
{
    public interface IRepository
    {
        //GERAL
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //USUÁRIO
        Task<Usuario[]> GetAllUsuariosAsync();
        Task<Usuario> GetUsuarioAsyncById(int usuarioId);
        Task<Usuario> GetUsuarioAsyncByEmailAndPassword(string emailUsuario, string senhaUsuario);
    }
}