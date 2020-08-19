import ProdutoForm from './produto-form.vue';
import Produtos from './produtos.vue';

export default [

    {
        path: '/produtos',
        name: 'produtos',
        component: Produtos
    },
    {
        path: '/produto-form/:id_Produto?',
        name: 'produto-form',
        component: ProdutoForm
    }
]
