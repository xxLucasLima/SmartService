import services from '@/http'
import * as types from './mutation-types'

export const ActionCreateUsuario = ({ commit }, payload) => {
    services.usuario.criaUsuario(payload);
}

export const ActionGetAllUsuarios = ({ commit }) => {
    services.usuario.getUsuarios().then(res => {
        commit(types.GET_USUARIOS, res.data);
    });

}

export const ActionDeleteUsuarioById = ({ commit }, payload) => {
    services.usuario.deleteUsuarioById({ Id_Usuario: payload });
}

export const ActionGetAllEmpresasDDL = ({ commit }) => {
    return services.usuario.getAllEmpresasDDL();
}

export const ActionGetAllPerfisUsuarioDDL = ({ commit }) => {
    return services.usuario.getAllPerfisUsuarioDDL();
}