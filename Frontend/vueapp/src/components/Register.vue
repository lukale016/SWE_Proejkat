<template>
  <div class="login-form">
    <h2 class="login-heading">Register</h2>
    <form action="#" @submit.prevent="register">
      <div class="form-control1">
        <label for="username" class="lab">Username </label>
        <input
          type="text"
          name="name"
          id="Username"
          class="login-input"
          v-model="Username"
        />
        <b-alert v-model="this.jedan" variant="danger"
          >Molimo vas, unesite korisničko ime!
        </b-alert>
      </div>

      <div class="form-control1 ">
        <label for="password" class="lab">Password</label>
        <input
          type="password"
          name="password"
          id="Password"
          class="login-input"
          v-model="Password"
        />
        <b-alert v-model="this.dva" variant="danger"
          >Molimo vas, unesite lozinku!
        </b-alert>
      </div>

      <div class="form-control1">
        <label for="name" class="lab">Your name</label>
        <input
          type="text"
          name="name"
          id="Name"
          class="login-input"
          v-model="Name"
        />
        <b-alert v-model="this.tri" variant="danger"
          >Molimo vas, unesite ime!
        </b-alert>
      </div>

      <div class="form-control1">
        <label for="Surname" class="lab">Surname</label>
        <input
          type="text"
          name="surname"
          id="Surname"
          class="login-input"
          v-model="Surname"
        />
        <b-alert v-model="this.cetri" variant="danger"
          >Molimo vas, unesite prezime!
        </b-alert>
      </div>

      <div class="form-control1">
        <label for="City" class="lab">City</label>
        <input
          type="text"
          name="city"
          id="City"
          class="login-input"
          v-model="City"
        />
        <b-alert v-model="this.pet" variant="danger"
          >Molimo vas, unesite grad!
        </b-alert>
      </div>

      <div class="form-control1">
        <label class="d-flex justify-content-center , font-italic " for="Bio"
          >Tell us more about yourself</label
        >
        <input
          type="text"
          name="bio"
          id="Bio"
          class="form-control input-lg"
          placeholder="Do you cafe owner? 
What kind of music do you like?"
          v-model="Bio"
        />
        <b-alert v-model="this.sest" variant="danger"
          >Molimo vas, unesite biografiju!
        </b-alert>
      </div>

      <div class="form-control1">
        <b-form-group
          label="Your image:"
          label-for="file-small"
          label-cols-sm="3"
          label-size="sm"
        >
          <b-form-file
            id="file-small"
            size="sm"
            name="Image"
            v-model="image"
          ></b-form-file>
          <b-alert v-model="this.sedam" variant="danger"> Greška </b-alert>
        </b-form-group>
      </div>

      <div class="form-control1">
        <b-button type="submit" pill  variant="primary"  class="btn-submit" @click="register">
          Create Account
        </b-button>
      </div>

      <router-link to="/Login">
        Already have an account? Login.
      </router-link>
    </form>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  data() {
    return {
      Username: '',
      Password: '',
      Name: '',
      Surname: '',
      City: '',
      Bio: '',
      image: null,

      jedan: false,
      dva: false,
      tri: false,
      cetri: false,
      pet: false,
      sest: false,
      sedam: false
    }
  },
  methods: {
    register() {
      if (
        this.Username === '' ||
        this.Password === '' ||
        this.Name === '' ||
        this.Surname === '' ||
        this.City === '' ||
        this.Bio === ''
      ) {
        if (this.Username === '') this.jedan = true
        else this.jedan = false
        if (this.Password === '') this.dva = true
        else this.dva = false
        if (this.Name === '') this.tri = true
        else this.tri = false
        if (this.Surname === '') this.cetri = true
        else this.cetri = false
        if (this.City === '') this.pet = true
        else this.pet = false
        if (this.Bio === '') this.sest = true
        else this.sest = false
      } else {
        this.jedan = false
        this.dva = false
        this.tri = false
        this.cetri = false
        this.pet = false
        this.sest = false

        axios({
          method: 'post',
          url: 'http://localhost:8080/Vinyl/Add/User',
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json',
            Accept: 'application/json'
          },
          data: {
            Username: this.Username,
            Password: this.Password,
            Name: this.Name,
            Surname: this.Surname,
            City: this.City,
            Bio: this.Bio
          }
        })
          .then(response => {
            console.log(response.data)
            //const fd = new FormData()
            //fd.append('image', this.selectedFile, this.selectedFile.name)
            //fd.append('Image', this.image)

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
              .then(response2 => {
                console.log(response2.data)

                if (this.sedam !== null) {
                  const fd = new FormData()
                  fd.append('Image', this.image)

                  axios({
                    method: 'post',
                    url:
                      'http://localhost:8080/Vinyl/Add/UserImg/' +
                      this.Username,
                    headers: {
                      'Access-Control-Allow-Origin': '*',
                      'Content-Type': 'multipart/form-data',
                      Accept: 'application/json',
                      Authorization: 'Bearer ' + response2.data.token
                    },
                    data: fd
                  })
                    .then(response3 => {
                      console.log(response3.data)
                      this.$router.push({ name: 'Login' })
                    })
                    .catch(error => {
                      console.log(error.response3)
                    })
                } else {
                  this.$router.push({ name: 'Login' })
                }
              })
              .catch(error => {
                console.log(error.response2)
              })
          })
          .catch(error => {
            console.log(error.response)
          })
      }
    }
  }
}
</script>
