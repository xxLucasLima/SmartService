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
					<b-form @submit.stop.prevent="onSubmit">
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
							<b-button type="submit" variant="info">
								<i class="material-icons md-18">send</i>
								Entrar
							</b-button>
						</b-container>
					</b-form>
				</b-container>
			</b-card>
		</b-card-group>
		<br />
		<b-alert
			:show="dismissCountDown"
			dismissible
			variant="danger"
			@dismissed="dismissCountDown=0"
			@dismiss-count-down="countDownChanged"
		>Erro ao acessar. Favor verificar login e senha.</b-alert>
	</div>
</template>

<script>
import { mapActions } from "vuex";
import { encryptText, decryptText } from "@/helpers/encrypt/encryptService";

export default {
	data() {
		return {
			usuario: {
				email: "",
				senha: ""
			},
			dismissSecs: 5,
			dismissCountDown: 0
		};
	},

	methods: {
		countDownChanged(dismissCountDown) {
			this.dismissCountDown = dismissCountDown;
		},
		showAlert() {
			this.dismissCountDown = this.dismissSecs;
		},
		...mapActions("auth", ["ActionLogin"]),
		async onSubmit() {
			try {
				let _usuario = {
					email: this.usuario.email,
					senha: encryptText(this.usuario.senha)
				};
				await this.ActionLogin(_usuario);

				this.$router.push({ name: "usuarios" });
			} catch (err) {
				this.showAlert();
			}
		}
	}
};
</script>

<style lang='scss'>
.divLogin {
	margin: auto;
	width: 25%;
	padding: 10px;
	margin-top: 60px;
}
</style>