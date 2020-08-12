<template>
	<div class="usuarios">
		<Nav />

		<div class="divForm">
			<b-container>
				<div class="titulo">
					<label>Usuários</label>
				</div>
				<br />
				<b-row class="justify-content-md-center">
					<b-col sm="4" md="auto">
						<label>Pesquise pelo e-mail:</label>
					</b-col>
					<b-col sm="5">
						<b-form-input
							size="sm"
							md="auto"
							:type="'search'"
							id="filterInput"
							placeholder="Digite para pesquisar"
							v-model="filter"
						></b-form-input>
					</b-col>
					<b-col sm="3">
						<b-button pill class="buttonPadrao" size="sm">
							<i class="material-icons md-18">search</i>
							Pesquisar
						</b-button>
					</b-col>
				</b-row>
				<br />
				<div>
					<b-table
						id="tableUsuarios"
						striped
						empty-text="Não foi possível localizar registros de Usuários"
						empty-filtered-text="Não foi possível localizar Email de Usuários baseado na informação descrita"
						primary-key="email"
						:busy="busy"
						:head-variant="'dark'"
						:items="usuarios"
						:fields="fields"
						:tbody-transition-props="transProps"
						:filter="filter"
						:filterIncludedFields="['email']"
						show-empty
					>
						<template v-slot:empty="scope">
							<h4>{{ scope.emptyText }}</h4>
						</template>
						<template v-slot:emptyfiltered="scope">
							<h4>{{ scope.emptyFilteredText }}</h4>
						</template>
						<template v-slot:table-busy>
							<div class="text-center text-danger my-2">
								<b-spinner type="grow" class="align-middle"></b-spinner>
								<strong>Loading...</strong>
							</div>
						</template>

						<template v-slot:cell(Detalhes)="row">
							<div role="group">
								<b-button
									pill
									size="sm"
									class="mr-2 buttonExluir"
									v-b-tooltip.hover
									title="Excluir Usuário"
									@click.prevent="excluirUsuario(row.item.id_Usuario)"
								>
									<i class="material-icons md-24">delete</i>
								</b-button>
								<b-button
									pill
									size="sm"
									class="mr-2 buttonEditar"
									v-b-tooltip.hover
									title="Editar Usuário"
									@click.prevent="editarUsuario(row.item.id_Usuario)"
								>
									<i class="material-icons md-24">edit</i>
								</b-button>
								<b-button
									pill
									size="sm"
									class="mr-2 buttonPadrao"
									v-b-tooltip.hover
									title="Detalhes do usuário"
									@click="info(row.item, row.index, $event.target)"
								>
									<i class="material-icons md-24">menu</i>
								</b-button>
							</div>
						</template>

						<template v-slot:cell(nome)="data">{{ data.item.nome }} {{ data.item.sobrenome }}</template>
					</b-table>
				</div>
				<b-modal :id="infoModal.id" :title="infoModal.title" ok-only @hide="resetInfoModal">
					<pre>{{ infoModal.content }}</pre>
				</b-modal>
			</b-container>
		</div>
	</div>
</template>

<script>
import Nav from "../../components/_nav/Nav";
import { mapActions, mapState } from "vuex";
import { BModal, VBModal } from "bootstrap-vue";

export default {
	components: {
		Nav,
		"b-modal": BModal
	},
	data() {
		return {
			filter: null,
			busy: true,
			infoModal: {
				id: "info-modal",
				title: "",
				content: "",
				usuario: {}
			},
			transProps: {
				name: "flip-list"
			},
			fields: [
				{
					key: "email",
					label: "E-mail",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "nome",
					label: "Nome",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "empresa.nomeFantasia",
					label: "Empresa Associada",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "perfilUsuario.nome",
					label: "Perfil de Acesso",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "Detalhes",
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				}
			]
		};
	},
	methods: {
		...mapActions("usuario", [
			"ActionGetAllUsuarios",
			"ActionDeleteUsuarioById"
		]),
		async excluirUsuario(id_Usuario) {
			try {
				await this.ActionDeleteUsuarioById(id_Usuario);
				this.ActionGetAllUsuarios();
			} catch (err) {
				window.alert("Ocorreu algum erro");
				console.error(err);
			}
		},
		editarUsuario(id_Usuario) {
			try {
				this.$router.push( "usuario-form/" + id_Usuario );
			} catch (err) {
				window.alert("Ocorreu algum erro");
				console.error(err);
			}
		},
		info(item, index, button) {
			this.infoModal.title = `Row index: ${index}`;
			this.infoModal.content = JSON.stringify(item, null, 2);

			this.$bvModal.show(this.infoModal.id);
		},
		resetInfoModal() {
			this.infoModal.title = "";
			this.infoModal.content = "";
		}
	},
	mounted() {
		this.ActionGetAllUsuarios().then(() =>{
			this.busy = false;
		});
	},
	computed: {
		...mapState("usuario", ["usuarios"])
	}
};
</script>

<style  lang='scss' >
.flip-list-move {
	transition: transform 1s;
}

.buttonPadrao {
	background: coral !important;
	border-color: coral !important;
}
.buttonPadrao:hover,
.buttonPadrao:focus,
.buttonPadrao:active,
.open > .dropdown-toggle.buttonPadrao {
	background-color: rgb(235, 103, 55) !important;
	border-color: rgb(235, 103, 55) !important;
	transition: 500ms !important;
}

.buttonPadrao:active,
.open > .dropdown-toggle.buttonPadrao {
	background-color: rgb(240, 88, 33) !important;
	border-color: rgb(240, 88, 33) !important;
	transition: 500ms !important;
}

.buttonExluir {
	background: rgb(168, 0, 0) !important;
	border-color: rgb(168, 0, 0) !important;
}
.buttonExluir:hover,
.buttonExluir:focus,
.buttonExluir:active,
.open > .dropdown-toggle.buttonExluir {
	background-color: rgb(128, 1, 1) !important;
	border-color: rgb(128, 1, 1) !important;
	transition: 500ms !important;
}

.buttonExluir:active,
.open > .dropdown-toggle.buttonPadrao {
	background-color: rgb(83, 1, 1) !important;
	border-color: rgb(83, 1, 1) !important;
	transition: 500ms !important;
}

.buttonEditar {
	background: rgb(0, 46, 173) !important;
	border-color: rgb(0, 46, 173) !important;
}
.buttonEditar:hover,
.buttonEditar:focus,
.buttonEditar:active,
.open > .dropdown-toggle.buttonEditar {
	background-color: rgb(0, 31, 116) !important;
	border-color: rgb(0, 31, 116) !important;
	transition: 500ms !important;
}

.buttonEditar:active,
.open > .dropdown-toggle.buttonEditar {
	background-color: rgb(1, 23, 82) !important;
	border-color: rgb(1, 23, 82) !important;
	transition: 500ms !important;
}
.tdTable {
	min-width: 180px;
	text-align: center;
}
#tableUsuarios {
	margin-left: -180px;
}
</style>