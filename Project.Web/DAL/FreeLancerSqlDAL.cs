using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Web.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Project.Web.DAL
{
    public class FreeLancerSqlDAL : IFreeLancerDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["FreeLancerDB"].ConnectionString;

        const string SQL_GetLastTimeCard = "select * from timecard where user_name = @username ORDER BY end_datetime DESC;";
        const string SQL_ClockIn = "INSERT INTO timecard VALUES(@user_name, NULL, GETDATE(), NULL, null, 0);";
        const string SQL_ClockOut = "UPDATE timecard SET end_datetime = GetDate() WHERE user_name = @username AND end_datetime is NULL;"; //Whats the IDENTITY thing for SQL that I cant seem to find/Know what it is?
        const string SQL_CanClockIn = "SELECT * FROM timecard WHERE user_name = @Username AND end_datetime is NULL;";


        public bool CanClockIn(string userName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_CanClockIn, conn);
                    cmd.Parameters.AddWithValue("@username", userName);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read() == true)
                    {
                        return false;
                    }
                    else 
                    {
                        return true;
                    }

                }
            }
            catch(SqlException)
            {
                throw;
            }
        }

        public List<TimeCard> GetLastTimeCard(string userName)
        {
            List<TimeCard> output = new List<TimeCard>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetLastTimeCard, conn);
                    cmd.Parameters.AddWithValue("@username", userName);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TimeCard t = new TimeCard();
                        t.Id = Convert.ToInt32(reader["id"]);
                        t.Username = Convert.ToString(reader["user_name"]);
                        t.Project = Convert.ToString(reader["project"]);
                        t.ClockIn = Convert.ToDateTime(reader["start_datetime"]);

                        if(reader["end_datetime"] == DBNull.Value)
                        {
                            t.ClockOut = null;
                        }
                        else
                        {
                            t.ClockOut = Convert.ToDateTime(reader["end_datetime"]);

                        }
                        t.Notes = Convert.ToString(reader["notes"]);

                        if (reader["rate"] == DBNull.Value)
                        {
                            t.Rate = null;
                        }
                        else
                        {
                            t.Rate = Convert.ToDouble(reader["rate"]);
                        }

                        
                        output.Add(t);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return output;
        }

        public void ClockIn(TimeCard t) // clockIn and ClockOut should probably be One single method that says 
            // If("start_datetime = null") then clock in else("end_datetime = null") then clock out

        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ClockIn, conn);
                    cmd.Parameters.AddWithValue("@user_name", t.Username);
                    cmd.ExecuteNonQuery();
                    return;


                }
            }
            catch(SqlException )
            {
                throw;
            }
           
        }
        public void ClockOut(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ClockOut, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    //cmd.Parameters.AddWithValue("@project", DBNull.Value);
                    //cmd.Parameters.AddWithValue("@start_datetime", DBNull.Value);
                    //cmd.Parameters.AddWithValue("@end_date", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@notes", DBNull.Value);
                    //cmd.Parameters.AddWithValue("@rate", DBNull.Value);
                    cmd.ExecuteNonQuery();
                    return;

                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

    }
}