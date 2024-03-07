namespace GameDaOld.Infra.Integration.CacheService.Common
{
    public class CacheOptions
    {
        public TimeSpan? SlidingExpiration { get; set; }
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
    }
}
