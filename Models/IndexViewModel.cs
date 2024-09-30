namespace CadastroEquipesConsumidor.Models
{
    public class IndexViewModel
    {
        public List<EquipesPessoasModel> EquipesPessoas { get; set; } // Lista de equipes com pessoas
        public List<EquipeModel> EquipesDropdown { get; set; } // Lista para o combo de equipes
    }
}
