using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EContact.EContactClasses
{
    class EContactClass
    {
        public int ContactID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        static string my_connection_string = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;

        // selecting the data in the database
        public DataTable Select()
        {
            // step 1: database connection
            SqlConnection conn = new SqlConnection(my_connection_string);
            DataTable dt = new DataTable();

            try
            {
                // step 2: writing the sql query
                string sql = "SELECT * FROM EContactDbTable";

                // create the sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // opens the DB connection
                conn.Open();
                adapter.Fill(dt);


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }


        // insert data into database
        public bool Insert(EContactClass c)
        {
            bool isSuccess = false;

            // step 1: database connection
            SqlConnection conn = new SqlConnection(my_connection_string);

            try
            {
                // step 2: writing the sql query
                string sql = "INSERT INTO EContactDbTable (FirstName, LastName, Address, PhoneNumber, Gender) VALUES (@FirstName, @LastName, @Address, @PhoneNumber, @Gender)";

                // create the sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@PhoneNumber", c.PhoneNumber);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                // opens the DB connection
                conn.Open();

                // execute the query, if successful than value of rows will be > 0
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    isSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        // update data in database
        public bool Update(EContactClass c)
        {
            bool isSuccess = false;

            // step 1: database connection
            SqlConnection conn = new SqlConnection(my_connection_string);

            try
            {
                // step 2: writing the sql query
                string sql = "UPDATE EContactDbTable SET FirstName=@FirstName, LastName=@LastName, Address=@Address, PhoneNumber=@PhoneNumber, Gender=@Gender WHERE ContactId=@ContactId";
                               
                // create the sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@PhoneNumber", c.PhoneNumber);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactId", c.ContactID);

                // opens the DB connection
                conn.Open();

                // execute the query, if successful than value of rows will be > 0
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    isSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        // update data in database
        public bool Delete(EContactClass c)
        {
            bool isSuccess = false;

            // step 1: database connection
            SqlConnection conn = new SqlConnection(my_connection_string);

            try
            {
                // step 2: writing the sql query
                string sql = "DELETE FROM EContactDbTable WHERE ContactId=@ContactId";

                // create the sql command
                SqlCommand cmd = new SqlCommand(sql, conn);                
                cmd.Parameters.AddWithValue("@ContactId", c.ContactID);

                // opens the DB connection
                conn.Open();

                // execute the query, if successful than value of rows will be > 0
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    isSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
    }
}
