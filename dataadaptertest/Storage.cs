using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataadaptertest
{
    public class Storage
    {
        const string CONNESCTION_STRING = @"data source=dbmachine\DURR_SYSTEMS;Persist Security Info=false;database=EMOS_WEB;user id=User;password=user;Connection Timeout = 5";

        public static DataTable GetTable(string tablename)
        {
            DataTable dt = new DataTable(tablename);
            using (SqlConnection cn = new SqlConnection(CONNESCTION_STRING))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                string sqlcmd = string.Format("SELECT * FROM {0}", tablename);
                da.SelectCommand = new SqlCommand(sqlcmd, cn);
                da.Fill(dt);
            }
            return dt;
        }

        public static void UpdateMFPTable(DataTable table)
        {
            using (SqlConnection cn = new SqlConnection(CONNESCTION_STRING))
            {
                SqlDataAdapter da = GetMFPDataAdapter(cn);
                da.Update(table);
            }
        }

        private static SqlDataAdapter GetMFPDataAdapter(SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT * FROM DMS_MFP", connection);
            da.SelectCommand = command;

            command = new SqlCommand("INSERT INTO Customers (Id, homeskid) VALUES (@Id, @homeskid)", connection);
            command.Parameters.Add("@Id", SqlDbType.Int, 5, "Id");
            command.Parameters.Add("@homeskid", SqlDbType.NVarChar, 10, "homeskid");
            da.InsertCommand = command;

            command = new SqlCommand("UPDATE DMS_MFP SET homeskid = @homeskid WHERE Id = @Id", connection);
            command.Parameters.Add("@homeskid", SqlDbType.NVarChar, 10, "homeskid");
            SqlParameter parameter = command.Parameters.Add("@Id", SqlDbType.Int, 5, "Id");
            parameter.SourceVersion = DataRowVersion.Original;
            da.UpdateCommand = command;

            command = new SqlCommand("DELETE FROM DMS_MFP WHERE Id = @Id", connection);
            parameter = command.Parameters.Add("@Id", SqlDbType.Int, 5, "Id");
            parameter.SourceVersion = DataRowVersion.Original;
            da.DeleteCommand = command;

            return da;
        }
    }
}
