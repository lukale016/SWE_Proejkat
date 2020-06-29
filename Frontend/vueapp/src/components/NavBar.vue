<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="dark">
      <b-navbar-brand href="#">
        <router-link :to="{ name: 'Home' }" class="MyClas">
          <b-img
            src="https://scontent.fbeg4-1.fna.fbcdn.net/v/t1.15752-9/93819713_284741032687841_6741570393686933504_n.png?_nc_cat=102&_nc_sid=b96e70&_nc_eui2=AeEN7JwkVax3D5JGe5KpWUD7VfB9nQR608JV8H2dBHrTwllcbnJufi5gjHEsqvN5Amp-Rl4-aJ44Az-bngUFhzU6&_nc_ohc=hfsgpPdOCOsAX8r4G12&_nc_ht=scontent.fbeg4-1.fna&oh=9b20a944a1b2fd651e5496ccc0fb2b3f&oe=5F05751D"
            fluid
            alt="Responsive image"
          ></b-img>
        </router-link>
      </b-navbar-brand>
      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <!--      Right aligned nav items -->

        <b-navbar-nav class="ml-auto">
          <!-- PLOCE -->
          <b-nav-item href="#" text="Galerija ploca">
            <b-button @click="prosledi" variant="link"
              ><router-link :to="{ name: 'Gallery' }"
                >Galerija ploča</router-link
              ></b-button
            >
          </b-nav-item>

          <!-- O NAMA -->

          <b-nav-item href="#" text="O nama">
            <b-button @click="prosledi" variant="link"
              ><router-link :to="{ name: 'AboutUs' }"
                >O nama</router-link
              ></b-button
            >
          </b-nav-item>

          <!-- DOGADJAJI -->
          <b-nav-item href="#">
            <b-button @click="prosledi" variant="link"
              ><router-link :to="{ name: 'Calendar' }"
                >Kalendar događaja</router-link
              ></b-button
            >
          </b-nav-item>

          <!-- MESTA -->

          <b-nav-item href="#">
            <b-button @click="prosledi" variant="link"
              ><router-link :to="{ name: 'Cafes' }"
                >Mesta</router-link
              ></b-button
            >
          </b-nav-item>

          <!-- PROFIL -->
          <b-nav-item href="#" text="Profile">
            <b-button @click="prosledi" variant="link"
              ><router-link v-if="ispitaj()" :to="{ name: 'Profile' }"
                >Profil</router-link
              ></b-button
            >
          </b-nav-item>

          <b-nav-item href="#" text="Logout">
            <b-button @click="logout" variant="link"
              ><router-link v-if="ispitaj()" :to="{ name: 'Logout' }">
                <Label><Label>Logout</Label></Label></router-link
              ></b-button
            >
          </b-nav-item>

          <!-- PRIJAVA -->

          <b-nav-item-dropdown
            size="sm"
            class="m-2"
            style="margine-top :30px"
            id="my-nav-dropdown"
            text="Prijava"
            righttext-variant="light"
            toggle-class="nav-link-custom"
            v-if="!ispitaj()"
            right
          >
            <b-dropdown-item>
              <div class="row"></div>
            </b-dropdown-item>
            <b-nav-item>
              <b-button pill variant="outline-primary"
                ><router-link
                  :to="{ name: 'Login' }"
                  class="button"
                  >Uloguj se</router-link
                ></b-button
              >
              <div></div>

              <b-button pill variant="outline-primary">
                <router-link
                  :to="{ name: 'Register' }"
                  class="button"
                  >Registruj se</router-link
                >
              </b-button>
            </b-nav-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  methods: {
    logout() {
      localStorage.token = ''
      localStorage.user = ''
      this.$router.push({ name: 'Home' })
    },

    ispitaj() {
      const a = Boolean(localStorage.token)
      console.log(a)
      return a
    },

    ispitaj1() {
      if (localStorage.getItem === undefined) {
        return true
      } else return false
    },

    prosledi() {
      window.location.reload()
    }
  }
}
</script>

<style lang="scss">
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  font-size: 24px;
  height: 100vh;
}
.flex-center {
  display: flex;
  justify-content: center;
}
.nav {
  display: flex;
  list-style: none;
  padding: 15px 7px;
  margin: 0;
  justify-content: flex-end;
  background: #f5f8fa;
  border-bottom: 1px solid lightgrey;
  margin-bottom: 24px;
}
.nav a {
  color: #636b6f;
  padding: -2px 5px;
  font-size: 10px;
  font-weight: 100;
  letter-spacing: 4px;
  text-decoration: none;
  text-transform: uppercase;
}
// Auth Pages
label {
  display: block;
  margin-bottom: 4px;
}
.login-heading {
  margin-bottom: 16px;
}
.form-control {
  margin-bottom: 24px;
}
.form-control1 {
  margin-bottom: 15px;
}

.form-control11 {
  margin-bottom: 20px;
  width: 30%;
}

.lab {
  max-width: 500px;
  margin: auto;
  width: 20%;
  font-size: 20px;
  padding: -2px 10px;
  outline: 3;
  //border-radius: 6px;
  //border: 3px solid lightgrey;
}

.mb-more {
  margin-bottom: 42px;
}
.login-form {
  max-width: 500px;
  margin: auto;
}
.login-input {
  width: 70%;
  font-size: 20px;
  padding: -2px 10px;
  outline: 3;
  border-radius: 6px;
  border: 3px solid lightgrey;
}

.login-input1 {
  width: 70%;
  font-size: 10px;
  padding: -2px 15px;
  outline-width: 3;
  border-radius: 6px;
  border: 3px solid lightgrey;
}

.btn-submit {
  width: 100%;
  padding: -7px 5px;
  font-size: 12px;
  font-weight: bold;
  background: #60bd4f;
  color: rgb(5, 4, 4);
  border-radius: 7px;
  cursor: pointer;
  &:hover {
    background: darken(#60bd4f, 10%);
  }
}

.item {
  padding: 8px 10px;
  font-size: 20px;
}
</style>
