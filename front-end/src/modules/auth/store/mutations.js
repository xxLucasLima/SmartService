import * as types from './mutation-types'

export default{
    [types.SET_USUARIO] (state, payload){
        state.usuario = payload
    },
    [types.SET_TOKEN] (state, payload){
        state.token = payload
    },   
}