<template>
  <div id="app">
    <router-view :key="$route.fullPath"></router-view>
  </div>
</template>

<script>
export default {
  name: "App",
  components: {}
};
</script>

<style lang="scss">
body {
  background-color: rgb(85, 85, 85);
  font-family: "Montserrat", sans-serif;
}

.divForm {
  margin-top: 60px;
  margin: auto;
  width: 50%;
  padding: 40px 0 40px 0;
}

.buttonPadrao{
  background-color: coral
}
</style>