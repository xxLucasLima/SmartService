<template>
	<div class="lotes">
		<Nav />
		<div class="divForm">
			<b-container>
				<b-card-group deck style="padding:30px">
					<b-card header-tag="header" >
						<template v-slot:header>
							<h4 class="mb-0" v-if="!idLote">CRIAR NOVO LOTE</h4>
							<h4 class="mb-0" v-if="idLote">EDITAR LOTE</h4>
						</template>
						<b-form @submit.stop.prevent="onSubmit">
							<b-form-group id="descricaoGroup" label="Descrição:" label-for="descricao">
								<b-form-input
									id="descricao"
									v-model="$v.form.descricao.$model"
									placeholder="Digite a Descrição do Lote"
									:state="validateState('descricao')"
									aria-describedby="input-descricao-feedback"
								></b-form-input>
								<b-form-invalid-feedback id="input-descricao-feedback">
									<span v-if="!$v.form.descricao.required">O campo "Descrição" é obrigatório</span>
									<br />
									<span
										v-if="!$v.form.descricao.minLength"
									>O campo "Descrição" não pode ter menos de 3 caracteres</span>
								</b-form-invalid-feedback>
							</b-form-group>

							<b-form-group id="codEntradaGroup" label="Código de Entrada:" label-for="codEntrada">
								<b-form-input
									id="codEntrada"
									v-model="$v.form.codEntrada.$model"
									placeholder="Digite a Código de Entrada"
									:state="validateState('codEntrada')"
									aria-describedby="input-codEntrada-feedback"
								></b-form-input>
								<b-form-invalid-feedback id="input-codEntrada-feedback">
									<span v-if="!$v.form.codEntrada.required">O campo "Código de Entrada" é obrigatório</span>
								</b-form-invalid-feedback>
							</b-form-group>

							<b-form-group id="codSaidaGroup" label="Código de Saída:" label-for="codSaida">
								<b-form-input
									id="codSaida"
									v-model="$v.form.codSaida.$model"
									placeholder="Digite a Código de Saída"
									:state="validateState('codSaida')"
									aria-describedby="input-codSaida-feedback"
								></b-form-input>
								<b-form-invalid-feedback id="input-codSaida-feedback">
									<span v-if="!$v.form.codSaida.required">O campo "Código de Saída" é obrigatório</span>
								</b-form-invalid-feedback>
							</b-form-group>

							<div class="row">
								<div class="col-6">
									<label>Data de Entrada:</label>
									<b-form-datepicker
										size="ms"
										id="dataEntrada"
										type="date"
										v-model="$v.form.dataEntrada.$model"
										placeholder="Digite a Data de Entrada"
										:state="validateState('dataEntrada')"
										aria-describedby="input-dataEntrada-feedback"
									></b-form-datepicker>
									<b-form-invalid-feedback id="input-dataEntrada-feedback">
										<span v-if="!$v.form.dataEntrada.required">O campo "Data de Entrada" é obrigatório</span>
									</b-form-invalid-feedback>
								</div>
								<div class="col-6">
									<label>Data de Saída:</label>
									<b-form-datepicker
										size="ms"
										id="dataSaida"
										type="date"
										v-model="$v.form.dataSaida.$model"
										placeholder="Digite a Data de Saída"
										:state="validateState('dataSaida')"
										aria-describedby="input-dataSaida-feedback"
									></b-form-datepicker>
									<b-form-invalid-feedback id="input-dataSaida-feedback">
										<span v-if="!$v.form.dataSaida.required">O campo "Data de Saída" é obrigatório</span>
									</b-form-invalid-feedback>
								</div>
							</div>
                            <br/>
							<b-container class="text-center">
								<b-button v-if="!this.idLote" type="submit" class="buttonCriarNovo">Criar Lote</b-button>
								<b-button v-if="this.idLote" type="submit" class="buttonCriarNovo">Editar Lote</b-button>
							</b-container>
						</b-form>
					</b-card>
				</b-card-group>
			</b-container>
		</div>

		<b-toast id="createdSuccess" variant="success" solid>
			<template v-slot:toast-title>
				<div class="d-flex flex-grow-1 align-items-baseline">
					<b-img blank blank-color="green" class="mr-2" width="12" height="12"></b-img>
					<strong class="mr-auto">Sucesso!</strong>
				</div>
			</template>
			Lote criado com sucesso!
		</b-toast>
	</div>
</template>

<script>
import Nav from "../../components/_nav/Nav";
import { encryptText, decryptText } from "@/helpers/encrypt/encryptService";

import { mapActions } from "vuex";
import { required, minLength, helpers } from "vuelidate/lib/validators";
import { validationMixin } from "vuelidate";

export default {
	mixins: [validationMixin],
	components: {
		Nav
	},
	data() {
		return {
			form: {
				descricao: "",
				codEntrada: "",
				codSaida: "",
				dataEntrada: "",
				dataSaida: ""
			},
			idLote: this.$route.params.id_Lote
		};
	},
	validations: {
		form: {
			descricao: { required, minLength: minLength(3) },
			codEntrada: { required },
			codSaida: { required },
			dataEntrada: { required },
			dataSaida: { required }
		}
	},
	methods: {
		...mapActions("lote", [
			"ActionCreateLote",
			"ActionGetLoteById",
			"ActionEditLote"
		]),
		validateState(nome) {
			const { $dirty, $error } = this.$v.form[nome];
			return $dirty ? !$error : null;
		},
		async onSubmit(evt) {
			try {
				let _lote = {
					descricao: this.form.descricao,
					codEntrada: this.form.codEntrada,
					codSaida: this.form.codSaida,
					dataEntrada: this.form.dataEntrada,
					dataSaida: this.form.dataSaida
				};

				this.$v.form.$touch();
				if (this.$v.form.$anyError) {
					return;
				}
				if (this.idLote) {
					_lote.id_Lote = this.idLote;
					await this.ActionEditLote(_lote).then(() => {
						this.showToastConfirmation(false);
					});
				} else {
					await this.ActionCreateLote(_lote).then(() => {
						this.showToastConfirmation(true);
					});
				}
			} catch (err) {
				window.alert("Ocorreu algum erro");
				console.error(err);
			}
			this.cleanForm();
		},
		cleanForm() {
			this.form.descricao = "";
			this.form.codEntrada = "";
			this.form.codSaida = "";
			this.form.dataEntrada = "";
			this.form.dataSaida = "";
			this.$v.$reset();
		},
		showToastConfirmation(isCreated) {
			if (isCreated) {
				this.$bvToast.toast("Lote criado com sucesso!", {
					title: "Sucesso",
					autoHideDelay: 5000,
					variant: "success"
				});
			} else {
				this.$bvToast.toast("Lote editado com sucesso!", {
					title: "Sucesso",
					autoHideDelay: 5000,
					variant: "success"
				});
			}
		}
	},
	mounted() {
		if (this.idLote != null) {
			let loteReturn;
			this.ActionGetLoteById(this.idLote).then(res => {
				loteReturn = res.data;
				this.form.descricao = loteReturn.descricao;
				this.form.codEntrada = loteReturn.codEntrada;
				this.form.codSaida = loteReturn.codSaida;
				this.form.dataEntrada = loteReturn.dataEntrada;
				this.form.dataSaida = loteReturn.dataSaida;
			});
		}
	}
};
</script>

<style>
	#buttonsContainer {
		padding-top: 12px;
	}
</style>