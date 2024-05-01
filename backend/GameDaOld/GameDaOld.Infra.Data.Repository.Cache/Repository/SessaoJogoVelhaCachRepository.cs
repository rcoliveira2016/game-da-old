using GameDaOld.Domain.SessoesJogoVelha.Model;
using GameDaOld.Domain.SessoesJogoVelha.Repository;
using GameDaOld.Infra.Integration.CacheService;

namespace GameDaOld.Infra.Data.Repository.Cache.Repository;

public class SessaoJogoVelhaCachRepository : ISessaoJogoVelhaCacheRepository
{
    private string _namespaceKeyIndentificadorSessao = "SessaoJogoVelha";
    public readonly ICacheService _cacheService;
    public SessaoJogoVelhaCachRepository(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<SessaoJogoVelha?> Get(string identificador)
    {
        return await ObterSessao(identificador);
    }

    public async Task Remover(string identificador)
    {
        await _cacheService.RemoveAsync(ObterCacheKey(identificador));
    }

    public async Task Set(string identificador, SessaoJogoVelha? sessao)
    {
        await _cacheService.SetSerializableAsync(ObterCacheKey(identificador), sessao);
    }

    private async Task<SessaoJogoVelha?> ObterSessao(string identificador)
    {
        return await _cacheService.GetSerializableAsync<SessaoJogoVelha>(ObterCacheKey(identificador));
    }
    private string ObterCacheKey(string identificador)
    {
        return $"{_namespaceKeyIndentificadorSessao}:{identificador}";
    }
}
