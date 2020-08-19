import * as types from './mutation-types'

export default {
    [types.CREATE_PRODUTO](state, payload) {
        state.produto = payload
    },
    [types.GET_PRODUTOS](state, payload) {
        state.produtos = payload
    },
}