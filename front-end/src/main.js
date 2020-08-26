import Vue from 'vue'
import App from './App.vue'
import store from './store'
import router from './routes'
import './assets/scss/app.scss'


import { BootstrapVue, IconsPlugin, ModalPlugin, ToastPlugin } from 'bootstrap-vue'

import VueCryptojs from 'vue-cryptojs'

Vue.use(BootstrapVue)
Vue.use(ToastPlugin)
Vue.use(IconsPlugin)
Vue.use(ModalPlugin)
Vue.use(VueCryptojs)

Vue.config.productionTip = false

window._Vue = new Vue({
    router,
    store,
    render: h => h(App),
}).$mount('#app')
