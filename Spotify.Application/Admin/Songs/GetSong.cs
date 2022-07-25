
namespace Spotify.Application.Admin.Songs
{
    [Service]
    public class GetSong
    {
        private ISongsManager _songsManager;

        public GetSong(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        public Response Execute(int id) 
            => _songsManager.GetSongById(id, song => new Response
            {
                Id = song.Id,
                Name = song.Name,
                Plays = song.Plays,
                CreatorId = song.MusicianId,
                CreatorName = song.Creator.Name,
                AlbumId = song.AlbumId,
                AlbumName = song.Album.Name
            });

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Plays { get; set; }
            public int CreatorId { get; set; }
            public string CreatorName { get; set; }
            public int AlbumId { get; set; }
            public string AlbumName { get; set; }   
        }
    }
}
