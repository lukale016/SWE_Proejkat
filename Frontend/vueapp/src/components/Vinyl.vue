<template>
  <div>

    <div id='record-info'>

     <div class="okvir">
    <h3 id="naziv" class="naziv">{{ploca.name}}</h3>
    <h5 id="izvidjac" class="izvidjac">{{ploca.author}}</h5>
    <b-card  img-alt="Card image" img-left class="mb-3" img-width="20%" img-height="305px"
    :img-src="'http://localhost:8080/Vinyl/RecordImg/'+ ploca.name">
      <b-card-text >
        <h6 class="opis">Opis:</h6>
        {{ploca.info}}
      </b-card-text>
    </b-card>
  
    <div class="rating">
    
    </div>
     </div>
 

    <div >
      <b-card-group deck>
       <b-card>
         <template v-slot:header>
      <h5>Lista pesama: </h5>
    </template>
        <!-- <b-list-group flush v-for="item in items" :key="item.id"> -->
          
    <b-list-group md="3" v-for="songInfo in this.ploca.songs" :key="songInfo.id" >
      <b-list-group-item>{{songInfo.title}}</b-list-group-item>
    </b-list-group>

     <b-card  class="komentari"> 
       <template v-slot:header>
     
      <label v-if="ploca.comments.length===0">Nema komentara</label>
      <label v-if="!ploca.comments.length==0">Comments: </label>
    </template>
       
        <b-col v-for="commentInfo in this.ploca.comments" :key="commentInfo.id" class="col"> 
            <div class="komentar"> 
                <b-avatar variant="info"  :src="'http://localhost:8080/Vinyl/UserImg/'+ commentInfo.ownerRef"></b-avatar> <b-link>{{commentInfo.ownerRef}} </b-link> 
                 {{commentInfo.content}} 
                    <b-dropdown class="drop" variant="outline-dark">
                       <template v-slot:button-content>
        <b-icon icon="gear-fill" aria-hidden="true"></b-icon>
      </template>
                      <b-dropdown-item-button  @click="prosledi(commentInfo.id)">
                        <b-icon icon="tools"></b-icon> 
                           Izmeni
                      </b-dropdown-item-button>
                      <b-dropdown-item-button  @click="izbrisi(commentInfo.id)">
                        <b-icon icon="trash-fill" aria-hidden="true"></b-icon>
                           Izbrisi       
                      </b-dropdown-item-button>
                    </b-dropdown>
                   
            </div>
        </b-col>
       <div class="trazi">
        <form action="#" @submit.prevent="komentarisi(ploca.id)">

        <input
          maxlength="2500"
          multiple
          width="50px"
          type="text"
          class="form-control"
          placeholder="Ostavite komentar..."
          v-model="Content"
        />
        </form>
        <b-button v-if="izmena==1" class="dugmence" @click="izmeni(commentForEdit.id)">Izmeni</b-button>
      </div>
         <b-button type="submit" class="btn-submit" @click="komentarisi(ploca.id)">Komentarisi</b-button>
        
    </b-card>
      <!-- <b-img small src="https://media.giphy.com/media/5ehBR5qtLEXBe/giphy.gif"></b-img> -->
      </b-card>
       <b-card >
       <b-card-text >
         <h6 class="pesma">Poslusajte pesmu sa albuma:</h6>
        <div>
  <b-embed
  controls 
    type="iframe"
    aspect="16by9"
    :src="ploca.demo"
    allowfullscreen
  ></b-embed>
</div>


 <!-- <figure>
    <figcaption>Pusti demo::</figcaption>
    <b-audio
        controls
        src="https://drive.google.com/file/d/1XOgXThVrHZy2xjPzogeNJC5hsefw83xn/view?usp=sharing">
            Your browser does not support the
            <code>audio</code> element.
    </b-audio> 
