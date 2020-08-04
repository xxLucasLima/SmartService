<template>
	<div class="divLogin">
		<b-card-group deck>
			<b-card
				border-variant="secondary"
				header="Log in"
				header-bg-variant="secondary"
				header-text-variant="white"
				align="center"
			>
				<strong>
					<b-card-text style="font-size: 20pt; padding-bottom: 20px">SmartService</b-card-text>
				</strong>
				<b-container>
					<b-form @submit="onSubmit">
						<b-form-group id="input-group-1">
							<b-form-input
								id="input-1"
								v-model="usuario.email"
								type="email"
								required
								placeholder="Digite seu email"
							></b-form-input>
						</b-form-group>

						<b-form-group id="input-group-2">
							<b-form-input
								id="input-2"
								v-model="usuario.senha"
								type="password"
								required
								placeholder="Digite sua senha"
							></b-form-input>
						</b-form-group>
						<b-container class="text-center">
							<b-button type="submit" variant="info">Enviar</b-button>
						</b-container>
					</b-form>
				</b-container>
			</b-card>
		</b-card-group>
	</div>
</template>

<script>
import { mapActions } from "vuex";

export default {
	data() {
		return {
			usuario: {
				email: "",
				senha: ""
			}
		};
	},

	methods: {
		...mapActions("auth", ["ActionLogin"]),
		async onSubmit() {
			try {
				let _usuario = {
					email: this.usuario.email,
					senha: this.usuario.senha
				};

                await this.ActionLogin(_usuario);
                
                this.$router.push({name: 'Usuarios'})
			} catch (err) {
                alert(err.data ? err.data.message: 'Não foi possível fazer login')
            }
		}
	}
};
</script>

<style>
.divLogin {
	margin: auto;
	width: 25%;
	padding: 10px;
	margin-top: 60px;
}
</style>