using System;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
 

namespace NerveCentre
{
    public class Common
    {
        public static string strServerName;
        public SqlConnection conSQL = new SqlConnection ();
        public SqlTransaction sqlTxn;
        public string constr = ConfigurationManager.ConnectionStrings["NerveCentreEntities"].ConnectionString;
        public string ConStrRefinedForReports;
        public static int MID = 0;
        public static string controller;
        public static string method;
        public static string message;
        

        public bool ConnectDB() //connect to Database
        {
            if ( constr.ToLower ().StartsWith ( "metadata=" ) )
            {
                EntityConnectionStringBuilder RefineConStr = new EntityConnectionStringBuilder ( constr );
                constr = RefineConStr.ProviderConnectionString;
            }

            if ( string.IsNullOrEmpty ( conSQL.ConnectionString ) )
            {
                conSQL.ConnectionString = constr;
            }
            if ( conSQL.State == ConnectionState.Closed )
            {
                conSQL.Open ();
            }
            return true;
        }

        public bool DisconnetDB() //method to disconnect connection from database
        {
            if ( conSQL.State == ConnectionState.Open )
            {
                if ( sqlTxn == null )
                {
                    conSQL.Close ();
                }
            }
            return true;
        }

        public bool BeginTrans() //Method to begin transactions to database
        {
            ConnectDB ();
            sqlTxn = conSQL.BeginTransaction ();
            return true;
        }

        public bool CommitTrans() //Method to commit transactions to databse
        {
            if ( ( sqlTxn.Connection != null ) )
            {
                sqlTxn.Commit ();
            }

            sqlTxn = null;
            DisconnetDB ();

            return true;
        }

        public bool RollbackTrans() //MEthod to rollbacke from database in a wrong entry or a failure
        {
            if ( ( sqlTxn.Connection != null ) )
            {
                sqlTxn.Rollback ();
            }

            sqlTxn = null;
            DisconnetDB ();

            return true;
        }

        public DataSet ReturnDataSet( string strSqlCommand ) //method to return data from database to form
        {
            SqlCommand sqlCmd = default ( SqlCommand );
            SqlDataAdapter daComman = new SqlDataAdapter ();
            DataSet dsComman = new DataSet ();

            ConnectDB ();

            sqlCmd = new SqlCommand ( strSqlCommand , conSQL );
            daComman.SelectCommand = sqlCmd;
            daComman.Fill ( dsComman , "MySqlTable" );

            DisconnetDB ();

            return dsComman;
        }

        public int ExecuteProcedure( string CommandText , CommandType CommandType , SqlParameter[] Parameters ) //method to execute stored procedures
        {
            SqlCommand sqlComm = default ( SqlCommand );
            int res = 0;
            ConnectDB ();
            //Open the connection before execute the procedure

            sqlComm = new SqlCommand ( CommandText , conSQL );
            sqlComm.CommandType = CommandType;

            sqlComm.Transaction = sqlTxn;
            //Set the transaction property of the SQL Command

            if ( ( Parameters != null ) )
            {
                for ( int i = 0 ; i <= Parameters.Length - 1 ; i++ )
                {
                    if ( Parameters[i].Value == null )
                    {
                        Parameters[i].Value = DBNull.Value;
                        sqlComm.Parameters.Add ( Parameters[i] ).Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        sqlComm.Parameters.Add ( Parameters[i] );
                    }
                }
            }

            res = sqlComm.ExecuteNonQuery ();

            DisconnetDB ();
            //Close the connection after executing the procedure
            return res;
        }

        public bool TestDbConnection( string ServerName ) //method to test the conection of the database
        {
            strServerName = ServerName;
            return ConnectDB ();
        }

        public void ExceptionHandling()
        {
            BeginTrans ();
            SqlParameter[] Parameter = new SqlParameter[4];
            Parameter[0] = new SqlParameter ( "@ControllerName" , controller);
            Parameter[1] = new SqlParameter ( "@MethodName" , method);
            Parameter[2] = new SqlParameter ( "@LogMessage" ,message);
            Parameter[3] = new SqlParameter ( "@CreatedDate" , DateTime.Now);
            int saveRec = 0;
            saveRec = ExecuteProcedure ( "SP_SystemLogs" , CommandType.StoredProcedure , Parameter );
            CommitTrans ();
        }
    }
}