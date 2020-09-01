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
					<b-col sm="4">
						<b-form-input
							size="sm"
							md="auto"
							:type="'search'"
							id="filterInput"
							placeholder="Digite para pesquisar"
							v-model="filter"
						></b-form-input>
					</b-col>
					<b-col sm="4" class="text-right">
						<b-button :to="{ name: 'usuario-form' }" class="buttonCriarNovo" size="sm">Criar Novo Usuário</b-button>
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
									@click.prevent="showConfirmMsg('Usuário', bvModal, excluirUsuario, row.item.id_Usuario)"
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
					<b-pagination
						v-model="currentPage"
						:total-rows="rows"
						:per-page="perPage"
						aria-controls="tableUsuarios"
						align="fill"
						size="sm"
					></b-pagination>
				</div>
				<b-modal :id="infoModal.id" :title="infoModal.title" ok-only @hide="resetInfoModal" size="lg">
					<b-card-group deck style="padding:10px">
						<b-card header-tag="header">
							<div class="row">
								<div class="col-3">
									<label>
										<strong>Email:</strong>
									</label>
								</div>
								<div class="col-3">
									<b-form-input size="sm" disabled v-model="infoModal.content.email"></b-form-input>
								</div>

								<div class="col-3">
									<label>
										<strong>Data Criação:</strong>
									</label>
								</div>
								<div class="col-3">
									<b-form-input
										size="sm"
										disabled
										v-model="infoModal.content.dataCriacao"
										v-mask="'##/##/####'"
									></b-form-input>
								</div>
							</div>
							<br />
							<div class="row">
								<div class="col-3">
									<label>
										<strong>Nome:</strong>
									</label>
								</div>
								<div class="col-3">
									<b-form-input size="sm" disabled v-model="infoModal.content.nome"></b-form-input>
								</div>
								<div class="col-3">
									<label>
										<strong>Sobrenome:</strong>
									</label>
								</div>
								<div class="col-3">
									<b-form-input size="sm" disabled v-model="infoModal.content.sobrenome"></b-form-input>
								</div>
							</div>
							<br />
							<div class="row">
								<div class="col-3">
									<label>
										<strong>Empresa:</strong>
									</label>
								</div>
								<div class="col-3">
									<b-form-input
										size="sm"
										disabled
										v-if="infoModal.content.empresa"
										v-model="infoModal.content.empresa.nomeFantasia"
									></b-form-input>
								</div>
								<div class="col-3">
									<label>
										<strong>Perfil:</strong>
									</label>
								</div>
								<div class="col-3">
									<b-form-input
										size="sm"
										disabled
										v-if="infoModal.content.perfilUsuario"
										v-model="infoModal.content.perfilUsuario.nome"
									></b-form-input>
								</div>
							</div>
							<br />
							<div class="row">
								<div class="col-12 text-center">
									<b-button
										size="md"
										class="mr-2 buttonCriarNovo"
										v-b-tooltip.hover
										@click.prevent="showConfirmResetMsg(infoModal.content.nome, bvModal, resetarSenhaUsuario, infoModal.content.id_Usuario)"
									>Resetar Senha</b-button>
								</div>
							</div>
						</b-card>
					</b-card-group>
				</b-modal>
			</b-container>
		</div>
	</div>
</template>

<script>
import Nav from "../../components/_nav/Nav";
import { mapActions, mapState } from "vuex";
import {
	showConfirmMsg,
	showConfirmResetMsg
} from "../../helpers/messageBoxes/messageConfirm.js";
import { parseDateToLocal } from "../../helpers/dateHandler/dateHandler";

export default {
	components: {
		Nav
	},
	data() {
		return {
			perPage: 6,
			currentPage: 1,
			bvModal: this.$bvModal,
			showConfirmMsg: showConfirmMsg,
			showConfirmResetMsg: showConfirmResetMsg,
			filter: null,
			busy: true,
			infoModal: {
				id: "info-modal",
				title: "",
				content: "",
				usuario: {}
			},
			fields: [
				{
					key: "email",
					label: "E-MAIL",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "nome",
					label: "NOME",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "empresa.nomeFantasia",
					label: "EMP. ASSOCIADA",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "perfilUsuario.nome",
					label: "PERFIL ACESSO",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "Detalhes",
					label: " ",
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				}
			]
		};
	},
	methods: {
		...mapActions("usuario", [
			"ActionGetAllUsuarios",
			"ActionDeleteUsuarioById",
			"ActionResetPassUsuarioById"
		]),
		async excluirUsuario(id_Usuario) {
			try {
				await this.ActionDeleteUsuarioById(id_Usuario).then(() => {
					this.showToastDeleteConfirmation();
					this.ActionGetAllUsuarios();
				});
			} catch (err) {
				this.showToastDeleteErro(err.body);
			}
		},
		editarUsuario(id_Usuario) {
			try {
				this.$router.push("usuario-form/" + id_Usuario);
			} catch (err) {
				window.alert("Ocorreu algum erro");
				console.error(err);
			}
		},
		info(item, index, button) {
			this.infoModal.title = `Detalhes do Usuario: ${item.nome}`;
			item.dataCriacao = parseDateToLocal(item.dataCriacao);
			this.infoModal.content = item;

			this.$bvModal.show(this.infoModal.id);
		},
		async resetarSenhaUsuario(id_Usuario) {
			try {
				await this.ActionResetPassUsuarioById(id_Usuario).then(() => {
					this.showToastConfirmation(true);
				});
			} catch (err) {
				window.alert("Ocorreu algum erro");
				console.error(err);
			}
		},
		resetInfoModal() {
			this.infoModal.title = "";
			this.infoModal.content = "";
		},
		showToastConfirmation() {
			this.$bvToast.toast("Senha resetada com sucesso!", {
				title: "Sucesso",
				autoHideDelay: 5000,
				variant: "success"
			});
		},
		showToastDeleteConfirmation() {
			this.$bvToast.toast("Usuario excluido com sucesso!", {
				title: "Sucesso",
				autoHideDelay: 5000,
				variant: "success"
			});
		},

		showToastDeleteErro(err) {
			this.$bvToast.toast(`${err}`, {
				title: "Erro ao Excluir Usuario!",
				autoHideDelay: 5000,
				variant: "danger"
			});
		}
	},
	mounted() {
		this.ActionGetAllUsuarios().then(() => {
			this.busy = false;
		});
	},
	computed: {
		...mapState("usuario", ["usuarios"]),
		rows() {
			return this.usuarios.length;
		}
	}
};
</script>

<style  lang='scss' >
</style>