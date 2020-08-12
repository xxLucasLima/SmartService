<template>
	<div class="usuarios">
		<Nav />
		<div class="divForm">
			<b-container>
				<div class="titulo">
					<label>Usuário</label>
				</div>
				<b-form @submit.stop.prevent="onSubmit" v-if="show">
					<b-form-group
						id="nomeGroup"
						label="Nome:"
						label-for="nome"
						:class="{ 'form-group--error': $v.form.nome.$error }"
					>
						<b-form-input
							id="nome"
							v-model="$v.form.nome.$model"
							placeholder="Digite seu Nome"
							:state="validateState('nome')"
							aria-describedby="input-nome-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-nome-feedback">
							<span v-if="!$v.form.nome.required">O campo "Nome" é obrigatório</span>
							<span v-if="!$v.form.nome.minLength">O campo "Nome" não pode ter menos de 3 caracteres</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="sobrenomeGroup" label="Sobrenome:" label-for="sobrenome">
						<b-form-input
							id="sobrenome"
							v-model="$v.form.sobrenome.$model"
							placeholder="Digite seu Sobrenome"
							:state="validateState('sobrenome')"
							aria-describedby="input-sobrenome-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-sobrenome-feedback">
							<span v-if="!$v.form.sobrenome.required">O campo "Sobrenome" é obrigatório</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="emailGroup" label=" E-mail: " label-for="email">
						<b-form-input
							id="email"
							v-model="$v.form.email.$model"
							type="email"
							placeholder="Digite seu Email"
							:state="validateState('email')"
							aria-describedby="input-email-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-email-feedback">
							<span v-if="!$v.form.email.required">O campo "Email" é obrigatório</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="senhaGroup" label=" Senha: " label-for="senha">
						<b-form-input
							id="senha"
							v-model="$v.form.senha.$model"
							type="password"
							placeholder="Digite sua Senha"
							:state="validateState('senha')"
							aria-describedby="input-password-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-password-feedback">
							<span v-if="!$v.form.senha.required">O campo "Senha" é obrigatório</span>
							<span
								v-if="!$v.form.senha.passwordConfirmed"
							>O campo "Senha" dever ser igual ao campo "Confirmar Senha"</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="input-group-2" label=" Confirme Sua Senha: " label-for="passwordConfirm">
						<b-form-input
							id="passwordConfirm"
							v-model="$v.form.passwordConfirm.$model"
							placeholder="Digite confirmação de Senha"
							type="password"
							:state="validateState('passwordConfirm')"
							aria-describedby="input-passwordConfirm-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-passwordConfirm-feedback">
							<span v-if="!$v.form.passwordConfirm.required">O campo "Confirmar Senha" é obrigatório</span>
							<span
								v-if="!$v.form.passwordConfirm.passwordConfirmConfirmed"
							>O campo "Senha" dever ser igual ao campo "Confirmar Senha"</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="perfilGroup" label=" Perfil: ">
						<b-form-select
							id="ddlPerfil"
							v-model="$v.form.id_perfilUsuario.$model"
							value-field="id_PerfilUsuario"
							text-field="nome"
							:options="perfisUsuario"
							:state="validateState('id_perfilUsuario')"
							aria-describedby="input-ddlPerfil-feedback"
						></b-form-select>
						<b-form-invalid-feedback id="input-ddlPerfil-feedback">
							<span v-if="!$v.form.id_perfilUsuario.isDDLEmpty">Uma opção de "Perfil" deve ser escolhida</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="empresaGroup" label=" Empresa: ">
						<b-form-select
							id="ddlEmpresa"
							v-model="$v.form.id_empresa.$model"
							value-field="id_Empresa"
							text-field="razaoSocial"
							:options="empresas"
							:state="validateState('id_empresa')"
							aria-describedby="input-ddlEmpresa-feedback"
						></b-form-select>
						<b-form-invalid-feedback id="input-ddlEmpresa-feedback">
							<span v-if="!$v.form.id_empresa.isDDLEmpty">Uma opção de "Empresa" deve ser escolhida</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-container class="text-center">
						<b-button v-if="!this.idUsuario" type="submit" variant="primary">Criar Usuario</b-button>
						<b-button v-if="this.idUsuario" type="submit" variant="primary">Editar Usuario</b-button>
					</b-container>
				</b-form>
			</b-container>
		</div>
	</div>
