﻿using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entity;
using Store.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        =>await _context.Set<TEntity>().AddAsync(entity); 

        public void Delete(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        =>await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(Tkey? id)
        => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);
    }
}
