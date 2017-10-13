using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AjaxAndWebApi
{
    public class BrugerFac : AutoFac<Bruger>
    {
        public Bruger Login(string email, string adgangskode)
        {
            Bruger b = new Bruger();
            Mapper<Bruger> mapper = new Mapper<Bruger>();

            using (var CMD = new SqlCommand("select * from Bruger where Email=@email and Password=@adgangskode", Conn.CreateConnection()))
            {
                CMD.Parameters.AddWithValue("@email", email);
                CMD.Parameters.AddWithValue("@adgangskode", adgangskode);

                var r = CMD.ExecuteReader();

                if (r.Read())
                {
                    b = mapper.Map(r);
                }

            }

            return b;

        }
    }
}