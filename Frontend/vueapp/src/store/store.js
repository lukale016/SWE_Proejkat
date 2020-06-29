import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export const store = new Vuex.Store({
  state: {
    user: null
    //token: Window.localStorage.getItem('access_token') || null,
    // filter: 'all',
    // todos: []
  },
  mutattion: {
    CLEAR_USER_DATA() {
      Window.localStorage.removeItem('user')
      location.reload()
    }
  },
  action: {
    logout({ commit }) {
      commit('CLEAR_USER_DATA')
    }
  },
  getters: {
    loggedIn(state) {
      return !!state.user
    }
  }
})
