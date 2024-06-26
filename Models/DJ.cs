﻿namespace PayAndPlay.Models
{
    public class DJ
    {
        // Properties
        public int ID { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? confirmPassword { get; set; }

        // Foreign Key
        public int PerfilId { get; set; }

        // Navigation Property
        public virtual Perfil? Perfil { get; set; }

        // Navigation Properties
        public virtual ICollection<PlayList>? PlayLists { get; set; }
        public virtual ICollection<Pedido>? Pedidos { get; set; }
    }
}
