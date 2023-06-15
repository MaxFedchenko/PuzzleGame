import Vue from 'vue'
import App from './App.vue'

Vue.config.productionTip = false
Vue.prototype.secondsToTime = (seconds) => {
  const hours = Math.floor(seconds / 3600)
  seconds %= 3600
  const minutes = Math.floor(seconds / 60)
  seconds %= 60

  return `${hours}:${minutes}:${seconds}`
}

new Vue({
  render: (h) => h(App),
}).$mount('#app')
