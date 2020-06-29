<template class="MyContainer">
  <div class="MyBackground">
    <b-container>
      <b-row align-h="center">
        <b-col md="6">
          <CreateEvent :dogadjaj="this.nullObj" v-if="this.provera()"></CreateEvent>
        </b-col>
      </b-row>
      <b-row align-h="center" v-if="!this.provera()">
        <b-col>
          <h3>Morate biti ulogovani da biste kreirali događaj</h3>
        </b-col>
      </b-row>
      <b-row v-if="this.events.length">
        <b-col md="3" v-for="event in this.events" :key="event.id" class="MyRow">
          <b-card
            :border-variant="cardColor(event.owner)"
            :header="event.title"
            :header-bg-variant="cardColor(event.owner)"
            header-text-variant="white"
            align="center"
          >
            <b-card-text>Datum: {{event.date}}</b-card-text>
            <b-button @click="eventInfoFun(event)" v-b-modal.modal-2 variant="info">Info</b-button>
            <b-button
              :hidden="proveraKorisnika(event.owner)"
              variant="outline-primary"
              @click="izmeni(event)"
              v-b-modal.modal-1
            >Izmeni</b-button>
            <b-button
              :hidden="proveraKorisnika(event.owner)"
              variant="outline-danger"
              @click="eventForDeleteFun(event)"
              v-b-modal.modal-3
            >Izbriši</b-button>
          </b-card>
        </b-col>
      </b-row>
      <b-row v-if="this.events.length===0">
        <b-col class="text-center">
          <h3>Nema trentuno aktivnih događaja</h3>
        </b-col>
      </b-row>
      <b-row v-if="!this.events.length">
        <b-col class="text-center">
          <b-spinner
            variant="light"
            type="grow"
            label="Large Spinner"
            style="width: 3rem; height: 3rem; margin-top: 2em;"
          ></b-spinner>
        </b-col>
      </b-row>
      <b-row v-if="this.eventForEdit">
        <b-col>
          <b-modal id="modal-1" :title="'Izmena događaja '+ this.eventForEdit.title" hide-footer>
            <CreateEvent :dogadjaj="this.eventForEdit"></CreateEvent>
          </b-modal>
        </b-col>
      </b-row>
      <b-row v-if="this.eventInfo">
        <b-col>
          <b-modal
            id="modal-2"
            :title="'Informacije o događaju '+ this.eventInfo.title"
            hide-footer
          >
            <Event :dogadjaj="this.eventInfo"></Event>
          </b-modal>
        </b-col>
      </b-row>
      <b-row v-if="this.eventForDelete">
        <b-col>
          <b-modal
            id="modal-3"
            :title="'Brisanje događaja '+ this.eventForDelete.title"
            hide-footer
          >
            <p>Da li ste sigurni da želite da izbrišete događaj {{this.eventForDelete.title}}?</p>
            <b-button variant="success" @click="izbrisi(eventForDelete.id)">Potvrdi</b-button>
          </b-modal>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
import CreateEvent from '@/components/CreateEvent.vue'
import Event from '@/components/Event.vue'
import axios from 'axios'

