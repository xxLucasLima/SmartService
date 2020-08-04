import services from '@/http'
import * as types from './mutation-types'

export const ActionCreateUsuario = ({ commit }, payload) => {
    return services.criaUsuario(payload);
}
