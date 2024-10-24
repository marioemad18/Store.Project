﻿using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entity;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompletedAsync()
        => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (_repositories is null)
            {
                _repositories = new Hashtable();
            }
            var entityKey = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(entityKey))
            {
                var repositoeyType = typeof(GenericRepository<,>); 
                var RepositoryInstance = Activator.CreateInstance(repositoeyType.MakeGenericType(typeof(TEntity),typeof(TKey)),_context);
                _repositories.Add(entityKey, RepositoryInstance);
            }
            return (IGenericRepository<TEntity, TKey>)_repositories[entityKey];
        }
    }
}
