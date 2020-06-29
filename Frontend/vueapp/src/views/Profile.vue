<template>
  <div>
    <b-card
      border-variant="dark"
      :header="recievingArray.username"
      header-bg-variant="dark"
      header-text-variant="white"
      align="center"
    >
      <b-img
        :src="'http://localhost:8080/Vinyl/UserImg/' + recievingArray.username"
        align="left"
        height="40px"
        width="270px"
        fluid
        alt="Fluid image"
      >
      </b-img>

     <b-dropdown class="brisanje" right text=""  > 
        <b-dropdown-item href="#">Zahtevi <b-avatar  v-b-modal.modal-11 badge> </b-avatar> </b-dropdown-item>
        <b-dropdown-item href="#">Prijatelji <b-avatar icon="people-fill" v-b-modal.modal-111> </b-avatar> </b-dropdown-item>
        <b-dropdown-item-button  @click="izbrisiProfil(recievingArray.username)" >Izbrisi profil 
         <b-icon icon="trash-fill" aria-hidden="true"></b-icon>
       </b-dropdown-item-button>
       
     </b-dropdown>

      <div>
      </div>

      <b-card-text align="center">Ime: {{ recievingArray.name }} </b-card-text>
      <b-card-text align="center"
        >Prezime: {{ recievingArray.surname }}</b-card-text
      >
      <b-card-text align="center">Bio: {{ recievingArray.bio }}</b-card-text>
      <b-card-text align="center">Grad: {{ recievingArray.city }}</b-card-text>
      <b-card-text></b-card-text>

      <div class="input-group md-form form-sm form-1 pl-0">
        <div class="input-group-prepend">
          <span class="input-group-text cyan lighten-2" id="basic-text1">
            <mdbIcon icon="search" />
          </span>
        </div>

        <b-form-input
          class="form-control my-0 py-1"
          type="text"
          v-model="textForSearch"
          placeholder="Pronadji prijatelje"
          aria-label="Search"
        ></b-form-input>
        <button v-b-modal.modal-1 @click="ispisiKorisnike(textForSearch)">
          Pretraži
        </button>

        <b-modal id="modal-1" hide-header hide-footer>
          <b-col md="5" class="col-md-10">
            <b-list-group style="max-width: 600px;">
              <div class="row">
                <b-list-group-item
                  style="width:100%"
                  v-for="usersInfo in this.korisnici"
                  :key="usersInfo.username"
                  class="d-flex align-items-center"
                >
                  <b-card-text style="width:100%">
                    <b-avatar
                      variant="info"
                      :src="
                        'http://localhost:8080/Vinyl/UserImg/' +
                          usersInfo.username
                      "
                      class="mr-3"
                    ></b-avatar>
                    {{ usersInfo.username }}

                    <b-button size="sm" @click="izbrisiProfil(usersInfo.username)" v-if="korisnik && korisnik.isAdmin==1" class="dugme" type="danger" variant="outline-danger">Izbrisi korisnika</b-button>


                    <b-button
                      size="sm"
                      pill
                      variant="outline-primary"
                      v-if="usersInfo.status === 0"
                      v-b-modal.modal-2
                      @click="Dodajprijatelja(usersInfo.username)"
                      class="dugme"
                    >
                      Dodaj prijatelja
                    </b-button>
                    <b-button
                      class="dugme"
                      disabled
                      size="sm"
                      v-if="usersInfo.status === 1"
                      >Zahtev je poslat</b-button
                    >

                    <b-button
                      class="dugme"
                      disabled
                      size="sm"
                      v-if="usersInfo.status === 2"
                      >Prijatelji</b-button
                    >

                     
                  </b-card-text>
                </b-list-group-item>
              </div>
            </b-list-group>
          </b-col>
        </b-modal>

        <b-modal id="modal-11" hide-header hide-footer>
          <b-col md="5" class="col-md-10">
            <b-list-group style="max-width: 600px;">
              <div class="row">
                <b-list-group-item
                  v-for="usersInfo in this.nizCekanje"
                  :key="usersInfo.userSentRef"
                  class="d-flex align-items-center"
                >
                  <b-card-text>
                    <b-avatar
                      variant="info"
                      :src="
                        'http://localhost:8080/Vinyl/UserImg/' +
                          usersInfo.userSentRef
                      "
                      class="mr-3"
                    ></b-avatar>
                    {{ usersInfo.userSentRef }}
                    <div>
                      <b-button
                        pill
                        variant="outline-primary"
                        size="sm"
                        v-if="prihvacen === 0"
                        v-b-modal.modal-2
                        @click="PrihvatiZahtev(usersInfo)"
                      >
                        Prihvati zahtev
                      </b-button>

                      <b-button
                        pill
                        variant="outline-danger"
                        size="sm"
                        v-if="odbijen === 0"
                        @click="ObrisiZahtev(usersInfo)"
                      >
                        Odbi zahtev
                      </b-button>
                    </div>
                    <b-button pill disabled size="sm" v-if="prihvacen === 1"
                      >Zahtev je prihvaćen</b-button
                    >

                    <b-button pill disabled size="sm" v-if="odbijen === 1"
                      >Zahtev je odbijen</b-button
                    >
                  </b-card-text>
                </b-list-group-item>
              </div>
            </b-list-group>
          </b-col>
        </b-modal>

        <b-modal id="modal-111" hide-header hide-footer>
          <b-col md="5" class="col-md-10">
            <b-list-group style="max-width: 600px;">
              <div class="row">
                <b-list-group-item
                  style="width:100%"
                  v-for="usersInfo in this.nizPrijatelja"
                  :key="usersInfo.user1Ref"
                  class="d-flex align-items-center"
                >
                  <b-card-text style="width:100%">
                    <b-avatar
                      variant="info"
                      :src="
                        'http://localhost:8080/Vinyl/UserImg/' +
                          usersInfo.user2Ref
                      "
                      class="mr-3"
                    ></b-avatar>
                    {{ usersInfo.user2Ref }}

                    <b-button
                      class="dugme"
                      pill
                      variant="outline-danger"
                      size="sm"
                      v-if="odbijen === 0"
                      @click="UkloniPrijatelja(usersInfo)"
                    >
                      Ukloni prijatelja
                    </b-button>

                    <b-button pill disabled size="sm" v-if="odbijen === 1"
                      >Prijatelj je obrisan</b-button
                    >
                  </b-card-text>
                </b-list-group-item>
              </div>
            </b-list-group>
          </b-col>
        </b-modal>
      </div>

      <b-card-footer class="links-light profile-card-footer">
        <span class="right">
          <a class="p-2" href="#profile"> </a>
        </span>
      </b-card-footer>
    </b-card>

    <!-- Footer -->
  <mdb-footer color="stylish-color-dark" class="page-footer font-small pt-4 mt-4">
    
    <hr />
    <div class="text-center">
      
    </div>
    <div class="footer-copyright text-center py-3">
      <mdb-container fluid>
        &copy; 2020 Copyright: <a href=""> VLEteam </a>
      </mdb-container>
    </div>
  </mdb-footer>
  <!-- Footer -->


  </div>
