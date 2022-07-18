using System;

namespace MinhaApi.Core.Data
{
    /// Contrato para utilização do padrão "Unit Of Work"
    public interface IUnitOfWork : IDisposable
    {
        /// Realiza o commit de uma transação no banco de dados.
        bool CommitTransaction();
    }
}