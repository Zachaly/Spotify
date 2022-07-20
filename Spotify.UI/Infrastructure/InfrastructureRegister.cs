using Spotify.Database;
using Spotify.Domain.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureRegister
    {
        public static IServiceCollection AddApplicationInfrastucture(this IServiceCollection @this)
        {
            @this.AddTransient<IApplicationUserManager, ApplicationUserManager>();
            @this.AddTransient<ISongsManager, SongsManager>();
            @this.AddTransient<IArtistManager, ArtistsManager>();
            @this.AddTransient<IAlbumsManager, AlbumsManager>();
            return @this;
        }
    }
}
