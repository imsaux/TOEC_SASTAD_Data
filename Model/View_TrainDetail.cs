using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOEC_SASTAD_Data.Model
{
    public class View_TrainDetail : traindetail
    {
        public string ProblemType { get; set; }
        public DateTime TrainComeDate { get; set; }
        public int LineID { get; set; }
        public string TrainNo { get; set; }
    }
}
