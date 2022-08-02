
namespace Spotify.Application.Musicians
{
    [Service]
    public class GetMusicians
    {
        private IMusicianManager _musicianManager;

        public GetMusicians(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        /// <summary>
        /// Gets info about all musicians needed to create list of them in ui
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MusicianModel> Execute() => _musicianManager.GetMusicians(musician => new MusicianModel
        {
            Name = musician.Name,
            Plays = musician.Songs.Sum(song => song.Plays),
            Id = musician.Id,
            FileName = musician.FileName
        });

        public class MusicianModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Plays { get; set; }
            public string FileName { get; set; }
        }
    }
}
