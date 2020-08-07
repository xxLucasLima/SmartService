import * as types from './mutation-types'

export default {
    [types.CREATE_USUARIO](state, payload) {
        state.usuario = payload
    },
    [types.GET_USUARIOS](state, payload) {
        state.usuarios = payload
    },
}