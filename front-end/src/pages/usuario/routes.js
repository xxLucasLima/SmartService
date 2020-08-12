import UsuarioForm from './usuario-form.vue';
import Usuarios from './usuarios.vue';

export default [

    {
        path: '/usuarios',
        name: 'usuarios',
        component: Usuarios
    },
    {
        path: '/usuario-form/:id_Usuario?',
        name: 'usuario-form',
        component: UsuarioForm
    }
]
