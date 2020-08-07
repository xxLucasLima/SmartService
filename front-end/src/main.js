import Vue from 'vue'
import App from './App.vue'
import store from './store'
import router from './routes'
import './assets/scss/app.scss'


import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import { ModalPlugin } from 'bootstrap-vue'

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(ModalPlugin)



Vue.config.productionTip = false

window._Vue = new Vue({  
  router,
  store,
  render: h => h(App),
}).$mount('#app')
