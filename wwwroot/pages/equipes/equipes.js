

$('.clickable-row').on('click', function () {
    // Obtém os dados da linha clicada
    var id = $(this).data('id_equipe');
    var nome = $(this).data('nome');
    var idade = $(this).data('idade');
    var sexo = $(this).data('sexo');

    $('#Id_Equipe').val(id);
    $('#Nm_Equipe').val(nome);
    $('#Idad_Mini').val(idade);
    $('#Sexo').val(sexo);
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
function updateEquipe(id_quipe, nm_equipe, idade, sexo) {

    debugger;

    const equipeData = {
        Id_Equipe: id_quipe,
        Nm_Equipe: nm_equipe,
        Sexo: sexo,
        Idad_Mini: idade
    };
    
    fetch('/Equipes/Update', {
        method: 'PUT', // Chamada PUT
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(equipeData) // Converte o objeto para JSON
    })
        .then(response => {
            if (response.ok) {
                location.reload(); // Atualiza a página para refletir as mudanças
            } else {
                alert('Erro ao atualizar a pessoa.');
            }
        })
        .catch(error => console.error('Erro:', error));
}

function deleteEquipe(Id_Equipe) {


    fetch('/Equipes/Delete?Id_Equipe=' + Id_Equipe, { 
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
    $('#Id_Equipe').val('3fa85f64-5717-4562-b3fc-2c963f66afa6');
    $('#Nm_Equipe').val('');
    $('#Sexo').val(0);
    $('#Idad_Mini').val('');
}