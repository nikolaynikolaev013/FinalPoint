using System;
using Microsoft.Data.SqlClient;

namespace FinalPoint.Data
{
    public interface IDbService
    {
        public SqlDataReader RunProcedure(SqlCommand command);

    }
}
