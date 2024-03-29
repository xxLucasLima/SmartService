<template>
	<div class="produtos">
		<Nav />
		<div class="divForm">
			<b-container>
				<div class="titulo">
					<label>Produtos</label>
				</div>
				<br />
				<b-row class="justify-content-md-center">
					<b-col sm="4" md="auto">
						<label>Pesquise pelo tipo:</label>
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
						<b-button :to="{ name: 'produto-form' }" class="buttonCriarNovo" size="sm">Criar Novo Produto</b-button>
					</b-col>
				</b-row>
				<br />
				<div>
					<b-table
						id="tableProdutos"
						striped
						empty-text="Não foi possível localizar registros de Produtos"
						empty-filtered-text="Não foi possível localizar Tipo de Produtos baseado na informação descrita"
						primary-key="tipo"
						:busy="busy"
						:head-variant="'dark'"
						:items="produtos"
						:fields="fields"
						:filter="filter"
						:filterIncludedFields="['tipo']"
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
									title="Excluir Produto"
									@click.prevent="showConfirmMsg('Produto', bvModal, excluirProduto, row.item.id_Produto)"
								>
									<i class="material-icons md-24">delete</i>
								</b-button>
								<b-button
									pill
									size="sm"
									class="mr-2 buttonEditar"
									v-b-tooltip.hover
									title="Editar Produto"
									@click.prevent="editarUsuario(row.item.id_Produto)"
								>
									<i class="material-icons md-24">edit</i>
								</b-button>
								<b-button
									pill
									size="sm"
									class="mr-2 buttonPadrao"
									v-b-tooltip.hover
									title="Detalhes do Produto"
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
						aria-controls="tableProdutos"
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
			bvModal: this.$bvModal,
			showConfirmMsg: showConfirmMsg,
			filter: null,
			busy: true,
			infoModal: {
				id: "info-modal",
				title: "",
				content: "",
				produto: {}
			},
			fields: [
				{
					key: "tipo",
					label: "TIPO",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "observacao",
					label: "OBSERVAÇÃO",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "descricao",
					label: "DESCRIÇÃO",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "cliente.nomeFantasia",
					label: "CLIENTE",
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
		...mapActions("produto", [
			"ActionGetAllProdutos",
			"ActionDeleteProdutoById"
		]),
		async excluirProduto(id_Produto) {
			try {
				await this.ActionDeleteProdutoById(id_Produto);
				this.ActionGetAllProdutos();
			} catch (err) {
				window.alert("Ocorreu algum erro");
				console.error(err);
			}
		},
		editarUsuario(id_Produto) {
			try {
				this.$router.push("produto-form/" + id_Produto);
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
		this.ActionGetAllProdutos().then(() => {
			this.busy = false;
		});
	},
	computed: {
		...mapState("produto", ["produtos"]),
		rows() {
			return this.produtos.length;
		}
	}
};
</script>

<style>
</style>