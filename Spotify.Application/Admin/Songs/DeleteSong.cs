using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Admin.Songs
{
    public class DeleteSong
    {
        private readonly ISongsManager _songsManager;

        public DeleteSong(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        public async Task<bool> Execute(int id) => await _songsManager.RemoveSongAsync(id);
    }
}
