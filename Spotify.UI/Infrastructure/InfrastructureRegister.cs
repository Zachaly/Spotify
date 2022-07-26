﻿using Spotify.Database;
using Spotify.Domain.Infrastructure;

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
            return @this;
        }
    }
}
