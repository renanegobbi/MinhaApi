using System;

namespace MinhaApi.Core.Data
{
    public interface IRepository<T> : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}