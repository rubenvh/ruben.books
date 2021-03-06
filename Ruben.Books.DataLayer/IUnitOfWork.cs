﻿using System;
using System.Data.Entity;

namespace Ruben.Books.DataLayer
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        int Save();
        TContext Context { get; }

    }
}