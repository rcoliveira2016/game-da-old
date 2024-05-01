using GameDaOld.Domain.SessoesJogoVelha.Model;

namespace GameDaOld.Domain.SessoesJogoVelha.Repository;

public interface ISessaoJogoVelhaCacheRepository
{
    Task<SessaoJogoVelha?> Get(string identificador);
    Task Set(string identificador, SessaoJogoVelha sessao);
    Task Remover(string identificador);
}
