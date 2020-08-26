<template>
	<div class="m">
		<b-navbar toggleable="lg" type="dark" variant="dark" id="navBar">
			<b-navbar-brand id="smartServiceBrand" href="#">SmartService</b-navbar-brand>

			<b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

			<b-collapse id="nav-collapse" is-nav>
				<b-navbar-nav>
					<b-nav-item href="#"></b-nav-item>
					<b-nav-item-dropdown text="Empresas" right>
						<b-dropdown-item :to="{ name: 'empresa-form' }">Criar Nova Empresa</b-dropdown-item>
						<b-dropdown-item :to="{ name: 'empresas' }">Vizualizar Empresas</b-dropdown-item>
					</b-nav-item-dropdown>
					<b-nav-item-dropdown text="Usuarios" right>
						<b-dropdown-item :to="{ name: 'usuario-form' }">Criar Novo Usuario</b-dropdown-item>
						<b-dropdown-item :to="{ name: 'usuarios' }">Vizualizar Usuários</b-dropdown-item>
					</b-nav-item-dropdown>
					<b-nav-item-dropdown text="Clientes" right>
						<b-dropdown-item :to="{ name: 'clientePJ-form' }">Criar Novo Cliente PJ</b-dropdown-item>
						<b-dropdown-item :to="{ name: 'clientePF-form' }">Criar Novo Cliente PF</b-dropdown-item>
						<b-dropdown-item :to="{ name: 'clientes' }">Vizualizar Clientes</b-dropdown-item>
					</b-nav-item-dropdown>
					<b-nav-item-dropdown text="Lotes" right>
						<b-dropdown-item :to="{ name: 'lote-form' }">Criar Lotes</b-dropdown-item>
						<b-dropdown-item :to="{ name: 'lotes' }">Vizualizar Lotes</b-dropdown-item>
					</b-nav-item-dropdown>
					<b-nav-item-dropdown text="Ordem de Serviços" right>
						<b-dropdown-item href="#">Criar Ordem de Serviço</b-dropdown-item>
						<b-dropdown-item href="#">Vizualizar Ordens de Serviço</b-dropdown-item>
					</b-nav-item-dropdown>
					<b-nav-item-dropdown text="Produtos" right>
						<b-dropdown-item :to="{ name: 'produto-form' }">Criar Produto</b-dropdown-item>
						<b-dropdown-item :to="{ name: 'produtos' }">Vizualizar Produtos</b-dropdown-item>
					</b-nav-item-dropdown>
				</b-navbar-nav>

				<!-- Right aligned nav items -->
				<b-navbar-nav class="ml-auto">
					<b-nav-item-dropdown id="buttonUser" right>
						<template v-slot:button-content>
							<b-avatar id="avatar"></b-avatar>&nbsp;
							<em style="white-space: pre-line">Olá, {{usuario.nome}}!</em>
						</template>
						<b-dropdown-item href="#">Sobre</b-dropdown-item>
						<b-dropdown-item @click="signOut();">Sign Out</b-dropdown-item>
					</b-nav-item-dropdown>
				</b-navbar-nav>
			</b-collapse>
		</b-navbar>
	</div>
</template>

<script>
import { mapActions, mapState } from "vuex";

export default {
	name: "newLine",
	computed: {
		...mapState("auth", ["usuario"])
	},
	data() {
		return {
			text: "\n"
		};
	},
	methods: {
		...mapActions("auth", ["ActionSignOut", "ActionCheckToken"]),

		signOut() {
			this.ActionSignOut();
			location.reload();
		}
	}
};
</script>

<style lang='scss'>
	#buttonUser a {
		margin-left: 10px;
		margin-top: 5px;
	}

	#smartServiceBrand {
		color: #F59763;
    }
    
    #avatar{
		background-color: #F59763;
    }

	#navBar {
		padding-left: 20px;
		padding-right: 20px;
	}
</style>