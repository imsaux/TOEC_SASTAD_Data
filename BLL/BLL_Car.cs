using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TOEC_SASTAD_Data.Model;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using TOEC_Common;

namespace TOEC_SASTAD_Data.BLL
{
    /// <summary>
    /// 车厢相关业务逻辑
    /// </summary>
    public class BLL_Car
    {
        /// <summary>
        /// 查询车厢列表
        /// </summary>
        /// <param name="TrainID"></param>
        /// <param name="TrainComeDate"></param>
        /// <param name="TrainDetailCount"></param>
        /// <returns></returns>
        public DataTable GetCars(string TrainID, DateTime TrainComeDate, out int TrainDetailCount, out int TrainAlarmCount)
        {
            try
            {
                int DisplayAlarmLevel = 4;
                try
                {
                    DisplayAlarmLevel = int.Parse(Config.DisplayAlarmLevel);
                }
                catch { DisplayAlarmLevel = 4; }
                TrainAlarmCount = 0;
                using (MySqlConnection mycon = new MySqlConnection(Config.ConStr))
                {
                    mycon.Open();
                    //首先根据时间找到指定的分表
                    string TableName = "traindetail";
                    if (TrainComeDate < DateTime.Now)
                    {
                        for (DateTime i = TrainComeDate; i < DateTime.Now; i = i.AddMonths(1))
                        {
                            TableName = "traindetail_" + i.ToString("yyyyMM");
                            string SQL_BeSureTableExist = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='" + Config.DB_SCHEMA + "'  AND TABLE_NAME = '" + TableName + "'";
                            using (MySqlConnection mycon_schema = new MySqlConnection(Config.ConStr_INFORMATION_SCHEMA))
                            {
                                mycon_schema.Open();
                                MySqlCommand mycmd = new MySqlCommand(SQL_BeSureTableExist, mycon_schema);
                                MySqlDataReader mydr = mycmd.ExecuteReader();
                                if (mydr.HasRows)
                                {
                                    mydr.Close();
                                    break;
                                }
                                else { mydr.Close(); TableName = "traindetail"; }
                            }
                        }
                    }
                    //根据分表名称进行查询
                    string SQL_Query = @"SELECT 
                                        td.Train_ID,
                                        td.TrainDetail_ID,
                                        td.TrainDetail_OrderNo,
                                        td.TrainDetail_No,
                                        td.vehicletype,
                                        td.AlarmLevel,
                                        COUNT(ad.TrainDetail_ID) AS ProblemCount,
                                        CONCAT('[共:',COUNT(ad.TrainDetail_ID),']',GROUP_CONCAT(DISTINCT ad.ProblemType)) AS ProblemType
                                        FROM " + TableName + @" td
                                        LEFT JOIN alarmdetail ad ON ad.TrainDetail_ID= td.TrainDetail_ID AND ad.AlarmLevel> 0 AND ad.AlarmLevel<= " + DisplayAlarmLevel + @"
                                        WHERE 1=1
                                        AND td.Train_ID= '" + TrainID + @"'
                                        GROUP BY td.TrainDetail_ID
                                        ORDER BY AlarmLevel DESC, TrainDetail_OrderNo ";

                    MySqlDataAdapter myda = new MySqlDataAdapter(SQL_Query, mycon);
                    DataTable dt = new DataTable();
                    myda.Fill(dt);
                    if (dt != null)
                    {
                        TrainDetailCount = dt.Rows.Count;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TrainAlarmCount += Convert.ToInt16(dt.Rows[i]["ProblemCount"]);
                        }
                        return dt;
                    }
                    else
                    {
                        TrainDetailCount = 0;
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                TrainDetailCount = 0;
                TrainAlarmCount = 0;
                return null;
            }
        }

        /// <summary>
        /// 根据顺位号查询某一节车厢[上一节，下一节]
        /// </summary>
        /// <param name="TrainID"></param>
        /// <param name="TrainComeDate"></param>
        /// <param name="OrderNum"></param>
        /// <returns></returns>
        public View_TrainDetail GetCar(string TrainID, DateTime TrainComeDate, int OrderNum)
        {
            try
            {
                using (sartasEntities db = new sartasEntities())
                {
                    //首先根据时间找到指定的分表
                    string TableName = "traindetail";
                    if (TrainComeDate < DateTime.Now)
                    {
                        for (DateTime i = TrainComeDate; i < DateTime.Now; i = i.AddMonths(1))
                        {
                            TableName = "traindetail_" + i.ToString("yyyyMM");
                            string SQL_BeSureTableExist = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='" + Config.DB_SCHEMA + "'  AND TABLE_NAME = '" + TableName + "'";
                            string resName = db.Database.SqlQuery<string>(SQL_BeSureTableExist).FirstOrDefault();
                            if (!string.IsNullOrWhiteSpace(resName)) { break; } else { TableName = "traindetail"; }
                        }
                    }
                    //根据分表名称进行查询
                    string SQL_Query = @"SELECT * FROM " + Config.DB_SCHEMA + "." + TableName + @" td WHERE td.Train_ID = '" + TrainID + "' AND td.TrainDetail_OrderNo='" + OrderNum + "'";
                    traindetail td = db.Database.SqlQuery<traindetail>(SQL_Query).SingleOrDefault();
                    if (td != null)
                    {
                        View_TrainDetail dtd = new View_TrainDetail();
                        dtd.Train_ID = td.Train_ID;
                        dtd.TrainDetail_ID = td.TrainDetail_ID;
                        dtd.TrainDetail_No = td.TrainDetail_No;
                        dtd.TrainDetail_OrderNo = td.TrainDetail_OrderNo;
                        dtd.vehicletype = td.vehicletype;
                        dtd.AlarmLevel = td.AlarmLevel;
                        string SQL_QueryTrain = @"SELECT * FROM " + Config.DB_SCHEMA + ".train t WHERE t.Train_ID = '" + TrainID + "';";
                        train t = db.Database.SqlQuery<train>(SQL_QueryTrain).SingleOrDefault();
                        if (t != null)
                        {
                            dtd.TrainComeDate = Convert.ToDateTime(t.Train_ComeDate);
                            dtd.TrainNo = t.Train_No;
                            dtd.LineID = Convert.ToInt16(t.Line_ID);
                        }
                        return dtd;
                    }
                    else return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// 获取所有问题车厢
        /// </summary>
        /// <param name="TrainID"></param>
        /// <param name="TrainComeDate"></param>
        /// <returns></returns>
        public List<View_TrainDetail> GetAllProblemCar(string TrainID, DateTime TrainComeDate)
        {
            try
            {
                List<View_TrainDetail> List_dtd = new List<View_TrainDetail>();
                using (sartasEntities db = new sartasEntities())
                {
                    //首先根据时间找到指定的分表
                    string TableName = "traindetail";
                    if (TrainComeDate < DateTime.Now)
                    {
                        for (DateTime i = TrainComeDate; i < DateTime.Now; i = i.AddMonths(1))
                        {
                            TableName = "traindetail_" + i.ToString("yyyyMM");
                            string SQL_BeSureTableExist = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='" + Config.DB_SCHEMA + "'  AND TABLE_NAME = '" + TableName + "'";
                            string resName = db.Database.SqlQuery<string>(SQL_BeSureTableExist).FirstOrDefault();
                            if (!string.IsNullOrWhiteSpace(resName)) { break; } else { TableName = "traindetail"; }
                        }
                    }
                    //根据分表名称进行查询
                    string SQL_Query = @"SELECT * FROM " + Config.DB_SCHEMA + "." + TableName + @" td WHERE td.Train_ID = '" + TrainID + "' AND td.AlarmLevel <>'0' AND td.AlarmLevel <>'' AND td.AlarmLevel IS NOT Null ORDER BY td.TrainDetail_OrderNo ";
                    List<traindetail> list_td = db.Database.SqlQuery<traindetail>(SQL_Query).ToList();
                    if (list_td != null && list_td.Count > 0)
                    {
                        foreach (traindetail td in list_td)
                        {
                            View_TrainDetail dtd = new View_TrainDetail();
                            dtd.Train_ID = td.Train_ID;
                            dtd.TrainDetail_ID = td.TrainDetail_ID;
                            dtd.TrainDetail_No = td.TrainDetail_No;
                            dtd.TrainDetail_OrderNo = td.TrainDetail_OrderNo;
                            dtd.vehicletype = td.vehicletype;
                            dtd.AlarmLevel = td.AlarmLevel;
                            string SQL_QueryTrain = @"SELECT * FROM " + Config.DB_SCHEMA + ".train t WHERE t.Train_ID = '" + TrainID + "';";
                            train t = db.Database.SqlQuery<train>(SQL_QueryTrain).SingleOrDefault();
                            if (t != null)
                            {
                                dtd.TrainComeDate = Convert.ToDateTime(t.Train_ComeDate);
                                dtd.TrainNo = t.Train_No;
                                dtd.LineID = Convert.ToInt16(t.Line_ID);
                            }
                            List_dtd.Add(dtd);
                        }
                        return List_dtd;
                    }
                    else return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void RepairTable()
        {
            using (MySqlConnection mycon = new MySqlConnection(Config.ConStr))
            {
                MySqlDataAdapter mda_status = new MySqlDataAdapter("CHECK TABLE traindetail;", mycon);
                DataTable dt_status = new DataTable();
                mda_status.Fill(dt_status);
                if (dt_status != null && dt_status.Rows.Count > 0)
                {
                    if (dt_status.Rows[0]["Msg_text"].ToString() != "OK")
                    {
                        MySqlDataAdapter mda_res = new MySqlDataAdapter("REPAIR TABLE traindetail;", mycon);
                        DataTable dt_res = new DataTable();
                        mda_res.Fill(dt_res);
                    }
                }
            }
        }

        /// <summary>
        /// 更新车厢报警等级
        /// </summary>
        /// <param name="TrainDetailID"></param>
        public void Update_CarAlarmLevel(string TrainDetailID)
        {
            try
            {
                using (sartasEntities db = new sartasEntities())
                {
                    var q = db.alarmdetail.Where(n => n.TrainDetail_ID == TrainDetailID && n.AlarmLevel != 0 && n.AlarmLevel != null).Min(n => n.AlarmLevel);
                    Guid g = new Guid(TrainDetailID);
                    traindetail oldtd = db.traindetail.Where(n => n.TrainDetail_ID == g).FirstOrDefault();
                    if (oldtd != null)
                    {
                        oldtd.AlarmLevel = q.ToString();
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }



        /// <summary>
        /// 查询【当前月份】的列车车厢
        /// 科峰实时导出模块使用
        /// </summary>
        /// <param name="TrainID">列车ID</param>
        /// <param name="TrainAlarmCount">列车报警个数</param>
        /// <returns></returns>
        public List<traindetail> Get_Cars_CurrentMonth_ByTrainID(string TrainID, out int TrainAlarmCount)
        {
            using (sartasEntities db = new sartasEntities())
            {
                Guid tmp = new Guid(TrainID);
                List<traindetail> list_td = db.traindetail.Where(n => n.Train_ID == tmp).OrderBy(n => n.TrainDetail_OrderNo).ToList();
                TrainAlarmCount = db.alarmdetail.Where(n => n.Train_ID == TrainID
                && n.AlarmLevel != null
                && n.AlarmLevel != 0
                && n.AlarmLevel <= 3).ToList().Count;
                return list_td;
            }
        }
    }
}
