$(document).ready(function () {
    

    $('#equipesDropdown').on('change', function () {
        inicializaVariaveis();
    });


    $('.clickable-row').on('click', function () {
        // Obtém os dados da linha clicada
        var id_equipe = $(this).data('id_equipe');
        var nome = $(this).data('nome');
        var cpf = $(this).data('cpf');

        $('#equipesDropdown').val(id_equipe);
        $('#CPF').val(cpf);
        $('#Nome').val(nome);
    });

    $('#CPF').on("blur", function () {

        let cpf = $(this).val(); // Obtém o valor do campo CPF
        cpf = cpf.replace(/\D/g, ''); // Remove tudo que não for número (máscara)

        const isValid = validarCPF(cpf);
        if (!isValid) {
            alert("CPF inválido!");
        }
        else {
            getPessoaNome(cpf);
        }
    });
});


function getPessoaNome(cpf) {
    fetch('/EquipesPessoas/getPessoa?cpf=' + cpf, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    })

    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao buscar pessoa.');
        }
        return response.json(); // Retorna os dados como JSON
    })

    .then(data => {
        if (data.success === false) {
            alert(data.message); // Exibe a mensagem de erro
        } else {

            $('#Nome').val(data.nome);
        }
    })


    .catch(error => {
        alert(error.message);
        console.error('Erro:', error);
    });
}


function inicializaVariaveis() {

    var id = $('#equipesDropdown').val();

    $('#Id_Equipe').val(id);
    $('#Sexo').val('');

}

function deleteEquipesPessoas(Id_Equipe, cpf) {
    fetch('/EquipesPessoas/Delete?Id_Equipe=' + Id_Equipe + '&cpf=' + cpf, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then(response => {
            if (response.ok) {
                location.reload(); // Atualiza a página
            } else {
                alert('Erro ao excluir a equipe.');
            }
        })
        .catch(error => console.error('Erro:', error));
}



function cancela() {
    $('#CPF').val('');
    $('#Nome').val('');
    $('#equipesDropdown').val(0);
}