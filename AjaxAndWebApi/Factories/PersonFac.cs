using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace AjaxAndWebApi
{
    public class PersonFac:AutoFac<Person>
    {

        public List<Person> GetPersons(int From, int To)
        {
            string strSQL = "Select * from (select row_number() over(ORDER BY ID DESC) as row_number, * from Person) T where row_number BETWEEN " + From + " AND " + To;

            using (var cmd = new SqlCommand(strSQL, Conn.CreateConnection()))
            {
                var mapper = new Mapper<Person>();
                List<Person> list = mapper.MapList(cmd.ExecuteReader());
                cmd.Connection.Close();
                return list;
            }

        }
    }
}