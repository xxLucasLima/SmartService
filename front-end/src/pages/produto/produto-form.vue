<template>
	<div class="usuarios">
		<Nav />
		<div class="divForm">
			<b-container>
				<div class="titulo">
					<label>Produto</label>
				</div>
				<b-form @submit.stop.prevent="onSubmit" v-if="show">
					<b-form-group id="tipoGroup" label="Tipo:" label-for="tipo">
						<b-form-input
							id="tipo"
							v-model="$v.form.tipo.$model"
							placeholder="Digite o Tipo de Produto"
							:state="validateState('tipo')"
							aria-describedby="input-tipo-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-tipo-feedback">
							<span v-if="!$v.form.tipo.required">O campo "Tipo" é obrigatório</span>
							<span v-if="!$v.form.tipo.minLength">O campo "Tipo" não pode ter menos de 3 caracteres</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="descricaoGroup" label="Descrição:" label-for="descricao">
						<b-form-input
							id="descricao"
							v-model="$v.form.descricao.$model"
							placeholder="Digite a Descrição"
							:state="validateState('descricao')"
							aria-describedby="input-descricao-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-descricao-feedback">
							<span v-if="!$v.form.descricao.required">O campo "Descrição" é obrigatório</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="ean13Group" label="EAN13:" label-for="ean13">
						<b-form-input
							id="ean13"
							v-model="$v.form.ean13.$model"
							placeholder="Digite o EAN13"
							:state="validateState('ean13')"
							aria-describedby="input-ean13-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-ean13-feedback">
							<span v-if="!$v.form.ean13.required">O campo "EAN13" é obrigatório</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="imei1Group" label="IMEI1:" label-for="imei1">
						<b-form-input
							id="imei1"
							v-model="$v.form.imei1.$model"
							placeholder="Digite o IMEI1"
							:state="validateState('imei1')"
							aria-describedby="input-imei1-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-imei1-feedback">
							<span v-if="!$v.form.imei1.required">O campo "IMEI1" é obrigatório</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="imei2Group" label="IMEI2:" label-for="imei2">
						<b-form-input
							id="imei2"
							v-model="$v.form.imei2.$model"
							placeholder="Digite o IMEI2"
							:state="validateState('imei2')"
							aria-describedby="input-imei2-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-imei2-feedback">
							<span v-if="!$v.form.imei2.required">O campo "IMEI1" é obrigatório</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="modeloGroup" label="Modelo:" label-for="modelo">
						<b-form-input
							id="modelo"
							v-model="$v.form.modelo.$model"
							placeholder="Digite o Modelo"
							:state="validateState('modelo')"
							aria-describedby="input-modelo-feedback"
						></b-form-input>
						<b-form-invalid-feedback id="input-modelo-feedback">
							<span v-if="!$v.form.modelo.required">O campo "Modelo" é obrigatório</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="clienteGroup" label=" Cliente: ">
						<b-form-select
							id="ddlCliente"
							v-model="$v.form.id_cliente.$model"
							:options="clientes"
							:state="validateState('id_cliente')"
							aria-describedby="input-ddlCliente-feedback"
						></b-form-select>
						<b-form-invalid-feedback id="input-ddlCliente-feedback">
							<span v-if="!$v.form.id_cliente.isDDLEmpty">Uma opção de "Cliente" deve ser escolhida</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="loteGroup" label=" Lote: ">
						<b-form-select
							id="ddlLote"
							v-model="$v.form.id_lote.$model"
							:options="lotes"
							:state="validateState('id_lote')"
							aria-describedby="input-ddlLote-feedback"
						></b-form-select>
						<b-form-invalid-feedback id="input-ddlLote-feedback">
							<span v-if="!$v.form.id_lote.isDDLEmpty">Uma opção de "Lote" deve ser escolhida</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-form-group id="defeitoProdutoGroup" label=" Defeito do Produto: ">
						<b-form-select
							id="ddlDefeitoProduto"
							v-model="$v.form.id_defeitoProduto.$model"
							:options="defeitosProduto"
							:state="validateState('id_defeitoProduto')"
							aria-describedby="input-ddlDefeitoProduto-feedback"
						></b-form-select>
						<b-form-invalid-feedback id="input-ddlDefeitoProduto-feedback">
							<span
								v-if="!$v.form.id_defeitoProduto.isDDLEmpty"
							>Uma opção de "Defeito do Produto" deve ser escolhida</span>
						</b-form-invalid-feedback>
					</b-form-group>

					<b-container class="text-center">
						<b-button type="submit" variant="primary">Criar Produto</b-button>
					</b-container>
				</b-form>
			</b-container>
		</div>
	</div>