export default {
  data() {
    return {
      nizPrivatnih: [],
      korisnik: null,
      recievingArray: [],
      events: [],
      eventForEdit: null,
      eventInfo: null,
      eventForDelete: null,
      nullObj: {
        title: '',
        date: '',
        time: '',
        cafe: '',
        info: '',
        type: '',
        empty: true
      }
    }
  },
  components: {
    CreateEvent,
    Event
  },
  methods: {
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
    setEventsFromData() {
      this.recievingArray.map(item => {
        if (new Date(item.date) >= new Date().setHours(0)) {
          let event = {
            id: item.id,
            title: item.title,
            owner: item.owner.username,
            date: item.date,
            time: item.time,
            cafe:
              item.caffe.name +
              ', ' +
              item.caffe.address +
              ', ' +
              item.caffe.city,
            info: item.info,
            type: item.modifier,
            empty: false,
            interested: item.interested,
            caffeRef: item.caffeRef
          }
          this.events.push(event)
        }
      })
      this.events = this.events.sort(
        (a, b) => new Date(a.date) - new Date(b.date)
      )
    },
    izmeni(dogadjaj) {
      this.eventForEdit = dogadjaj
    },
    izbrisi(id) {
      axios
        .delete('http://localhost:8080/Vinyl/Delete/Event/' + id, {
          headers: { Authorization: 'Bearer ' + localStorage.token }
        })
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
    },
    eventInfoFun(dogadjaj) {
      this.eventInfo = dogadjaj
    },
    eventForDeleteFun(dogadjaj) {
      this.eventForDelete = dogadjaj
    },
    cardColor(owner) {
      if (localStorage.user === owner) return 'dark'
      else return 'dark'
    }
  },
  created() {
    axios
      .get('http://localhost:8080/Vinyl/PublicEvents')
      .then(response1 => {
        this.recievingArray = response1.data
        console.log('ovo je pre svih', response1.data)
        if (this.provera()) {
          console.log('nebitno')

          axios
            .get('http://localhost:8080/Vinyl/User/' + localStorage.user, {
              headers: { Authorization: 'Bearer ' + localStorage.token }
            })
            .then(response2 => {
              this.korisnik = response2.data

              axios
                .get('http://localhost:8080/Vinyl/PrivateEvents', {
                  headers: { Authorization: 'Bearer ' + localStorage.token }
                })
                .then(response3 => {
                  this.nizPrivatnih = response3.data
                  this.korisnik.friends.map(item1 => {
                    this.nizPrivatnih.map(item2 => {
                      if (
                        item1.user2Ref == item2.ownerRef ||
                        item1.user1Ref == item2.ownerRef
                      ) {
                        this.recievingArray.push(item2)
                      }
                    })
                  })
                  this.setEventsFromData()
                })
                .catch(error3 => {
                  if (error3.response.status == 401) {
                    localStorage.token = ''
                    localStorage.user = ''
                    this.$router.push({ name: 'Login' })
                  } else window.location.reload()
                })
            }) //ovde se zatvara then2
            .catch(error2 => {
              if (error2.response.status == 401) {
                localStorage.token = ''
                localStorage.user = ''
                this.$router.push({ name: 'Login' })
              } else window.location.reload()
            })
        } else {
          console.log('da li se ovo izvrsava')
          this.setEventsFromData()
        }

        console.log('ovo je niz events posle prvi get', this.events)

        // if (this.provera()) {
        //   axios
        //     .get('http://localhost:8080/Vinyl/User/' + localStorage.user, {
        //       headers: { Authorization: 'Bearer ' + localStorage.token }
        //     })
        //     .then(response => {
        //       this.korisnik = response.data
        //       console.log('ovo je korisnik', this.korisnik)
        //       if (this.korisnik.friends.length > 0) {
        //         this.korisnik.friends.map(item => {
        //           axios
        //             .get(
        //               'http://localhost:8080/Vinyl/UserPrivateEvents/' +
        //                 item.user2Ref,
        //               {
        //                 headers: {
        //                   Authorization: 'Bearer ' + localStorage.token
        //                 }
        //               }
        //             )
        //             .then(response2 => {
        //               response2.data.map(item2 => {
        //                 console.log('ovo je privatni dogadjaj', item2)
        //                 this.recievingArray.push(item2)
        //               })
        //               //this.setEventsFromData()
        //             })
        //             .catch(error2 => {
        //               console.log(error2.response)
        //             })
        //           console.log(
        //             'ovo je niz RA posle svih prijatelja',
        //             this.recievingArray
        //           )
        //           this.setEventsFromData()
        //         })
        //       } else {
        //         console.log('nesto', this.recievingArray)
        //         this.setEventsFromData()
        //       }
        //     })
        //     .catch(error => {
        //       console.log(error.response)
        //     })
        // }
      })
      .catch(error1 => {
        console.log(error1.response)
      })
  }
}
</script>

<style scoped>
.MyDiv {
  margin-top: 10px;
  width: 270px;
}

.MyRow {
  margin-top: 20px;
}

.MyBackground {
  height: 100%;
  background-image: url('C:/Didi carica/DEVELOP/si.20.15.vinyl/Aplikacija/Frontend/vueapp/src/assets/calendarBackground.jpg');
  background-repeat: no-repeat;
  background-attachment: fixed;
  background-size: cover;
  display: flex;
}
#temp {
  display: flex;
}

.MyContainer {
  height: 100%;
}

h3 {
  text-align: center;
  color: white;
}
</style>