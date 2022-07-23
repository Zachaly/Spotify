using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Admin.Songs
{
    [Service]
    public class AddSong
    {
        private ISongsManager _songsManager;

        public AddSong(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        public async Task<bool> Execute(Request request) 
            => await _songsManager.AddSongAsync(new Song
            {
                Name = request.Name,
                AlbumId = request.AlbumId,
                MusicianId = request.CreatorId,
            });

        public class Request
        {
            public string Name { get; set; }
            public int CreatorId { get; set; }
            public int AlbumId { get; set; }
        }
    }
}
