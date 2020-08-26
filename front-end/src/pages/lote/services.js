export default {
    criaLote: { method: 'post', url: 'http://localhost:8000/api/lote' },
    getLotes: { method: 'get', url: 'http://localhost:8000/api/lote' },
    getLoteById: { method: 'get', url: 'http://localhost:8000/api/lote{/idLote}' },
    deleteLoteById: { method: 'delete', url: 'http://localhost:8000/api/lote{/idLote}' },
    editLote: { method: 'put', url: 'http://localhost:8000/api/lote{/idLote}' },
    getAllClientesDDL: { method: 'get', url: 'http://localhost:8000/api/lote/getAllClientesDDL' },
    getAllDefeitosLoteDDL: { method: 'get', url: 'http://localhost:8000/api/lote/getAllDefeitosLoteDDL' },
    getAllLotesDDL: { method: 'get', url: 'http://localhost:8000/api/lote/getAllLotesDDL' }
}