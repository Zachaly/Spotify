
namespace Spotify.Application.Admin.Albums
{
    [Service]
    public class GetManagerAlbums
    {
        private readonly IMusicianManager _musicianManager;

        public GetManagerAlbums(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        /// <summary>
        /// Gets album models used in admin panel grouped by musician
        /// </summary>
        public IEnumerable<MusicianViewModel> Execute(string managerId) 
            => _musicianManager.GetMusiciansOfManager(managerId, musician => new MusicianViewModel
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
