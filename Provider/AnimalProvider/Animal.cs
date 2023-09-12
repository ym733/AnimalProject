
using System.Data.SqlClient;

namespace AnimalProvider
{
    public class Animal : Core.Disposable
    {
        public List<Entities.Animal> getAllAnimals()
        {
            using var DAL = new DataAccess.DataAccessLayer();
            return DAL.ExecuteReader<Entities.Animal>("Animal.spGetAllAnimals");
        }

        public Entities.Animal getAnimal(int id)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter>
                {
                    new SqlParameter{ ParameterName = "@Id", Value = id }
            };

            return DAL.ExecuteReader<Entities.Animal>("Animal.spGetAnimal").FirstOrDefault();
        }

        public bool addAnimal(ViewModel.Animal entity)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Name", Value =  entity.Name },
                new SqlParameter{ ParameterName = "@age", Value =  entity.age },
                new SqlParameter{ ParameterName = "@CategoryID", Value =  entity.CategoryID }
            };

            return DAL.ExecuteNonQuery("Animal.spAddAnimal");
        }

        public bool updateAnimal(ViewModel.Animal entity)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value =  entity.ID },
                new SqlParameter{ ParameterName = "@Name", Value =  entity.Name },
                new SqlParameter{ ParameterName = "@age", Value =  entity.age },
                new SqlParameter{ ParameterName = "@CategoryID", Value =  entity.CategoryID }
            };

            return DAL.ExecuteNonQuery("Animal.spUpdateAnimal");
        }

        public bool deleteAnimal(int id)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value =  id }
            };

            return DAL.ExecuteNonQuery("Animal.spDeleteAnimal");
        }
    }
}
