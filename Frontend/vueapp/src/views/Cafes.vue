<template>
  <div>
    <div id="cafe-info">
      <!-- <b-row v-if="this.caffeForEdit">
            <b-col>
               <Vinyl :caffe="this.caffeForEdit"></Vinyl>
            </b-col>
        </b-row> -->

        
       <div class="dugme">
      <button v-if="korisnik && korisnik.isOwner==1" class="btn btn-default" type="submit"
                  v-b-modal.modal-1>Dodaj kafic</button>
                  <b-icon-building v-if="korisnik && korisnik.isOwner==1" class="ikonica"></b-icon-building>
                  
       </div>
       

    <b-modal id="modal-1" title="Dodavanje kafica" hide-footer>
      <div> Naziv kafica:
        <input
          type="text"
          class="form-control"
          placeholder="Naziv ploce"
          v-model="Name"
        />
      </div>
      <div> Radno vreme:
        <input
          type="text"
          class="form-control"
          placeholder="Autor"
          v-model="Info"
        />
      </div>
      <div> Adresa:
        <input
          type="text"
          class="form-control"
          placeholder="Naziv ploce"
          v-model="Address"
        />
      </div>
      <div> Grad:
        <input
          type="text"
          class="form-control"
          placeholder="Zanr"
          v-model="City"
        />
      </div>
       <div> Geografska duzina:
        <input
          type="text"
          class="form-control"
          placeholder="Zanr"
          v-model="Long"
        />
      </div>
       <div> Geografska sirina:
        <input
          type="text"
          class="form-control"
          placeholder="Zanr"
          v-model="Lat"
        />
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
        </b-form-group>
           </div>

        <div> <b-button  @click="dodajKafic"> Dodaj kafic</b-button></div>
        </b-modal>




      <b-row v-if="this.caffes.length">
        <b-row
          v-for="caffeInfo in this.caffes"
          :key="caffeInfo.id"
          class="MyRow"
        >
          <b-col md="11" class="kol">
            <b-card
              border-variant="dark"
              :header="caffeInfo.name"
              header-bg-variant="light"
              header-text-variant="dark"
              align="center"
            >
              <b-img
                :src="'http://localhost:8080/Vinyl/CaffeImg/' + caffeInfo.name"
                height="20px"
                width="500px"
                fluid
                alt="Fluid image"
              ></b-img>
              <b-card-text
                ><h2>{{ caffeInfo.name }}</h2></b-card-text
              >
              <b-card-text>Radno vreme: {{ caffeInfo.info }}</b-card-text>
              <b-card-text>Adresa: {{ caffeInfo.address }}</b-card-text>
              <b-card-text>Grad: {{ caffeInfo.city }}</b-card-text>
              <b-card-footer class="links-light profile-card-footer">
                <span class="right">
                  <a class="p-2" href="#profile">
                    

                    <router-link :to="{ name: 'Cafe',params:{id: caffeInfo.id} }">See more info</router-link>
                  </a>
                  <b-button v-b-modal.modal-sm class="dugme" variant="outline-danger" v-if="korisnik && korisnik.isAdmin==1 || korisnik &&  korisnik.isOwner==1" >Obrisi Kafic</b-button>
                  <b-modal id="modal-sm" hide-footer  size="sm" title="Da li ste sigurni da zelite da obrisete kafic?">
  <b-button variant="danger" @click="obrisiKafic(caffeInfo.id)">Obrisi
     <b-icon icon="trash-fill" aria-hidden="true"></b-icon>
     </b-button></b-modal>
                </span>
              </b-card-footer>
            </b-card>
          </b-col>
        </b-row>
      </b-row>



      <!-- <b-row v-if="!this.vinyls.length">
     <h1 class="nema"> Nema ploca za prikazivanje!</h1>
    </b-row> -->

      <b-row v-if="this.prom == 0">
        <b-col class="text-center">
          <b-spinner class="m-5" label="Busy"></b-spinner>
        </b-col>
      </b-row>

      
    </div>

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
      prom: 0,
      recievingArray: [],
      caffes: [],
      caffeForEdit: null,

      Name:null,
      City:null,
      Address:null,
      Info:null, 
      OwnerRef:null,
      Long:null,
      Lat:null,

      image:null,

      korisnik:null
    }
  },
  methods: {
    setCaffesFromData() {
      this.recievingArray.map(item => {
        let caffeInfo = {
          address: item.address,
          backgroundImg: item.backgroundImg,
          city: item.city,
          id: item.id,
          info: item.info,
          lat: item.lat,
          long: item.long,
          name: item.name,
          organizedEvents: item.organizedEvents,
          owner: item.owner,
          ownerRef: item.ownerRef
        }

        this.caffes.push(caffeInfo)
      })
    },
    Prosledi(id) {
      this.caffeForEdit = this.recievingArray.find(x => x.id === id)
      console.log(this.caffeForEdit)
    },


    dodajKafic()
  {
     axios({
        method: 'post',
        url: 'http://localhost:8080/Vinyl/Add/Caffe',
        headers: {
          Authorization:"Bearer " + localStorage.token,
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
          Accept: 'application/json'
        },
        data: {
          Name:this.Name,
          City:this.City,
          Address:this.Address,
          Info:this.Info,
          OwnerRef:this.korisnik.username,
          Long:this.Long,
          Lat:this.Lat
        },
      })
        .then(response => {
          console.log(response.data)
           const fd = new FormData()
                  fd.append('Image', this.image)
            axios({
                    method: 'post',
                    url:
                      'http://localhost:8080/Vinyl/Add/CaffeImg/' + this.Name,
                    headers: {
                      'Access-Control-Allow-Origin': '*',
                      'Content-Type': 'multipart/form-data',
                      Accept: 'application/json',
                      Authorization: 'Bearer ' + localStorage.token
                    },
                    data: fd
                  })
                    .then(response2 => {
                      console.log(response2.data)
                          this.Name="",
                          this.City="",
                          this.Address="",
                          this.Info="",
                          this.OwnerRef="",
                          this.Long="",
                          this.Lat=""
                        
                    })
                    .catch(error => {
                      console.log(error.response2)
                    })
              })
           
        
        .catch(error => {
          console.log(error.response)
        }) 
  },

proveraKorisnika(owner) {
      if (localStorage.user === owner) {
        return false
      } else {
        return true
      }
    },

   obrisiKafic(id) {
      let izbrisanKafic = this.caffes.find(x => x.id == id)
      console.log(izbrisanKafic)
      
      axios
        .delete('http://localhost:8080/Vinyl/Delete/Caffe/' + izbrisanKafic.id, {
          headers: { Authorization: 'Bearer ' + localStorage.token }
        })
        .then(response => {
          console.log(response.data)
          window.location.reload()
        })
        .catch(error => {
          if (error.response.status == 401) {
            localStorage.token = ''
            localStorage.user = ''
            this.$router.push({ name: 'Login' })
          }else  window.location.reload()
        })
       
    },
  },
  created() {
    axios
      .get('http://localhost:8080/Vinyl/Caffes')
      .then(response => {
        this.prom = 1
        console.log(response.data)
        this.recievingArray = response.data
        this.setCaffesFromData()
      })
      .catch(error => {
        console.log(error.response)
      })

       axios
        .get('http://localhost:8080/Vinyl/LogedUser', 
        {headers:
        {
          Authorization:"Bearer " + localStorage.token}})
        .then(response => {
          console.log(response.data)
          this.korisnik=response.data
         
         
        })
        .catch(error => {
          console.log(error.response)
        })

  }
}
</script>

<style scoped>
.profile-card-footer {
  background-color: #f7f7f7 !important;
  padding: 1rem;
}
.card.card-cascade .view {
  box-shadow: 0 3px 10px 0 rgba(0, 0, 0, 0.15), 0 3px 12px 0 rgba(0, 0, 0, 0.15);
}
.nema {
  padding-left: 30px;
  text-align: center;
}

.myRow {
  margin: 5px;
}

.kol {
  margin: 5px;
  margin-left: 40px;
}

.pages{
  margin-left: 510px;
  margin-top: 25px;
}

.dugme{
  float:right;
}


.ikonica{
  margin-right: 30px;
}
</style>
