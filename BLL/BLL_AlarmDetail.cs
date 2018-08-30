using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TOEC_SASTAD_Data.Model;
using TOEC_Inspection;
using TOEC_Common;
using System.Threading;

namespace TOEC_SASTAD_Data.BLL
{
    public class BLL_AlarmDetail
    {

        /// <summary>
        /// 查询问题图片路径
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <returns> [0]原图像路径[1]新图像文件名[2]报警类型</returns>
        public DataTable GetAlarmPicPath(DateTime st, DateTime ed, List<string> listAlarmType)
        {
            try
            {
                DataTable dt_Paths = new DataTable();
                StringBuilder SQLcmd = new StringBuilder();
                //先查询所有报警类型
                using (MySqlConnection mycon = new MySqlConnection(Config.ConStr))
                {
                    mycon.Open();

                    //历史月份查询分表【traindetail_月份】
                    //cur只比对年月
                    for (DateTime cur = new DateTime(st.Year, st.Month, 1); cur < new DateTime(ed.Year, ed.Month, 28); cur = cur.AddMonths(1))
                    {
                        string TrainDetailTableName = "traindetail";
                        if (cur < DateTime.Now) //小于当前月
                        {
                            TrainDetailTableName = "traindetail_" + cur.ToString("yyyyMM");
                            string SQL_CheckTable = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='" + Config.DB_SCHEMA + "'  AND TABLE_NAME = '" + TrainDetailTableName + "'";
                            MySqlCommand mycmd = new MySqlCommand(SQL_CheckTable, mycon);
                            MySqlDataReader mydr = mycmd.ExecuteReader();
                            if (mydr.HasRows)
                            {
                                mydr.Close();
                            }
                            else { mydr.Close(); TrainDetailTableName = "traindetail"; }
                        }
                        SQLcmd.Clear();
                        SQLcmd.Append(@"SELECT DISTINCT 
CONCAT(l.GQCode,'\\',DATE_FORMAT(t.Train_ComeDate,'%Y%m%d%H%i%s'),'\\',ad.Side,LPAD(td.TrainDetail_OrderNo,3,0),'_',td.TrainDetail_OrderNo,'.jpg') as OldPath,
CONCAT(TRIM(td.vehicletype),'_',l.GQCode,'_',DATE_FORMAT(t.Train_ComeDate,'%Y%m%d%H%i%s'),'_',ad.Side,LPAD(td.TrainDetail_OrderNo,3,0),'_',td.TrainDetail_OrderNo,'.jpg') as NewPath,
ad.ProblemType,
ad.Source
FROM alarmdetail ad 
LEFT JOIN " + TrainDetailTableName + @" td on td.TrainDetail_ID=ad.TrainDetail_ID
LEFT JOIN train t on t.Train_ID=td.Train_ID
LEFT JOIN line l on l.Line_ID= t.Line_ID
WHERE ad.AlarmLevel>0 AND (1=0  ");
                        foreach (string item in listAlarmType)
                        {
                            SQLcmd.Append("OR ad.ProblemType = '" + item + "' ");
                        }
                        SQLcmd.Append(")  ");
                        SQLcmd.Append("AND t.Train_ComeDate >= '" + st.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                        SQLcmd.Append("AND t.Train_ComeDate <= '" + ed.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                        MySqlCommand mc = new MySqlCommand(SQLcmd.ToString(), mycon);
                        MySqlDataAdapter mda = new MySqlDataAdapter(mc);
                        DataTable curDT = new DataTable();
                        mda.Fill(curDT);
                        if (curDT != null && curDT.Rows.Count > 0)
                        {
                            if (dt_Paths.Rows.Count == 0)
                            { dt_Paths = curDT.Copy(); }
                            else
                            {
                                DataTable tmp = new DataTable();
                                tmp = curDT.Copy();
                                foreach (DataRow dr in tmp.Rows)
                                {
                                    dt_Paths.ImportRow(dr);
                                }
                            }
                        }
                        else { continue; }
                    }
                    mycon.Close();
                    return dt_Paths;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 查询当前车厢所有报警
        /// </summary>
        /// <param name="CarID">车厢ID</param>
        /// <param name="DisplayAlarmLevel">报警显示等级</param>
        /// <returns></returns>
        public List<alarmdetail> GetAlarmByCarID(string CarID)
        {
            try
            {
                int DisplayAlarmLevel = 5;
                try
                {
                    DisplayAlarmLevel = int.Parse(Config.DisplayAlarmLevel);
                }
                catch { DisplayAlarmLevel = 5; }
                using (sartas3 db = new sartas3())
                {
                    return db.alarmdetail.Where(n => n.TrainDetail_ID == CarID && n.AlgResult == 0 && n.AlarmLevel <= DisplayAlarmLevel).ToList();
                }
            }
            catch { return null; }
        }

        /// <summary>
        /// 查询当前列车所有报警
        /// </summary>
        /// <param name="TrainID">列车ID</param>
        /// <param name="DisplayAlarmLevel">报警显示等级</param>
        /// <returns></returns>
        public List<alarmdetail> GetAlarmByTrainID(string TrainID)
        {
            try
            {
                int DisplayAlarmLevel = 5;
                try
                {
                    DisplayAlarmLevel = int.Parse(Config.DisplayAlarmLevel);
                }
                catch { DisplayAlarmLevel = 5; }
                using (sartas3 db = new sartas3())
                {
                    return db.alarmdetail.Where(n => n.Train_ID == TrainID && n.AlgResult == 0 && n.AlarmLevel <= DisplayAlarmLevel).ToList();
                }
            }
            catch { return null; }
        }

        /// <summary>
        /// 获取列车报警个数
        /// </summary>
        /// <param name="TrainID"></param>
        /// <returns></returns>
        public string GetAlarmCount_ByTrainID(string TrainID)
        {
            try
            {
                using (sartas3 db = new sartas3())
                {
                    int tmp = db.alarmdetail.Where(n => n.Train_ID == TrainID).ToList().Count;
                    if (tmp == 0)
                    { return ""; }
                    else
                    { return tmp.ToString(); }
                }
            }
            catch { return ""; }
        }

        /// <summary>
        /// 保存问题（修改和新增）
        /// 【WebService 使用】【不根据id进行判别】
        /// </summary>
        /// <returns></returns>
        public bool SaveAlarms(List<alarmdetail> list_ad, int LineID, DateTime TrainComeDate, string TelexCode, int TrainDetailOrderNum)
        {
            try
            {
                //由于入库导致的
                Thread.Sleep(new TimeSpan(0, Config.DelayTime4Save, 0));
                if (list_ad != null && list_ad.Count > 0)
                {
                    using (sartas3 db = new sartas3())
                    {
                        train t = db.train.SingleOrDefault(n => n.Train_ComeDate == TrainComeDate && n.Line_ID == LineID && n.TelexCode == TelexCode);
                        if (t != null)
                        {
                            //首先根据时间找到指定的分表
                            string TableName = "traindetail";
                            if (TrainComeDate.Month < DateTime.Now.Month)
                            {
                                for (DateTime i = TrainComeDate; i.Month < DateTime.Now.Month; i = i.AddMonths(1))
                                {
                                    TableName = "traindetail_" + i.ToString("MM");
                                    string SQL_BeSureTableExist = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='" + Config.DB_SCHEMA + "'  AND TABLE_NAME = 'traindetail_" + i.ToString("yyyyMM") + "'";
                                    string resName = db.Database.SqlQuery<string>(SQL_BeSureTableExist).FirstOrDefault();
                                    if (!string.IsNullOrWhiteSpace(resName)) { break; } else { TableName = "traindetail"; }
                                }
                            }
                            //根据分表名称进行查询
                            string SQL_Query = @"SELECT * FROM " + Config.DB_SCHEMA + "." + TableName + @" td WHERE td.Train_ID = '" + t.Train_ID + "' AND td.TrainDetail_OrderNo='" + TrainDetailOrderNum + "' ORDER BY td.TrainDetail_OrderNo";
                            traindetail td = db.Database.SqlQuery<traindetail>(SQL_Query).SingleOrDefault();
                            if (td != null)
                            {
                                foreach (alarmdetail ad in list_ad)
                                {
                                    if (!string.IsNullOrWhiteSpace(ad.AlarmDetail_ID))
                                    {
                                        alarmdetail oldone = db.alarmdetail.SingleOrDefault(n => n.AlarmDetail_ID == ad.AlarmDetail_ID);
                                        if (oldone != null)
                                        {
                                            //修改
                                            oldone.HandleResult = ad.HandleResult;
                                            oldone.OpTime = DateTime.Now;
                                        }
                                        else
                                        {
                                            //新增
                                            ad.Train_ID = t.Train_ID.ToString();
                                            ad.TrainDetail_ID = td.TrainDetail_ID.ToString();
                                            ad.HandleResult = "0";//新增
                                            ad.AlarmLevel = 1;
                                            db.alarmdetail.Add(ad);
                                        }
                                        db.SaveChanges();
                                    }
                                    else { throw new Exception("AlarmDetailID为Null"); }
                                }
                                //更新车厢报警等级
                                new BLL_Car().Update_CarAlarmLevel(td.TrainDetail_ID.ToString());
                            }
                            else { throw new Exception("未找到该车车厢"); }

                            //更新列车报警等级
                            new BLL_Train().Update_TrainAlarmLevel(t.Train_ID.ToString());
                        }
                        else { throw new Exception("未找到该列车"); }

                    }
                    return true;
                }
                else { throw new Exception("AlarmDetail为空"); }

            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// 保存问题
        /// </summary>
        /// <param name="list_ad"></param>
        /// <returns></returns>
        public bool SaveAlarm(alarmdetail ad)
        {
            using (sartas3 db = new sartas3())
            {
                try
                {
                    alarmdetail old = db.alarmdetail.FirstOrDefault(n => n.AlarmDetail_ID == ad.AlarmDetail_ID);
                    if (old != null)
                    {
                        old.HandleResult = ad.HandleResult;
                        old.OpUser = ad.OpUser;
                    }
                    else
                    {
                        ad.AlarmDetail_ID = Guid.NewGuid().ToString();
                        ad.AlarmLevel = 1;
                        ad.AlgResult = 0;
                        db.alarmdetail.Add(ad);
                    }
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e) { throw e; }
            }
        }
    }
}
