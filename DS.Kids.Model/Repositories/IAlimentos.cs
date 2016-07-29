﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface IAlimentos
    {
        Task<Alimento> ObterPorId(int idAlimento);
        Task<IEnumerable<Alimento>> ObterPorGrupoAlimentar(int idGrupo);
    }
}
