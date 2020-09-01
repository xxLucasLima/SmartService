
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

function showConfirmResetMsg(nomeUsuario, bvModal, funcReset, param) {
    bvModal
        .msgBoxConfirm(
            "Deseja Resetar Senha do Usuário: "+ nomeUsuario,
            {
                title: "Resetar Senha ",
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
                funcReset(param);
            }
        })
        .catch(err => {
            window.alert("Ocorreu algum erro: " + err);
        });
}
export { showConfirmMsg, showConfirmResetMsg };
