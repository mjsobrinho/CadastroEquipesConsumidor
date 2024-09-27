$(document).ready(function () {
    $('#CPF').mask('000.000.000-00');
});

$('.clickable-row').on('click', function () {
    // Obtém os dados da linha clicada
    const cpf = $(this).data('cpf');
    const nome = $(this).data('nome');
    const sexo = $(this).data('sexo');
    const dtNasc = $(this).data('dt-nasc');

    // Preenche os campos do formulário
    $('#CPF').val(cpf);
    $('#Nome').val(nome);
    $('#Sexo').val(sexo);
    $('#Dt_Nasc').val(dtNasc);
});

// Função para verificar se o CPF já existe no grid
function cpfExistsInGrid(cpf) {
    let exists = false;
    $('#pessoasGrid tbody tr').each(function () {
        const rowCpf = $(this).find('td:first').text().trim();
        if (rowCpf === cpf) {
            exists = true;
            return false; // Para o loop se encontrar
        }
    });
    return exists;
}
function updatePessoa(cpf, nome, sexo, dtNasc) {


    const pessoaData = {
        Cpf: cpf,
        Nome: nome,
        Sexo: sexo,
        Dt_Nasc: dtNasc
    };

    
    fetch('/Pessoas/Update', {
        method: 'PUT', // Chamada PUT
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(pessoaData) // Converte o objeto para JSON
    })
        .then(response => {
            if (response.ok) {
                alert('Pessoa atualizada com sucesso!');
                location.reload(); // Atualiza a página para refletir as mudanças
            } else {
                alert('Erro ao atualizar a pessoa.');
            }
        })
        .catch(error => console.error('Erro:', error));
}

function deletePessoa(cpf) {
    fetch('/Pessoas/Delete?cpf=' + cpf, { 
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then(response => {
            if (response.ok) {
                location.reload(); // Atualiza a página
            } else {
                alert('Erro ao excluir a pessoa.');
            }
        })
        .catch(error => console.error('Erro:', error));
}

function cancela() {
    $('#CPF').val('');
    $('#Nome').val('');
    $('#Sexo').val(0);
    $('#Dt_Nasc').val('');
}