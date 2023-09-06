<template>
  <div id="app">
    <div
      id="start-game-screen"
      v-show="isGameStartScreenShown"
      style="z-index: 2"
    >
      <button v-on:click="startGame">Start Game</button>
    </div>

    <add-score-form
      v-show="isAddScoreFormShown"
      :gameTimeSeconds="gameTimeSeconds"
      :movesCount="movesCount"
      :quitForm="onQuitAddScoreForm"
      style="z-index: 2"
    />

    <game-scores
      ref="scores"
      v-show="isScoresInfoShown"
      :quitScores="onQuitScores"
      style="z-index: 2"
    />

    <div id="blur-screen" v-show="isBlurScreenShown" style="z-index: 1"></div>
    <puzzle-game ref="game" :gameOver="onGameOver" style="z-index: 0" />
  </div>
</template>

<script>
import PuzzleGame from './components/PuzzleGame.vue'
import AddScoreForm from './components/scores/AddScoreForm.vue'
import GameScores from './components/scores/GameScores.vue'

export default {
  name: 'app',
  components: {
    PuzzleGame,
    AddScoreForm,
    GameScores,
  },
  data: function () {
    return {
      isGameStartScreenShown: true,
      isBlurScreenShown: true,
      isAddScoreFormShown: false,
      isScoresInfoShown: false,
      gameTimeSeconds: null,
      movesCount: null,
    }
  },
  methods: {
    startGame: function () {
      this.isGameStartScreenShown = false
      this.isBlurScreenShown = false

      this.$refs.game.startGame()
    },
    onGameOver: function (gameTimeSeconds, movesCount) {
      this.isBlurScreenShown = true
      this.isAddScoreFormShown = true

      this.gameTimeSeconds = gameTimeSeconds
      this.movesCount = movesCount
    },
    onQuitAddScoreForm: function (user_name) {
      this.isAddScoreFormShown = false
      this.isScoresInfoShown = true

      this.$refs.scores.loadScores(user_name)
    },
    onQuitScores: function () {
      this.isScoresInfoShown = false
      this.isGameStartScreenShown = true
    },
  },
}
</script>

<style>
* {
  box-sizing: border-box !important;
  font-family: Rubik;
}
html,
body {
  height: 100%;
}
html {
  font-size: 200% !important;
}
body {
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: rgb(228, 228, 228);
}
#app {
  position: relative;
  width: 550px;
  aspect-ratio: 2/3;
  box-shadow: 0 5px 20px 1px rgba(0, 0, 0, 0.726);
  background-color: white;
}
#app > * {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}
#blur-screen {
  background-color: white;
  opacity: 0.5;
}
#start-game-screen {
  display: flex;
  justify-content: center;
  align-items: center;
}
input[type='text'] {
  width: 100%;
  display: block;
  padding: 15px;
  font-size: 1em;
  border-width: 0px;
  background-color: rgb(228, 228, 228);
}
input[type='text']:focus {
  outline: none;
}
button {
  padding: 10px;
  font-size: 1em;
  background-color: black;
  color: white;
}
button:hover {
  background-color: white;
  color: black;
}
</style>
