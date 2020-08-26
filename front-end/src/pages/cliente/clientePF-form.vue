<template>
	<div class="clientes">
		<Nav />
		<div class="divForm">
			<b-container>
				<b-card-group deck style="padding:30px">
					<b-card header-tag="header">
						<template v-slot:header>
							<h4 class="mb-0" v-if="!idCliente">CRIAR NOVO CLIENTE PF</h4>
							<h4 class="mb-0" v-if="idCliente">EDITAR CLIENTE PF</h4>
						</template>
						<b-form @submit.stop.prevent="onSubmit">
							<b-form-group id="nomeCompletoGroup" label="Nome Completo:" label-for="nomeCompleto">
								<b-form-input
									id="nomeCompleto"
									v-model="$v.form.nomeCompleto.$model"
									placeholder="Digite o Nome Completo"
									:state="validateState('nomeCompleto')"
									aria-describedby="input-nomeCompleto-feedback"
								></b-form-input>
								<b-form-invalid-feedback id="input-nomeCompleto-feedback">
									<span v-if="!$v.form.nomeCompleto.required">O campo "Nome Completo" é obrigatório</span>
									<br />
									<span
										v-if="!$v.form.nomeCompleto.minLength"
									>O campo "Nome Completo" não pode ter menos de 3 caracteres</span>
								</b-form-invalid-feedback>
							</b-form-group>

							<b-form-group id="cpfGroup" label="CPF:" label-for="cnpjcpf">
								<b-form-input
									id="cnpjcpf"
									v-model="$v.form.cnpjcpf.$model"
									placeholder="Digite o CPF"
									:state="validateState('cnpjcpf')"
									aria-describedby="input-cnpjcpf-feedback"
								></b-form-input>
								<b-form-invalid-feedback id="input-cnpjcpf-feedback">
									<span v-if="!$v.form.cnpjcpf.required">O campo "CPF" é obrigatório</span>
								</b-form-invalid-feedback>
							</b-form-group>
							<div class="row">
								<div class="col-6">
									<label>Endereço:</label>
									<b-form-input
										id="endereco"
										v-model="$v.form.endereco.$model"
										placeholder="Digite o Endereço"
										:state="validateState('endereco')"
										aria-describedby="input-endereco-feedback"
									></b-form-input>
									<b-form-invalid-feedback id="input-endereco-feedback">
										<span v-if="!$v.form.endereco.required">O campo "Endereço" é obrigatório</span>
									</b-form-invalid-feedback>
								</div>
								<div class="col-4">
									<label>CEP:</label>
									<b-form-input
										id="cep"
										v-model="$v.form.cep.$model"
										placeholder="Digite o CEP:"
										:state="validateState('cep')"
										aria-describedby="input-cep-feedback"
									></b-form-input>
									<b-form-invalid-feedback id="input-cep-feedback">
										<span v-if="!$v.form.cep.required">O campo "CEP" é obrigatório</span>
									</b-form-invalid-feedback>
								</div>
								<div class="col-2">
									<label>Nº:</label>
									<b-form-input
										id="numero"
										v-model="$v.form.numero.$model"
										placeholder="Digite o Numero:"
										:state="validateState('numero')"
										aria-describedby="input-numero-feedback"
									></b-form-input>
									<b-form-invalid-feedback id="input-numero-feedback">
										<span v-if="!$v.form.numero.required">O campo "Número" é obrigatório</span>
									</b-form-invalid-feedback>
								</div>
							</div>
							<br />
							<div class="row">
								<div class="col-6">
									<label>Cidade:</label>
									<b-form-input
										id="cidade"
										v-model="$v.form.cidade.$model"
										placeholder="Digite o Cidade:"
										:state="validateState('cidade')"
										aria-describedby="input-cidade-feedback"
									></b-form-input>
									<b-form-invalid-feedback id="input-cidade-feedback">
										<span v-if="!$v.form.cidade.required">O campo "Cidade" é obrigatório</span>
									</b-form-invalid-feedback>
								</div>
								<div class="col-6">
									<label>Bairro:</label>

									<b-form-input
										id="bairro"
										v-model="$v.form.bairro.$model"
										placeholder="Digite o Bairro:"
										:state="validateState('bairro')"
										aria-describedby="input-bairro-feedback"
									></b-form-input>
									<b-form-invalid-feedback id="input-bairro-feedback">
										<span v-if="!$v.form.bairro.required">O campo "Bairro" é obrigatório</span>
									</b-form-invalid-feedback>
								</div>
							</div>
							<br />
							<b-form-group id="dataNascimento" label="Data de Nascimento:" label-for="dataNascimento">
								<b-form-datepicker
									size="ms"
									id="dataNascimento"
									type="date"
									v-model="$v.form.dataNascimento.$model"
									placeholder="Digite a Data de Nascimento"
									:state="validateState('dataNascimento')"
									aria-describedby="input-dataNascimento-feedback"
								></b-form-datepicker>
								<b-form-invalid-feedback id="input-dataNascimento-feedback">
									<span v-if="!$v.form.dataNascimento.required">O campo "Data de Nascimento" é obrigatório</span>
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
				nomeCompleto: "",
				cnpjcpf: "",
				tipoPessoa: "Física",
				endereco: "",
				cep: "",
				numero: "",
				bairro: "",
				cidade: "",
				dataNascimento: ""
			},
			idCliente: this.$route.params.id_Cliente
		};
	},
	validations: {
		form: {
			nomeCompleto: { required, minLength: minLength(3) },
			cnpjcpf: { required },
			tipoPessoa: { required },
			endereco: { required },
			cep: { required },
			numero: { required },
			bairro: { required },
			cidade: { required },
			dataNascimento: { required }
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
					nomeCompleto: this.form.nomeCompleto,
					cnpjcpf: this.form.cnpjcpf,
					tipoPessoa: this.form.tipoPessoa,
					endereco: this.form.endereco,
					cep: this.form.cep,
					numero: this.form.numero,
					bairro: this.form.bairro,
					cidade: this.form.cidade,
					dataNascimento: this.form.dataNascimento
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
			this.form.nomeCompleto = "";
			this.form.cnpjcpf = "";
			this.form.endereco = "";
			this.form.cep = "";
			this.form.numero = "";
			this.form.bairro = "";
			this.form.cidade = "";
			this.form.dataNascimento = "";
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
				this.form.nomeCompleto = clienteReturn.nomeCompleto;
				this.form.cnpjcpf = clienteReturn.cnpjcpf;
				this.form.endereco = clienteReturn.endereco;
				this.form.cep = clienteReturn.cep;
				this.form.numero = clienteReturn.numero;
				this.form.bairro = clienteReturn.bairro;
				this.form.cidade = clienteReturn.cidade;
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