</template>

<script>
import Nav from "../../components/_nav/Nav";
import { encryptText, decryptText } from "@/helpers/encrypt/encryptService";

import { mapActions } from "vuex";
import { required, minLength, helpers, email } from "vuelidate/lib/validators";
import { validationMixin } from "vuelidate";

// const passwordConfirmed = value =>
// 	!helpers.req(value) || value === this.$v.form.passwordConfirm;
// const passwordConfirmConfirmed = value =>
// 	!helpers.req(value) || value === this.$v.form.senha;
const isDDLEmpty = value => !helpers.req(value) || value != 0;

export default {
	components: {
		Nav
	},
	mixins: [validationMixin],
	data() {
		return {
			form: {
				nome: "",
				sobrenome: "",
				email: "",
				senha: "",
				passwordConfirm: "",
				id_empresa: null,
				id_perfilUsuario: null
			},
			empresas: [],
			perfisUsuario: [],
			selected: null,
			show: true,
			idUsuario: this.$route.params.id_Usuario
		};
	},
	validations: {
		form: {
			nome: { required, minLength: minLength(3) },
			sobrenome: { required },
			email: { required, email },
			senha: {
				required,
				passwordConfirmed: (value, vm) => {
					return !helpers.req(value) || value === vm.passwordConfirm;
				}
			},
			passwordConfirm: {
				required,
				passwordConfirmConfirmed: (value, vm) => {
					return !helpers.req(value) || value === vm.senha;
				}
			},
			id_perfilUsuario: { isDDLEmpty },
			id_empresa: { isDDLEmpty }
		}
	},
	methods: {
		...mapActions("usuario", [
			"ActionGetAllEmpresasDDL",
			"ActionGetAllPerfisUsuarioDDL",
			"ActionCreateUsuario",
			"ActionGetUsuarioById",
			"ActionEditUsuario"
		]),
		validateState(nome) {
			const { $dirty, $error } = this.$v.form[nome];
			return $dirty ? !$error : null;
		},
		async onSubmit() {
			try {
				let _usuario = {
					nome: this.form.nome,
					sobrenome: this.form.sobrenome,
					email: this.form.email,
					senha: encryptText(this.form.senha),
					id_perfilUsuario: this.form.id_perfilUsuario,
					id_empresa: this.form.id_empresa
				};

				this.$v.form.$touch();
				if (this.$v.form.$anyError) {
					return;
				}
				if (this.idUsuario) {
					_usuario.id_Usuario = this.idUsuario;
					await this.ActionEditUsuario(_usuario).then(() => {
						alert("Usuario editado com sucesso");
					});
				} else {
					await this.ActionCreateUsuario(_usuario).then(() => {
						alert("Usuario criado com sucesso");
					});
				}
			} catch (err) {
				window.alert("Ocorreu algum erro");
				console.error(err);
			}
		}
	},
	mounted() {
		this.ActionGetAllEmpresasDDL().then(res => {
			this.empresas = this.empresas.concat({
				razaoSocial: "--Selecione Empresa--",
				id_Empresa: 0
			});
			this.empresas = this.empresas.concat(res.data);
		});
		this.ActionGetAllPerfisUsuarioDDL().then(res => {
			this.perfisUsuario = this.perfisUsuario.concat({
				nome: "--Selecione Perfil--",
				id_PerfilUsuario: 0,
				usuarios: null
			});
			this.perfisUsuario = this.perfisUsuario.concat(res.data);
		});

		if (this.idUsuario != null) {
			let usuarioReturn;
			this.ActionGetUsuarioById(this.idUsuario).then(res => {
				usuarioReturn = res.data;
				this.form.nome = usuarioReturn.nome;
				this.form.sobrenome = usuarioReturn.sobrenome;
				this.form.email = usuarioReturn.email;
				this.form.id_empresa = usuarioReturn.empresa.id_Empresa;
				this.form.id_perfilUsuario =
					usuarioReturn.perfilUsuario.id_PerfilUsuario;
			});
		}
	}
};
</script>

<style lang='scss'>
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