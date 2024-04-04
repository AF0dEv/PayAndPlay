namespace PayAndPlay.Models
{
    public class Perfil
    {
        // Properties
        public int ID { get; set; }
        public string? Tipo_Perfil { get; set; }

        // Navigation Properties
        public virtual ICollection<DJ>? DJs { get; set; }
        public virtual ICollection<Utilizador>? Utilizadores { get; set; }
    }
}
