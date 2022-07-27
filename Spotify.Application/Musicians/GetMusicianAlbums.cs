
namespace Spotify.Application.Musicians
{
    [Service]
    public class GetMusicianAlbums
    {
        private IAlbumsManager _albumsManager;

        public GetMusicianAlbums(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        public IEnumerable<AlbumModel> Execute(int musicianId)
            => _albumsManager.GetAlbumsOfMusician(musicianId, album => new AlbumModel
                {
                    Id = album.Id,
                    CreatorName = album.Musician.Name,
                    Name = album.Name
                });

        public class AlbumModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CreatorName { get; set; }
        }
    }
}
