<template>
  <div>
    <div id="scores-container">
      <div id="scores">
        <game-score
          id="my-scores"
          :title="`Scores by time of player ${user_name}`"
          :scores="user_scores"
          :isNameColumnHidden="true"
          v-show="isMyScoresShown"
        ></game-score>

        <game-score
          id="top-scores"
          :title="`Top ${top} scores by time`"
          :scores="top_scores"
          v-show="isTopScoresShown"
        ></game-score>
      </div>

      <br />

      <div>
        <button v-on:click="switchShownScores">Switch</button>
        <button v-on:click="quitScores">Quit</button>
      </div>
    </div>
  </div>
</template>

<script>
import GameScore from './GameScore.vue'

export default {
  props: ['quitScores'],
  components: {
    GameScore,
  },
  data: function () {
    return {
      user_name: null,
      top: 10,
      top_scores: null,
      user_scores: null,
      isMyScoresShown: true,
      isTopScoresShown: false,
    }
  },
  methods: {
    mapDTO: function (scoreDTO) {
      return {
        name: scoreDTO.name,
        time: this.secondsToTime(scoreDTO.timeSeconds),
        moves: scoreDTO.movesAmount,
      }
    },
    loadScores: function (user_name) {
      this.user_name = user_name

      const baseUrl = process.env.VUE_APP_WEB_API ?? ''
      fetch(baseUrl + 'api/score/top/' + this.top)
        .then((res) => res.json())
        .then((scores) => (this.top_scores = scores.map(this.mapDTO)))
      fetch(baseUrl + 'api/score/' + user_name)
        .then((res) => res.json())
        .then((scores) => (this.user_scores = scores.map(this.mapDTO)))
    },
    switchShownScores: function () {
      this.isMyScoresShown = !this.isMyScoresShown
      this.isTopScoresShown = !this.isTopScoresShown
    },
  },
}
</script>

<style scoped>
#scores-container {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}
#scores-container > * {
  margin: 20px;
}
#scores {
  position: relative;
  flex-grow: 1;
  overflow-y: scroll;
}
#scores > * {
  position: absolute;
  left: 0;
  top: 0;
}
button {
  margin-right: 15px;
}
* {
  scrollbar-width: 0;
}
::-webkit-scrollbar {
  width: 0;
}
</style>