</figure> -->
      
 </b-card-text>
     
       </b-card>
    </b-card-group>
   
    </div>
  </div>
  <div class="line-title">
    <div class="line-title-left">
      <div class="line-title-spacer">
      </div>
    </div>
    <div class="line-title-center">
       <h4>POGLEDAJTE I OSTALE PLOČE</h4>
    </div>
    <div class="line-title-right">
      <div class="line-title-spacer">
      </div>
    </div>
  </div>

  <b-row v-if="this.vinyls.length" id="nesto">
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
                       <button @click="osvezi"> <router-link :to="{ name: 'Vinyl',params:{id: vinylInfo.id} }">See more info</router-link></button>
                        
                    </a>
                </span>
               
           </b-card-footer>
        </b-card>
      </b-col> 
  </b-row>

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
   props:["id"],
   components: {
    mdbFooter,
    mdbContainer
  },

    data () {
    return {
      izmena:0,
      commentForEdit: null,
      vinyls:[],
      ploca:null,
      recievingArray: [],
      comments: [],
      songs: [],
  
      BoundRecordRef:0,
      OwnerRef:"",
      Content:""

    }
    },
  // props: {
  //   record: Object
  // }, 
  methods:{
     komentarisi(id) {
       axios({
        method: 'post',
        url: 'http://localhost:8080/Vinyl/Add/Comment',
        headers: {
          'Authorization':"Bearer " + localStorage.token,
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
          Accept: 'application/json'
        },
        data: {
          BoundRecordRef:id,
          OwnerRef:localStorage.user,
          Content:this.Content
        },
      })
        .then(response => {
          console.log(response.data)
            this.comments.push(this.Content)
            this.Content=""
            window.location.reload()
        })
        .catch(error => {
          console.log(error.response)
        }) 
       
      },

    setCommentsFromData:function() {
      this.ploca.comments.map(item => {
        let commentInfo={
          boundRecordRef: item.boundRecordRef,
          content: item.content,
          id: item.id,
          owner: item.owner,
          ownerRef: item.ownerRef,
          
        }
       
        this.comments.push(commentInfo)
        this.Content=""
      })
  },

  setSongsFromData() {
      this.ploca.songs.map(item => {
        let songInfo={
          id: item.id,
          recordRef: item.recordRef,
          title: item.title
          
        }
        this.songs.push(songInfo)
      })
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
    osvezi(){
       window.location.reload()
    },

  dodajKomentar()
  {
     this.comments.push({commentInfo: this.Content})
     this.Content=""
  },

   proveraKorisnika(owner) {
      if (localStorage.user === owner) {
        return false
      } else {
        return true
      }
    },

  prosledi(id){
       this.commentForEdit = this.ploca.comments.find(x => x.id == id)
      this.Content=this.commentForEdit.content;
      this.izmena=1
  },

  izmeni(id) {
    console.log(id)
     this.commentForEdit = this.ploca.comments.find(x => x.id == id)
      this.proveraKorisnika(this.commentForEdit.owner)
       axios({
            method: 'put',
            url: 'http://localhost:8080/Vinyl/Update/Comment',
            headers: {
              'Access-Control-Allow-Origin': '*',
              'Content-Type': 'application/json',
              Accept: 'application/json',
              Authorization: 'Bearer ' + localStorage.token
            },
            data: {
             Id:id,
             BoundRecordRef: this.ploca.id,
             OwnerRef:localStorage.user,
             Content:this.Content
            }
          })
            .then(response => {
              console.log(response)
               this.Content=""
               window.location.reload()
              
            })
            .catch(error => {
              if (error.response.status == 401) {
                localStorage.token = ''
                localStorage.user = ''
                this.$router.push({ name: 'Login' })
              }
            })

      
    },

  izbrisi(id) {
      let izbrisanKomentar = this.ploca.comments.find(x => x.id == id)
      console.log(izbrisanKomentar)
       this.proveraKorisnika(this.ploca.owner)
      axios
        .delete('http://localhost:8080/Vinyl/Delete/Comment/' + izbrisanKomentar.id, {
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

   created()
     {
       axios
      .get('http://localhost:8080/Vinyl/Record/' + this.id)
      .then(response => {
        this.ploca=response.data
        console.log(response.data)
        
      })
      .catch(error => {
        console.log(error.response)
      })

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
     
}
</script>

<style>
.okvir{
  padding: 3%;
 
   background-color: rgb(204, 198, 192);
}
.opis{
  text-align: left;
  padding-left: 40px;
}
.pesma{
  padding-top: 10px;
   padding-bottom: 17px;
}
.rating{
  padding-top: 100px;
}

.lista{
  padding-left: 90px;
   background-color: rgb(204, 198, 192);
   padding-right: 900px;
}
.col{
  width: 100%;
}
.komentari{
  
  margin-top: 25px;
}
.komentar{
  width: 100%;
}

.line-title-spacer {
    position: absolute;
    top: 19px;
    left: 0;
    right: 0;
    background: #000;
    height: 1px;
}

.line-title {
  margin-top: 80px;
  margin-bottom: 50px;
    height: auto;
    overflow: auto;
    display: flex;
}

.line-title-left, .line-title-right {
    flex: 1 1 auto;
    flex-grow: 1;
    flex-shrink: 1;
    flex-basis: auto;
    height: 40px;
    position: relative;
}

.trazi{
  display: flex;
  align-items: center;
}

.form-control{
  margin-top: 13px;
}

.dugmence{
  margin-bottom: 11px;
  margin-left: 8px;
}

.drop{
   float:right;
}
</style>