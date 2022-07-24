
namespace Spotify.Application.Admin.Musicians
{
    [Service]
    public class GetMusicians
    {
        private readonly IMusicianManager _musicianManager;

        public GetMusicians(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        public IEnumerable<MusicianViewModel> Execute() 
            => _musicianManager.GetMusicians(musician => new MusicianViewModel
            {
                Name = musician.Name,
                Id = musician.Id,
                NumberOfSongs = musician.Songs.Count(),
                NumberOfFollowers = musician.Followers.Count(),
                NumberOfPlays = musician.Songs.Sum(song => song.Plays),
            });

        public class MusicianViewModel
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public int NumberOfSongs { get; set; }
            public int NumberOfFollowers { get; set; }
            public long NumberOfPlays { get; set; }
        }
    }
}
