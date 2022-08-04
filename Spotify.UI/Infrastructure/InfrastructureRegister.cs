using Spotify.Database;
using Spotify.Domain.Infrastructure;
using Spotify.UI.Infrastructure.FileManager;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureRegister
    {
        /// <summary>
        /// Add managers used in this application
        /// </summary>
        public static IServiceCollection AddApplicationInfrastucture(this IServiceCollection @this)
        {
            @this.AddTransient<IApplicationUserManager, ApplicationUserManager>();
            @this.AddTransient<ISongsManager, SongsManager>();
            @this.AddTransient<IMusicianManager, MusicianManager>();
            @this.AddTransient<IAlbumsManager, AlbumsManager>();
            @this.AddTransient<IFileManager, FileManager>();
            @this.AddTransient<IPlaylistManager, PlaylistManager>();
            return @this;
        }
    }
}
