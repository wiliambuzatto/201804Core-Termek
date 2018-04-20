namespace Termek.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Marca { get; set; } 
        public string Modelo { get; set; } 
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public Categoria Categoria { get; set; }

    }
}