import EmpresaForm from './empresa-form.vue';
import Empresas from './empresas.vue';

export default [

    {
        path: '/empresas',
        name: 'empresas',
        component: Empresas
    },
    {
        path: '/empresa-form/:id_Empresa?',
        name: 'empresa-form',
        component: EmpresaForm
    }
]
