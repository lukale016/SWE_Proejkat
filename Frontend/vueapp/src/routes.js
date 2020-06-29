import Vue from 'vue'
//import App from './App'

import VueRouter from 'vue-router'
import Home from './views/Home'
import Login from './components/Login'
import Logout from './components/Logout'
import Register from './components/Register'
import AboutUs from './views/AboutUs'
import Cafes from './views/Cafes'
import Gallery from './views/Gallery'
import Calendar from './views/Calendar'
import Profile from './views/Profile'
import Vinyl from './components/Vinyl'
import Cafe from './views/Cafe'

//import App from '../components/App.vue'
//import Todo from '../components/Todo'

Vue.use(VueRouter)

const routes = new VueRouter({
  base: process.env.BASE_URL,
  mode: 'history',
  duplicateNavigationPolicy: 'ignore',
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },

    {
      path: '/about-us',
      name: 'AboutUs',
      component: AboutUs
    },
    {
      path: '/gallery',
      name: 'Gallery',
      component: Gallery
    },
    {
      path: '/calendar',
      name: 'Calendar',
      component: Calendar
    },
    {
      path: '/cafes',
      name: 'Cafes',
      component: Cafes
    },
    {
      path: '/cafe/:id',
      name: 'Cafe',
      component: Cafe,
      props:true
    },
    {
      path: '/vinyl/:id',
      name: 'Vinyl',
      component: Vinyl,
      props:true
    },
    {
      path: '/Profile',
      name: 'Profile',
      component: Profile
      //meta: {
     //   requiresVisitor: true
     // }
    },
    {
      path: '/Login',
      name: 'Login',
      component: Login
    },
    {
      path: '/Register',
      name: 'Register',
      component: Register
    },
    {
      path: '/Logout',
      name: 'Logout',
      component: Logout
    }
  ]
})

routes.beforeEach((to, from, next) => {
  const loggedIn = localStorage.getItem('access_token')

  if (to.matched.some(record => record.meta.requiresAuth) && !loggedIn) {
    next()
  }
  next()
})

export default routes
