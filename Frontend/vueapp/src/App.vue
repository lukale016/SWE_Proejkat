<template>
  <div id="app">
    <NavBar />
    <router-view />
    <todo-list></todo-list>
  </div>
</template>

<script>
import TodoList from './components/TodoList'
//import {Overlay} from 'vuejs-overlay.js';
import NavBar from './components/NavBar.vue'
export default {
  name: 'App',
  components: {
    TodoList,
    NavBar

    //Overlay
  }
}
</script>

<style>
.container {
  max-width: 600px;
  margin: 0 auto;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
