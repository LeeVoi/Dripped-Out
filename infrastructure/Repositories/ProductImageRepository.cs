using System.Data;
using infrastructure.DatabaseManager;
using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
using infrastructure.Repositories.Interface;
using Npgsql;

namespace infrastructure.Repositories;

public class ProductImageRepository : ICrud<ProductImage>, IProductImageMapper
{
    private DBConnection _dbConnection;
    public ProductImageRepository(IDBConnection dbConnection)
    {
        _dbConnection = new DBConnection();
    }

    public ProductImage Create(ProductImage productImage)
    {
        using (var con = _dbConnection.GetConnection())
        {
            con.Open();
            const string sql =
                "INSERT INTO productimage(productid, colorid, bloburl) VALUES (@productid, @colorid, @bloburl)";
            using (var command = new NpgsqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@productid", productImage.ProductId);
                command.Parameters.AddWithValue("@colorid", productImage.ColorId);
                command.Parameters.AddWithValue("@bloburl", productImage.BlobUrl);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ProductImage
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("productid")),
                            ColorId = reader.GetInt32(reader.GetOrdinal("colorid")),
                            BlobUrl = reader.GetString(reader.GetOrdinal("bloburl"))
                        };
                    }
                }
            }
        }

        return null;
    }

    public ProductImage Read(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(ProductImage item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public ProductImage getProductImage(int productId, int colorId)
    {
        using (var con = _dbConnection.GetConnection())
        {
            con.Open();
            const string sql = "SELECT * FROM productimage WHERE productid = @productid AND colorid = @colorid";

            using (var command = new NpgsqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@productid", productId);
                command.Parameters.AddWithValue("@colorid", colorId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ProductImage
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("productid")),
                            ColorId = reader.GetInt32(reader.GetOrdinal("colorid")),
                            BlobUrl = reader.GetString(reader.GetOrdinal("bloburl"))
                        };
                    }
                }
            }
        }

        return null;
    }

    public IEnumerable<ProductImage> getAllProductsImages(int productId)
    {
        List<ProductImage> productImages = new List<ProductImage>();
        using (var con = _dbConnection.GetConnection())
        {
            con.Open();
            const string sql = "SELECT * FROM productimage WHERE productid = @productid";

            using (var command = new NpgsqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@productid", productId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductImage productImage = new ProductImage
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("productid")),
                            ColorId = reader.GetInt32(reader.GetOrdinal("colorid")),
                            BlobUrl = reader.GetString(reader.GetOrdinal("bloburl"))
                        };
                        productImages.Add(productImage);
                    }
                }
            }
        }
        return productImages;
    }
}