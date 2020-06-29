import Vue from 'vue'
import routes from './routes'
import { store } from './store/store'
import NavBar from './components/NavBar'
import VueRouter from 'vue-router'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import axios from 'axios'

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

window.eventBus = new Vue()

Vue.config.productionTip = false
Vue.use(VueRouter)

new Vue({
  el: '#app',
  router: routes,
  store: store,
  create() {
    const userString = Window.localStorage.getItem('user')
    if (userString) {
      const userData = JSON.parse(userString)
      this.$store.commit('SET_USER_DATA', userData)
    }
    axios.interceptors.response.use(
      response => response,
      error => {
        if (error.response.status == 401) {
          this.$store.dispatch('logout')
        }
      }
    )
  },

  components: { NavBar },
  template: '<NavBar/>'
})
