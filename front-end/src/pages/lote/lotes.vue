<template>
	<div class="lotes">
		<Nav />
		<div class="divForm">
			<b-container>
				<div class="titulo">
					<label>Lotes</label>
				</div>
				<br />
				<b-row class="justify-content-md-center">
					<b-col sm="4" md="auto">
						<label>Pesquise pela descrição:</label>
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
						<b-button :to="{ name: 'lote-form' }" class="buttonCriarNovo" size="sm">Criar Novo Lote</b-button>
					</b-col>
				</b-row>
				<br />

				<div>
					<b-table
						id="tableLotes"
						striped
						empty-text="Não foi possível localizar registros de Lotes"
						empty-filtered-text="Não foi possível localizar Descrição de Lotes baseado na informação descrita"
						primary-key="tipo"
						:busy="busy"
						:head-variant="'dark'"
						:items="lotes"
						:fields="fields"
						:filter="filter"
						:filterIncludedFields="['descricao']"
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
									title="Excluir Lote"
									@click.prevent="showConfirmMsg('Lote', bvModal, excluirLote, row.item.id_Lote)"
								>
									<i class="material-icons md-24">delete</i>
								</b-button>
								<b-button
									pill
									size="sm"
									class="mr-2 buttonEditar"
									v-b-tooltip.hover
									title="Editar Lote"
									@click.prevent="editarLote(row.item.id_Lote)"
								>
									<i class="material-icons md-24">edit</i>
								</b-button>
								<b-button
									pill
									size="sm"
									class="mr-2 buttonPadrao"
									v-b-tooltip.hover
									title="Detalhes do Lote"
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
						aria-controls="tableLotes"
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
			LotesTrataveis: this.lotes,
			bvModal: this.$bvModal,
			showConfirmMsg: showConfirmMsg,
			filter: null,
			busy: true,
			infoModal: {
				id: "info-modal",
				title: "",
				content: "",
				lote: {}
			},
			fields: [
				{
					key: "descricao",
					label: "DESCRIÇÃO",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "codEntrada",
					label: "CÓDIGO ENTR.",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "codSaida",
					label: "CÓDIGO SAÍDA",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "dataEntrada",
					label: "DATA ENTRADA",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "dataSaida",
					label: "DATA SAÍDA",
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
		...mapActions("lote", ["ActionGetAllLotes", "ActionDeleteLoteById"]),
		async excluirLote(id_Lote) {
			try {
				await this.ActionDeleteLoteById(id_Lote);
				this.ActionGetAllLotes();
			} catch (err) {
				window.alert("Ocorreu algum erro");
				console.error(err);
			}
		},
		editarLote(id_Lote) {
			try {
				this.$router.push("lote-form/" + id_Lote);
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
		this.ActionGetAllLotes().then(() => {
			this.busy = false;
		});
	},
	computed: {
		...mapState("lote", ["lotes"]),
		rows() {
			return this.lotes.length;
		}
	}
};
</script>

<style>
</style>