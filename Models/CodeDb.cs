using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
//using System.ComponentModel;
//using System.Web.Mvc;

namespace AngApp.Models
{
    public class CodeDb
    {
        public SqlConnection con = new SqlConnection(@WebConfigurationManager.ConnectionStrings["dbrpConnectionString"].ToString());
        public bool Open(string Connection = "dbrpConnectionString")
        {

            try
            {
                bool o = true;
                if (con.State.ToString() != "open")
                {

                    con.Open();
                }
                return o;
            }
            catch (SqlException)
            {
                con.Close();
                return false;
            }
        }
        public bool Close()
        {
            try
            {

                return true;
            }
            catch (SqlException )
            {
                return false;
            }

        }
        public DataTable SqlDatatbl(string sqlstr)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand
            {
                CommandText = sqlstr
            };
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter
            {
                SelectCommand = cmd
            };
            cmd.Connection = con;
            sda.Fill(dt);
            con.Close();
            return dt;
        }

        public int SqlInsert(string sqlstr)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand
            {
                CommandText = sqlstr,
                Connection = con
            };
            con.Open();
            i = Convert.ToInt16(cmd.ExecuteNonQuery());
            con.Close();
            return i;
        }
        SqlCommand cmd;
        public double SqlScalar(string sqlstr)
        {
            double i = 0;
            cmd = new SqlCommand
            {
                CommandText = sqlstr,
                Connection = con
            };
            con.Open();
            i = Convert.ToDouble(cmd.ExecuteScalar());
            con.Close();
            return i;
        }

        public class CodeDbs

        {
            public string ACCT_CODE { get; set; }
            public string ACCT_DESC { get; set; }
            public string OPN_BAL { get; set; }
            public decimal CLS_BAL { get; set; }
            public decimal TOTAL_CR { get; set; }
            public decimal TOTAL_DR { get; set; }
        }

        public class Sales
        {
            public string INV_NUM { get; set; }
            public DateTime INV_DATE { get; set; }
            public string LOC_CODE { get; set; }
            public string LOC_NAME { get; set; }
            public string CUST_NAME { get; set; }
            public decimal NETAMT { get; set; }
        }

        public class Country
        {
            public string CN_CD { get; set; }
            public string CN_NAME { get; set; }
            public int CN_CURR_ID { get; set; }
        }

        public class Department
        {
            public string DP_CD { get; set; }
            public string DP_NAME { get; set; }
        }

        public class Designation
        {
            public string DS_CD { get; set; }
            public string DS_NAME { get; set; }
        }

        public class Employee {
            public string COMPCD { get; set; }
            public string EM_CODE { get; set; }
            public string EM_NAME { get; set; }
            public string EM_LASTNAME { get; set; }
            public string EM_ADDRESS { get; set; }
            public string EM_PHONE { get; set; }
            public string EM_GENDER { get; set; }
            public DateTime EM_DOB { get; set; }
            public DateTime EM_DOJ { get; set; }
            public string EM_PHOTO { get; set; }
            public string EM_COUNTRY { get; set; }
            public Boolean EM_ACTIVE { get; set; }
        }

        //public class OrderVM
        //{
        //    public OrderMaster order { get; set; }
        //    public List<OrderDetail> orderDetails { get; set; }
        //}

        //[Required]
        //[Display(Name = "User name")]
        public string UserName { get; set; }
        public string INV_NUM { get; set; }

        public class Area
        {
            public string Area_code { get; set; }
            public string Area_name { get; set; }
        }

        public class Category
        {
            public string Cat_code { get; set; }
            public string Cat_Desc { get; set; }
        }
        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        // [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public DataTable IsValid(string _username, string _password)
        {
            bool flg = new bool();
            DataTable dt = new DataTable();
            string _sql = @" select FilePath,EmpName,username,passwd from PASSWORD A INNER JOIN COMPANY B ON A.COMPCD=B.CM_COMPCD " +
                       @" WHERE [Username] = @u AND [Passwd] = @p";
            var cmd = new SqlCommand(_sql, con);
            cmd.Parameters
                .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                .Value = _username;
            cmd.Parameters
                .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                .Value = _password;
            con.Open();
            var reader = cmd.ExecuteReader();            
            if (reader.HasRows)
            {
                flg = true;
            }
            else
            { flg = false; }
            reader.Dispose();
            cmd.Dispose();
            con.Close();

            if (flg == true) { 
            _sql = @" select cm_path + FilePath FilePath,EmpName,username,passwd from PASSWORD A INNER JOIN COMPANY B ON A.COMPCD=B.CM_COMPCD " +
                     @" WHERE [Username] = '"+ _username +"'";
                dt = SqlDatatbl(_sql);                
                return dt;
            }
            else
            {
                return dt;
            }
        }
        //public bool IsGrid(string _invNum)
        //{

        //    //string _sql = @"select count(*) as cnt from credit_invoice where inv_num=@u";
        //    //var cmd = new SqlCommand(_sql, con);
        //    //cmd.Parameters
        //    //    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
        //    //    .Value = _invNum;
        //    //con.Open();
        //    //int reader = Convert.ToInt16(cmd.ExecuteScalar());
        //    //if (reader>0)
        //    //{                
        //    //cmd.Dispose();
        //    return true;
        //    //}
        //    //else
        //    //{                
        //    //    cmd.Dispose();
        //    //    return false;
        //    //}
        //}
        public abstract class CommonViewModel
        {
            public string CompanyName { get; set; }

            public class City
            {
                public int Id { get; set; }
                public string Name { get; set; }

            }

        }


    }


}