import * as actions from './actions';
import state from './state';
import  mutations from './mutations';
import  * as getters from './getters';

export default{
    actions,
    state,
    getters,
    mutations,
    namespaced: true

}