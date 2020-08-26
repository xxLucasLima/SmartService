export default {
    criaEmpresa: { method: 'post', url: 'http://localhost:8000/api/empresa' },
    getEmpresas: { method: 'get', url: 'http://localhost:8000/api/empresa' },
    getEmpresaById: { method: 'get', url: 'http://localhost:8000/api/empresa{/idEmpresa}' },
    deleteEmpresaById: { method: 'delete', url: 'http://localhost:8000/api/empresa{/idEmpresa}' },
    editEmpresa: { method: 'put', url: 'http://localhost:8000/api/empresa{/idEmpresa}' },
}