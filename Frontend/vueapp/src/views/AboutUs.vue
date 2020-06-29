<template>
<div>
<b-row>
      <b-col md="2">
        <b-card cascade narrow>
            <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQ9KsGSg3Acfnp5Hz2fiO644AHrUibVLpUpi7H3-h7w2v3z1QRG&usqp=CAU" alt="Project" class="img-fluid"/>
          <b-card-body class="text-center">
            <h8 class="text-left">
              <strong>FOLLOW US: </strong>
            </h8>
            <b-button variant="link">Facebook Instagram</b-button>
             <h8 class="text-sm-left">
              <strong>COMPANY: </strong>
            </h8>
            <p class="font-italic">Privacy Policy Terms of Service</p>
            <h8 class="text-justify">
              <strong>CONTACT: </strong>
            </h8>
            <b-button variant="link">vinyl@mail.com</b-button>
            <div class="text-right">
            </div>
          </b-card-body>
        </b-card>
       </b-col>

       <b-col md="9">
         <div>
  <b-card
    overlay
    
    img-src="https://66.media.tumblr.com/fd28f37f39199062f644246ccff1e4bb/tumblr_n19iyhD4vR1qzcq51o1_500.gif"
    img-alt="Card Image"
    text-variant="black"
    sub-title=""
    img-height="500"
  >
   <div class="razmak"></div>
    <b-card-text>
     
        Mi smo na misiji da izgradimo najveću i sveobuhvatniju muzičku bazu podataka.
                 Zamislite stranu sa diskografijama svih umetnika, svih muzičkih pravaca koja je izgrađena iz te baze podataka.
                Naša zamisao je bila da širimo dobru muziku, da vratimo starinsku lepotu i oživimo korišćenje gramofonskih ploča.
                Na našim stranama možete naći informacije o domaćim i stranim autorima i bendovima, njihovim albumima ili neke zanimljivosti o tome.
                Sarađujemo i sa vlasnicima kafića, u kojima, ukoliko nemate gramofon, a imate neku dobru ploču, ili samo volite efekat pucketavog zvuka,
                možete da odete i uživate u dobroj muzici.
    </b-card-text>
  </b-card>
</div>
         <!-- <b-card>
          <b-card-body class="text-center">
              <b-card-text>
                Mi smo na misiji da izgradimo najveću i sveobuhvatniju muzičku bazu podataka.
                 Zamislite stranu sa diskografijama svih umetnika, svih muzičkih pravaca koja je izgrađena iz te baze podataka.
                Naša zamisao je bila da širimo dobru muziku, da vratimo starinsku lepotu i oživimo korišćenje gramofonskih ploča.
                Na našim stranama možete naći informacije o domaćim i stranim autorima i bendovima, njihovim albumima ili neke zanimljivosti o tome.
                Sarađujemo i sa vlasnicima kafića, u kojima, ukoliko nemate gramofon, a imate neku dobru ploču, ili samo volite efekat pucketavog zvuka,
                možete da odete i uživate u dobroj muzici.
      </b-card-text>
      <b-img src="https://www.espreso.rs/data/images/2016/09/27/12/85219_giphy.gif?ver=1474970641" fluid alt="Responsive image"></b-img>
          </b-card-body>
          </b-card> -->
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
 import { mdbFooter, mdbContainer} from 'mdbvue';
  export default {
  name: 'AboutUs',
  components: {
    mdbFooter,
    mdbContainer
    
   
  }
}
</script>

<style scoped>
.razmak{
    height: 20px;
}
</style>