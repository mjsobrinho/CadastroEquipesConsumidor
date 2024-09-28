using System.ComponentModel.DataAnnotations;

namespace CadastroEquipesConsumidor.Models
{
    public class EquipesModel
    {
        public Guid? Id { get; set; } // Chave primária do tipo GUID

        [StringLength(100)] // Limita o tamanho do nome a 100 caracteres
        public string Nm_Equipe { get; set; } // Nome da equipe

        [Range(0, 99, ErrorMessage = "A idade mínima deve ser entre 0 e 99.")]
        public int Idad_Mini { get; set; } // Idade mínima
        public string Sexo { get; set; } // Sexo
    }
}
