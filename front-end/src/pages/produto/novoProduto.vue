<template>
  <div class="usuarios">
    <Nav />
    <div class="divForm">
      <b-container>
        <div class="titulo">
          <label>Produto</label>
        </div>
        <b-form @submit="onSubmit" @reset="onReset" v-if="show">
          <b-form-group id="input-group-1" label="Tipo:" label-for="input-1">
            <b-form-input
              id="input-1"
              v-model="form.tipo"
              required
              placeholder="Digite o Tipo"
            ></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-2" label=" Descrição: " label-for="input-2">
            <b-form-input
              id="input-1"
              v-model="form.descricao"
              required
              placeholder="Digite a descrição"
            ></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-2" label=" EAN13: " label-for="input-2">
            <b-form-input
              id="input-1"
              v-model="form.ean13"
              required
              placeholder="Digite codigo EAN13"
            ></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-2" label=" IMEI1: " label-for="input-2">
            <b-form-input
              id="input-1"
              v-model="form.imei1"
              required
              placeholder="Digite codigo IMEI1"
            ></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-2" label=" IMEI2: " label-for="input-2">
            <b-form-input
              id="input-1"
              v-model="form.imei2"
              required
              placeholder="Digite codigo IMEI2"
            ></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-2" label=" Modelo: " label-for="input-2">
            <b-form-input
              id="input-1"
              v-model="form.modelo"
              required
              placeholder="Digite o Modelo"
            ></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-2" label=" Defeito: " label-for="input-2">
            <b-form-select v-model="selected" :options="options"></b-form-select>
          </b-form-group>

          <b-container class="text-center">
            <b-button type="submit" variant="primary">Criar Novo Usuario</b-button>
          </b-container>
        </b-form>

        <!-- <b-card class="mt-3" header="Form Data Result">
        <pre class="m-0">{{ form }}</pre>
        </b-card>-->
      </b-container>
    </div>
  </div>
</template>

<script>
import Nav from "../_nav/Nav";

export default {
  components: {
    Nav
  },
  data() {
    return {
      form: {
        tipo: "",
        descricao: "",
        ean13: "",
        imei1: "",
        imei2:"",
        modelo:"",
        defeito:""
      },
      selected: null,
      options: [
        { value: null, text: "-Selecione defeito-" },
        { value: 1, text: "Mal Uso" },
        { value: 2, text: "Queda" },
        { value: 2, text: "Agua" }
      ],
      show: true
    };
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      alert(JSON.stringify(this.form));
    },
    onReset(evt) {
      evt.preventDefault();
      // Reset our form values
      this.form.email = "";
      this.form.password = "";
      // Trick to reset/clear native browser form validation state
      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    }
  }
};
</script>

<style>
.titulo {
  margin: auto;
  width: 25%;
}
.titulo label {
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
  font-size: 42pt;
}
</style>