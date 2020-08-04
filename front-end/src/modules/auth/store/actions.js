import services from '@/http'
import * as storage from '../storage'
import * as types from './mutation-types'

export const ActionLogin = ({ dispatch }, payload) => {
    return services.auth.login(payload).then(res => {
        dispatch('ActionSetToken', res.data.token);
        dispatch('ActionSetUsuario', res.data.usuario);

    })
}

export const ActionCheckToken = ({dispatch, state}) => {

    if(state.token){
        return Promise.resolve(state.token)
    }

    const token = storage.getLocalToken()

    if(!token){
        return Promise.reject(new Error('Token inválido'))
    }

    dispatch('ActionSetToken', token)
    return dispatch('ActionLoadSession')    
}

export const ActionLoadSession = ({ dispatch }) => {
    return new Promise ( async (resolve, reject) => { // eslint-disable-line no-async-promise-executor
        try {
            const { data: { usuario } } = await services.auth.loadSession()
            dispatch('ActionSetUsuario', usuario);
            alert("resolved")
            resolve()
        } catch (err) {
            dispatch('ActionSignOut');
            reject(err);
        }
    })
}

export const ActionSetUsuario = ({ commit }, payload) => {
    commit(types.SET_USUARIO, payload);
}

export const ActionSetToken = ({ commit }, payload) => {
    storage.setLocalToken(payload)
    storage.setHeaderToken(payload)
    commit(types.SET_TOKEN, payload);
}

export const ActionSignOut = ({ dispatch }) => {
    storage.setHeaderToken('')
    storage.deleteLocalToken()
    
    dispatch('ActionSetUsuario', {});
    dispatch('ActionSetToken', '');
}