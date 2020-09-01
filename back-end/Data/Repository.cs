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

        #region Base
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

        #endregion

        #region Usuário
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

            query = query.AsNoTracking().OrderBy(a => a.Id_Usuario).Where(usuario => usuario.Email == emailUsuario && usuario.Senha == senhaUsuario);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Usuario> GetUsuarioAsyncByEmail(string emailUsuario)
        {
            IQueryable<Usuario> query = _context.Usuario;

            query = query.Include(p => p.Empresa);
            query = query.Include(p => p.PerfilUsuario);

            query = query.AsNoTracking().OrderBy(a => a.Id_Usuario).Where(usuario => usuario.Email == emailUsuario);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Empresa[]> GetAllEmpresasDDL()
        {
            IQueryable<Empresa> query = _context.Empresa;

            query = query.AsNoTracking().OrderBy(a => a.RazaoSocial);

            return await query.ToArrayAsync();
        }

        public async Task<PerfilUsuario[]> GetAllPerfisUsuarioDDL()
        {
            IQueryable<PerfilUsuario> query = _context.PerfilUsuario;

            query = query.AsNoTracking().OrderBy(a => a.Nome);

            return await query.ToArrayAsync();
        }

        #endregion

        #region Produto
        public async Task<Produto[]> GetAllProdutosAsync()
        {
            IQueryable<Produto> query = _context.Produto;

            query = query.Include(p => p.Lote);
            query = query.Include(p => p.Cliente);
            query = query.Include(p => p.DefeitoProduto);

            query = query.AsNoTracking().OrderBy(a => a.Id_Produto);

            return await query.ToArrayAsync();
        }

        public async Task<Produto> GetProdutoAsyncById(int produtoId)
        {
            IQueryable<Produto> query = _context.Produto;

            query = query.Include(p => p.Lote);
            query = query.Include(p => p.Cliente);
            query = query.Include(p => p.DefeitoProduto);

            query = query.AsNoTracking().OrderBy(a => a.Id_Produto).Where(p => p.Id_Produto == produtoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<DefeitoProduto[]> GetAllDefeitosProdutoDDL()
        {
            IQueryable<DefeitoProduto> query = _context.DefeitoProduto;

            query = query.AsNoTracking().OrderBy(a => a.Descricao);

            return await query.ToArrayAsync();
        }

        public async Task<Cliente[]> GetAllClientesDDL()
        {
            IQueryable<Cliente> query = _context.Cliente;

            query = query.AsNoTracking().OrderBy(a => a.NomeFantasia);

            return await query.ToArrayAsync();
        }

        public async Task<Lote[]> GetAllLotesDDL()
        {
            IQueryable<Lote> query = _context.Lote;

            query = query.AsNoTracking().OrderBy(a => a.CodEntrada);

            return await query.ToArrayAsync();
        }
        #endregion

        #region Lote
        public async Task<Lote[]> GetAllLotesAsync()
        {
            IQueryable<Lote> query = _context.Lote;

            query = query.AsNoTracking().OrderBy(a => a.Id_Lote);

            return await query.ToArrayAsync();
        }

        public async Task<Lote> GetLoteAsyncById(int loteId)
        {
            IQueryable<Lote> query = _context.Lote;

            query = query.AsNoTracking().OrderBy(a => a.Id_Lote).Where(aluno => aluno.Id_Lote == loteId);

            return await query.FirstOrDefaultAsync();
        }

        #endregion

        #region Cliente
        public async Task<Cliente[]> GetAllClientesAsync()
        {
            IQueryable<Cliente> query = _context.Cliente;

            query = query.AsNoTracking().OrderBy(a => a.Id_Cliente);

            return await query.ToArrayAsync();
        }

        public async Task<Cliente> GetClienteAsyncById(int clienteId)
        {
            IQueryable<Cliente> query = _context.Cliente;

            query = query.AsNoTracking().OrderBy(a => a.Id_Cliente).Where(aluno => aluno.Id_Cliente == clienteId);

            return await query.FirstOrDefaultAsync();
        }

        #endregion

        #region Empresa
        public async Task<Empresa[]> GetAllEmpresasAsync()
        {
            IQueryable<Empresa> query = _context.Empresa;

            query = query.AsNoTracking().OrderBy(a => a.Id_Empresa);
            query = query.Include(p => p.Usuarios).ThenInclude(p => p.PerfilUsuario);

            return await query.ToArrayAsync();
        }
        public async Task<Empresa> GetEmpresaAsyncById(int empresaId)
        {
            IQueryable<Empresa> query = _context.Empresa;

            query = query.AsNoTracking().OrderBy(a => a.Id_Empresa).Where(empresa => empresa.Id_Empresa == empresaId);

            return await query.FirstOrDefaultAsync();
        }

        #endregion
    }
}