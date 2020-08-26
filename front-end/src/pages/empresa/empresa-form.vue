<template>
	<div class="empresas">
		<Nav />
		<div class="divForm">
			<b-container>
				<b-card-group deck style="padding:30px">
					<b-card header-tag="header">
						<template v-slot:header>
							<h4 class="mb-0" v-if="!idEmpresa">CRIAR NOVA EMPRESA</h4>
							<h4 class="mb-0" v-if="idEmpresa">EDITAR EMPRESA</h4>
						</template>
						<b-form @submit.stop.prevent="onSubmit">
							<b-form-group id="nomeFantasiaGroup" label="Nome Fantasia:" label-for="nomeFantasia">
								<b-form-input
									id="nomeFantasia"
									v-model="$v.form.nomeFantasia.$model"
									placeholder="Digite o Nome Fantasia"
									:state="validateState('nomeFantasia')"
									aria-describedby="input-nomeFantasia-feedback"
								></b-form-input>
								<b-form-invalid-feedback id="input-nomeFantasia-feedback">
									<span v-if="!$v.form.nomeFantasia.required">O campo "Nome Fantasia" é obrigatório</span>
									<br />
									<span
										v-if="!$v.form.nomeFantasia.minLength"
									>O campo "Nome Fantasia" não pode ter menos de 3 caracteres</span>
								</b-form-invalid-feedback>
							</b-form-group>

							<b-form-group id="cnpjGroup" label="CNPJ:" label-for="cnpj">
								<b-form-input
									id="cnpj"
									v-model="$v.form.cnpj.$model"
									placeholder="Digite o CNPJ"
									:state="validateState('cnpj')"
									aria-describedby="input-cnpj-feedback"
								></b-form-input>
								<b-form-invalid-feedback id="input-cnpj-feedback">
									<span v-if="!$v.form.cnpj.required">O campo "CNPJ" é obrigatório</span>
								</b-form-invalid-feedback>
							</b-form-group>

							<b-form-group id="razaoSocialGroup" label="Razão Social:" label-for="razaoSocial">
								<b-form-input
									id="razaoSocial"
									v-model="$v.form.razaoSocial.$model"
									placeholder="Digite a Razão Social"
									:state="validateState('razaoSocial')"
									aria-describedby="input-razaoSocial-feedback"
								></b-form-input>
								<b-form-invalid-feedback id="input-razaoSocial-feedback">
									<span v-if="!$v.form.razaoSocial.required">O campo "Razão Social" é obrigatório</span>
								</b-form-invalid-feedback>
							</b-form-group>

							<b-container class="text-center">
								<b-button v-if="!this.idEmpresa" type="submit" class="buttonCriarNovo">Criar Empresa</b-button>
								<b-button v-if="this.idEmpresa" type="submit" class="buttonCriarNovo">Editar Empresa</b-button>
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
			Empresa criado com sucesso!
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
				nomeFantasia: "",
				cnpj: "",
				razaoSocial: ""
			},
			idEmpresa: this.$route.params.id_Empresa
		};
	},
	validations: {
		form: {
			nomeFantasia: { required, minLength: minLength(3) },
			cnpj: { required },
			razaoSocial: { required }
		}
	},
	methods: {
		...mapActions("empresa", [
			"ActionCreateEmpresa",
			"ActionGetEmpresaById",
			"ActionEditEmpresa"
		]),
		validateState(nome) {
			const { $dirty, $error } = this.$v.form[nome];
			return $dirty ? !$error : null;
		},
		async onSubmit(evt) {
			try {
				let _empresa = {
					nomeFantasia: this.form.nomeFantasia,
					cnpj: this.form.cnpjcpf,
					razaoSocial: this.form.razaoSocial
				};

				this.$v.form.$touch();
				if (this.$v.form.$anyError) {
					return;
				}
				if (this.idEmpresa) {
					_empresa.id_Empresa = this.idEmpresa;
					await this.ActionEditEmpresa(_empresa).then(() => {
						this.showToastConfirmation(false);
					});
				} else {
					await this.ActionCreateEmpresa(_empresa).then(() => {
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
			this.form.nomeFantasia = "";
			this.form.cnpj = "";
			this.form.razaoSocial = "";
			this.$v.$reset();
		},
		showToastConfirmation(isCreated) {
			if (isCreated) {
				this.$bvToast.toast("Empresa criado com sucesso!", {
					title: "Sucesso",
					autoHideDelay: 5000,
					variant: "success"
				});
			} else {
				this.$bvToast.toast("Empresa editado com sucesso!", {
					title: "Sucesso",
					autoHideDelay: 5000,
					variant: "success"
				});
			}
		}
	},
	mounted() {
		if (this.idEmpresa != null) {
			let empresaReturn;
			this.ActionGetEmpresaById(this.idEmpresa).then(res => {
				empresaReturn = res.data;
				this.form.nomeFantasia = empresaReturn.nomeFantasia;
				this.form.cnpj = empresaReturn.cnpj;
				this.form.razaoSocial = empresaReturn.razaoSocial;
			});
		}
	}
};
</script>

<style>

</style>