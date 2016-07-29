using System;
using System.Collections.Generic;
using TiposSugeridos = System.Collections.Generic.Dictionary<DS.Kids.Model.TipoRefeicao, System.Collections.Generic.List<DS.Kids.Model.TipoGrupoRefeicao>>;

using DS.Kids.Model.Support;

namespace DS.Kids.Model
{
    public class Diario : BaseModel
    {
        public static readonly TiposSugeridos GruposSugeridos2A3 =
            new TiposSugeridos
                {
                    {
                        TipoRefeicao.CafeDaManha, new List<TipoGrupoRefeicao>
                                                      {
                                                          TipoGrupoRefeicao.CereaisTuberculosERaizes,
                                                          TipoGrupoRefeicao.LeitesIogurtesEQueijos,
                                                          TipoGrupoRefeicao.Frutas
                                                      }
                    },
                    {
                        TipoRefeicao.LancheDaManha, new List<TipoGrupoRefeicao>
                                                        {
                                                            TipoGrupoRefeicao.CereaisTuberculosERaizes,
                                                            TipoGrupoRefeicao.LeitesIogurtesEQueijos,
                                                            TipoGrupoRefeicao.Frutas
                                                        }
                    },
                    {
                        TipoRefeicao.Almoco, new List<TipoGrupoRefeicao>
                                                 {
                                                     TipoGrupoRefeicao.CereaisTuberculosERaizes,
                                                     TipoGrupoRefeicao.FeijoesESimilares,
                                                     TipoGrupoRefeicao.CarnesEOvos,
                                                     TipoGrupoRefeicao.VerdurasELegumes
                                                 }
                    },
                    {
                        TipoRefeicao.LancheDaTarde, new List<TipoGrupoRefeicao>
                                                        {
                                                            TipoGrupoRefeicao.CereaisTuberculosERaizes,
                                                            TipoGrupoRefeicao.LeitesIogurtesEQueijos,
                                                            TipoGrupoRefeicao.Frutas
                                                        }
                    },
                    {
                        TipoRefeicao.Jantar, new List<TipoGrupoRefeicao>
                                                 {
                                                     TipoGrupoRefeicao.CereaisTuberculosERaizes,
                                                     TipoGrupoRefeicao.CarnesEOvos,
                                                     TipoGrupoRefeicao.VerdurasELegumes,
                                                 }
                    },
                    {
                        TipoRefeicao.LancheDaNoite, new List<TipoGrupoRefeicao>
                                                        {
                                                            TipoGrupoRefeicao.Frutas
                                                        }
                    }
                };

        public static readonly TiposSugeridos GruposSugeridos4A10 =
            new TiposSugeridos
                {
                    {
                        TipoRefeicao.CafeDaManha, new List<TipoGrupoRefeicao>
                                                      {
                                                          TipoGrupoRefeicao.CereaisTuberculosERaizes,
                                                          TipoGrupoRefeicao.LeitesIogurtesEQueijos,
                                                          TipoGrupoRefeicao.Frutas
                                                      }
                    },
                    {
                        TipoRefeicao.LancheDaManha, new List<TipoGrupoRefeicao>
                                                        {
                                                            TipoGrupoRefeicao.CereaisTuberculosERaizes,
                                                            TipoGrupoRefeicao.LeitesIogurtesEQueijos,
                                                            TipoGrupoRefeicao.Frutas
                                                        }
                    },
                    {
                        TipoRefeicao.Almoco, new List<TipoGrupoRefeicao>
                                                 {
                                                     TipoGrupoRefeicao.CereaisTuberculosERaizes,
                                                     TipoGrupoRefeicao.FeijoesESimilares,
                                                     TipoGrupoRefeicao.CarnesEOvos,
                                                     TipoGrupoRefeicao.VerdurasELegumes
                                                 }
                    },
                    {
                        TipoRefeicao.LancheDaTarde, new List<TipoGrupoRefeicao>
                                                        {
                                                            TipoGrupoRefeicao.CereaisTuberculosERaizes,
                                                            TipoGrupoRefeicao.LeitesIogurtesEQueijos
                                                        }
                    },
                    {
                        TipoRefeicao.Jantar, new List<TipoGrupoRefeicao>
                                                 {
                                                     TipoGrupoRefeicao.CereaisTuberculosERaizes,
                                                     TipoGrupoRefeicao.CarnesEOvos,
                                                     TipoGrupoRefeicao.VerdurasELegumes,
                                                 }
                    },
                    {
                        TipoRefeicao.LancheDaNoite, new List<TipoGrupoRefeicao>
                                                        {
                                                            TipoGrupoRefeicao.Frutas
                                                        }
                    }
                };

        public RefeicaoDiario CafeDaManha { get; set; }
        public RefeicaoDiario LancheDaManha { get; set; }
        public RefeicaoDiario Almoco { get; set; }
        public RefeicaoDiario LancheDaTarde { get; set; }
        public RefeicaoDiario Jantar { get; set; }
        public RefeicaoDiario LancheDaNoite { get; set; }
    }
}
