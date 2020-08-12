import services from '@/http'
import * as types from './mutation-types'
import routes from '@/routes'


export const ActionCreateUsuario = ({ commit }, payload) => {
    services.usuario.criaUsuario(payload);
}

export const ActionGetAllUsuarios = ({ commit }) => {
    services.usuario.getUsuarios().then(res => {
        commit(types.GET_USUARIOS, res.data);
    });

}

export const ActionGetUsuarioById = ({ commit }, payload) => {
    return services.usuario.getUsuarioById({ idUsuario: payload });
}

export const ActionEditUsuario = ({ commit }, payload) => {
    console.log(payload)
    services.usuario.editUsuario({idUsuario: routes.currentRoute.params.id_Usuario},payload);
}

export const ActionDeleteUsuarioById = ({ commit }, payload) => {
    services.usuario.deleteUsuarioById({ idUsuario: payload });
}

export const ActionGetAllEmpresasDDL = ({ commit }) => {
    return services.usuario.getAllEmpresasDDL();
}

export const ActionGetAllPerfisUsuarioDDL = ({ commit }) => {
    return services.usuario.getAllPerfisUsuarioDDL();
}