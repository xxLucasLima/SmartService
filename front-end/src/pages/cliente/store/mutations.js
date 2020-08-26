import * as types from './mutation-types'

export default {
    [types.CREATE_CLIENTE](state, payload) {
        state.cliente = payload
    },
    [types.GET_CLIENTES](state, payload) {
        state.clientes = payload
    },
}