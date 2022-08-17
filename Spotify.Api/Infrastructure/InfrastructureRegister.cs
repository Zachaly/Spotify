using Spotify.Database;
using Spotify.Domain.Infrastructure;
using Spotify.Api.Infrastructure.FileManager;
using Spotify.Api.Infrastructure.AuthManager;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureRegister
    {
        /// <summary>
        /// Add managers used in this application
        /// </summary>
        public static IServiceCollection AddApplicationInfrastucture(this IServiceCollection @this)
        {
            @this.AddHttpContextAccessor();
            @this.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            @this.AddScoped<ISongsManager, SongsManager>();
            @this.AddScoped<IMusicianManager, MusicianManager>();
            @this.AddScoped<IAlbumsManager, AlbumsManager>();
            @this.AddScoped<IFileManager, FileManager>();
            @this.AddScoped<IPlaylistManager, PlaylistManager>();
            @this.AddScoped<IAuthManager, AuthManager>();
            return @this;
        }
    }
}
