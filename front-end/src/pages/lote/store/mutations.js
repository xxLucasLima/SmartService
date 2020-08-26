import * as types from './mutation-types'

export default {
    [types.CREATE_LOTE](state, payload) {
        state.lote = payload
    },
    [types.GET_LOTES](state, payload) {
        state.lotes = payload
    },
}