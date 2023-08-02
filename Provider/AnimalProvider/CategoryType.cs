
using System.Data.SqlClient;

namespace AnimalProvider
{
    public class CategoryType : Core.Disposable
    {
        public List<Entities.CategoryType> getAllCategories()
        {
            using var DAL = new DataAccess.DataAccessLayer();
            return DAL.ExecuteReader<Entities.CategoryType>("animal.spGetAllCategories");
        }

        public Entities.CategoryType getCategoryType(int id)
        {
            using var DAL = new DataAccess.DataAccessLayer();

            DAL.Parameters = new List<SqlParameter>
            {
                new SqlParameter{ParameterName = "@Id", Value = id}
            };

            return DAL.ExecuteReader<Entities.CategoryType>("animal.spGetCategoryType").FirstOrDefault();
        }

        public bool addCategoryType(Entities.CategoryType entity)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@CategoryName", Value =  entity.CategoryName},
                new SqlParameter{ParameterName = "@ImagePath", Value = entity.ImagePath}
            };

            return DAL.ExecuteNonQuery("animal.spAddCategoryType");
        }

        public bool updateCategoryType(Entities.CategoryType entity)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value = entity.Id},
                new SqlParameter{ ParameterName = "@CategoryName", Value =  entity.CategoryName},
                new SqlParameter{ParameterName = "@ImagePath", Value = entity.ImagePath}
            };

            return DAL.ExecuteNonQuery("animal.spUpdateCategoryType");
        }

        public bool deleteCategoryType(int id)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value = id}
            };

            return DAL.ExecuteNonQuery("animal.spDeleteCategoryType");
        }

    }
}
