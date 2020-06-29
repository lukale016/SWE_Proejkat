<template>
  <div class="home">
    <div class="background">
      <b-carousel
        id="carousel-fade"
        style="text-shadow: 0px 0px 2px #000"
        fade
        :interval="3500"
        img-width="1000"
        img-height="280"
      >
        <b-carousel-slide
      img-src="https://scontent.fbeg4-1.fna.fbcdn.net/v/t1.15752-9/95184092_897417737428054_839181913229885440_n.jpg?_nc_cat=104&_nc_sid=b96e70&_nc_eui2=AeH-qMi32E0F-aHGS8dsdtAH6QMDS2YLo8zpAwNLZgujzN-OfoAEQHT7-jpC9UNPR2KXpSqJgTH_krJDTLN8Xm0z&_nc_ohc=l8_sL0g2pPoAX-TD0hO&_nc_ht=scontent.fbeg4-1.fna&oh=4eeb1ccee5fd0cb524ba039f6cc6177b&oe=5F057DF7" 
    ></b-carousel-slide>
    <b-carousel-slide
      img-src="https://scontent.fbeg4-1.fna.fbcdn.net/v/t1.15752-9/95657269_229530961477681_1875148434709676032_n.jpg?_nc_cat=101&_nc_sid=b96e70&_nc_eui2=AeGYDrs51MG57-O4bF9xKA9DMzBtOo6LmrAzMG06jouasO82h6sYagHHqkKCXLqsV4w9L6LAkej54696oe08Gt6S&_nc_ohc=zigw60QjMikAX_5NeOq&_nc_ht=scontent.fbeg4-1.fna&oh=0867586cc250bd8e788c5638649b4054&oe=5F058B56" 
    ></b-carousel-slide>
    <b-carousel-slide
      img-src="https://scontent.fbeg4-1.fna.fbcdn.net/v/t1.15752-9/95241592_276851216653957_1780943811745153024_n.png?_nc_cat=103&_nc_sid=b96e70&_nc_eui2=AeHuXvB5quzuQJdJ27pIhGctXGr_Z9rolD9cav9n2uiUP2AsBjODRtXarytZZPuEzsokAg_PiXWtvF4fhn2j2mnN&_nc_ohc=KyAymFQm5uwAX8AQQv5&_nc_ht=scontent.fbeg4-1.fna&oh=b187f2add21aa7e2ba29ee032d167e46&oe=5F0678BE" 
    ></b-carousel-slide>
    <b-carousel-slide
      img-src="https://scontent.fbeg4-1.fna.fbcdn.net/v/t1.15752-9/95175428_289805055349564_1866345401020317696_n.jpg?_nc_cat=102&_nc_sid=b96e70&_nc_eui2=AeH4DbTa3siWCm65nz3lzRzpB7i--RhIgJAHuL75GEiAkI4AvaTycdC3jqldsKvswYZtVh03YJZm0AZoZydu0mFC&_nc_ohc=T4TjxOxGFiUAX_MXNvh&_nc_ht=scontent.fbeg4-1.fna&oh=493a0e998f2f8922e18bfd26a56866c2&oe=5F07B0FD"
    ></b-carousel-slide>
    <b-carousel-slide
      img-src="https://scontent.fbeg4-1.fna.fbcdn.net/v/t1.15752-9/95161138_1829276760542376_8716975726001127424_n.jpg?_nc_cat=109&_nc_sid=b96e70&_nc_eui2=AeHEgMuRK4kHhwQ3DWCoHPiLjeDKgLT65QaN4MqAtPrlBkgCYkeu3iYn336VMiQsGZotsMIk-RYGTx-nKEv52lEW&_nc_ohc=2ISXZyfeocIAX-Z2Ztf&_nc_ht=scontent.fbeg4-1.fna&oh=95b65819b862d142443592cfe647a9c4&oe=5F065C85"
    ></b-carousel-slide>
      </b-carousel>
    </div>

    <!-- CITATI -->

    <!-- <div class="background">
  <b-carousel
    id="carousel-fade"
    style="text-shadow: 0px 0px 2px #000"
    fade
    :interval="3500"
    img-width="40"
    img-height="0"
  >
     <b-carousel-slide
     caption="“The music is not in the notes, but in the silence in between.” – Mozart"
      img-src="https://scontent.fbeg5-1.fna.fbcdn.net/v/t1.15752-9/95453695_565692130724806_911239150785527808_n.png?_nc_cat=110&_nc_sid=b96e70&_nc_eui2=AeFomUyv_YTIjbBolCxB5NjFRjQJJww6rOZGNAknDDqs5oUznXYJgCeBGoDXhx4p503eT0xJz-OttsyFbLBs3ZMg&_nc_ohc=4OA0PXtoHDwAX_QUXzA&_nc_ht=scontent.fbeg5-1.fna&oh=efc68decac1202d0e70c8852164f0634&oe=5ED28724" 
    ></b-carousel-slide>
    <b-carousel-slide
      img-src="https://scontent.fbeg5-1.fna.fbcdn.net/v/t1.15752-9/95407981_258436285281198_4446737614740914176_n.png?_nc_cat=102&_nc_sid=b96e70&_nc_eui2=AeFE65H9Y1xHUUcBFkUs9HNJ2GfGHgUufpfYZ8YeBS5-l5anvJwv51h5ZQLcb4kpBp_s2rBoC9cX6LTMFyJSwd7S&_nc_ohc=Fg64IFqPyK0AX-cyu7D&_nc_ht=scontent.fbeg5-1.fna&oh=17f99a0ce674ec60ce2cb69e0084dd5f&oe=5ECF9EDF" 
    ></b-carousel-slide>
    <b-carousel-slide
      img-src="https://scontent.fbeg5-1.fna.fbcdn.net/v/t1.15752-9/95185258_992473404503096_3863552537084821504_n.png?_nc_cat=110&_nc_sid=b96e70&_nc_eui2=AeHIsqA9nK4Az5p1BCIofpVf8ajCAkgq_9LxqMICSCr_0kfkmVmdTqrPBJXi7v4myAWruvFZRl984oUcGyXVM0Ec&_nc_ohc=x27VuXdCbxcAX-iP0PT&_nc_ht=scontent.fbeg5-1.fna&oh=59acaaa7bd86b8be49ceabec0db766ed&oe=5ED13F7B" 
    ></b-carousel-slide>
  </b-carousel>