</template>

<script>
import axios from 'axios'
import { mdbFooter, mdbContainer} from 'mdbvue';
export default {
  components: {
    
    mdbFooter,
    mdbContainer
  },
  data() {
    return {
      recievingArray: [],
      items: [],
      korisnici: [],
      textForSearch: '',
      dodato: 0,
      nizZahteva: [],
      nizPrijatelja: [],
      nizCekanje: [],
      prihvacen: 0,
      odbijen: 0,

      korisnik:null
    }
  },
  methods: {
    btnVal(userVal) {
      if (userVal === 0) return 'Zahtev poslat'
      return 'Prijatelji'
    },
    statusCheck()
    {

    },

    izbrisiProfil(id)
    {
      let izbrisanKorisnik = this.korisnici.find(x => x.username == id)
      console.log(izbrisanKorisnik)
      axios
        .delete('http://localhost:8080/Vinyl/Delete/User/' + izbrisanKorisnik.username, {
          headers: { Authorization: 'Bearer ' + localStorage.token }
        })
        .then(response => {
          console.log(response.data)
          localStorage.token = ''
           localStorage.user = ''
         this.$router.push({ name: 'Register' })

        })
        .catch(error => {
          if (error.response.status == 401) {
            localStorage.token = ''
            localStorage.user = ''
            this.$router.push({ name: 'Login' })
          }else  window.location.reload()
        })
    },


    ispisiKorisnike(text) {
      //   console.log('text', text)
      axios({
        method: 'get',
        url: 'http://localhost:8080/Vinyl/Users',
        headers: {
          Authorization: 'Bearer ' + localStorage.token,
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
          Accept: 'application/json'
        }
      })
        .then(response => {
          this.korisnici = response.data.filter(e => e.username.includes(text))
          console.log(localStorage.user)
          let usernames = this.korisnici.map(e => e.username)
          this.korisnici.splice(usernames.indexOf(localStorage.user), 1)

          this.korisnici = this.korisnici.map(e =>{
            const pending = this.nizZahteva.map(el => el.userReceavedRef)
            const frens = this.nizPrijatelja.map(el => el.user2Ref)
            console.log("pend", pending)
            console.log("frens", frens)
            let obj = {...e, status: pending.includes(e.username) ? 1 :
              frens.includes(e.username) ? 2 : 0
            }
            console.log('OBJ',obj)
            return obj
            })
          console.log('Korisnici', this.korisnici)
        })
        .catch(error => {
          console.log('error', error.response)
        })
    },
    PrihvatiZahtev(pendingRequest) {
      this.prihvacen = 1
      axios({
        method: 'post',
        url: 'http://localhost:8080/Vinyl/Add/Friend',
        headers: {
          Authorization: 'Bearer ' + localStorage.token,
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
          Accept: 'application/json'
        },
        data: {
          User1Ref: pendingRequest.userSentRef,
          User2Ref: pendingRequest.userReceavedRef
        }
      })
        .then(resp => {
          console.log(resp)
          window.location.reload()
        })
        .catch(err => {
          console.log(err.response)
        })
    },
    Dodajprijatelja(ime) {
      ;(this.dodato = 1),
        axios({
          method: 'post',
          url: 'http://localhost:8080/Vinyl/Add/PendingRequest',
          headers: {
            Authorization: 'Bearer ' + localStorage.token,
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json',
            Accept: 'application/json'
          },
          data: {
            UserSentRef: this.recievingArray.username,
            UserReceavedRef: ime
          }
        })
          .then(response => {
            console.log(response)
            window.location.reload()
          })
          .catch(() => {
            console.log(localStorage.username)
          })
    },
    ObrisiZahtev(pendingRequest) {
      ;(this.odbijen = 1),
        axios
          .delete(
            'http://localhost:8080/Vinyl/Delete/PendingRequest/' +
              pendingRequest.userSentRef +
              '/' +
              pendingRequest.userReceavedRef,
            {
              headers: { Authorization: 'Bearer ' + localStorage.token }
            }
          )
          .then(response => {
            console.log(response.data)
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

    UkloniPrijatelja(friend) {
      axios
        .delete(
          'http://localhost:8080/Vinyl/Delete/Friend/' +
            friend.user1Ref +
            '/' +
            friend.user2Ref,
          {
            headers: { Authorization: 'Bearer ' + localStorage.token }
          }
        )
        .then(response => {
          console.log(response.data)
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
  created() {
    axios({
      method: 'get',
      url: 'http://localhost:8080/Vinyl/LogedUser',
      headers: {
        Authorization: 'Bearer ' + localStorage.token,
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
        Accept: 'application/json'
      }
    })
      .then(response => {
        console.log('res', response)
        this.korisnik=response.data
        this.recievingArray = response.data
        this.nizZahteva = response.data.sentRequests
        this.nizPrijatelja = response.data.friends
        this.nizCekanje = response.data.pendingRequests
        console.log("niz", this)
        //this.setUsersFromData()
      })
      .catch(error => {
        console.log(error.response)
      })

    axios({
      method: 'get',
      url: 'http://localhost:8080/Vinyl/Users',
      headers: {
        Authorization: 'Bearer ' + localStorage.token,
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
        Accept: 'application/json'
      }
    })
      .then(response => {
        this.korisnici = response.data
        console.log('Korisnici', this.korisnici)
      })
      .catch(error => {
        console.log(error.response)
      })
  }
}
</script>

<style scoped>
.razmak {
  height: 100px;
}

.dugme {
  float: right;
  margin-right: 5px;
}

.neki {
  margin-left: 820px;
}

.brisanje{
  float:right;
}


</style>
