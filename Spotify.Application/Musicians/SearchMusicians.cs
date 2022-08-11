
namespace Spotify.Application.Musicians
{
    [Service]
    public class SearchMusicians
    {
        private IMusicianManager _musicianManager;

        public SearchMusicians(IMusicianManager musicianManager)
        {
            _musicianManager = musicianManager;
        }

        public IEnumerable<MusicianModel> Execute(string name)
            => _musicianManager.GetMusiciansByName(name, 10, musician => new MusicianModel
            {
                Id = musician.Id,
                FileName = musician.FileName,
                Name = musician.Name
            });

        public class MusicianModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string FileName { get; set; }
        }
    }
}
