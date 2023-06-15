<template>
  <div>
    <table>
      <caption>
        <b>{{ title }}</b>
      </caption>
      <thead>
        <tr>
          <th></th>
          <th v-show="!isNameColumnHidden">Name</th>
          <th>Time</th>
          <th>Moves</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(score, index) in scores">
          <td>{{ index + 1 }}</td>
          <td v-show="!isNameColumnHidden">{{ score.name }}</td>
          <td>{{ score.time }}</td>
          <td>{{ score.moves }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  props: ['title', 'scores', 'isNameColumnHidden'],
}
</script>

<style scoped>
th,
td {
  padding: 8px 15px;
}
</style>
