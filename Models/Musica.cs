namespace PayAndPlay.Models
{
    public class Musica
    {
        // Properties
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Artista { get; set; }
        public int? Duracao { get; set; }
        public decimal Custo { get; set; }
    }
}
