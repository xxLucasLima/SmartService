export default {
    criaProduto: { method: 'post', url: 'http://localhost:8000/api/produto' },
    getProdutos: { method: 'get', url: 'http://localhost:8000/api/produto' },
    getProdutoById: { method: 'get', url: 'http://localhost:8000/api/produto{/idProduto}' },
    deleteProdutoById: { method: 'delete', url: 'http://localhost:8000/api/produto{/idProduto}' },
    editProduto: { method: 'put', url: 'http://localhost:8000/api/produto{/idProduto}' },
    getAllClientesDDL: { method: 'get', url: 'http://localhost:8000/api/produto/getAllClientesDDL' },
    getAllDefeitosProdutoDDL: { method: 'get', url: 'http://localhost:8000/api/produto/getAllDefeitosProdutoDDL' },
    getAllLotesDDL: { method: 'get', url: 'http://localhost:8000/api/produto/getAllLotesDDL' }
}