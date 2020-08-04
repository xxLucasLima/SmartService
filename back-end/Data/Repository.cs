using System.Linq;
using System.Threading.Tasks;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data
{
    public class Repository : IRepository
    {
        public DataContext _context { get; }
        public Repository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        //ALUNOS
        public async Task<Usuario[]> GetAllUsuariosAsync()
        {
            IQueryable<Usuario> query = _context.Usuario;

            query = query.Include(p => p.Empresa);
            query = query.Include(p => p.PerfilUsuario);

            query = query.AsNoTracking().OrderBy(a => a.Id_Usuario);

            return await query.ToArrayAsync();
        }

        public async Task<Usuario> GetUsuarioAsyncById(int usuarioId)
        {
            IQueryable<Usuario> query = _context.Usuario;

            query = query.Include(p => p.Empresa);
            query = query.Include(p => p.PerfilUsuario);

            query = query.AsNoTracking().OrderBy(a => a.Id_Usuario).Where(aluno => aluno.Id_Usuario == usuarioId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUsuarioAsyncByEmailAndPassword(string emailUsuario, string senhaUsuario)
        {
            IQueryable<Usuario> query = _context.Usuario;

            query = query.Include(p => p.Empresa);
            query = query.Include(p => p.PerfilUsuario);

            query = query.AsNoTracking().OrderBy(a => a.Id_Usuario).Where(aluno => aluno.Email == emailUsuario && aluno.Senha == senhaUsuario);

            return await query.FirstOrDefaultAsync();
        }

    }
}