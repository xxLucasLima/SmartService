
function showConfirmMsg(itemExcluido, bvModal, funcExcluir, param) {
    bvModal
        .msgBoxConfirm(
            "Deseja Excluir "+ itemExcluido +" Permanentemente ?",
            {
                title: "Excluir " + itemExcluido,
                size: "md",
                buttonSize: "sm",
                okVariant: "info",
                okTitle: "Sim",
                cancelTitle: "Não",
                footerClass: "p-2",
                hideHeaderClose: false,
                centered: true
            }
        )
        .then(value => {
            if (value == true) {
                funcExcluir(param);
            }
        })
        .catch(err => {
            window.alert("Ocorreu algum erro: " + err);
        });
}
export { showConfirmMsg };
