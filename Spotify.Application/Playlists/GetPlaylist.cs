
namespace Spotify.Application.Playlists
{
    [Service]
    public class GetPlaylist
    {
        private IPlaylistManager _playlistManager;

        public GetPlaylist(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        /// <summary>
        /// Gets all info about given playlist
        /// </summary>
        public Response Execute(int id) => _playlistManager.GetPlaylist(id, playlist => new Response
        {
            Id = playlist.Id,
            Name = playlist.Name,
            FileName = playlist.FileName,
            CreatorId = playlist.CreatorId,
            CreatorName = playlist.Creator.UserName,
            Songs = playlist.Songs.Select(song => new SongModel
            {
                Id = song.SongId,
                Name = song.Song.Name,
                CreatorId = song.Song.MusicianId,
                CreatorName = song.Song.Creator.Name,
                AlbumFileName = song.Song.Album.FileName,
                AlbumId = song.Song.AlbumId,
                FileName = song.Song.FileName,
            })
        });

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CreatorId { get; set; }
            public string CreatorName { get; set; }
            public string FileName { get; set; }
            public IEnumerable<SongModel> Songs { get; set; }
        }

        public class SongModel
        {
            public int Id { get; set; }
            public int CreatorId { get; set; }
            public string CreatorName { get; set; }
            public string Name { get; set; }
            public int AlbumId { get; set; }
            public string FileName { get; set; }
            public string AlbumFileName { get; set; }
        }
    }
}
