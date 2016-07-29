using System;

namespace DS.Kids.Model.Repositories
{
    /// <summary>
    /// Exceção que deve ser lançada toda vez que uma entidade for duplicada em um repositório
    /// </summary>
    public class DuplicateEntityException : Exception { }
}
