using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spotify.Domain.Models;

namespace Spotify.Database
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        public DbSet<SongLike> SongLikes { get; set; }
        public DbSet<AlbumLike> AlbumLikes { get; set; }
        public DbSet<MusicianFollow> MusicianFollows { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PlaylistSong>().HasKey(song => new { song.PlaylistId, song.SongId });

            builder.Entity<SongLike>().HasKey(like => new { like.SongId, like.ApplicationUserId });
            builder.Entity<AlbumLike>().HasKey(like => new { like.AlbumId, like.ApplicationUserId });
            builder.Entity<MusicianFollow>().HasKey(follow => new { follow.MusicianId, follow.ApplicationUserId });

            builder.Entity<Song>().
                HasOne(song => song.Album).
                WithMany(album => album.Songs).
                HasForeignKey(song => song.AlbumId).
                OnDelete(DeleteBehavior.NoAction);
        }
    }
}
