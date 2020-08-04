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
						<b-form-select v-model="selected" :options="perfis"></b-form-select>
					</b-form-group>

					<b-form-group id="ddlEmpresa" label=" Empresa: ">
						<b-form-select v-model="selected" :options="empresas"></b-form-select>
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
				senha: ""
			},
			selected: null,
			perfis: [
				{ value: null, text: "-Selecione Perfil-" },
				{ value: 1, text: "Admin" },
				{ value: 2, text: "Técnico" },
				{ value: 3, text: "Operador" }
			],
			empresas: [
				{ value: null, text: "-Selecione Empresa-" },
				{ value: 1, text: "Empresa 1" },
				{ value: 2, text: "Empresa 2" },
				{ value: 3, text: "Empresa 3" }
			],
			show: true
		};
	},
	methods: {
		onSubmit() {
			let _usuario = {
				nome: this.form.nome,
				sobrenome: this.form.sobrenome,
				email: this.form.email,
				senha: this.form.senha,
				id_perfilUsuario: 1,
				id_empresa: 1
			};
			this.$http
				.post("http://localhost:8000/api/usuario", _usuario)
				.then(res => res.json())
				.then(usuarioRetornado => {
					this.form = "";
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