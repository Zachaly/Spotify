
namespace Spotify.Application.Admin.Albums
{
    [Service]
    public class GetAlbums
    {
        private IAlbumsManager _albumsManager;
        private IMusicianManager _musicianManager;

        public GetAlbums(IAlbumsManager albumsManager, IMusicianManager musicianManager)
        {
            _albumsManager = albumsManager;
            _musicianManager = musicianManager;
        }

        public IEnumerable<MusicianViewModel> Execute() => _musicianManager.GetMusicians(musician => new MusicianViewModel
        {
            Name = musician.Name,
            Id = musician.Id,
            Albums = musician.Albums.Select(album => new AlbumViewModel
            {
                Name = album.Name,
                Id = album.Id,
                SongCount = album.Songs.Count()
            })
        });

        public class MusicianViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<AlbumViewModel> Albums { get; set; }
        }

        public class AlbumViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int SongCount { get; set; }
        }
    }
}
