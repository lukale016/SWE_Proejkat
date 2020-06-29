<template>
    <div>
      <link href='https://api.mapbox.com/mapbox-gl-js/v1.11.0/mapbox-gl.css' rel='stylesheet' />
        <b-card   class="kartica"
           :img-src="'http://localhost:8080/Vinyl/CaffeImg/'+ kafic.name "  fluid alt="Fluid image"
            border-variant="dark"
            img-width=50%
            img-height="550px"
            img-left
            header-bg-variant="dark"
            header-text-variant="white"
            align="center"
            >
             
             <b-card-text >
                 <h2 class="opis">{{kafic.name}}</h2> 
             </b-card-text>
             <b-card-text>
                 <h6 class="opis">ADRESA:</h6> 
             </b-card-text>
             <b-card-text><h6 class="opis">{{kafic.address}}</h6></b-card-text> 
             <b-card-text >
                 <h6 class="opis">GRAD:</h6> 
             </b-card-text>
             <b-card-text><h6 class="opis">{{kafic.city}}</h6></b-card-text>
             <b-card-text >
                 <h6 class="opis">RADNO VREME:</h6> 
             </b-card-text>
             <b-card-text><h6 class="opis">{{kafic.info}}</h6></b-card-text>

 <b-card >
      <MglMap class="map-view" :accessToken="accessToken" :mapStyle="mapStyle" :zoom=15 :center="coordinates" >
        <MglMarker :coordinates.sync="markerCoordinates" color='red'/> </MglMap>
             </b-card>
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
// import mapboxgl from 'mapbox-gl'
import { MglMap } from "vue-mapbox";
import { MglMarker } from "vue-mapbox";
import { mdbFooter, mdbContainer} from 'mdbvue';

// mapboxgl.accessToken = 'pk.eyJ1IjoiZGlkaWRydW0iLCJhIjoiY2tidGw5MjBxMGI0NzJ4bzB2dXoyM3RsYyJ9.jGGFyUXgE4yJDJ14HFZgcA';
// var map = new mapboxgl.Map({
// container: 'map',
// style: 'mapbox://styles/mapbox/streets-v11', // stylesheet location
// center: [-74.5, 40], // starting position [lng, lat]
// zoom: 9 // starting zoom
// });


export default {
    props:["id"],

  // computed: {
  //   mapConfig () {
  //     return {
  //       ...mapSettings,
  //       center: { lat: 0, lng: 0 }
  //     }
  //   }
  // },
  
    data()
    {
     return{
        kafic: null,
        long:0,
        lat:0,
        accessToken: 'pk.eyJ1IjoiZGlkaWRydW0iLCJhIjoiY2tidGw5MjBxMGI0NzJ4bzB2dXoyM3RsYyJ9.jGGFyUXgE4yJDJ14HFZgcA',
        mapStyle: 'mapbox://styles/dididrum/ckbtr0x91158n1iqqay0thrnp',
        markerCoordinates:null,
        coordinates:null
      }
    },
    components:{
      MglMap, 
      MglMarker,
      mdbFooter,
      mdbContainer
    },
    created()
     {
       axios
      .get('http://localhost:8080/Vinyl/Caffe/' + this.id)
      .then(response => {
        this.kafic=response.data
        this.long=this.kafic.long
        this.lat=this.kafic.lat
        this.markerCoordinates=[this.long, this.lat]
         this.coordinates=[this.long, this.lat]
        console.log(response.data)
        this.recievingArray = response.data
        
      })
      .catch(error => {
        console.log(error.response)
      })

     

  // mapboxgl.accessToken = 'pk.eyJ1IjoiZGlkaWRydW0iLCJhIjoiY2tidGw5MjBxMGI0NzJ4bzB2dXoyM3RsYyJ9.jGGFyUXgE4yJDJ14HFZgcA';
  //     new mapboxgl.Map({
  //     container: 'map-container',
  //     style: 'mapbox://styles/mapbox/streets-v11', // stylesheet location
  //     center: [-74.5, 40], // starting position [lng, lat]
  //     zoom: 9 // starting zoom
  //     });
    },

    

}
</script>

<style>
.kartica{
  margin: 2px;
  margin-top: 20px;
  margin-left: 135px;
  width: 70%;
}

#map {
	width: 100%;
	height: 500px;
}

.map-view {
  width: 100%;
  height: 400px;
}
</style>