using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Admin.Songs
{
    public class UpdateSong
    {
        private ISongsManager _songsManager;

        public UpdateSong(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        public async Task<bool> Execute(Request request) 
            => await _songsManager.UpdateSongAsync(request.Id, song =>
            {
                song.Name = request.Name;
                song.AlbumId = request.AlbumId;
                song.MusicianId = request.CreatorId;
            });

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CreatorId { get; set; }
            public int AlbumId { get; set; }
        }
    }
}
