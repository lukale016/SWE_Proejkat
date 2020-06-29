<template>
  <div class="login-form">
    <h2 class="login-heading">Login</h2>
    <form action="#" @submit.prevent="login">
      <div class="form-control1">
        <label class="lab" for="username">Username</label>
        <input
          type="username"
          name="username"
          id="Username"
          class="login-input"
          v-model="Username"
        />
      </div>

      <div class="form-control1 ">
        <label class="lab" for="password">Password</label>
        <input
          type="password"
          name="password"
          id="Password"
          class="login-input"
          v-model="Password"
        />
      </div>

      <div class="form-control1">
        <b-button
          type="submit"
          class="btn-submit"
          pill
          variant="primary"
          @click="login"
          >Login</b-button
        >
      </div>

      <p>{{ error }}</p>

      <router-link to="/Register">
        Don't have an account? Register.
      </router-link>
    </form>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'Login',
  data() {
    return {
      Username: '',
      Password: '',
      error: null
    }
  },
  methods: {
    login() {
      axios({
        method: 'post',
        url: 'http://localhost:8080/Vinyl/Login',
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
          Accept: 'application/json'
        },
        data: {
          Username: this.Username,
          Password: this.Password
        }
      })
        //.then(response => {
        //  console.log(response)
        // })
        .then(resp => {
          //window.localStorage.setItem('access_token', resp.data.token)
          console.log(resp)

          localStorage.token = ''
          localStorage.token = resp.data.token
          // console.log(localStorage.token)

          axios
            .get('http://localhost:8080/Vinyl/LogedUser', {
              headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                Accept: 'application/json',
                Authorization: 'Bearer ' + localStorage.token
              }
            })
            .then(response => {
              this.recievingArray = response.data
              localStorage.user = ''
              localStorage.user = response.data.username
            })
            .catch(error => {
              console.log(error.response)
            })

          this.$router.push({ name: 'Profile' })
        })

        .catch(error => {
          this.error = error.response.data.error
        })
    }
  }
}
</script>
