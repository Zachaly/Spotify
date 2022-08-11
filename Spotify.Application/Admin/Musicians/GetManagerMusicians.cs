
namespace Spotify.Application.Admin.Musicians
{
    [Service]
    public class GetManagerMusicians
    {
        private readonly IMusicianManager _musicianManager;

        public GetManagerMusicians(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        /// <summary>
        /// Gets musician models used in admin panel, filtered with manager id
        /// </summary>
        public IEnumerable<MusicianViewModel> Execute(string managerId)
            => _musicianManager.GetMusiciansOfManager(managerId, musician => new MusicianViewModel
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
