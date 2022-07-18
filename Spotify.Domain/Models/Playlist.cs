﻿
namespace Spotify.Domain.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public ICollection<Song> Songs { get; set; }
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
    }
}
