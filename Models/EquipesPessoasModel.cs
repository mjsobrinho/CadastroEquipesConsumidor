
namespace CadastroEquipesConsumidor.Models
{
    public class EquipeModel
    {
        public Guid Id_Equipe { get; set; } // ID da equipe
        public string Nm_Equipe { get; set; } // Nome da equipe
    }

    public class EquipesPessoasModel
    {
        public Guid Id_Equipe { get; set; } // Chave primária do tipo GUID

        public string ?Nome { get; set; } // Nome da pessoa
        public int Idade { get; set; } // Idade da pessoa
        public string ?Sexo { get; set; } // Sexo da pessoa
        public string Cpf { get; set; } // Cpf
        public string ?nm_equipe { get; set; } // Cpf
    }
}
