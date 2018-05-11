using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOEC_SASTAD_Data.Model;
namespace TOEC_Inspection.Model
{
    public class View_Train : train
    {
        /// <summary>
        /// 线路名称
        /// </summary>
        public string LineName { get; set; }
        /// <summary>
        /// 报警个数
        /// </summary>
        public string AlarmCount { get; set; }
        /// <summary>
        /// 查询时，临时用于检查包含的问题类型
        /// </summary>
        public string ProblemType { get; set; }
    }
}
