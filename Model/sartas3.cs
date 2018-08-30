namespace TOEC_SASTAD_Data.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class sartas3 : DbContext
    {
        public sartas3()
            : base("name=sartas3")
        {
        }

        public virtual DbSet<account_r_role_function> account_r_role_function { get; set; }
        public virtual DbSet<account_r_users_roles> account_r_users_roles { get; set; }
        public virtual DbSet<account_t_function> account_t_function { get; set; }
        public virtual DbSet<account_t_role> account_t_role { get; set; }
        public virtual DbSet<account_t_station> account_t_station { get; set; }
        public virtual DbSet<account_t_user> account_t_user { get; set; }
        public virtual DbSet<alarmdetail> alarmdetail { get; set; }
        public virtual DbSet<hotwheel_axle> hotwheel_axle { get; set; }
        public virtual DbSet<line> line { get; set; }
        public virtual DbSet<qbdetail> qbdetail { get; set; }
        public virtual DbSet<statistics_image> statistics_image { get; set; }
        public virtual DbSet<sys_code> sys_code { get; set; }
        public virtual DbSet<sys_codemap> sys_codemap { get; set; }
        public virtual DbSet<sys_log> sys_log { get; set; }
        public virtual DbSet<train> train { get; set; }
        public virtual DbSet<traindetail> traindetail { get; set; }
        public virtual DbSet<qbmaster> qbmaster { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account_r_role_function>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<account_r_role_function>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<account_r_role_function>()
                .Property(e => e.FunctionID)
                .IsUnicode(false);

            modelBuilder.Entity<account_r_users_roles>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<account_r_users_roles>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<account_r_users_roles>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_function>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_function>()
                .Property(e => e.PID)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_function>()
                .Property(e => e.FunctionName)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_function>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_role>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_station>()
                .Property(e => e.StationID)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_station>()
                .Property(e => e.StationName)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_station>()
                .Property(e => e.ConnectString)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_station>()
                .Property(e => e.TelexCode)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_station>()
                .Property(e => e.StationType)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_user>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_user>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_user>()
                .Property(e => e.RealName)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_user>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_user>()
                .Property(e => e.ShiftName)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_user>()
                .Property(e => e.VoiceName)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_user>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<account_t_user>()
                .Property(e => e.TelexCode)
                .IsUnicode(false);

            modelBuilder.Entity<alarmdetail>()
                .Property(e => e.AlarmDetail_ID)
                .IsUnicode(false);

            modelBuilder.Entity<alarmdetail>()
                .Property(e => e.Train_ID)
                .IsUnicode(false);

            modelBuilder.Entity<alarmdetail>()
                .Property(e => e.TrainDetail_ID)
                .IsUnicode(false);

            modelBuilder.Entity<alarmdetail>()
                .Property(e => e.Side)
                .IsUnicode(false);

            modelBuilder.Entity<alarmdetail>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<alarmdetail>()
                .Property(e => e.FileContent)
                .IsUnicode(false);

            modelBuilder.Entity<alarmdetail>()
                .Property(e => e.ProblemType)
                .IsUnicode(false);

            modelBuilder.Entity<alarmdetail>()
                .Property(e => e.Source)
                .IsUnicode(false);

            modelBuilder.Entity<alarmdetail>()
                .Property(e => e.HandleResult)
                .IsUnicode(false);

            modelBuilder.Entity<alarmdetail>()
                .Property(e => e.OpUser)
                .IsUnicode(false);

            modelBuilder.Entity<hotwheel_axle>()
                .Property(e => e.HWAxleID)
                .IsUnicode(false);

            modelBuilder.Entity<hotwheel_axle>()
                .Property(e => e.TrainDetail_ID)
                .IsUnicode(false);

            modelBuilder.Entity<hotwheel_axle>()
                .Property(e => e.LeftWave)
                .IsUnicode(false);

            modelBuilder.Entity<hotwheel_axle>()
                .Property(e => e.RightWave)
                .IsUnicode(false);

            modelBuilder.Entity<hotwheel_axle>()
                .Property(e => e.NextAxleDistance)
                .IsUnicode(false);

            modelBuilder.Entity<hotwheel_axle>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<line>()
                .Property(e => e.LineID)
                .IsUnicode(false);

            modelBuilder.Entity<line>()
                .Property(e => e.LineCode)
                .IsUnicode(false);

            modelBuilder.Entity<line>()
                .Property(e => e.LineName)
                .IsUnicode(false);

            modelBuilder.Entity<line>()
                .Property(e => e.BaseServiceSocket)
                .IsUnicode(false);

            modelBuilder.Entity<line>()
                .Property(e => e.ClientSocket)
                .IsUnicode(false);

            modelBuilder.Entity<line>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.QBDetailID)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.QBMasterID)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.CC)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.SWBZ)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.CH)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.SWH)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.CZ)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.CX)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.PM)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.JS)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.PB)
                .IsUnicode(false);

            modelBuilder.Entity<qbdetail>()
                .Property(e => e.JYBZ)
                .IsUnicode(false);

            modelBuilder.Entity<statistics_image>()
                .Property(e => e.MonitorImageRemark)
                .IsUnicode(false);

            modelBuilder.Entity<statistics_image>()
                .Property(e => e.CallAlgImageRemark)
                .IsUnicode(false);

            modelBuilder.Entity<statistics_image>()
                .Property(e => e.SucCheckImageRemark)
                .IsUnicode(false);

            modelBuilder.Entity<statistics_image>()
                .Property(e => e.FailCheckImageRemark)
                .IsUnicode(false);

            modelBuilder.Entity<statistics_image>()
                .Property(e => e.NoCheckImageRemark)
                .IsUnicode(false);

            modelBuilder.Entity<statistics_image>()
                .Property(e => e.DataSource)
                .IsUnicode(false);

            modelBuilder.Entity<sys_code>()
                .Property(e => e.CodeMap_ID)
                .IsUnicode(false);

            modelBuilder.Entity<sys_code>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<sys_code>()
                .Property(e => e.Meaning)
                .IsUnicode(false);

            modelBuilder.Entity<sys_code>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<sys_code>()
                .Property(e => e.IsEnable)
                .IsUnicode(false);

            modelBuilder.Entity<sys_codemap>()
                .Property(e => e.CodeMap_ID)
                .IsUnicode(false);

            modelBuilder.Entity<sys_codemap>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<sys_codemap>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<sys_codemap>()
                .Property(e => e.Meaning)
                .IsUnicode(false);

            modelBuilder.Entity<sys_log>()
                .Property(e => e.LogID)
                .IsUnicode(false);

            modelBuilder.Entity<sys_log>()
                .Property(e => e.ApplicationName)
                .IsUnicode(false);

            modelBuilder.Entity<sys_log>()
                .Property(e => e.FunctionName)
                .IsUnicode(false);

            modelBuilder.Entity<sys_log>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<sys_log>()
                .Property(e => e.Detail)
                .IsUnicode(false);

            modelBuilder.Entity<sys_log>()
                .Property(e => e.ErrorID)
                .IsUnicode(false);

            modelBuilder.Entity<sys_log>()
                .Property(e => e.Version)
                .IsUnicode(false);

            modelBuilder.Entity<train>()
                .Property(e => e.Train_No)
                .IsUnicode(false);

            modelBuilder.Entity<train>()
                .Property(e => e.Train_Speed)
                .IsUnicode(false);

            modelBuilder.Entity<train>()
                .Property(e => e.Train_type)
                .IsUnicode(false);

            modelBuilder.Entity<train>()
                .Property(e => e.source)
                .IsUnicode(false);

            modelBuilder.Entity<train>()
                .Property(e => e.TelexCode)
                .IsUnicode(false);

            modelBuilder.Entity<train>()
                .Property(e => e.AlarmLevel)
                .IsUnicode(false);

            modelBuilder.Entity<traindetail>()
                .Property(e => e.TrainDetail_No)
                .IsUnicode(false);

            modelBuilder.Entity<traindetail>()
                .Property(e => e.TrainDetail_Remark)
                .IsUnicode(false);

            modelBuilder.Entity<traindetail>()
                .Property(e => e.vehicletype)
                .IsUnicode(false);

            modelBuilder.Entity<traindetail>()
                .Property(e => e.startstation)
                .IsUnicode(false);

            modelBuilder.Entity<traindetail>()
                .Property(e => e.endstation)
                .IsUnicode(false);

            modelBuilder.Entity<traindetail>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<traindetail>()
                .Property(e => e.smoke_result)
                .IsUnicode(false);

            modelBuilder.Entity<traindetail>()
                .Property(e => e.AlarmLevel)
                .IsUnicode(false);

            modelBuilder.Entity<qbmaster>()
                .Property(e => e.QBMasterID)
                .IsUnicode(false);

            modelBuilder.Entity<qbmaster>()
                .Property(e => e.TrainID)
                .IsUnicode(false);
        }
    }
}
