import services from '@/http'
import * as types from './mutation-types'
import routes from '@/routes'


export const ActionCreateProduto = ({ commit }, payload) => {
    services.produto.criaProduto(payload);
}

export const ActionGetAllProdutos = ({ commit }) => {
    services.produto.getProdutos().then(res => {
        commit(types.GET_PRODUTOS, res.data);
    });

}

export const ActionGetProdutoById = ({ commit }, payload) => {
    return services.produto.getProdutoById({ idProduto: payload });
}

export const ActionEditProduto = ({ commit }, payload) => {
    console.log(payload)
    services.produto.editProduto({idProduto: routes.currentRoute.params.id_Produto},payload);
}

export const ActionDeleteProdutoById = ({ commit }, payload) => {
    services.produto.deleteProdutoById({ idProduto: payload });
}

export const ActionGetAllClientesDDL = ({ commit }) => {
    return services.produto.getAllClientesDDL();
}

export const ActionGetAllDefeitosProdutoDDL = ({ commit }) => {
    return services.produto.getAllDefeitosProdutoDDL();
}

export const ActionGetAllLotesDDL = ({ commit }) => {
    return services.produto.getAllLotesDDL();
}