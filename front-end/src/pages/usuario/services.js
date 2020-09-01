export default {
    criaUsuario: { method: 'post', url: 'http://localhost:8000/api/usuario' },
    getUsuarios: { method: 'get', url: 'http://localhost:8000/api/usuario' },
    getUsuarioById: { method: 'get', url: 'http://localhost:8000/api/usuario{/idUsuario}' },
    deleteUsuarioById: { method: 'delete', url: 'http://localhost:8000/api/usuario{/idUsuario}' },
    getAllEmpresasDDL: { method: 'get', url: 'http://localhost:8000/api/usuario/getAllEmpresasDDL' },
    getAllPerfisUsuarioDDL: { method: 'get', url: 'http://localhost:8000/api/usuario/getAllPerfisUsuarioDDL' },
    editUsuario: { method: 'put', url: 'http://localhost:8000/api/usuario{/idUsuario}' },
    resetPassUsuario: { method: 'get', url: 'http://localhost:8000/api/usuario/resetPassUsuario{/idUsuario}' }

}