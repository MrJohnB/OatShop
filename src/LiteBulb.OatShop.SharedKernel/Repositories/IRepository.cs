﻿namespace LiteBulb.OatShop.SharedKernel.Repositories;
public interface IRepository<T>
{
    Task<ICollection<T>> GetAsync();
    Task<T?> GetAsync(int id);
    Task<T> AddAsync(T model);
    Task<int?> UpdateAsync(T model);
    Task<int?> DeleteAsync(int id);
}
