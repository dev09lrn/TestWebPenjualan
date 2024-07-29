using Dapper;
using TestWebPenjualan.Domain.Dtos.Product;
using TestWebPenjualan.Infrastructure.Interfaces;
using TestWebPenjualan.Infrastructure.Persistance.Dapper;

namespace TestWebPenjualan.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly TestWebPenjualanDbContext _dbContext;

    public ProductRepository(TestWebPenjualanDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                SELECT  a.[ProductId]
		                ,a.[ProductCode]
                        ,a.[Name]
                        ,a.[UnitTypeId]
                        ,b.[Name] AS UnitTypeName
	                    ,a.[CategoryId]
                        ,c.[Name] AS CategoryName
	                    ,a.[BrandId]
                        ,d.[Name] AS BrandName
                        ,a.[Barcode]
	                    ,a.[Price]
                        ,a.[CreatedDate]
                        ,a.[CreatedBy]
                        ,a.[ModifiedDate]
                        ,a.[ModifiedBy]		  
                FROM MsProduct a
                INNER JOIN MsUnitType b ON a.[UnitTypeId] = b.[UnitTypeId]  
                INNER JOIN MsCategory c ON a.[CategoryId] = c.[CategoryId]  
                INNER JOIN MsBrand d ON a.[BrandId] = d.[BrandId]             
                ";

            var queryParams = new { };

            var result = await context.QueryAsync<Product>(query, queryParams);

            return result;
        }
    }

    public async Task<Product?> GetProductByProductId(int productId)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                SELECT  a.[ProductId]
                        ,a.[ProductCode]
                        ,a.[Name]
                        ,a.[UnitTypeId]
                        ,b.[Name] AS UnitTypeName
	                    ,a.[CategoryId]
                        ,c.[Name] AS CategoryName
	                    ,a.[BrandId]
                        ,d.[Name] AS BrandName
                        ,a.[Barcode]
	                    ,a.[Price]
                        ,a.[CreatedDate]
                        ,a.[CreatedBy]
                        ,a.[ModifiedDate]
                        ,a.[ModifiedBy]		  
                FROM MsProduct a
                INNER JOIN MsUnitType b ON a.[UnitTypeId] = b.[UnitTypeId]  
                INNER JOIN MsCategory c ON a.[CategoryId] = c.[CategoryId]  
                INNER JOIN MsBrand d ON a.[BrandId] = d.[BrandId]                         
                WHERE  a.[ProductId]=@ProductId
                ";

            var queryParams = new
            {
                ProductId = productId
            };

            var result = await context.QueryFirstOrDefaultAsync<Product>(query, queryParams);

            return result;
        }
    }

    public async Task<int> CreateProduct(CreateProductDto productToAdd)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                INSERT INTO [dbo].[MsProduct]
                   ([ProductCode]
                   ,[Name]
                   ,[UnitTypeId]
                   ,[CategoryId]
                   ,[BrandId]
                   ,[Barcode]
                   ,[Price]
                   ,[CreatedDate]
                   ,[CreatedBy])
                 VALUES
                    (@ProductCode
                    ,@Name
                    ,@UnitTypeId
                    ,@CategoryId
                    ,@BrandId
                    ,@Barcode
                    ,@Price
                    ,@CreatedDate
                    ,@CreatedBy)

                SELECT CAST(SCOPE_IDENTITY() AS INT);

                ";

            var queryParams = new
            {
                ProductCode = productToAdd.ProductCode,
                Name = productToAdd.Name,
                UnitTypeId = productToAdd.UnitTypeId,
                CategoryId = productToAdd.CategoryId,
                BrandId = productToAdd.BrandId,
                Barcode = productToAdd.Barcode,
                Price = productToAdd.Price,
                CreatedDate = productToAdd.CreatedDate,
                CreatedBy = productToAdd.CreatedBy
            };

            var lastInsertId = await context.QuerySingleAsync<int>(query, queryParams);

            return lastInsertId;
        }

    }

    public async Task<int> UpdateProduct(int productId, UpdateProductDto productToUpdate)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                UPDATE [dbo].[MsProduct] 
                    SET [ProductCode]=@ProductCode,
                       [Name]=@Name,
                       [UnitTypeId]=@UnitTypeId,
                       [CategoryId]=@CategoryId,
                       [BrandId]=@BrandId,
                       [Barcode]=@Barcode,
                       [Price]=@Price,
                       [ModifiedDate]=@ModifiedDate,
                       [ModifiedBy]=@ModifiedBy
                     WHERE [ProductId]=@ProductId
                ";

            var queryParams = new
            {
                ProductId = productId,
                ProductCode = productToUpdate.ProductCode,
                Name = productToUpdate.Name,
                UnitTypeId = productToUpdate.UnitTypeId,
                CategoryId = productToUpdate.CategoryId,
                BrandId = productToUpdate.BrandId,
                Barcode = productToUpdate.Barcode,
                Price = productToUpdate.Price,
                ModifiedDate = productToUpdate.ModifiedDate,
                ModifiedBy = productToUpdate.ModifiedBy
            };

            var result = await context.ExecuteAsync(query, queryParams);

            return result;
        }
    }

    public async Task<int> DeleteProduct(int productId)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                DELETE FROM [dbo].[MsProduct] 
                WHERE [ProductId]=@ProductId
                ";

            var queryParams = new
            {
                ProductId = productId
            };

            var result = await context.ExecuteAsync(query, queryParams);

            return result;
        }
    }

    public async Task<IEnumerable<Product>> GetProductsWithPaging(GetProductsWithPagingFilter filter)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                SELECT  *
                FROM    ( 
		                    SELECT ROW_NUMBER() OVER ( ORDER BY a.[ProductCode] ) AS RowNum
		                            ,a.[ProductId]
		                            ,a.[ProductCode]
                                    ,a.[Name]
                                    ,a.[UnitTypeId]
                                    ,b.[Name] AS UnitTypeName
	                                ,a.[CategoryId]
                                    ,c.[Name] AS CategoryName
	                                ,a.[BrandId]
                                    ,d.[Name] AS BrandName
                                    ,a.[Barcode]
	                                ,a.[Price]
                                    ,a.[CreatedDate]
                                    ,a.[CreatedBy]
                                    ,a.[ModifiedDate]
                                    ,a.[ModifiedBy]		  
                            FROM MsProduct a
                            INNER JOIN MsUnitType b ON a.[UnitTypeId] = b.[UnitTypeId]  
                            INNER JOIN MsCategory c ON a.[CategoryId] = c.[CategoryId]  
                            INNER JOIN MsBrand d ON a.[BrandId] = d.[BrandId] 
                            WHERE LOWER(a.[Name]) LIKE @Name AND LOWER(a.[ProductCode]) LIKE @ProductCode
				                AND a.[UnitTypeId] LIKE @UnitTypeId
                        ) AS RowConstrainedResult
                WHERE   RowNum >= @RowNumberStart AND RowNum <= @RowNumberEnd
                ORDER BY RowNum
                ";

            var unitTypeIdParam = filter.UnitTypeId == 0 ? string.Empty : filter.UnitTypeId.ToString();
            var productCodeParam = filter.ProductCode == null ? "" : filter.ProductCode.ToLower().Trim();
            var nameParam = filter.Name == null ? "" : filter.Name.ToLower().Trim();

            var queryParams = new
            {
                ProductCode = $"%{productCodeParam}%",
                Name = $"%{nameParam}%",
                UnitTypeId = $"%{unitTypeIdParam}%",
                RowNumberStart = filter.Start + 1,
                RowNumberEnd = filter.Start + filter.Length
            };

            var result = await context.QueryAsync<Product>(query, queryParams);

            return result;
        }
    }

    public async Task<int> GetProductsWithPagingRowsCount(GetProductsWithPagingRowsCountFilter filter)
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                 SELECT COUNT(1) AS TotalRows FROM MsProduct
                 WHERE LOWER([Name]) LIKE @Name AND LOWER([ProductCode]) LIKE @ProductCode
				       AND [UnitTypeId] LIKE @UnitTypeId
                ";

            var unitTypeIdParam = filter.UnitTypeId == 0 ? string.Empty : filter.UnitTypeId.ToString();
            var productCodeParam = filter.ProductCode == null ? "" : filter.ProductCode.ToLower().Trim();
            var nameParam = filter.Name == null ? "" : filter.Name.ToLower().Trim();

            var queryParams = new
            {
                ProductCode = $"%{productCodeParam}%",
                Name = $"%{nameParam}%",
                UnitTypeId = $"%{unitTypeIdParam}%"
            };

            var result = await context.QueryFirstOrDefaultAsync<int>(query, queryParams);

            return result;
        }
    }

    public async Task<IEnumerable<UnitType>> GetUnitTypes()
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                  SELECT [UnitTypeId]
                      ,[Name]
                      ,[CreatedBy]
                      ,[CreatedDate]
                      ,[ModifiedBy]
                      ,[ModifiedDate]
                  FROM [MsUnitType]  
                ";

            var queryParams = new { };

            var result = await context.QueryAsync<UnitType>(query, queryParams);

            return result;
        }
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                  SELECT [CategoryId]
                      ,[Name]
                      ,[CreatedBy]
                      ,[CreatedDate]
                      ,[ModifiedBy]
                      ,[ModifiedDate]
                  FROM [MsCategory]  
                ";

            var queryParams = new { };

            var result = await context.QueryAsync<Category>(query, queryParams);

            return result;
        }
    }

    public async Task<IEnumerable<Brand>> GetBrands()
    {
        using (var context = _dbContext.OpenConnection())
        {
            var query = @"
                  SELECT [BrandId]
                      ,[Name]
                      ,[CreatedBy]
                      ,[CreatedDate]
                      ,[ModifiedBy]
                      ,[ModifiedDate]
                  FROM [MsBrand]  
                ";

            var queryParams = new { };

            var result = await context.QueryAsync<Brand>(query, queryParams);

            return result;
        }
    }
}
