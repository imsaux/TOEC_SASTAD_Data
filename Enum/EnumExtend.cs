using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOEC_SASTAD_Data
{
    public static class EnumExtend
    {
        /// <summary>
        /// 报警等级
        /// </summary>
        /// <param name="us"></param>
        /// <returns></returns>
        public static string ToNum(this AlarmLevel al)
        {
            switch (al)
            {
                case AlarmLevel.Level_0:
                    return "0";
                case AlarmLevel.Level_1:
                    return "1";
                case AlarmLevel.Level_2:
                    return "2";
                case AlarmLevel.Level_3:
                    return "3";
                case AlarmLevel.Level_4:
                    return "4";
                default:
                    return "3";
            }
        }
    }
}
