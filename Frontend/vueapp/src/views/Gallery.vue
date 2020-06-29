<template>
  <div id="nesto2">
    <div id="record-info">

    <div>
      <b-tabs content-class="mt-3" fill>
        <b-tab title="Rok"  id="Rok" @click="PribaviPlocu('Rok')"><p></p></b-tab>
        <b-tab title="Pop" id="pop" @click="PribaviPlocu('Pop')"><p></p></b-tab>
        <b-tab title="Bluz" id="bluz" @click="PribaviPlocu('Bluz')"><p></p></b-tab>
        <b-tab title="Jazz"  id="Jazz"  @click="PribaviPlocu('Jazz')"><p></p></b-tab>
        <b-tab title="Hip-hop" id="hiphop" @click="PribaviPlocu('Hip-hop')"><p></p></b-tab>
        <b-tab title="Folk/Country" id="folk" @click="PribaviPlocu('Folk')"><p></p></b-tab>
        <b-tab title="Metal" id="metal" @click="PribaviPlocu('Metal')"><p></p></b-tab>
        <b-tab title="Punk" id="punk" @click="PribaviPlocu('Punk')"><p></p></b-tab>
      </b-tabs>
    </div>

    <div class="trazi">
 
        <div class="p-3">
          
         
             <input type="text" class="form-control" v-model="textForSearch" placeholder="Pretrazi ploce..">
            
            </div>
      <div class="dugme">
         <button type="submit" class="btn btn-default" @click="Submit(textForSearch)">Submit</button>
              <b-icon-search></b-icon-search>
         
      </div>

       <div class="dugme">
      <button v-if="korisnik && korisnik.isAdmin==1" class="btn btn-default" type="submit"
                  v-b-modal.modal-1>Dodaj plocu</button>
                  <b-icon-bullseye v-if="korisnik && korisnik.isAdmin==1"></b-icon-bullseye>
       </div>
       

    </div>

    <b-modal id="modal-1" title="Dodavanje ploce" hide-footer>
      <div> Naziv ploce:
        <input
          type="text"
          class="form-control"
          placeholder="Naziv ploce"
          v-model="Name"
        />
      </div>
      <div> Autor:
        <input
          type="text"
          class="form-control"
          placeholder="Autor"
          v-model="Author"
        />
      </div>
      <div> Opis ploce:
        <input
          type="text"
          class="form-control"
          placeholder="Naziv ploce"
          v-model="Info"
        />
      </div>
      <div> Zanr:
        <input
          type="text"
          class="form-control"
          placeholder="Zanr"
          v-model="Category"
        />
      </div>
      <div> Snimak:
        <input
          type="text"
          class="form-control"
          placeholder="link sa youtube-a"
          v-model="Demo"
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

        <div> <b-button  @click="dodajPlocu"> Dodaj plocu</b-button></div>
        </b-modal>


               
      <b-row v-if="this.vinyls.length" id="nesto" class="red">
      <b-col md="3" v-for="vinylInfo in this.vinyls" :key="vinylInfo.id" class="col">
        <b-card 
          border-variant="dark"
          :header="vinylInfo.name"
          header-bg-variant="dark"
          header-text-variant="white"
          align="center"
  
        >
          <b-img :src="'http://localhost:8080/Vinyl/RecordImg/'+ vinylInfo.name" height=100px width="800px" fluid alt="Fluid image"></b-img>
          <b-card-text>Autor: {{vinylInfo.author}}</b-card-text>
          <b-card-text>Format: Vinyl, LP, Album</b-card-text>
          <b-card-text>Zanr: {{vinylInfo.category}}</b-card-text>
           <b-card-footer class="links-light profile-card-footer">
              <span class="right">
                    <a class="p-2" href="#profile" >
                       
                        <router-link :to="{ name: 'Vinyl',params:{id: vinylInfo.id} }">See more info</router-link>
                        
                    </a>
                </span>
               
           </b-card-footer>
        </b-card>
      </b-col> 
    </b-row>

       

    <!-- <b-row v-if="!this.vinyls.length">
     <h1 class="nema"> Nema ploca za prikazivanje!</h1>
    </b-row> -->

    <b-row v-if="this.prom==0">
      <b-col class="text-center">
        <b-spinner class="m-5" label="Busy"></b-spinner>
      </b-col>
    </b-row>

  
 </div>   
    

