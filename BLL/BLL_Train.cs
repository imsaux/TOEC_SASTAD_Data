using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using TOEC_Inspection.Model;
using TOEC_SASTAD_Data.Model;
using TOEC_Common;

namespace TOEC_SASTAD_Data.BLL
{
    /// <summary>
    /// 列车相关业务逻辑
    /// </summary>
    public class BLL_Train
    {
        /// <summary>
        /// 查询列车
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="LineID"></param>
        /// <returns></returns>
        public train GetTrain(DateTime dt, int LineID)
        {
            using (sartasEntities db = new sartasEntities())
            {
                train old = db.train.FirstOrDefault(n => n.Train_ComeDate == dt && n.Line_ID == LineID);
                return old;
            }
        }

        /// <summary>
        /// 终于TMD写完了，但是Enumerable的执行效率很低
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <param name="LineID"></param>
        /// <param name="TrainNo"></param>
        /// <param name="TrainDetailNo"></param>
        /// <param name="HasProblem"></param>
        /// <param name="ProblemTypes"></param>
        /// <param name="TrainCount"></param>
        /// <returns></returns>
        public List<View_Train> GetTrainList(DateTime st, DateTime ed, string LineID, string TrainNo, string TrainDetailNo, string HasProblem, List<string> ProblemTypes, out int TrainCount)
        {
            using (sartasEntities db = new sartasEntities())
            {
                List<View_Train> res = new List<View_Train>();
                var query = from t in db.train.AsEnumerable()
                                //左联必须使用into语句
                            join a in db.alarmdetail on t.Train_ID.ToString() equals a.Train_ID into ad
                            from adi in ad.DefaultIfEmpty(
                                new alarmdetail
                                {
                                    //给出为空的默认值
                                    AlarmDetail_ID = null,
                                    AlarmLevel = 0,
                                    AlgResult = 0,
                                    FileContent = null,
                                    FileName = null,
                                    HandleResult = null,
                                    InsertTime = DateTime.Now,
                                    Train_ID = "",
                                    OpTime = DateTime.Now,
                                    OpUser = "",
                                    ProblemType = "",
                                    Side = "",
                                    Source = "",
                                    TrainDetail_ID = ""
                                }
                                )
                            select new View_Train
                            {
                                //AlarmCount = null,
                                //LineName = null,
                                ProblemType = adi.ProblemType,
                                AlarmLevel = t.AlarmLevel,
                                Line_ID = t.Line_ID,
                                TelexCode = t.TelexCode,
                                Train_ComeDate = t.Train_ComeDate,
                                Train_Count = t.Train_Count,
                                Train_ID = t.Train_ID,
                                Train_No = t.Train_No,
                                Train_Speed = t.Train_Speed,
                                Train_type = t.Train_type
                                //source = t.source,
                                //Train_Real = t.Train_Real,
                                //Train_WorkFlag = t.Train_WorkFlag
                            };
                if (st != null && ed != null)
                {
                    query = query.Where(t => t.Train_ComeDate >= st && t.Train_ComeDate <= ed);
                }
                if (!string.IsNullOrWhiteSpace(LineID))
                {
                    int line = Convert.ToInt16(LineID);
                    query = query.Where(t => t.Line_ID == line);
                }
                if (!string.IsNullOrWhiteSpace(TrainNo))
                {
                    query = query.Where(t => t.Train_No.Contains(TrainNo));
                }
                if (!string.IsNullOrWhiteSpace(HasProblem))
                {
                    if (ProblemTypes != null && ProblemTypes.Count > 0)
                    {
                        query = query.Where(t => t.AlarmLevel != null && t.AlarmLevel != "0" && t.AlarmLevel != "");
                        //使用表达式扩展
                        var where = PredicateBuilder.False<View_Train>();
                        foreach (string p in ProblemTypes)
                        {
                            //query = query.Where(t => t.ProblemType.Contains(p));
                            where = where.Or(n => n.ProblemType == p);
                        }
                        query = query.Where(where.Compile());
                    }
                    else
                        query = query.Where(t => t.AlarmLevel == "0" || t.AlarmLevel == null || t.AlarmLevel == "");
                }
                //去重+排序
                query = query.DistinctBy(t => t.Train_ID).OrderByDescending(t => t.Train_ComeDate);
                //车号查询
                if (!string.IsNullOrEmpty(TrainDetailNo) && query.Count() > 0)
                {
                    //此处必须ToList(),将上一次查询关闭
                    //foreach (View_Train item in query.ToList())
                    //AsEnumerable将表转换为IEnumerable类型即可
                    foreach (View_Train item in query)
                    {
                        if (st.Month >= DateTime.Now.Month)
                        {
                            //当前月查询表【traindetail】
                            string SQL_QueryTrainDetailBy_tdNo = @"SELECT 1 FROM " + Config.DB_SCHEMA + ".traindetail td WHERE td.Train_ID='" + item.Train_ID + "' AND td.TrainDetail_No like '%" + TrainDetailNo + "%';";
                            string QueryRes = db.Database.SqlQuery<string>(SQL_QueryTrainDetailBy_tdNo).FirstOrDefault();
                            if (QueryRes != null)
                            {
                                res.Add(item);
                            }
                        }
                        else
                        {
                            //历史月份查询分表【traindetail_月份】
                            for (DateTime i = st; i.Month <= ed.Month; i = i.AddMonths(1))
                            {
                                string SQL_BeSureTableExist = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='" + Config.DB_SCHEMA + "' AND TABLE_NAME = 'traindetail_" + i.ToString("yyyyMM") + "'";
                                string tableName = db.Database.SqlQuery<string>(SQL_BeSureTableExist).FirstOrDefault();
                                if (tableName != null)
                                {
                                    string SQL_QueryTrainDetailBy_tdNo = @"SELECT 1 FROM " + Config.DB_SCHEMA + "." + tableName + " td WHERE td.Train_ID='" + item.Train_ID + "' AND td.TrainDetail_No like '%" + TrainDetailNo + "%';";
                                    string QueryRes = db.Database.SqlQuery<string>(SQL_QueryTrainDetailBy_tdNo).FirstOrDefault();
                                    if (QueryRes != null)
                                    {
                                        res.Add(item);
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            if (ed.Month >= DateTime.Now.Month)
                            {
                                //查询时间直至本月，则需要额外查询表【traindetail】
                                string SQL_QueryTrainDetailBy_tdNo = @"SELECT 1 FROM " + Config.DB_SCHEMA + ".traindetail td WHERE td.Train_ID='" + item.Train_ID + "' AND td.TrainDetail_No like '%" + TrainDetailNo + "%';";
                                string QueryRes = db.Database.SqlQuery<string>(SQL_QueryTrainDetailBy_tdNo).FirstOrDefault();
                                if (QueryRes != null)
                                {
                                    res.Add(item);
                                }
                            }
                        }
                    }
                    //车号查询
                    TrainCount = res.Count;
                    return res;
                }
                else
                {
                    //无车号查询
                    res = query.ToList();
                    TrainCount = res.Count;
                    return res;
                }
            }
        }

        /// <summary>
        /// 主查询：查询列车信息
        /// 拼SQL的方式要快很多
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <param name="LineID"></param>
        /// <param name="TrainNo"></param>
        /// <param name="TrainDetailNo"></param>
        /// <param name="HasProblem"></param>
        /// <param name="ProblemTypes"></param>
        /// <param name="TrainCount"></param>
        /// <returns></returns>
        public DataTable GetTrainDataTable(DateTime st, DateTime ed, string LineID, string TrainNo, string TrainDetailNo, string HasProblem, List<string> ProblemTypes, out int TrainCount)
        {
            try
            {
                using (MySqlConnection mycon = new MySqlConnection(Config.ConStr))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"SELECT DISTINCT 
                        t.Train_ID, 
                        t.Line_ID,
                        CONCAT('线路',t.Line_ID) AS LineName, 
                        t.Train_No,
                        t.Train_Speed, 
                        t.Train_ComeDate,
                        t.Train_Count,
                        t.Train_type, 
                        t.source, 
                        t.AlarmLevel, 
                        t.TelexCode ");
                    //(SELECT COUNT(0) FROM alarmdetail WHERE alarmdetail.Train_ID=t.Train_ID) AS AlarmCount, 这个字段效率低
                    sql.Append("FROM train t ");
                    if (ProblemTypes != null)
                    {
                        sql.Append("LEFT JOIN alarmdetail ad on t.Train_ID= ad.Train_ID ");
                    }
                    sql.Append("WHERE 1=1 ");

                    if (st != null && ed != null)
                    {
                        sql.Append("AND t.Train_ComeDate>='" + st + "' AND t.Train_ComeDate<='" + ed + "' ");
                    }
                    if (!string.IsNullOrWhiteSpace(LineID))
                    {
                        sql.Append("AND t.Line_ID ='" + LineID + "' ");
                    }
                    if (!string.IsNullOrWhiteSpace(TrainNo))
                    {
                        sql.Append("AND t.Train_No LIKE '%" + TrainNo + "%' ");
                    }
                    if (!string.IsNullOrWhiteSpace(HasProblem))
                    {
                        if (HasProblem == "有问题")
                        {
                            sql.Append("AND t.AlarmLevel IS NOT NULL AND t.AlarmLevel <>'0' AND t.AlarmLevel <>'' ");
                            if (ProblemTypes != null && ProblemTypes.Count > 0)
                            {
                                //有问题类型，根据问题类型查询
                                sql.Append("AND (1=0 ");
                                foreach (string p in ProblemTypes)
                                {
                                    sql.Append("OR ad.ProblemType = '" + p + "' ");
                                }
                                sql.Append(") ");
                            }
                        }
                        else if (HasProblem == "无问题")
                        {
                            sql.Append("AND (t.AlarmLevel ='0' OR t.AlarmLevel ='' OR t.AlarmLevel IS NULL ) ");
                        }
                    }
                    sql.Append("ORDER BY t.Train_ComeDate DESC ");
                    DataTable res = new DataTable();
                    mycon.Open();
                    MySqlDataAdapter mda = new MySqlDataAdapter(sql.ToString(), mycon);
                    mda.Fill(res);//已经查出满足上述条件的列车了

                    //车号查询筛选
                    if (!string.IsNullOrEmpty(TrainDetailNo) && res.Rows.Count > 0)
                    {
                        DataTable res_Filter = res.Clone();
                        foreach (DataRow dr in res.Rows)
                        {
                            if (st.Month >= DateTime.Now.Month) //当前月查询表【traindetail】
                            {
                                MySqlCommand mycmd = new MySqlCommand(@"SELECT 1 FROM " + Config.DB_SCHEMA + ".traindetail td WHERE td.Train_ID='" + dr["Train_ID"] + "' AND td.TrainDetail_No like '%" + TrainDetailNo + "%';", mycon);
                                using (MySqlDataReader mydr = mycmd.ExecuteReader())
                                {
                                    if (mydr.HasRows)
                                    {
                                        res_Filter.Rows.Add(dr.ItemArray);
                                    }
                                }
                            }
                            else
                            {
                                //历史月份查询分表【traindetail_月份】
                                for (DateTime i = st; i.Month <= ed.Month; i = i.AddMonths(1))
                                {
                                    MySqlCommand mycmd = new MySqlCommand(@"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='" + Config.DB_SCHEMA + "'  AND TABLE_NAME = 'traindetail_" + i.ToString("yyyyMM") + "'", mycon);
                                    using (MySqlDataReader mydr = mycmd.ExecuteReader())
                                    {
                                        string tableName = "";
                                        if (mydr.HasRows)
                                        {
                                            while (mydr.Read())
                                            {
                                                tableName = (string)mydr["TABLE_NAME"];
                                            }
                                            if (!string.IsNullOrEmpty(tableName))
                                            {
                                                mydr.Close();
                                                MySqlCommand mycmd2 = new MySqlCommand(@"SELECT 1 FROM " + Config.DB_SCHEMA + "." + tableName + " td WHERE td.Train_ID='" + dr["Train_ID"] + "' AND td.TrainDetail_No like '%" + TrainDetailNo + "%';", mycon);
                                                using (MySqlDataReader mydr2 = mycmd2.ExecuteReader())
                                                {
                                                    if (mydr2.HasRows)
                                                    {
                                                        res_Filter.Rows.Add(dr.ItemArray);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                                if (ed.Month >= DateTime.Now.Month)
                                {
                                    //查询时间直至本月，则需要额外查询表【traindetail】
                                    MySqlCommand mycmd = new MySqlCommand(@"SELECT 1 FROM " + Config.DB_SCHEMA + ".traindetail td WHERE td.Train_ID='" + dr["Train_ID"] + "' AND td.TrainDetail_No like '%" + TrainDetailNo + "%';", mycon);
                                    using (MySqlDataReader mydr = mycmd.ExecuteReader())
                                    {
                                        if (mydr.HasRows)
                                        {
                                            res_Filter.Rows.Add(dr.ItemArray);
                                        }
                                    }
                                }
                            }
                        }
                        //车号查询
                        TrainCount = res.Rows.Count; mycon.Close();
                        return res_Filter;
                    }
                    else
                    {
                        //无车号查询
                        TrainCount = res.Rows.Count; mycon.Close();
                        return res;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 通过车型查找列车
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <param name="TrainType">【客车】【货车】</param>
        /// <returns></returns>
        public DataTable Get_TrainDataTable_ByVehicleType(DateTime st, DateTime ed, string[] TrainType)
        {
            try
            {
                using (MySqlConnection mycon = new MySqlConnection(Config.ConStr))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"SELECT (SELECT CASE t.Line_ID WHEN 1 THEN '202.202.202.2' WHEN 2 THEN '202.202.202.3' END) AS LineName,DATE_FORMAT(t.Train_ComeDate,'%Y%m%d%H%i%s') AS Train_ComeDate ");
                    sql.Append("FROM train t WHERE 1=1 ");
                    if (st != null && ed != null)
                    {
                        sql.Append("AND t.Train_ComeDate>='" + st + "' AND t.Train_ComeDate<='" + ed + "' ");
                    }
                    if (TrainType.Length > 0)
                    {
                        sql.Append("AND (1=0 ");
                        foreach (var item in TrainType)
                        {
                            sql.Append("OR t.Train_type LIKE '%" + item.Trim() + "%'");
                        }
                        sql.Append(") ");
                    }
                    sql.Append("ORDER BY t.Train_ComeDate DESC ");
                    DataTable res = new DataTable();
                    mycon.Open();
                    MySqlDataAdapter mda = new MySqlDataAdapter(sql.ToString(), mycon);
                    mda.Fill(res);//已经查出满足上述条件的列车了
                    return res;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 查询最新列车信息
        /// </summary>
        /// <param name="LineID">线路ID</param>
        /// <param name="TrainComeDate"></param>
        /// <returns></returns>
        public train Get_LatestTrain_ByLineID(int LineID, DateTime TrainComeDate)
        {
            try
            {
                using (sartasEntities db = new sartasEntities())
                {
                    train t = db.train.Where(n => n.Line_ID == LineID && n.Train_ComeDate >= TrainComeDate).OrderByDescending(n => n.Train_ComeDate).FirstOrDefault();
                    return t;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 更新列车报警等级
        /// </summary>
        /// <param name="TrainID"></param>
        public void Update_TrainAlarmLevel(string TrainID)
        {
            try
            {
                using (sartasEntities db = new sartasEntities())
                {
                    Guid tmp = new Guid(TrainID);
                    var q = db.traindetail.Where(n => n.Train_ID == tmp && n.AlarmLevel != "0" && n.AlarmLevel != "" && n.AlarmLevel != null).Min(n => n.AlarmLevel);
                    train oldt = db.train.Where(n => n.Train_ID == tmp).FirstOrDefault();
                    if (oldt != null)
                    {
                        oldt.AlarmLevel = q;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
