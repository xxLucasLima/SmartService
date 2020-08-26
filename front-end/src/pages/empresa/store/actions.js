import services from '@/http'
import * as types from './mutation-types'
import routes from '@/routes'
import {parseDateToLocal} from '@/helpers/dateHandler/dateHandler.js'


export const ActionCreateEmpresa = ({ commit }, payload) => {
    services.empresa.criaEmpresa(payload);
}

export const ActionGetAllEmpresas = ({ commit }) => {
    services.empresa.getEmpresas().then(res => {
        commit(types.GET_EMPRESAS, res.data);
    });
}

export const ActionGetEmpresaById = ({ commit }, payload) => {
    return services.empresa.getEmpresaById({ idEmpresa: payload });
}

export const ActionEditEmpresa = ({ commit }, payload) => {
    services.empresa.editEmpresa({ idEmpresa: routes.currentRoute.params.id_Empresa }, payload);
}

export const ActionDeleteEmpresaById = ({ commit }, payload) => {
    services.empresa.deleteEmpresaById({ idEmpresa: payload });
}
