<template>
	<div class="clientes">
		<Nav />
		<div class="divForm">
			<b-container>
				<div class="titulo">
					<label>Clientes</label>
				</div>
				<br />
				<b-row class="justify-content-md-center">
					<b-col sm="4" md="auto">
						<label>Pesquise pelo CPF/CNPJ:</label>
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
						<b-button
							:to="{ name: 'cliente-form' }"
							disabled
							class="buttonCriarNovo"
							size="sm"
						>Criar Novo Cliente</b-button>
					</b-col>
				</b-row>
				<br />
				<div>
					<b-table
						id="tableClientes"
						striped
						empty-text="Não foi possível localizar registros de Clientes"
						empty-filtered-text="Não foi possível localizar Descrição de Clientes baseado na informação descrita"
						primary-key="tipo"
						:busy="busy"
						:head-variant="'dark'"
						:items="clientes"
						:fields="fields"
						:filter="filter"
						:filterIncludedFields="['cnpjcpf']"
						:per-page="perPage"
						:current-page="currentPage"
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
									title="Excluir Cliente"
									@click.prevent="showConfirmMsg('Cliente', bvModal, excluirCliente, row.item.id_Cliente)"
								>
									<i class="material-icons md-24">delete</i>
								</b-button>
								<b-button
									pill
									size="sm"
									class="mr-2 buttonEditar"
									v-b-tooltip.hover
									title="Editar Cliente"
									@click.prevent="editarCliente(row.item.id_Cliente)"
								>
									<i class="material-icons md-24">edit</i>
								</b-button>
								<b-button
									pill
									size="sm"
									class="mr-2 buttonPadrao"
									v-b-tooltip.hover
									title="Detalhes do Cliente"
									@click="info(row.item, row.index, $event.target)"
								>
									<i class="material-icons md-24">menu</i>
								</b-button>
							</div>
						</template>
					</b-table>
					<b-pagination
						v-model="currentPage"
						:total-rows="rows"
						:per-page="perPage"
						aria-controls="tableClientes"
						align="fill"
						size="sm"
					></b-pagination>
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
import { showConfirmMsg } from "../../helpers/messageBoxes/messageConfirm.js";

export default {
	components: {
		Nav
	},
	data() {
		return {
			perPage: 6,
			currentPage: 1,
			ClientesTrataveis: this.clientes,
			bvModal: this.$bvModal,
			showConfirmMsg: showConfirmMsg,
			filter: null,
			busy: true,
			infoModal: {
				id: "info-modal",
				title: "",
				content: "",
				cliente: {}
			},
			fields: [
				{
					key: "nomeCompleto",
					label: "NOME COMPLETO",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "nomeFantasia",
					label: "NOME FANTASIA",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "cnpjCpf",
					label: "CNPJ/CPF",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "tipoPessoa",
					label: "TIPO CLIENTE",
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
		...mapActions("cliente", [
			"ActionGetAllClientes",
			"ActionDeleteClienteById",
			"ActionGetClienteById"
		]),
		async excluirCliente(id_Cliente) {
			try {
				await this.ActionDeleteClienteById(id_Cliente).then(() => {
					this.showToastDeleteConfirmation();
					this.ActionGetAllClientes();
				});
			} catch (err) {
				this.showToastDeleteErro(err.body);
			}
		},
		editarCliente(id_Cliente) {
			try {
				let clienteReturn = this.ActionGetClienteById(id_Cliente);
				if (clienteReturn.tipoPessoa == "Jurídica")
					this.$router.push("clientePJ-form/" + id_Cliente);
				else this.$router.push("clientePF-form/" + id_Cliente);
			} catch (err) {
				window.alert("Ocorreu algum erro: " + err);
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
		},
		showToastDeleteConfirmation() {
			this.$bvToast.toast("Cliente excluido com sucesso!", {
				title: "Sucesso",
				autoHideDelay: 5000,
				variant: "success"
			});
		},

		showToastDeleteErro(err) {
			this.$bvToast.toast(`${err}`, {
				title: "Erro ao Excluir Cliente!",
				autoHideDelay: 5000,
				variant: "danger"
			});
		}
	},
	mounted() {
		this.ActionGetAllClientes().then(() => {
			this.busy = false;
		});
	},
	computed: {
		...mapState("cliente", ["clientes"]),
		rows() {
			return this.clientes.length;
		}
	}
};
</script>

<style>
</style>