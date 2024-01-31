namespace GameDaOld.UI.Api;

public static class CacheService
{
    public static void AddCacheConfiguration(this IHostApplicationBuilder builder){
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });
    }
}
