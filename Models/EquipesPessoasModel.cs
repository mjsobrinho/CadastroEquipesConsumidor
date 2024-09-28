using System.ComponentModel.DataAnnotations;

namespace CadastroEquipesConsumidor.Models
{
    public class EquipesPessoasModel
    {

        public Guid? Id { get; set; } // Chave primária do tipo GUID

        public string Nm_Equipe { get; set; } // Nome da equipe
        public string Nome { get; set; } // Nome da equipe

        public int idade { get; set; } // Idade mínima

        public string Sexo { get; set; } // Sexo
        public string Cpf { get; set; } // Cpf
    }
}
