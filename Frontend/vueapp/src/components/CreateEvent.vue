<template>
  <div>
    <b-button
      :hidden="!this.dogadjaj.empty"
      v-b-toggle.collapse-1
      variant="secondary"
      class="MyCollapseButton"
    >Kreiraj dogadjaj</b-button>
    <b-collapse id="collapse-1" class="mt-2" :visible="!this.dogadjaj.empty">
      <div class="MyPadding">
        <label :class="textColor(dogadjaj.empty)">Unesite naziv događaja</label>
        <b-form-input v-model="textTitle"></b-form-input>
        <b-alert
          v-model="this.showDismissibleAlert1"
          variant="danger"
        >Molimo vas, unesite naziv događaja</b-alert>
      </div>
      <div class="MyPadding">
        <label for="example-datepicker" :class="textColor(dogadjaj.empty)">Izaberite datum</label>
        <b-form-datepicker
          id="example-datepicker"
          v-model="valueDate"
          class="mb-2"
          locale="sr"
          :min="min"
        ></b-form-datepicker>
        <b-alert
          v-model="this.showDismissibleAlert2"
          variant="danger"
        >Molimo vas, izaberite datum održavanja događaja</b-alert>
      </div>
      <div class="MyPadding">
        <label :class="textColor(dogadjaj.empty)">Izaberite vreme</label>
        <b-form-timepicker v-model="valueTime" locale="sr"></b-form-timepicker>
        <b-alert
          v-model="this.showDismissibleAlert3"
          variant="danger"
        >Molimo vas, izaberite vreme održavanja događaja</b-alert>
      </div>
      <div class="MyPadding">
        <label :class="textColor(dogadjaj.empty)">Izaberite kafić</label>
        <b-form-select v-model="selectedCafe" :options="options"></b-form-select>
        <b-alert
          v-model="this.showDismissibleAlert4"
          variant="danger"
        >Molimo vas, izaberite kafić u kome želite da održite događaj</b-alert>
      </div>
      <div>
        <label :class="textColor(dogadjaj.empty)">Unesite opis događaja</label>
        <b-form-textarea
          id="textarea"
          v-model="textInfo"
          placeholder="Opis događaja"
          rows="3"
          max-rows="6"
          class="MyPadding"
        ></b-form-textarea>
        <b-alert
          v-model="this.showDismissibleAlert5"
          variant="danger"
        >Molimo vas, unesite opis događaja</b-alert>
      </div>
      <div class="MyPadding">
        <label :class="textColor(dogadjaj.empty)">Izaberite tip događaja</label>
        <b-form-group>
          <b-form-radio
            v-model="selectedtypeOfEvent"
            name="typeOfEvent"
            value="p"
            :class="textColor(dogadjaj.empty)"
          >Javni događaj</b-form-radio>
          <b-form-radio
            v-model="selectedtypeOfEvent"
            name="typeOfEvent"
            value="h"
            :class="textColor(dogadjaj.empty)"
          >Privatni događaj</b-form-radio>
        </b-form-group>
        <b-alert
          v-model="this.showDismissibleAlert6"
          variant="danger"
        >Molimo vas, izaberite tip događaja</b-alert>
      </div>
      <div class="MyDivButtons">
        <b-button variant="success" @click="createEvent">Potvrdi</b-button>
        <b-button variant="secondary" @click="resetForm" class="MyLeftReset">Resetuj vrednosti</b-button>
      </div>
    </b-collapse>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      options: [],
      textTitle: this.dogadjaj.title,
      valueDate: this.dogadjaj.date,
      valueTime: this.dogadjaj.time,
      selectedCafe: this.dogadjaj.cafe,
      textInfo: this.dogadjaj.info,
      selectedtypeOfEvent: this.dogadjaj.type,
      min: this.limitMinDate(),
      caffesArray: [],
      datesToDisable: [],
      showDismissibleAlert1: false,
      showDismissibleAlert2: false,
      showDismissibleAlert3: false,
      showDismissibleAlert4: false,
      showDismissibleAlert5: false,
      showDismissibleAlert6: false
    }
  },
  props: {
    dogadjaj: Object
  },
  methods: {
    textColor(flag) {
      if (flag) return 'MyLabel'
      else return 'MyText'
    },
    resetForm() {
      this.textTitle = ''
      this.valueDate = ''
      this.valueTime = ''
      this.selectedCafe = ''
      this.textInfo = ''
      this.selectedtypeOfEvent = ''
    },
    createEvent() {
      if (
        this.textTitle === '' ||
        this.valueDate === '' ||
        this.valueTime === '' ||
        this.selectedCafe === '' ||
        this.textInfo === '' ||
        this.selectedtypeOfEvent === ''
      ) {
        if (this.textTitle === '') this.showDismissibleAlert1 = true
        else this.showDismissibleAlert1 = false
        if (this.valueDate === '') this.showDismissibleAlert2 = true
        else this.showDismissibleAlert2 = false
        if (this.valueTime === '') this.showDismissibleAlert3 = true
        else this.showDismissibleAlert3 = false
        if (this.selectedCafe === '') this.showDismissibleAlert4 = true
        else this.showDismissibleAlert4 = false
        if (this.textInfo === '') this.showDismissibleAlert5 = true
        else this.showDismissibleAlert5 = false
        if (this.selectedtypeOfEvent === '') this.showDismissibleAlert6 = true
        else this.showDismissibleAlert6 = false
      } else {
        this.showDismissibleAlert1 = false
        this.showDismissibleAlert2 = false
        this.showDismissibleAlert3 = false
        this.showDismissibleAlert4 = false
        this.showDismissibleAlert5 = false
        this.showDismissibleAlert6 = false

        let kaficid = null
        for (let i = 0; i < this.caffesArray.length; i++) {
          let fullname =
            this.caffesArray[i].name +
            ', ' +
            this.caffesArray[i].address +
            ', ' +
            this.caffesArray[i].city
          if (fullname === this.selectedCafe) {
            kaficid = this.caffesArray[i].id
            break
          }
        }

        if (this.dogadjaj.empty === false) {
          axios({
            method: 'put',
            url: 'http://localhost:8080/Vinyl/Update/Event',
            headers: {
              'Access-Control-Allow-Origin': '*',
              'Content-Type': 'application/json',
              Accept: 'application/json',
              Authorization: 'Bearer ' + localStorage.token
            },
            data: {
              Id: this.dogadjaj.id,
              Title: this.textTitle,
              OwnerRef: localStorage.user,
              Modifier: this.selectedtypeOfEvent,
              CaffeRef: kaficid,
              Date: this.valueDate,
              Time: this.valueTime,
              Info: this.textInfo
            }
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
        } else {
          axios({
            method: 'post',
            url: 'http://localhost:8080/Vinyl/Add/Event',
            headers: {
              'Access-Control-Allow-Origin': '*',
              'Content-Type': 'application/json',
              Accept: 'application/json',
              Authorization: 'Bearer ' + localStorage.token
            },
            data: {
              Title: this.textTitle,
              OwnerRef: localStorage.user,
              Modifier: this.selectedtypeOfEvent,
              CaffeRef: kaficid,
              Date: this.valueDate,
              Time: this.valueTime,
              Info: this.textInfo
            }
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
        }
        this.textTitle = ''
        this.valueDate = ''
        this.valueTime = ''
        this.selectedCafe = ''
        this.textInfo = ''
        this.selectedtypeOfEvent = ''
      }
    },
    limitMinDate() {
      const now = new Date()
      const today = new Date(now.getFullYear(), now.getMonth(), now.getDate())
      const minDate = new Date(today)
      minDate.setMonth(minDate.getMonth())
      minDate.setDate(minDate.getDate())
      return minDate
    },
    setCaffes() {
      this.caffesArray.map(caffe => {
        this.options.push(caffe.name + ', ' + caffe.address + ', ' + caffe.city)
      })
    }
  },
  created() {
    axios
      .get('http://localhost:8080/Vinyl/Caffes')
      .then(response => {
        this.caffesArray = response.data
        this.setCaffes()
      })
      .catch(error => {
        console.log(error.response)
      })
  }
}
</script>

<style scoped>
.MyDivButtons {
  margin-top: 10px;
  display: flex;
}

.MyPadding {
  margin-top: 10px;
}

.MyLeftReset {
  margin-left: auto;
}

.MyCollapseButton {
  width: 100%;
  margin-top: 10px;
}

.MyLabel {
  color: white;
}

.MyText {
  color: black;
}
</style>