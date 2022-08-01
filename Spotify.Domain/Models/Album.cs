
namespace Spotify.Domain.Models
{
    public class Album
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int MusicianId { get; set; }
        public Musician Musician { get; set; }
        public ICollection<Song> Songs { get; set; }   
        public ICollection<AlbumLike> AlbumLikes { get; set; }
        public string FileName { get; set; }
    }
}
