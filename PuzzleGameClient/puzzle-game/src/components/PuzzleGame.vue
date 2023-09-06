<template>
  <div>
    <div id="pieces-container">
      <div
        v-for="(piece, index) in pieces"
        class="piece"
        :style="{ left: piece.x * 25 + '%', top: piece.y * 25 + '%' }"
        v-on:click="movePiece(piece)"
      >
        <img :src="`${base_pieces_path}piece_${index + 1}.jpg`" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['gameOver'],
  data: function () {
    return {
      base_pieces_path: (process.env.VUE_APP_WEB_API ?? '') + 'static/pieces/',
      pieces: null,
      empty_cell: null,
      isGameOn: false,
      startTime: null,
      movesCount: 0,
    }
  },
  methods: {
    isPuzzleSolved: function () {
      for (let piece of this.pieces)
        if (piece.x + piece.y * 4 != piece.index) return false
      return true
    },
    isPuzzleSolvable: function () {
      const cur_indexes = this.pieces.map((p) => p.x + p.y * 4)

      let is_even = true
      for (let i = 0; i < 14; i++) {
        for (let j = i + 1; j < 15; j++) {
          if (cur_indexes[i] > cur_indexes[j]) is_even = !is_even
        }
      }

      return is_even
    },
    placePieces: function () {
      this.pieces = []
      for (let y = 0; y < 4; y++) {
        for (let x = 0; x < (y == 3 ? 3 : 4); x++) {
          this.pieces.push({ index: x + y * 4, x, y })
        }
      }
      this.empty_cell = { x: 3, y: 3 }
    },
    shufflePieces: function () {
      for (let i = 14; i > 0; i--) {
        let j = Math.floor(Math.random() * (i + 1))

        let temp = { x: this.pieces[i].x, y: this.pieces[i].y }
        this.pieces[i].x = this.pieces[j].x
        this.pieces[i].y = this.pieces[j].y
        this.pieces[j].x = temp.x
        this.pieces[j].y = temp.y
      }
    },
    movePiece: function (piece) {
      if (
        this.isGameOn &&
        Math.abs(piece.x - this.empty_cell.x) +
          Math.abs(piece.y - this.empty_cell.y) ==
          1
      ) {
        let temp = { x: piece.x, y: piece.y }
        piece.x = this.empty_cell.x
        piece.y = this.empty_cell.y
        this.empty_cell.x = temp.x
        this.empty_cell.y = temp.y

        this.movesCount++

        if (this.isPuzzleSolved()) this.endGame()
      }
    },
    startGame: function () {
      this.placePieces()

      // this.isGameOn = true
      // this.movesCount = 0
      // this.startTime = Date.now()
      setTimeout(() => {
        while (true) {
          this.shufflePieces()
          if (!this.isPuzzleSolved() && this.isPuzzleSolvable()) {
            this.isGameOn = true
            this.movesCount = 0
            this.startTime = Date.now()
            break
          }
        }
      }, 2000)
    },
    endGame: function () {
      this.isGameOn = false
      const gameTimeSeconds = Math.floor((Date.now() - this.startTime) / 1000)
      this.gameOver(gameTimeSeconds, this.movesCount)
    },
  },
}
</script>

<style scoped>
#pieces-container {
  position: relative;
  width: 100%;
  height: 100%;
}
.piece {
  position: absolute;
  width: 25%;
  height: 25%;
  transition-property: left top;
  transition-duration: 1s;
}
.piece img {
  width: 100%;
  height: 100%;
}
</style>
