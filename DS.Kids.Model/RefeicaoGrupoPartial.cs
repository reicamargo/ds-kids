using System;
using System.Collections.Generic;
using System.Linq;

using DS.Kids.Model.Support;

using Newtonsoft.Json;

namespace DS.Kids.Model
{
    public partial class RefeicaoGrupo : BaseModel
    {
        private bool _sugerido;

        private TipoGrupoRefeicao _tipoGrupoRefeicao;

        public RefeicaoGrupo(DateTime dataNascimentoCrianca, TipoRefeicao grupoRefeicao, TipoGrupoRefeicao tipoGrupoRefeicao)
        {
            TipoGrupoRefeicao = tipoGrupoRefeicao;
            Sugerido = GetSugerido(dataNascimentoCrianca, grupoRefeicao, tipoGrupoRefeicao);
            Alimentos = new List<Alimento>();
        }

        public static bool GetSugerido(DateTime dataNascimentoCrianca, TipoRefeicao grupoRefeicao, TipoGrupoRefeicao tipoGrupoRefeicao)
        {
            Dictionary<TipoRefeicao, List<TipoGrupoRefeicao>> gruposSugeridos;
            if (dataNascimentoCrianca.GetIdade() <= 3)
            {
                gruposSugeridos = Diario.GruposSugeridos2A3;
            }
            else
            {
                gruposSugeridos = Diario.GruposSugeridos4A10;
            }

            List<TipoGrupoRefeicao> grupoSugeridos;
            if (gruposSugeridos.TryGetValue(grupoRefeicao, out grupoSugeridos))
            {
                return grupoSugeridos.Contains(tipoGrupoRefeicao);
            }

            return false;
        }

        public TipoGrupoRefeicao TipoGrupoRefeicao
        {
            get
            {
                return _tipoGrupoRefeicao;
            }
            set
            {
                _tipoGrupoRefeicao = value;
                Notify();
            }
        }

        public bool Sugerido
        {
            get
            {
                return _sugerido;
            }
            set
            {
                _sugerido = value;
                Notify();
            }
        }

        [JsonIgnore]
        public bool RefeicaoRealizada
        {
            get
            {
                return Alimentos.Any() || IdRefeicaoGrupo != 0;
            }
        }
    }
}
