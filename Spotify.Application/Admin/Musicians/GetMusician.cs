
namespace Spotify.Application.Admin.Musicians
{
    [Service]
    public class GetMusician
    {
        private IMusicianManager _musicianManager;

        public GetMusician(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        public Response Execute(int id)
        => _musicianManager.GetMusicianById(id, musician => new Response
        {
            Id = musician.Id,
            Name = musician.Name,
            Description = musician.Description,
            NumberOfPlays = musician.Songs.Sum(song => song.Plays),
            Albums = musician.Albums.Select(album => new AlbumViewModel
            {
                Id = album.Id,
                Name = album.Name,
                Songs = album.Songs.Select(song => new SongViewModel
                {
                    Id = song.Id,
                    Name = song.Name,
                    Plays = song.Plays
                })
            })
        });

        public class Response
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Id { get; set; }
            public IEnumerable<AlbumViewModel> Albums { get; set; }
            public long NumberOfPlays { get; set; }
        }

        public class SongViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Plays { get; set; }
        }

        public class AlbumViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<SongViewModel> Songs { get; set; }
        }
    }
}
