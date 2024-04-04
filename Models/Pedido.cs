namespace PayAndPlay.Models
{
    public class Pedido
    {
        // Properties
        public int ID { get; set; }
        public DateOnly Data { get; set; }
        public Decimal Custo_Pedido { get; set; }
        public string? Estado { get; set; }

        // Foreign Keys
        public int UtilizadorId { get; set; }
        public int DJId { get; set; }
        public int MusicaInPlayListId { get; set;}

        // Navigation Properties
        public virtual Utilizador? Utilizador { get; set; }
        public virtual DJ? DJ { get; set; }
        public virtual MusicaInPlayList? MusicaInPlayList { get; set; }
    }
}
