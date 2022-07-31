using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Admin.Songs
{
    [Service]
    public class GetSongFileName
    {
        private ISongsManager _songsManager;

        public GetSongFileName(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        public string Execute(int id) => _songsManager.GetSongById(id, song => song.FileName);
    }
}
