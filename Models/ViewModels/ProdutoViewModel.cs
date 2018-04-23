namespace Termek.Models.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Marca { get; set; } 
        public string Modelo { get; set; } 
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
    }
}