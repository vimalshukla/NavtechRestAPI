using AutoMapper;
using Database;
using Models.Customer;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace Services.Helper
{
    public static class ModelMapper
    {

        /// <summary>
        ///   Translate to destination type.
        /// </summary>
        /// <param id="source">Entity/Model to translate.</param>
        /// <returns>returns translated entity or model.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static TDest Map<TDest>(object source)
        {
            Mapper.DynamicMap<TDest>(source);
            TDest tDest = Mapper.Map<TDest>(source);
            return tDest;
        }


        /// <summary>
        /// This functions converts DataRow to POCO List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ReadData<T>(DataTable dt)
        {
            return Mapper.DynamicMap<IDataReader, List<T>>(dt.CreateDataReader());
        }
    }
}

