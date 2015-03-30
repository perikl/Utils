using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using log4net;



namespace AutoCADTest
{
    public static class CRMHelper
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(CRMHelper));
        private static SqlConnection connection;
        public static SqlConnection connectDB(string connString)
        {
            connection = new SqlConnection(connString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                log.Error("Не удалось подключиться к CRM!");                
                return null;
            }
            return connection;
        }

        private static Dictionary<string, string> getDictionaryFromDB(string field2, string table)
        {
            var result = new Dictionary<string, string>();
            var conn = connectDB(DBConstants.ConnectionString);
            if (conn == null)
                return null;
            SqlCommand myCommand = new SqlCommand("SELECT [id],[" + field2 + "] FROM " + DBConstants.DataBase + "." + table, conn);
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                result.Add(myReader["id"].ToString(), myReader[field2].ToString());
            }
            connection.Close();
            return result;
        }

        public static Dictionary<string, string> getEventNamesDirectly()
        {
            return getDictionaryFromDB("Name", DBConstants.EventTable);
        }

        public static Dictionary<string,string> getEventNames()
        {
            return getDictionaryFromDB(DBConstants.EventNameTableNameRus, DBConstants.EventNameTable);
        }
        public static Dictionary<string, string> getStagesByEvent(string selectedEventId)
        {
            var result = new Dictionary<string,string>();
            var conn = connectDB(DBConstants.ConnectionString);
            if (conn == null)
                return null;
            SqlCommand myCommand = new SqlCommand(string.Format("SELECT [id],[Name] FROM {0}.{1} WHERE [{2}] = '{3}'",DBConstants.DataBase, DBConstants.ExpositionTable,DBConstants.ExpositionTableEventId,selectedEventId), conn);
            SqlDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                result.Add(myReader["id"].ToString(), myReader["Name"].ToString());
            }
            connection.Close();
            return result;
        }
/*
        public static bool fillStageFieldsByStageId(string selectedStageId, Stage stage)
        {
            var conn = connectDB(DBConstants.ConnectionString);
            if (conn != null)
            {
                SqlCommand myCommand = new SqlCommand(string.Format("SELECT * FROM {0}.{1} WHERE [id] = '{2}'", DBConstants.DataBase, DBConstants.ExpositionTable, selectedStageId), conn);
                SqlDataReader myReader = myCommand.ExecuteReader();
                myReader.Read();
                stage.author = myReader[DBConstants.EmployeeName].ToString();
                stage.carpetColor = myReader[DBConstants.CarpetName].ToString();
                stage.companyName = myReader[DBConstants.AccountName].ToString();
                stage.companyNameAtPlan = myReader[DBConstants.AccountName].ToString();
                stage.date = myReader[DBConstants.CreatedOn].ToString();
                stage.location = myReader[DBConstants.TSFacility].ToString();
                stage.orientation = myReader[DBConstants.TypeLayoutName].ToString();
                stage.plannedSquare = string.Format("{0} x {1} = {2}", myReader[DBConstants.WidthPlan].ToString(), myReader[DBConstants.LengthPlan].ToString(), myReader[DBConstants.AreaPlan].ToString());
                return true;
            }
            return false;
        }
        
        public static bool fillEventFieldsByEventId(string selectedEventId, Event evnt)
        {
            var conn = connectDB(DBConstants.ConnectionString);
            if (conn != null)
            {
                SqlCommand myCommand = new SqlCommand(string.Format("SELECT * FROM {0}.{1} WHERE [id] = '{2}'", DBConstants.DataBase, DBConstants.EventTable, selectedEventId), conn);
                SqlDataReader myReader = myCommand.ExecuteReader();
                myReader.Read();
                evnt.companyName = myReader[DBConstants.AccountName].ToString();
                evnt.contactName = myReader[DBConstants.ConactName].ToString();
                evnt.contactPhone = myReader[DBConstants.ContactPhones].ToString() + "  email: " + myReader[DBConstants.ContactEmail].ToString();
                evnt.location = myReader[DBConstants.TSFacility].ToString();
                evnt.author = myReader[DBConstants.EmployeeName].ToString();
                evnt.date = myReader[DBConstants.CreatedOn].ToString();
                return true;
            }
            return false;
        }
        */
        public static bool updateXmlFile(string xmlData, string dbTable, string selectedStageId)
        {
            var conn = connectDB(DBConstants.ConnectionString);
            if (conn == null)
                return false;
            SqlCommand myCommand = new SqlCommand(string.Format("UPDATE {0}.{1} SET [{2}]='{3}' WHERE [id]='{4}'", DBConstants.DataBase, dbTable,DBConstants.AutocadXMLData,xmlData, selectedStageId), conn);
            myCommand.CommandTimeout = 0;
            int rows = myCommand.ExecuteNonQuery();
            if (rows == 0)
            {
                log.Error("Не удалось загрузить данные в CRM!");
                return false;
            }
            log.Debug("Данные успешно загружены в CRM!" + Environment.NewLine);
            return true;
        }

        public static bool getAutoCadFile(string dbTable, string id)
        {
            var conn = connectDB(DBConstants.ConnectionString);
            if (conn != null)
            {
                using (var sqlQuery = new SqlCommand(string.Format(@"SELECT [{0}] FROM {1}.{2} WHERE [id] = @varID", DBConstants.AutocadFileData, DBConstants.DataBase, dbTable), conn))
                {
                    sqlQuery.Parameters.AddWithValue("@varID", id);
                    using (var sqlQueryResult = sqlQuery.ExecuteReader())
                        if (sqlQueryResult != null)
                        {
                            sqlQueryResult.Read();
                            if (!string.IsNullOrEmpty(sqlQueryResult[DBConstants.AutocadFileData].ToString()))
                            {
                                var blob = new Byte[(sqlQueryResult.GetBytes(0, 0, null, 0, int.MaxValue))];
                                sqlQueryResult.GetBytes(0, 0, blob, 0, blob.Length);
                                using (var fs = new FileStream(Path.Combine(Configuration.xmlPath, StringConstants.ACAD_FILENAME_PREFIX + id + ".dwg"), FileMode.Create, FileAccess.Write)) fs.Write(blob, 0, blob.Length);
                            }
                            else
                            {
                                log.Error("Отсутсвует файл чертежа!");
                            }
                        }
                }
                connection.Close();
                return true;
            }
            return false;
        }
        //UPDATE instead of insert TODO: replace with procedures
        public static void setAutoCadFile(string dbTable, string id)
        {
            byte[] file;
            using (var stream = new FileStream(Path.Combine(Configuration.xmlPath, StringConstants.ACAD_FILENAME_PREFIX + id + ".dwg"), FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);

                }

            }
            using (var sqlWrite = new SqlCommand("INSERT INTO [master].[dbo].[_AutocadEvent] (AutocadFileData) Values(@File)", connectDB(DBConstants.ConnectionString)))
            //using (var sqlWrite = new SqlCommand("INSERT INTO [master].[dbo].[_AutocadExposition] (AutocadFileData) Values(@File)", myConnection))
            {
                sqlWrite.Parameters.Add("@File", SqlDbType.VarBinary, file.Length).Value = file;
                sqlWrite.ExecuteNonQuery();
            }
        }
    }
}