</template>

<script>
import Nav from "../../components/_nav/Nav";
import { encryptText, decryptText } from "@/helpers/encrypt/encryptService";

import { mapActions } from "vuex";
import { required, minLength, helpers } from "vuelidate/lib/validators";
import { validationMixin } from "vuelidate";

const isDDLEmpty = value => !helpers.req(value) || value != 0;

export default {
	mixins: [validationMixin],
	components: {
		Nav
	},
	data() {
		return {
			form: {
				tipo: "",
				descricao: "",
				ean13: "",
				imei1: "",
				imei2: "",
				modelo: "",
				id_defeitoProduto: 0,
				id_lote: 0,
				id_cliente: 0
			},
			clientes: [],
			lotes: [],
			defeitosProduto: [],
			show: true,
			idProduto: this.$route.params.id_Produto
		};
	},
	validations: {
		form: {
			tipo: { required, minLength: minLength(3) },
			descricao: { required },
			ean13: { required },
			imei1: { required },
			imei2: { required },
			modelo: { required },
			id_defeitoProduto: { isDDLEmpty },
			id_lote: { isDDLEmpty },
			id_cliente: { isDDLEmpty }
		}
	},
	methods: {
		...mapActions("produto", [
			"ActionGetAllClientesDDL",
			"ActionGetAllLotesDDL",
			"ActionGetAllDefeitosProdutoDDL",
			"ActionCreateProduto",
			"ActionGetProdutoById",
			"ActionEditProduto"
		]),
		validateState(nome) {
			const { $dirty, $error } = this.$v.form[nome];
			return $dirty ? !$error : null;
		},
		async onSubmit() {
			try {
				let _produto = {
					tipo: this.form.tipo,
					descricao: this.form.descricao,
					ean13: this.form.ean13,
					imei1: this.form.imei1,
					imei2: this.form.imei2,
					modelo: this.form.modelo,
					id_defeitoProduto: this.form.id_defeitoProduto,
					id_lote: this.form.id_lote,
					id_cliente: this.form.id_cliente
				};

				this.$v.form.$touch();
				if (this.$v.form.$anyError) {
					return;
				}
				if (this.idProduto) {
					_produto.id_Produto = this.idProduto;
					await this.ActionEditProduto(_produto).then(() => {
						alert("Usuario editado com sucesso");
					});
				} else {
					await this.ActionCreateProduto(_produto).then(() => {
						alert("Usuario criado com sucesso");
					});
				}
			} catch (err) {
				window.alert("Ocorreu algum erro");
				console.error(err);
			}
		}
	},
	mounted() {
		this.ActionGetAllClientesDDL().then(res => {
			this.clientes = this.clientes.concat({
				text: "--Selecione Cliente--",
				value: 0
			});
			this.clientes = this.clientes.concat(res.data);
		});
		this.ActionGetAllLotesDDL().then(res => {
			this.lotes = this.lotes.concat({
				text: "--Selecione Lote--",
				value: 0
			});
			this.lotes = this.lotes.concat(res.data);
		});
		this.ActionGetAllDefeitosProdutoDDL().then(res => {
			this.defeitosProduto = this.defeitosProduto.concat({
				text: "--Selecione Lote--",
				value: 0
			});
			this.defeitosProduto = this.defeitosProduto.concat(res.data);
		});

		if (this.idProduto != null) {
			let produtoReturn;
			this.ActionGetProdutoById(this.idProduto).then(res => {
				produtoReturn = res.data;
				this.form.tipo = produtoReturn.tipo;
				this.form.descricao = produtoReturn.descricao;
				this.form.ean13 = produtoReturn.ean13;
				this.form.imei1 = produtoReturn.imei1;
				this.form.imei2 = produtoReturn.imei2;
				this.form.modelo = produtoReturn.modelo;
				this.form.id_defeitoProduto =
					produtoReturn.defeitoProduto.id_DefeitoProduto;
				this.form.id_lote = produtoReturn.lote.id_Lote;
                this.form.id_cliente = produtoReturn.cliente.id_Cliente;
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