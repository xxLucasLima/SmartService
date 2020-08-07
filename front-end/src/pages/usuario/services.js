export default {
    criaUsuario: { method: 'post', url: 'http://localhost:8000/api/usuario' },
    getUsuarios: { method: 'get', url: 'http://localhost:8000/api/usuario' },
    deleteUsuarioById: { method: 'delete', url: 'http://localhost:8000/api/usuario{/Id_Usuario}' },
    getAllEmpresasDDL: { method: 'get', url: 'http://localhost:8000/api/usuario/getAllEmpresasDDL' },
    getAllPerfisUsuarioDDL: { method: 'get', url: 'http://localhost:8000/api/usuario/getAllPerfisUsuarioDDL' }

}