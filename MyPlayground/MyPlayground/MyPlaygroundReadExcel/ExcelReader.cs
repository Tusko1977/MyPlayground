using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayground.MyPlaygroundReadExcel
{
    public class ExcelReader
    {

        private OleDbConnection OpenConnection(string fileLocation)
        {
            OleDbConnection oledbConn = null;
            try
            {
                if (Path.GetExtension(fileLocation) == ".xls")
                    oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + fileLocation +
                        "; Extendend Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                else if (Path.GetExtension(fileLocation) == ".xlsx")
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileLocation + "; Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1;';");

                oledbConn.Open();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {

                }
            }
            return oledbConn;
        }

        private IList<Entity> ExtractEntityExcel(OleDbConnection oledbConn)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter oleda = new OleDbDataAdapter();
            DataSet dsEntityInfo = new DataSet();

            cmd.Connection = oledbConn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM [InsertEntity$]";
            oleda = new OleDbDataAdapter(cmd);
            oleda.Fill(dsEntityInfo, "Entity");

            var dsEntityInfoList = dsEntityInfo.Tables[0].AsEnumerable().Select(s => new Entity
            {
                Property = Convert.ToString(s["Property"] != DBNull.Value ? s["Property"] : ""),
                Column = Convert.ToString(s["Column"] != DBNull.Value ? s["Column"] : ""),
                Description = Convert.ToString(s["Description"] != DBNull.Value ? s["Description"] : ""),
                Type = Convert.ToString(s["Type"] != DBNull.Value ? s["Type"] : ""),
                Length = Convert.ToString(s["Length"] != DBNull.Value ? s["Length"] : ""),
                Constraint = Convert.ToString(s["Constraint"] != DBNull.Value ? s["Constraint"] : ""),
                Table = Convert.ToString(s["Table"] != DBNull.Value ? s["Table"] : ""),
                HelpId = Convert.ToString(s["HelpId"] != DBNull.Value ? s["HelpId"] : ""),
                Transformation = Convert.ToString(s["Transformation"] != DBNull.Value ? s["Transformation"] : "")
            }).ToList();

            return dsEntityInfoList;
        }

        public IList<Entity> ReadExcel(string fileLocation)
        {
            IList<Entity> objEntityInfo = new List<Entity>();
            try
            {
                OleDbConnection oledbConn = OpenConnection(fileLocation);
                if (oledbConn.State == ConnectionState.Open)
                {
                    objEntityInfo = ExtractEntityExcel(oledbConn);
                    oledbConn.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {

                }
            }
            return objEntityInfo;
        }


        //public bool ManageExcelRecordsAsync()
        //{
        //    OleDbCommand cmd = new OleDbCommand();
        //    OleDbDataAdapter oleda = new OleDbDataAdapter();
        //    DataSet dsEntityInfo = new DataSet();

        //    bool IsSave = false;

        //    cmd.Connection = oledbConn;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.AddWithValue("@HelpId", rec.HelpId);
        //    cmd.Parameters.AddWithValue("@NodeId1", rec.NodeId1);
        //    cmd.Parameters.AddWithValue("@NodeId2", rec.NodeId2);

        //    cmd.CommandText = "Udpate [InsertHelp$] set NodeId1=@NodeId1,NodeId2=@NodeId2 where HelpId=@HelpId";

        //    int result = cmd.ExecuteNonQuery();
        //    if (result > 0)
        //    {
        //        IsSave = true;
        //    }
        //    oledbConn.Close();

        //    return IsSave;
        //}
    }
}
