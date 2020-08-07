<template>
	<div class="usuarios">
		<Nav />
		<div class="divForm">
			<b-container>
				<div class="titulo">
					<label>Usuário</label>
				</div>
				<b-form @submit="onSubmit" v-if="show">
					<b-form-group id="input-group-1" label="Nome:" label-for="nome">
						<b-form-input id="nome" v-model="form.nome" required placeholder="Digite seu Nome"></b-form-input>
					</b-form-group>

					<b-form-group id="input-group-1" label="Sobrenome:" label-for="sobrenome">
						<b-form-input
							id="sobrenome"
							v-model="form.sobrenome"
							required
							placeholder="Digite seu Sobrenome"
						></b-form-input>
					</b-form-group>

					<b-form-group id="input-group-2" label=" E-mail: " label-for="email">
						<b-form-input
							id="email"
							v-model="form.email"
							type="email"
							required
							placeholder="Digite seu Email"
						></b-form-input>
					</b-form-group>

					<b-form-group id="input-group-2" label=" Senha: " label-for="password">
						<b-form-input
							id="password-1"
							v-model="form.senha"
							type="password"
							required
							placeholder="Digite sua Senha"
						></b-form-input>
					</b-form-group>

					<b-form-group id="input-group-2" label=" Confirme Sua Senha: " label-for="passwordConfirm">
						<b-form-input
							id="passwordConfirm"
							v-model="form.password"
							required
							placeholder="Digite confirmação de Senha"
						></b-form-input>
					</b-form-group>

					<b-form-group id="ddlPerfil" label=" Perfil: ">
						<b-form-select
							v-model="form.id_perfilUsuario"
							value-field="id_PerfilUsuario"
							text-field="nome"
							:options="perfisUsuario"
						></b-form-select>
					</b-form-group>

					<b-form-group id="ddlEmpresa" label=" Empresa: ">
						<b-form-select
							v-model="form.id_empresa"
							value-field="id_Empresa"
							text-field="razaoSocial"
							:options="empresas"
						></b-form-select>
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
import Nav from "../../components/_nav/Nav";
import { mapActions } from "vuex";

export default {
	components: {
		Nav
	},
	data() {
		return {
			form: {
				nome: "",
				sobrenome: "",
				email: "",
				senha: "",
				id_empresa: 0,
				id_perfilUsuario: 0
			},
			empresas: [],
			perfisUsuario: [],
			selected: null,
			show: true
		};
	},
	methods: {
		...mapActions("usuario", [
			"ActionGetAllEmpresasDDL",
			"ActionGetAllPerfisUsuarioDDL",
			"ActionCreateUsuario"
		]),

		async onSubmit() {
			try {
				let _usuario = {
					nome: this.form.nome,
					sobrenome: this.form.sobrenome,
					email: this.form.email,
					senha: this.form.senha,
					id_perfilUsuario: this.form.id_perfilUsuario,
					id_empresa: this.form.id_empresa
				};
				await this.ActionCreateUsuario(_usuario);
				alert("Usuario criado com sucesso");
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

			console.log(this.perfisUsuario);
		});
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