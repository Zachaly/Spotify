using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.UI.ViewModels
{
    public class SongViewModel
    {
        public int AlbumId { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public long Plays { get; set; }
        public string CreatorName { get; set; }
        public int CreatorId { get; set; }
    }
}
