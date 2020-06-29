<template>
  <div>
    <b-card
      border-variant="primary"
      :header="this.dogadjaj.title"
      header-bg-variant="primary"
      header-text-variant="white"
      align="center"
    >
      <b-card-text>Oraginzator: {{this.dogadjaj.owner}}</b-card-text>
      <b-card-text>Datum: {{this.dogadjaj.date}}</b-card-text>
      <b-card-text>Vreme: {{this.dogadjaj.time}}</b-card-text>
      <b-card-text>
        Lokacija:
        <router-link
          :to="{ name: 'Cafe',params:{id: this.dogadjaj.caffeRef} }"
        >{{this.dogadjaj.cafe}}</router-link>
      </b-card-text>
      <b-card-text>Opis: {{this.dogadjaj.info}}</b-card-text>
      <b-form-checkbox
        v-if="provera() && proveraKorisnika(this.dogadjaj.owner)"
        id="checkbox-1"
        v-model="status"
        name="checkbox-1"
        value="interested"
        unchecked-value="notInterested"
        @input="posaljiZahtev"
      >Zainteresovan/a</b-form-checkbox>
      <b-button v-b-toggle.collapse-2 variant="info">Zainteresovani</b-button>
      <b-collapse id="collapse-2" class="mt-2">
        <b-card-text
          v-for="zainteresovan in this.dogadjaj.interested"
          :key="zainteresovan.attenderRef"
        >{{zainteresovan.attenderRef}}</b-card-text>
      </b-collapse>
    </b-card>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  data() {
    return {
      status: 'notInterested',
      zainteresovani: []
    }
  },
  props: {
    dogadjaj: Object
  },
  methods: {
    kaocao() {
      console.log('ajde sads', this.status)
    },
    provera() {
      if (localStorage.user === undefined || localStorage.user === '') {
        return false
      } else {
        return true
      }
    },
    proveraKorisnika(owner) {
      if (localStorage.user === owner) {
        return false
      } else {
        return true
      }
    },
    posaljiZahtev() {
      if (this.status == 'interested') {
        axios({
          method: 'post',
          url: 'http://localhost:8080/Vinyl/Add/Interested',

          headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json',
            Accept: 'application/json',
            Authorization: 'Bearer ' + localStorage.token
          },
          data: {
            EventRef: this.dogadjaj.id,
            AttenderRef: localStorage.user
          }
        })
          .then(response => {
            console.log(response)
            window.location.reload()
          })
          .catch(error => {
            if (error.response.status == 401) {
              localStorage.token = ''
              localStorage.user = ''
              this.$router.push({ name: 'Login' })
            } else window.location.reload()
          })
      } else {
        axios
          .delete(
            'http://localhost:8080/Vinyl/Delete/Interested/' +
              this.dogadjaj.id +
              '/' +
              localStorage.user,
            { headers: { Authorization: 'Bearer ' + localStorage.token } }
          )
          .then(() => {
            window.location.reload()
          })
          .catch(error => {
            if (error.response.status == 401) {
              localStorage.token = ''
              localStorage.user = ''
              this.$router.push({ name: 'Login' })
            } else window.location.reload()
          })
      }
    },
    kaocao2() {
      this.dogadjaj.interested.map(item => {
        if (item.attenderRef === localStorage.user) {
          this.status = 'interested'
          return
        } else this.status = 'notInterested'
      })
    }
  },
  created() {
    console.log('pre funkcije', this.status)
    this.kaocao2()
    console.log('posle funkcije', this.status)
  }
}
</script>

<style>
</style>