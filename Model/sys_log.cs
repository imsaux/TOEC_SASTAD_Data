//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class sys_log
    {
        public string LogID { get; set; }
        public Nullable<System.DateTime> OpTime { get; set; }
        public string ApplicationName { get; set; }
        public string FunctionName { get; set; }
        public string State { get; set; }
        public string Detail { get; set; }
        public string ErrorID { get; set; }
        public string Version { get; set; }
    }
}