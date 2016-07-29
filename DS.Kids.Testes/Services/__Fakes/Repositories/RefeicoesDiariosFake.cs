using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    public class RefeicoesDiariosFake : IRefeicoesDiarios
    {
        private readonly Database _database;

        public RefeicoesDiariosFake(Database database)
        {
            _database = database;
        }

        public async Task<IList<RefeicaoDiario>> ObterPorIdDataAsync(int idCrianca, DateTime dataDiario)
        {
            await Task.Delay(0);

            var result = _database.refeicoes_Diario.Where(x => dataDiario.Date == x.DataCriacao.Date && idCrianca == x.IdCrianca).ToList();
            return result;
        }
        public async Task<RefeicaoDiario> ObterPorIdDataRefeicaoAsync(int idCrianca, DateTime dataDiario, TipoRefeicao tipoRefeicao)
        {
            await Task.Delay(0);

            var result = _database.refeicoes_Diario.FirstOrDefault(x => dataDiario.Date == x.DataCriacao.Date && idCrianca == x.IdCrianca && x.IdTipoRefeicao == (int)tipoRefeicao);
            return result;
        }

        public async Task InserirAsync(RefeicaoDiario refeicaoDiario)
        {
            await Task.Delay(0);

            if (_database.refeicoes_Diario.Any(x => refeicaoDiario.TipoRefeicao == x.TipoRefeicao &&
               refeicaoDiario.DataCriacao.Date == x.DataCriacao.Date && 
               refeicaoDiario.IdCrianca == x.IdCrianca))
            {
                throw new DuplicateEntityException();
            }
            else
            {
                refeicaoDiario.IdRefeicao = _database.refeicoes_Diario.Count + 1;
                _database.refeicoes_Diario.Add(refeicaoDiario);            
            }
        }

        public async Task AtualizarAsync(RefeicaoDiario refeicaoDiario)
        {
            await Task.Delay(0);

            var oldItem = _database.refeicoes_Diario.FirstOrDefault(x => x.IdRefeicao == refeicaoDiario.IdRefeicao);
            _database.refeicoes_Diario[_database.refeicoes_Diario.IndexOf(oldItem)] = refeicaoDiario;
        }

        public async Task RemoverAsync(RefeicaoDiario refeicaoDiario)
        {
            await Task.Delay(0);
            _database.refeicoes_Diario.Remove(refeicaoDiario);
        }
    }
}
