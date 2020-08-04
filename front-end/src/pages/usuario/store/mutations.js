import * as types from './mutation-types'

export default{
    [types.CREATE_USUARIO] (state, payload){
        state.usuario = payload
    }, 
}