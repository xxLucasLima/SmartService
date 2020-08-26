import * as types from './mutation-types'

export default {
    [types.CREATE_EMPRESA](state, payload) {
        state.empresa = payload
    },
    [types.GET_EMPRESAS](state, payload) {
        state.empresas = payload
    },
}