import services from '@/http'
import * as types from './mutation-types'
import routes from '@/routes'
import {parseDateToLocal} from '@/helpers/dateHandler/dateHandler.js'


export const ActionCreateCliente = ({ commit }, payload) => {
    services.cliente.criaCliente(payload);
}

export const ActionGetAllClientes = ({ commit }) => {
    services.cliente.getClientes().then(res => {
        res.data.forEach((cliente) => {
            if(cliente.tipoPessoa == "Física")
                cliente.dataNascimento = parseDateToLocal(cliente.dataNascimento);
        })
        commit(types.GET_CLIENTES, res.data);
    });
}

export const ActionGetClienteById = ({ commit }, payload) => {
    return services.cliente.getClienteById({ idCliente: payload });
}

export const ActionEditCliente = ({ commit }, payload) => {
    services.cliente.editCliente({ idCliente: routes.currentRoute.params.id_Cliente }, payload);
}

export const ActionDeleteClienteById = ({ commit }, payload) => {
    services.cliente.deleteClienteById({ idCliente: payload });
}
