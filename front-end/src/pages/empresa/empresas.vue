<template>
	<div class="empresas">
		<Nav />
		<div class="divForm">
			<b-container>
				<div class="titulo">
					<label>Empresas</label>
				</div>
				<br />
				<b-row class="justify-content-md-center">
					<b-col sm="4" md="auto"> 
						<label>Pesquise pelo CNPJ:</label>
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
						<b-button :to="{ name: 'empresa-form' }" disabled class="buttonCriarNovo" size="sm">Criar Nova Empresa</b-button>
					</b-col>
				</b-row>
				<br />
				<div>
					<b-table
						id="tableEmpresas"
						striped
						empty-text="Não foi possível localizar registros de Empresas"
						empty-filtered-text="Não foi possível localizar Descrição de Empresas baseado na informação descrita"
						primary-key="tipo"
						:busy="busy"
						:head-variant="'dark'"
						:items="empresas"
						:fields="fields"
						:filter="filter"
						:filterIncludedFields="['cnpj']"
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
									title="Excluir Empresa"
									@click.prevent="showConfirmMsg('Empresa', bvModal, excluirEmpresa, row.item.id_Empresa)"
								>
									<i class="material-icons md-24">delete</i>
								</b-button>
								<b-button
									pill
									size="sm"
									class="mr-2 buttonEditar"
									v-b-tooltip.hover
									title="Editar Empresa"
									@click.prevent="editarEmpresa(row.item.id_Empresa)"
								>
									<i class="material-icons md-24">edit</i>
								</b-button>
								<b-button
									pill
									size="sm"
									class="mr-2 buttonPadrao"
									v-b-tooltip.hover
									title="Detalhes do Empresa"
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
						aria-controls="tableEmpresas"
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
			EmpresasTrataveis: this.empresas,
			bvModal: this.$bvModal,
			showConfirmMsg: showConfirmMsg,
			filter: null,
			busy: true,
			infoModal: {
				id: "info-modal",
				title: "",
				content: "",
				empresa: {}
			},
			fields: [
				{
					key: "nomeFantasia",
					label: "NOME FANTASIA",
					sortable: true,
					tdClass: "tdTable",
					thStyle: { minWidth: "180px", textAlign: "center" }
				},
				{
					key: "cnpj",
					label: "CNPJ",
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
		...mapActions("empresa", [
			"ActionGetAllEmpresas",
			"ActionDeleteEmpresaById"
		]),
		async excluirEmpresa(id_Empresa) {
			try {
				await this.ActionDeleteEmpresaById(id_Empresa);
				this.ActionGetAllEmpresas();
			} catch (err) {
				window.alert("Ocorreu algum erro");
				console.error(err);
			}
		},
		editarEmpresa(id_Empresa) {
			try {
					this.$router.push("empresa-form/" + id_Empresa);
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
		this.ActionGetAllEmpresas().then(() => {
			this.busy = false;
		});
	},
	computed: {
		...mapState("empresa", ["empresas"]),
		rows() {
			return this.empresas.length;
		}
	}
};
</script>

<style>



</style>