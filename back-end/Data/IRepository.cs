using System.Collections.Generic;
using System.Threading.Tasks;
using back_end.Models;

namespace back_end.Data
{
    public interface IRepository
    {
        #region Base
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        #endregion

        #region Usuário
        Task<Usuario[]> GetAllUsuariosAsync();
        Task<Empresa[]> GetAllEmpresasDDL();
        Task<PerfilUsuario[]> GetAllPerfisUsuarioDDL();
        Task<Usuario> GetUsuarioAsyncById(int usuarioId);
        Task<Usuario> GetUsuarioAsyncByEmailAndPassword(string emailUsuario, string senhaUsuario);
        #endregion

        #region Produto
        Task<Produto[]> GetAllProdutosAsync();
        Task<Produto> GetProdutoAsyncById(int usuarioId);
        Task<DefeitoProduto[]> GetAllDefeitosProdutoDDL();
        Task<Cliente[]> GetAllClientesDDL();
        Task<Lote[]> GetAllLotesDDL();

        #endregion

        #region Lote

        Task<Lote[]> GetAllLotesAsync();
        Task<Lote> GetLoteAsyncById(int loteId);
        #endregion

        #region Cliente

        Task<Cliente[]> GetAllClientesAsync();
        Task<Cliente> GetClienteAsyncById(int loteId);
        #endregion
        
        #region Empresa

        Task<Empresa[]> GetAllEmpresasAsync();
        Task<Empresa> GetEmpresaAsyncById(int empresaId);
        #endregion

    }
}