</div> -->

    <!-- NOVOSTI -->
    <div class="razmak">
      <h1 class="naslov"><i>MAGAZIN</i></h1>
    </div>


    <b-modal id="modal-3" title="Dodavanje novosti" hide-footer>
      <div> Veliki naslov:
        <input
          type="text"
          class="form-control"
          placeholder="Veliki naslov"
          v-model="TitleLarge"
        />
      </div>
      <div> Mali naslov:
        <input
          type="text"
          class="form-control"
          placeholder="Autor"
          v-model="TitleSmall"
        />
      </div>
      <div> Veliki tekst za novost:
        <input
          type="text"
          class="form-control"
          placeholder="Naziv ploce"
          v-model="TextLarge"
        />
      </div>
      <div> Najava novosti:
        <input
          type="text"
          class="form-control"
          placeholder="Zanr"
          v-model="TextSmall"
        />
      </div>
     
        <div class="form-control1">
         <b-form-group
          label="Mala slika:"
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

           
        <div class="form-control1">
         <b-form-group
          label="Velika slika:"
          label-for="file-small"
          label-cols-sm="3"
          label-size="sm"
        >
          <b-form-file
            id="file-small"
            size="sm"
            name="Image"
            v-model="image2"
          ></b-form-file>
        </b-form-group>
           </div>

        <div> <b-button  @click="dodajNovost"> Dodaj novost</b-button></div>
        </b-modal>
        



    <div>
      <b-card-group columns>
        <b-card
           :title="news[0].titleLarge"
           :img-src=" 'http://localhost:8080/Vinyl/NewsImgLarge/' + news[0].titleSmall "
          img-alt="Image"
          img-top
        >
          <b-card-text>
            {{news[0].textSmall}}
          </b-card-text>

          <div>
            <b-button v-b-modal.modal-1>Saznaj više</b-button>
            <div class="dugme">
      <button @click="izbrisiNovost(news[0].id)" v-if="korisnik && korisnik.isAdmin==1" class="btn btn-default" type="submit"
                  v-b-modal.modal-3>Zameni novost</button>
                  <b-icon-newspaper v-if="korisnik && korisnik.isAdmin==1"></b-icon-newspaper>
       </div>
            <b-modal
              id="modal-1"
              :title="news[0].titleSmall"
              
            >
              <p
                class="col-xs-10 col-xs-offset-1 col-sm-6 col-sm-offset-3 col-md-4 col-md-offset-4 "
              ></p>
              <b-col>
                <b-img
                  thumbnail
                  fluid
                  :src=" 'http://localhost:8080/Vinyl/NewsImgSmall/' + news[0].titleSmall "
                  alt="Image 3"
                ></b-img>
              </b-col>
               {{news[0].textLarge}}
            </b-modal>
          </div>
        </b-card>

        <b-card header="Quote">
          <blockquote class="blockquote mb-0">
            <p>
             „Muzika je jezik koji se ne kazuje rečima. Ona govori kroz emocije, i ako je u kostima – onda je u kostima.“ 
            </p>
            <footer class="blockquote-footer">
              Keith Richards
            </footer>
          </blockquote>
        </b-card>

        <b-card
          :title="news[1].titleLarge"
         :img-src=" 'http://localhost:8080/Vinyl/NewsImgLarge/' + news[1].titleSmall "
          img-alt="Image"
          img-top
        >
          <b-card-text>
             {{news[1].textSmall}}
          </b-card-text>
          <b-card-text class="small text-muted"
            >Last updated 3 mins ago</b-card-text
          >

          <div>
            <b-button v-b-modal.modal>Saznaj više</b-button>

           
            <div class="dugme">
      <button @click="izbrisiNovost(news[1].id)" v-if="korisnik && korisnik.isAdmin==1" class="btn btn-default" type="submit"
                  v-b-modal.modal-3>Zameni novost</button>
                  <b-icon-newspaper v-if="korisnik && korisnik.isAdmin==1"></b-icon-newspaper>
       </div>

            <b-modal
              id="modal"
              :title="news[1].titleSmall"
            >
              <p
                class="col-xs-10 col-xs-offset-1 col-sm-6 col-sm-offset-3 col-md-4 col-md-offset-4 "
              ></p>
            {{news[1].textLarge}}
        
              <b-container fluid class="p-4 bg-dark" type="light">
                    <b-img
                      thumbnail
                      fluid
                      :src=" 'http://localhost:8080/Vinyl/NewsImgSmall/' + news[1].titleSmall"
                      alt="Image 1"
                    ></b-img>
                  
              </b-container>
            </b-modal>
          </div>
        </b-card>

        <b-card bg-variant="dark" text-variant="white">
          <blockquote class="card-blockquote">
            <p>
              „Ja mislim da je muzuka, sama po sebi, isceljujuća. Ona je
              eksplozivna ekspresija čovečanstva. Ona je nešto čime smo svi
              dotaknuti. Bez obzira na kulturu iz koje potičemo, svi vole
              muziku.“
            </p>
            <footer>
              <small><cite title="Source Title">– Billy Joel</cite></small>
            </footer>
          </blockquote>
        </b-card>

        <b-card>
         
          <b-card-text>
            „Muzika se ne nalazi u notama, već u tišini između njih.“ 
          </b-card-text>
          <b-card-text class="small text-muted"
            >– W. A. Mozart</b-card-text
          >
        </b-card>

        <b-card
           :title="news[2].titleLarge"
          :img-src=" 'http://localhost:8080/Vinyl/NewsImgLarge/' + news[2].titleSmall"
          img-alt="Image"
          img-top
        >
          <b-card-text>
             {{news[2].textSmall}}
          </b-card-text>

          <div>
            <b-button v-b-modal.modal->Saznaj više</b-button>

            
            <div class="dugme">
      <button @click="izbrisiNovost(news[2].id)"  v-if="korisnik && korisnik.isAdmin==1" class="btn btn-default" type="submit"
                  v-b-modal.modal-3>Zameni novost</button>
                  <b-icon-newspaper v-if="korisnik && korisnik.isAdmin==1"></b-icon-newspaper>
       </div>
            <b-modal
              id="modal-"
              :title="news[2].titleSmall"
            >
              <p
                class="col-xs-10 col-xs-offset-1 col-sm-6 col-sm-offset-3 col-md-4 col-md-offset-4 "
              ></p>
              <b-col>
                <b-img
                  thumbnail
                  fluid
                  :src=" 'http://localhost:8080/Vinyl/NewsImgSmall/' + news[2].titleSmall "
                  alt="Image 3"
                ></b-img>
              </b-col>
               {{news[2].textLarge}}
            </b-modal>
          </div>
        </b-card>
      </b-card-group>
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
   data () {
    return {
     news:[],

      textLarge: null,
      textSmall: null,
      titleLarge: null,
      titleSmall: null,
      korisnik:null,

      image: null,
      image2: null
    }
   },
   components: {
    mdbFooter,
    mdbContainer
  },

  name: 'Home',

  methods: {

    setNewsFromData() {
      this.recievingArray.map(item => {
        let newInfo={
          id: item.id,
          imageLarge: item.imageLarge,
          imageSmall: item.imageSmall,
          textLarge: item.textLarge,
          textSmall: item.textSmall,
          titleLarge: item.titleLarge,
          titleSmall: item.titleSmall
        }
       
        this.news.push(newInfo)
      })
     
    },

  izbrisiNovost(id)
  {
    let izbrisanaNovost = this.news.find(x => x.id == id)
      console.log(izbrisanaNovost)
      axios
        .delete('http://localhost:8080/Vinyl/Delete/News/' + izbrisanaNovost.id, {
          headers: { Authorization: 'Bearer ' + localStorage.token }
        })
        .then(response => {
          console.log(response.data)
          
        })
        .catch(error => {
          if (error.response.status == 401) {
            localStorage.token = ''
            localStorage.user = ''
            this.$router.push({ name: 'Login' })
          }
        })
       
  },

  dodajNovost()
  {
    
     axios({
        method: 'post',
        url: 'http://localhost:8080/Vinyl/Add/News',
        headers: {
          Authorization:"Bearer " + localStorage.token,
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
          Accept: 'application/json'
        },
        data: {
          TextLarge: this.TextLarge,
          TextSmall: this.TextSmall,
          TitleLarge: this.TitleLarge,
          TitleSmall: this.TitleSmall
        },
      })
        .then(response => {
          console.log(response.data)
              const fd = new FormData()
              fd.append('Image', this.image)

              const fd2 = new FormData()
              fd2.append('Image', this.image2)
              
            axios({
                    method: 'post',
                    url:
                      'http://localhost:8080/Vinyl/Add/NewsImgSmall/' + this.TitleSmall,
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
                    })
                    .catch(error => {
                      console.log(error.response2)
                    })

               axios({
                    method: 'post',
                    url:
                      'http://localhost:8080/Vinyl/Add/NewsImgLarge/' + this.TitleSmall,
                    headers: {
                      'Access-Control-Allow-Origin': '*',
                      'Content-Type': 'multipart/form-data',
                      Accept: 'application/json',
                      Authorization: 'Bearer ' + localStorage.token
                    },
                    data: fd2
                  })
                    .then(response2 => {
                      console.log(response2.data)
                         this.TextLarge="",
                         this.TextSmall="",
                         this.TitleLarge="",
                         this.TitleSmall=""
                         window.location.reload()
                    })
                    .catch(error => {
                      console.log(error.response2)
                    })
             
        })
        
        .catch(error => {
          console.log(error.response)
        }) 
         
  }

  },

  created()
      {
        axios
        .get('http://localhost:8080/Vinyl/News')
        .then(response => {
          console.log(response.data)
          this.recievingArray = response.data
          this.setNewsFromData()
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

<style>
.naslov {
  font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande',
    'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
}

.razmak {
  height: 220px;
  margin-left: 540px;
  padding-top: 135px;
}

.dugme{
  margin-bottom: 15px;
  float:right;
}
</style>
