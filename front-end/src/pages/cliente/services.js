export default {
    criaCliente: { method: 'post', url: 'http://localhost:8000/api/cliente' },
    getClientes: { method: 'get', url: 'http://localhost:8000/api/cliente' },
    getClienteById: { method: 'get', url: 'http://localhost:8000/api/cliente{/idCliente}' },
    deleteClienteById: { method: 'delete', url: 'http://localhost:8000/api/cliente{/idCliente}' },
    editCliente: { method: 'put', url: 'http://localhost:8000/api/cliente{/idCliente}' }
}