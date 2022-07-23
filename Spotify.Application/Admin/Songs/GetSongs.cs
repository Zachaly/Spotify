using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Admin.Songs
{
    public class GetSongs
    {
        private ISongsManager _songsManager;

        public GetSongs(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        public IEnumerable<SongViewModel> Execute() 
            => _songsManager.GetSongs(song => new SongViewModel
            {
                Id = song.Id,
                Name = song.Name,
                Plays = song.Plays,
            });

        public class SongViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Plays { get; set; }
        }
    }
}
