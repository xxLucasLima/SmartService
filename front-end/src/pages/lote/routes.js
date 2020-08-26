import LoteForm from './lote-form.vue';
import Lotes from './lotes.vue';

export default [

    {
        path: '/lotes',
        name: 'lotes',
        component: Lotes
    },
    {
        path: '/lote-form/:id_Lote?',
        name: 'lote-form',
        component: LoteForm
    }
]
