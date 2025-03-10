﻿
namespace Spotify.Domain.Models
{
    public class Musician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Album> Albums { get; set; }
        public ICollection<Song> Songs { get; set; }
        public ICollection<MusicianFollow> Followers { get; set; }
        public string FileName { get; set; }
        public string? ManagerId { get; set; }
        public ApplicationUser? Manager { get; set; }
    }
}
