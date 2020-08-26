import services from '@/http'
import * as types from './mutation-types'
import routes from '@/routes'
import {parseDateToLocal} from '@/helpers/dateHandler/dateHandler.js'


export const ActionCreateLote = ({ commit }, payload) => {
    services.lote.criaLote(payload);
}

export const ActionGetAllLotes = ({ commit }) => {
    services.lote.getLotes().then(res => {
        res.data.forEach((lote) => {
            lote.dataEntrada = parseDateToLocal(lote.dataEntrada);
            lote.dataSaida = parseDateToLocal(lote.dataSaida);
        })
        commit(types.GET_LOTES, res.data);
    });
}

export const ActionGetLoteById = ({ commit }, payload) => {
    return services.lote.getLoteById({ idLote: payload });
}

export const ActionEditLote = ({ commit }, payload) => {
    services.lote.editLote({ idLote: routes.currentRoute.params.id_Lote }, payload);
}

export const ActionDeleteLoteById = ({ commit }, payload) => {
    services.lote.deleteLoteById({ idLote: payload });
}

export const ActionGetAllClientesDDL = ({ commit }) => {
    return services.lote.getAllClientesDDL();
}

export const ActionGetAllDefeitosLoteDDL = ({ commit }) => {
    return services.lote.getAllDefeitosLoteDDL();
}

export const ActionGetAllLotesDDL = ({ commit }) => {
    return services.lote.getAllLotesDDL();
}

function trasnformaDatas(date) {
    return new Date(date.substring(0, 10)).toLocaleDateString()
}