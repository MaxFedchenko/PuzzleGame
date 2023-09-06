<template>
  <div>
    <div id="form-container">
      <div>
        <h1>Scores:</h1>
        <p>
          <b>Total time spent - {{ secondsToTime(gameTimeSeconds) }}</b>
        </p>
        <p>
          <b>Total moves made - {{ movesCount }}</b>
        </p>
      </div>
      <div id="form-block">
        <input v-model="name" type="text" placeholder="Name" />
        <button v-on:click.once="addScore">Submit</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['gameTimeSeconds', 'movesCount', 'quitForm'],
  data: function () {
    return {
      name: '',
    }
  },
  methods: {
    addScore: async function () {
      console.log('addScore')

      await fetch((process.env.VUE_APP_WEB_API ?? '') + 'api/score', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          Name: this.name,
          TimeSeconds: this.gameTimeSeconds,
          MovesAmount: this.movesCount,
        }),
      }).catch((e) => console.error('Failed to add score', e))

      this.quitForm(this.name)
    },
  },
}
</script>

<style scoped>
#form-container {
  display: flex;
  flex-direction: column;
}
#form-container > div {
  margin: 20px;
}
#form-block {
  margin-top: 100px;
  display: flex;
}
</style>
