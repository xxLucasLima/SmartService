<template>
	<div class="clientes">
		<Nav />
		<div class="divForm">
			<b-container>
				<b-card-group deck style="padding:30px">
					<b-card header-tag="header">
						<template v-slot:header>
							<h4 class="mb-0" v-if="!idCliente">CRIAR NOVO CLIENTE PJ</h4>
							<h4 class="mb-0" v-if="idCliente">EDITAR CLIENTE PJ</h4>
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
									<span v-if="!$v.form.nomeFantasia.required">O campo "Nome Fantasia" é obrigatório</span>&nbsp;&nbsp;
									<span
										v-if="!$v.form.nomeFantasia.minLength"
									>O campo "Nome Fantasia" não pode ter menos de 3 caracteres</span>
								</b-form-invalid-feedback>
							</b-form-group>

							<b-form-group id="cnpjGroup" label="CNPJ:" label-for="cnpj">
								<b-form-input
									id="cnpj"
									v-model="$v.form.cnpjcpf.$model"
									placeholder="Digite o CNPJ"
									:state="validateState('cnpjcpf')"
									aria-describedby="input-cnpj-feedback"
									v-mask="'##.###.###/####-##'"
								></b-form-input>
								<b-form-invalid-feedback id="input-cnpj-feedback">
									<span v-if="!$v.form.cnpjcpf.required">O campo "CNPJ" é obrigatório</span>&nbsp;&nbsp;
									<span v-if="!$v.form.cnpjcpf.minLength">O campo "CNPJ" está incompleto</span>
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

							<b-form-group id="inscEstadualGroup" label="Inscrição Estadual:" label-for="inscEstadual">
								<b-form-input
									id="inscEstadual"
									v-model="$v.form.inscEstadual.$model"
									placeholder="Digite a Inscrição Estadual:"
									:state="validateState('inscEstadual')"
									aria-describedby="input-inscEstadual-feedback"
								></b-form-input>
								<b-form-invalid-feedback id="input-inscEstadual-feedback">
									<span v-if="!$v.form.inscEstadual.required">O campo "Inscrição Estadual" é obrigatório</span>
								</b-form-invalid-feedback>
							</b-form-group>

							<b-form-group
								id="inscricaoMunicipalGroup"
								label="Inscrição Municipal:"
								label-for="inscricaoMunicipal"
							>
								<b-form-input
									id="inscricaoMunicipal"
									v-model="$v.form.inscricaoMunicipal.$model"
									placeholder="Digite a Inscrição Municipal:"
									:state="validateState('inscricaoMunicipal')"
									aria-describedby="input-inscricaoMunicipal-feedback"
								></b-form-input>
								<b-form-invalid-feedback id="input-inscricaoMunicipal-feedback">
									<span
										v-if="!$v.form.inscricaoMunicipal.required"
									>O campo "Inscrição Municipal" é obrigatório</span>
								</b-form-invalid-feedback>
							</b-form-group>
							<b-container class="text-center">
								<b-button v-if="!this.idCliente" type="submit" class="buttonCriarNovo">Criar Cliente</b-button>
								<b-button v-if="this.idCliente" type="submit" class="buttonCriarNovo">Editar Cliente</b-button>
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
			Cliente criado com sucesso!
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
				cnpjcpf: "",
				tipoPessoa: "Jurídica",
				razaoSocial: "",
				inscEstadual: "",
				inscricaoMunicipal: ""
			},
			idCliente: this.$route.params.id_Cliente
		};
	},
	validations: {
		form: {
			nomeFantasia: { required, minLength: minLength(3) },
			cnpjcpf: { required,  minLength: minLength(18) },
			tipoPessoa: { required },
			razaoSocial: { required },
			inscEstadual: { required },
			inscricaoMunicipal: { required }
		}
	},
	methods: {
		...mapActions("cliente", [
			"ActionCreateCliente",
			"ActionGetClienteById",
			"ActionEditCliente"
		]),
		validateState(nome) {
			const { $dirty, $error } = this.$v.form[nome];
			return $dirty ? !$error : null;
		},
		async onSubmit(evt) {
			try {
				let _cliente = {
					nomeFantasia: this.form.nomeFantasia,
					cnpjcpf: this.form.cnpjcpf,
					tipoPessoa: this.form.tipoPessoa,
					razaoSocial: this.form.razaoSocial,
					inscEstadual: this.form.inscEstadual,
					inscricaoMunicipal: this.form.inscricaoMunicipal
				};

				this.$v.form.$touch();
				if (this.$v.form.$anyError) {
					return;
				}
				if (this.idCliente) {
					_cliente.id_Cliente = this.idCliente;
					await this.ActionEditCliente(_cliente).then(() => {
						this.showToastConfirmation(false);
					});
				} else {
					await this.ActionCreateCliente(_cliente).then(() => {
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
			this.form.cnpjcpf = "";
			this.form.razaoSocial = "";
			this.form.inscEstadual = "";
			this.form.inscricaoMunicipal = "";
			this.$v.$reset();
		},
		showToastConfirmation(isCreated) {
			if (isCreated) {
				this.$bvToast.toast("Cliente criado com sucesso!", {
					title: "Sucesso",
					autoHideDelay: 5000,
					variant: "success"
				});
			} else {
				this.$bvToast.toast("Cliente editado com sucesso!", {
					title: "Sucesso",
					autoHideDelay: 5000,
					variant: "success"
				});
			}
		}
	},
	mounted() {
		if (this.idCliente != null) {
			let clienteReturn;
			this.ActionGetClienteById(this.idCliente).then(res => {
				clienteReturn = res.data;
				this.form.nomeFantasia = clienteReturn.nomeFantasia;
				this.form.cnpjcpf = clienteReturn.cnpjcpf;
				this.form.razaoSocial = clienteReturn.razaoSocial;
				this.form.inscEstadual = clienteReturn.inscEstadual;
				this.form.inscricaoMunicipal = clienteReturn.inscricaoMunicipal;
				this.form.bairro = clienteReturn.bairro;
				this.form.dataNascimento = clienteReturn.dataNascimento;
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