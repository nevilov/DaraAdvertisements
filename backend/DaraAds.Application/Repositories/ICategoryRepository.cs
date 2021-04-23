﻿using DaraAds.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<Category> FindCategoryById(int id, CancellationToken cancellationToken);
    }
}
