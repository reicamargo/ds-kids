using System;
using System.Collections.Generic;
using System.Linq;

using DS.Kids.Model.Support;

using Newtonsoft.Json;

namespace DS.Kids.Model
{
    public partial class RefeicaoDiario : BaseModel
    {
        public RefeicaoDiario(DateTime dataNascimento, TipoRefeicao tipoRefeicao)
        {
            TipoRefeicao = tipoRefeicao;
            RefeicoesGrupos = DefaultRefeicoesGrupo(dataNascimento, tipoRefeicao);
        }

        public static List<RefeicaoGrupo> DefaultRefeicoesGrupo(DateTime dataNascimento, TipoRefeicao tipoRefeicao)
        {
            return new List<RefeicaoGrupo>()
                                    {
                                        new RefeicaoGrupo(dataNascimento, tipoRefeicao, TipoGrupoRefeicao.CereaisTuberculosERaizes), 
                                        new RefeicaoGrupo(dataNascimento, tipoRefeicao, TipoGrupoRefeicao.FeijoesESimilares), 
                                        new RefeicaoGrupo(dataNascimento, tipoRefeicao, TipoGrupoRefeicao.Frutas), 
                                        new RefeicaoGrupo(dataNascimento, tipoRefeicao, TipoGrupoRefeicao.VerdurasELegumes), 
                                        new RefeicaoGrupo(dataNascimento, tipoRefeicao, TipoGrupoRefeicao.CarnesEOvos), 
                                        new RefeicaoGrupo(dataNascimento, tipoRefeicao, TipoGrupoRefeicao.LeitesIogurtesEQueijos), 
                                        new RefeicaoGrupo(dataNascimento, tipoRefeicao, TipoGrupoRefeicao.OleosEGorduras), 
                                        new RefeicaoGrupo(dataNascimento, tipoRefeicao, TipoGrupoRefeicao.AcucaresEDoces),
                                        new RefeicaoGrupo(dataNascimento, tipoRefeicao, TipoGrupoRefeicao.Bebidas)
                                    };
        }

        [JsonIgnore]
        public bool TipoRefeicaoRealizada
        {
            get
            {
                return RefeicoesGrupos.Any(r => r.RefeicaoRealizada);
            }
        }

        public int CountRefeicoes(TipoGrupoRefeicao tipoGrupoRefeicao)
        {
            var refeicaoTipoGrupo = RefeicoesGrupos.FirstOrDefault(r => r.TipoGrupoRefeicao == tipoGrupoRefeicao);

            if (refeicaoTipoGrupo != null)
            {
                var refeicoesCount = refeicaoTipoGrupo.Alimentos.Count;
                return refeicoesCount + ((refeicaoTipoGrupo.IdRefeicaoGrupo != 0 && refeicoesCount == 0) ? 1 : 0);
            }

            return 0;
        }
    }
}
