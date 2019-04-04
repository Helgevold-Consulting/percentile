using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace API
{
    public class PercentileDataAccess : IPercentileDataAccess
    {
        private readonly IConfiguration _config;

        public PercentileDataAccess(IConfiguration config)
        {
            this._config = config;
        }

        public List<double> GetPercentileData() 
        {
            var data = new List<double>();
            using (var conn = new NpgsqlConnection(this._config.GetSection("Connection").Value))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("select * from public.get_data();", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetDouble(0));
                    }
                }
            }

            return data;
        }
    }
}
