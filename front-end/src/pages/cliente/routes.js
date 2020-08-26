import ClientePJForm from './clientePJ-form.vue';
import ClientePFForm from './clientePF-form.vue';
import Clientes from './clientes.vue';

export default [

    {
        path: '/clientes',
        name: 'clientes',
        component: Clientes
    },
    {
        path: '/clientePJ-form/:id_Cliente?',
        name: 'clientePJ-form',
        component: ClientePJForm
    },
    {
        path: '/clientePF-form/:id_Cliente?',
        name: 'clientePF-form',
        component: ClientePFForm
    }
]
