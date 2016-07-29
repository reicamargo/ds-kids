using System;
using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    public class RefeicoesFake : IRefeicoes
    {
        private readonly Database _database;

        public RefeicoesFake(Database database)
        {
            _database = database;
        }

        public async Task<Refeicao> ListarPorMesesDeIdadeTipoRefeicaoAsync(int mesesDeidade, TipoRefeicao tipoRefeicao, int idParceiro)
        {
            await Task.Delay(0);
            switch (tipoRefeicao)
            {
                /*
                JANTAR;
                LANCHE_DA_NOITE;
                */
                case TipoRefeicao.CafeDaManha:
                    lock (_database.cafe_Da_Manha)
                    {
                        return _database.cafe_Da_Manha
                            .OrderBy(r => Guid.NewGuid())
                                        .Take(1)
                                        .FirstOrDefault();
                    }
                case TipoRefeicao.LancheDaManha:
                    lock (_database.lanche_Da_Manha)
                    {
                        return _database.lanche_Da_Manha
                            .OrderBy(r => Guid.NewGuid())
                            .Take(1)
                            .FirstOrDefault();
                    }
                case TipoRefeicao.Almoco:
                    lock (_database.almoco)
                    {
                        return _database.almoco
                            .OrderBy(r => Guid.NewGuid())
                            .Take(1)
                            .FirstOrDefault();
                    }
                case TipoRefeicao.LancheDaTarde:
                    lock (_database.lanche_Da_Tarde)
                    {
                        return _database.lanche_Da_Tarde
                            .OrderBy(r => Guid.NewGuid())
                            .Take(1)
                            .FirstOrDefault();
                    }
                case TipoRefeicao.Jantar:
                    lock (_database.jantar)
                    {
                        return _database.jantar
                            .OrderBy(r => Guid.NewGuid())
                            .Take(1)
                            .FirstOrDefault();
                    }
                default:
                case TipoRefeicao.LancheDaNoite:
                    lock (_database.lanche_Da_Noite)
                    {
                        return _database.lanche_Da_Noite
                            .OrderBy(r => Guid.NewGuid())
                            .Take(1)
                            .FirstOrDefault();
                    }
            }
        }
    }
}