<!-- Footer -->
  <mdb-footer  class="page-footer font-small pt-4 mt-4">
    
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
      ploce:[],
      textForSearch:'',
      ploca:null,
      prom:0,
      recievingArray: [],
      comments: [],
      commentsToBeDeleted: [],
      vinyls:[],
      recordForEdit : null,
      zanr:null,

       Name:null,
       Info:null,
       Category:null,
       Author:null,
       Demo:null,

       korisnik:null,
   

       image: null

    }

  },

  components: {
    mdbFooter,
    mdbContainer
  },
  
  // props:[author,displayImg,info,id,name,comments],
  methods:{
    PribaviPlocu(zanr)
  {
    if(zanr=='n')
     {
       axios
      .get('http://localhost:8080/Vinyl/Records')
      .then(response => {
        this.prom=1
        console.log(response.data)
        this.recievingArray = response.data
        this.setVinylsFromData()
      })
      .catch(error => {
        console.log(error.response)
      })
     }
     else
     {
       axios
      .get('http://localhost:8080/Vinyl/RecordsFrom/' + zanr)
      .then(response => {
        this.prom=1
        console.log(response.data)
        document.getElementById("nesto").innerHTML=""
        this.recievingArray = response.data
        this.setVinylsFromData()
      })
      .catch(error => {
        console.log(error.response)
      })
     }
      
  },

  setVinylsFromData() {
      this.recievingArray.map(item => {
        let vinylInfo={
          author: item.author,
          category: item.category,
          demo: item.demo,
          id: item.id,
          info: item.info,
          name: item.name,
          comments: item.comments
        }
       
        this.vinyls.push(vinylInfo)
      })
     
    },
  Prosledi(id){
    this.recordForEdit = this.recievingArray.find(x => x.id === id);
    console.log(this.recordForEdit);
    window.scrollTo(0,200)
     
  },

  Submit(text)
  {
     console.log('text', text)
      axios
      .get('http://localhost:8080/Vinyl/Records')
      .then(response => {
        this.ploce = response.data.filter(e => e.name.includes(text))
      
        console.log('Ploce' ,this.ploce)
      
        this.recievingArray = this.ploce
        this.PretraziPloce()
      })

      .catch(error => {
        console.log('error', error.response)
      })
  },

  PretraziPloce()
  {
   document.getElementById("nesto").innerHTML=""
      this.setVinylsFromData()
   
  },

  dodajPlocu()
  {
     axios({
        method: 'post',
        url: 'http://localhost:8080/Vinyl/Add/Record',
        headers: {
          Authorization:"Bearer " + localStorage.token,
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
          Accept: 'application/json'
        },
        data: {
          Name:this.Name,
          Info:this.Info,
          Category:this.Category,
          Author:this.Author,
          Demo:this.Demo,
          
        },
      })
        .then(response => {
          console.log(response.data)
           const fd = new FormData()
                  fd.append('Image', this.image)
            axios({
                    method: 'post',
                    url:
                      'http://localhost:8080/Vinyl/Add/RecordImg/' + this.Name,
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
                        this.Name=""
                        this.Info=""
                        this.Category=""
                        this.Author=""
                        this.Demo=""
                        
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


  created() {

    this.PribaviPlocu('n')

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

  },

  

}
</script>

<style scoped>
.profile-card-footer {
  background-color: #F7F7F7 !important;
  padding: 1rem;
}
.card.card-cascade .view {
  box-shadow: 0 3px 10px 0 rgba(0, 0, 0, 0.15), 0 3px 12px 0 rgba(0, 0, 0, 0.15);
}

.nema{
  padding-left: 30px;
  text-align: center;
}

.col{
  margin: 2px;
  margin-left: 37px;
  margin-top: 10px;
}

.p-3{
  width: 20%;
}

.trazi{
  display: flex;
  align-items: center;
}

.dugme{
  margin-bottom: 25px;
  margin-left: 5px;
}

.pages{
  margin-left: 500px;
  margin-top: 25px;
}

.dodajPlocu{
  margin-left: 35px;
}
</style>