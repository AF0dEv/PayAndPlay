﻿namespace PayAndPlay.Models
{
    public class PlayList
    {
        // Properties
        public int ID { get; set; }
        public string? Nome { get; set; }

        // Foreign Key
        public int DJId { get; set; }

        // Navigation Property
        public virtual DJ? DJ { get; set; }

        // Navigation Properties
        public virtual ICollection<MusicaInPlayList>? MusicasInPlayLists { get; set; }
    }
}
