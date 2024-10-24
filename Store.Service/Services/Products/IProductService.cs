﻿using Store.Service.Services.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Products
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int? id);
        Task<IReadOnlyList<ProductDto>> GetAllProductsAsync();
        Task<IReadOnlyList<BrandtypeDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandtypeDetailsDto>> GetAllTypesAsync();
    }
}
