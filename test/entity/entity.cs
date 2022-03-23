using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace test
{
    /// <summary>
    /// EntityEmployee
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "Employee")]
    public class EntityEmployee : BaseDataContract
    {
        /// <summary>
        /// emp_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "emp_id", DbType = DbType.AnsiString, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.String emp_id { get; set; }

        /// <summary>
        /// card_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "card_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.String card_id { get; set; }

        /// <summary>
        /// emp_fname
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "emp_fname", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String emp_fname { get; set; }

        /// <summary>
        /// IDType
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "IDType", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.Int32 IDType { get; set; }

        /// <summary>
        /// id_card
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "id_card", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String id_card { get; set; }

        /// <summary>
        /// no_sign
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "no_sign", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.Boolean no_sign { get; set; }

        /// <summary>
        /// depart_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "depart_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.String depart_id { get; set; }

        /// <summary>
        /// job_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "job_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.String job_id { get; set; }

        /// <summary>
        /// rule_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "rule_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 9)]
        public System.String rule_id { get; set; }

        /// <summary>
        /// edu_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "edu_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.String edu_id { get; set; }

        /// <summary>
        /// native_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "native_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 11)]
        public System.String native_id { get; set; }

        /// <summary>
        /// nation_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "nation_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 12)]
        public System.String nation_id { get; set; }

        /// <summary>
        /// status_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 13)]
        public System.String status_id { get; set; }

        /// <summary>
        /// CardKind
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "CardKind", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 14)]
        public System.Int32? CardKind { get; set; }

        /// <summary>
        /// put_up
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "put_up", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 15)]
        public System.Boolean? put_up { get; set; }

        /// <summary>
        /// dorm_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "dorm_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 16)]
        public System.String dorm_id { get; set; }

        /// <summary>
        /// bed_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bed_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 17)]
        public System.String bed_id { get; set; }

        /// <summary>
        /// group_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "group_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 18)]
        public System.String group_id { get; set; }

        /// <summary>
        /// birth_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "birth_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 19)]
        public System.DateTime? birth_date { get; set; }

        /// <summary>
        /// sex
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sex", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 20)]
        public System.String sex { get; set; }

        /// <summary>
        /// marriage
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "marriage", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 21)]
        public System.String marriage { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "email", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 22)]
        public System.String email { get; set; }

        /// <summary>
        /// phone_code
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "phone_code", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 23)]
        public System.String phone_code { get; set; }

        /// <summary>
        /// address
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "address", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 24)]
        public System.String address { get; set; }

        /// <summary>
        /// post_code
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "post_code", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 25)]
        public System.String post_code { get; set; }

        /// <summary>
        /// link_man
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "link_man", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 26)]
        public System.String link_man { get; set; }

        /// <summary>
        /// hire_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "hire_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 27)]
        public System.DateTime? hire_date { get; set; }

        /// <summary>
        /// contract_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "contract_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 28)]
        public System.DateTime? contract_date { get; set; }

        /// <summary>
        /// contract_over_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "contract_over_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 29)]
        public System.DateTime? contract_over_date { get; set; }

        /// <summary>
        /// leave_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "leave_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 30)]
        public System.DateTime? leave_date { get; set; }

        /// <summary>
        /// dimission_type_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "dimission_type_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 31)]
        public System.String dimission_type_id { get; set; }

        /// <summary>
        /// leave_cause
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "leave_cause", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 32)]
        public System.String leave_cause { get; set; }

        /// <summary>
        /// pre_balance
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "pre_balance", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 33)]
        public System.Decimal? pre_balance { get; set; }

        /// <summary>
        /// pre_sequ
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "pre_sequ", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 34)]
        public System.Int32? pre_sequ { get; set; }

        /// <summary>
        /// icid
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "icid", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 35)]
        public System.Boolean? icid { get; set; }

        /// <summary>
        /// rest_kind
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "rest_kind", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 36)]
        public System.Int32? rest_kind { get; set; }

        /// <summary>
        /// rest_days
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "rest_days", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 37)]
        public System.String rest_days { get; set; }

        /// <summary>
        /// worktime_kind
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "worktime_kind", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 38)]
        public System.Int32? worktime_kind { get; set; }

        /// <summary>
        /// shifts
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "shifts", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 39)]
        public System.String shifts { get; set; }

        /// <summary>
        /// shift_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "shift_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 40)]
        public System.String shift_id { get; set; }

        /// <summary>
        /// work_hrs
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "work_hrs", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 41)]
        public System.Decimal? work_hrs { get; set; }

        /// <summary>
        /// photo
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "photo", DbType = DbType.Binary, IsPK = false, IsSeq = false, SerNo = 42)]
        public System.Byte[] photo { get; set; }

        /// <summary>
        /// remark
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "remark", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 43)]
        public System.String remark { get; set; }

        /// <summary>
        /// zlgput_up
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "zlgput_up", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 44)]
        public System.Boolean? zlgput_up { get; set; }

        /// <summary>
        /// access_level
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "access_level", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 45)]
        public System.Int32? access_level { get; set; }

        /// <summary>
        /// access_pwd
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "access_pwd", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 46)]
        public System.String access_pwd { get; set; }

        /// <summary>
        /// ContactPhone
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "ContactPhone", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 47)]
        public System.String ContactPhone { get; set; }

        /// <summary>
        /// Bless
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Bless", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 48)]
        public System.Boolean? Bless { get; set; }

        /// <summary>
        /// foodtype
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "foodtype", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 49)]
        public System.String foodtype { get; set; }

        /// <summary>
        /// graduate_college
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "graduate_college", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 50)]
        public System.String graduate_college { get; set; }

        /// <summary>
        /// specialty
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "specialty", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 51)]
        public System.String specialty { get; set; }

        /// <summary>
        /// introducer
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "introducer", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 52)]
        public System.String introducer { get; set; }

        /// <summary>
        /// redeploy_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "redeploy_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 53)]
        public System.DateTime? redeploy_date { get; set; }

        /// <summary>
        /// redeploy_date2
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "redeploy_date2", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 54)]
        public System.DateTime? redeploy_date2 { get; set; }

        /// <summary>
        /// work_age
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "work_age", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 55)]
        public System.Decimal? work_age { get; set; }

        /// <summary>
        /// work_status
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "work_status", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 56)]
        public System.String work_status { get; set; }

        /// <summary>
        /// social_insurance_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "social_insurance_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 57)]
        public System.DateTime? social_insurance_date { get; set; }

        /// <summary>
        /// social_insurance_money
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "social_insurance_money", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 58)]
        public System.Decimal? social_insurance_money { get; set; }

        /// <summary>
        /// Auot_shift
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Auot_shift", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 59)]
        public System.Boolean? Auot_shift { get; set; }

        /// <summary>
        /// Inwork_Age
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Inwork_Age", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 60)]
        public System.Int32? Inwork_Age { get; set; }

        /// <summary>
        /// age
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "age", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 61)]
        public System.Int32? age { get; set; }

        /// <summary>
        /// Stop_social_insurance_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Stop_social_insurance_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 62)]
        public System.DateTime? Stop_social_insurance_date { get; set; }

        /// <summary>
        /// Stop_Job_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Stop_Job_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 63)]
        public System.DateTime? Stop_Job_date { get; set; }

        /// <summary>
        /// Long_Holiday_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Long_Holiday_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 64)]
        public System.DateTime? Long_Holiday_date { get; set; }

        /// <summary>
        /// contract_labour
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "contract_labour", DbType = DbType.String, IsPK = false, IsSeq = false, SerNo = 65)]
        public System.String contract_labour { get; set; }

        /// <summary>
        /// be_regular_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "be_regular_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 66)]
        public System.DateTime? be_regular_date { get; set; }

        /// <summary>
        /// dorm_building
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "dorm_building", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 67)]
        public System.String dorm_building { get; set; }

        /// <summary>
        /// salary_Acount
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "salary_Acount", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 68)]
        public System.String salary_Acount { get; set; }

        /// <summary>
        /// PassWord
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "PassWord", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 69)]
        public System.String PassWord { get; set; }

        /// <summary>
        /// Bank_No
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Bank_No", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 70)]
        public System.String Bank_No { get; set; }

        /// <summary>
        /// is_black
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "is_black", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 71)]
        public System.Boolean? is_black { get; set; }

        /// <summary>
        /// is_white
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "is_white", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 72)]
        public System.Boolean? is_white { get; set; }

        /// <summary>
        /// balance_time
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "balance_time", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 73)]
        public System.DateTime? balance_time { get; set; }

        /// <summary>
        /// day_consume
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "day_consume", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 74)]
        public System.Decimal? day_consume { get; set; }

        /// <summary>
        /// DayMaxMoney
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "DayMaxMoney", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 75)]
        public System.Decimal? DayMaxMoney { get; set; }

        /// <summary>
        /// DayTime
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "DayTime", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 76)]
        public System.Int32? DayTime { get; set; }

        /// <summary>
        /// TimeMaxMoney
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "TimeMaxMoney", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 77)]
        public System.Decimal? TimeMaxMoney { get; set; }

        /// <summary>
        /// DayTotalMoney
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "DayTotalMoney", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 78)]
        public System.Decimal? DayTotalMoney { get; set; }

        /// <summary>
        /// DayTotalTime
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "DayTotalTime", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 79)]
        public System.Int32? DayTotalTime { get; set; }

        /// <summary>
        /// isOver_time
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "isOver_time", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 80)]
        public System.Boolean? isOver_time { get; set; }

        /// <summary>
        /// emp_pass_word
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "emp_pass_word", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 81)]
        public System.String emp_pass_word { get; set; }

        /// <summary>
        /// Lift_holiday
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Lift_holiday", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 82)]
        public System.Boolean? Lift_holiday { get; set; }

        /// <summary>
        /// Lift_limitdate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Lift_limitdate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 83)]
        public System.DateTime? Lift_limitdate { get; set; }

        /// <summary>
        /// Access_holiday
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Access_holiday", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 84)]
        public System.Boolean? Access_holiday { get; set; }

        /// <summary>
        /// Access_LimitDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Access_LimitDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 85)]
        public System.DateTime? Access_LimitDate { get; set; }

       
    }




    /// <summary>
    /// EntityMealRecords
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "MealRecords")]
    public class EntityMealRecords : BaseDataContract
    {
        /// <summary>
        /// emp_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "emp_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 1)]
        public System.String emp_id { get; set; }

        /// <summary>
        /// card_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "card_id", DbType = DbType.String, IsPK = true, IsSeq = false, SerNo = 2)]
        public System.String card_id { get; set; }

        /// <summary>
        /// WalletFlag
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "WalletFlag", DbType = DbType.Int32, IsPK = true, IsSeq = false, SerNo = 3)]
        public System.Int32 WalletFlag { get; set; }

        /// <summary>
        /// card_sequ
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "card_sequ", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.Int32 card_sequ { get; set; }

        /// <summary>
        /// clock_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "clock_id", DbType = DbType.Int32, IsPK = true, IsSeq = false, SerNo = 5)]
        public System.Int32 clock_id { get; set; }

        /// <summary>
        /// pos_sequ
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "pos_sequ", DbType = DbType.Int32, IsPK = true, IsSeq = false, SerNo = 6)]
        public System.Int32 pos_sequ { get; set; }

        /// <summary>
        /// opcard_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "opcard_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.String opcard_id { get; set; }

        /// <summary>
        /// ver_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "ver_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.String ver_id { get; set; }

        /// <summary>
        /// card_times
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "card_times", DbType = DbType.Int32, IsPK = true, IsSeq = false, SerNo = 9)]
        public System.Int32 card_times { get; set; }

        /// <summary>
        /// card_consume
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "card_consume", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 10)]
        public System.Decimal card_consume { get; set; }

        /// <summary>
        /// card_balance
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "card_balance", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 11)]
        public System.Decimal card_balance { get; set; }

        /// <summary>
        /// card_oldbalance
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "card_oldbalance", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 12)]
        public System.Decimal card_oldbalance { get; set; }

        /// <summary>
        /// kind
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "kind", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 13)]
        public System.String kind { get; set; }


        public string mealtypeStr
        {
            get
            {
                if (kind == "1")
                    return "早餐";
                else if (kind == "2")
                    return "午餐";
                else if (kind == "3")
                    return "晚餐";

                return "";
            }
        }

        /// <summary>
        /// CardTypeCode
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "CardTypeCode", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 14)]
        public System.String CardTypeCode { get; set; }

        /// <summary>
        /// AreaCode
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "AreaCode", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 15)]
        public System.String AreaCode { get; set; }

        /// <summary>
        /// PSAMAppSN
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "PSAMAppSN", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 16)]
        public System.String PSAMAppSN { get; set; }

        /// <summary>
        /// TAC
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "TAC", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 17)]
        public System.String TAC { get; set; }

        /// <summary>
        /// CompanyID
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "CompanyID", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 18)]
        public System.String CompanyID { get; set; }

        /// <summary>
        /// CommonEmpID
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "CommonEmpID", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 19)]
        public System.String CommonEmpID { get; set; }

        /// <summary>
        /// CompanyEmpID
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "CompanyEmpID", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 20)]
        public System.String CompanyEmpID { get; set; }

        /// <summary>
        /// BusiType
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "BusiType", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 21)]
        public System.Int32 BusiType { get; set; }

        /// <summary>
        /// BusiStyle
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "BusiStyle", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 22)]
        public System.Int32 BusiStyle { get; set; }

        /// <summary>
        /// CardFormat
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "CardFormat", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 23)]
        public System.Int32 CardFormat { get; set; }

        /// <summary>
        /// sign_time
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sign_time", DbType = DbType.DateTime, IsPK = true, IsSeq = false, SerNo = 24)]
        public System.DateTime sign_time { get; set; }

        [DataMember]
        public string signTimeStr { get; set; }

        /// <summary>
        /// mark
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "mark", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 25)]
        public System.Int32? mark { get; set; }

        /// <summary>
        /// flag
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "flag", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 26)]
        public System.Int32? flag { get; set; }

        /// <summary>
        /// passed
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "passed", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 27)]
        public System.Boolean? passed { get; set; }

        /// <summary>
        /// mealtype
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "mealtype", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 28)]
        public System.Int32? mealtype { get; set; }

        /// <summary>
        /// op_ymd
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "op_ymd", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 29)]
        public System.DateTime? op_ymd { get; set; }

        /// <summary>
        /// difine_sequ
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "difine_sequ", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 30)]
        public System.Int32? difine_sequ { get; set; }

        /// <summary>
        /// op_user
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "op_user", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 31)]
        public System.String op_user { get; set; }

        /// <summary>
        /// op_date
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "op_date", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 32)]
        public System.DateTime? op_date { get; set; }

        /// <summary>
        /// AllowanceBalance
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "AllowanceBalance", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 33)]
        public System.Decimal? AllowanceBalance { get; set; }

        /// <summary>
        /// MoneyType
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "MoneyType", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 34)]
        public System.Int32? MoneyType { get; set; }

        /// <summary>
        /// Modify_balance
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Modify_balance", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 35)]
        public System.Decimal? Modify_balance { get; set; }

        /// <summary>
        /// OnLineId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "OnLineId", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 36)]
        public System.Int32? OnLineId { get; set; }

        /// <summary>
        /// other_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "other_id", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 37)]
        public System.Int32? other_id { get; set; }

        /// <summary>
        /// PushResult
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "PushResult", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 38)]
        public System.Int32? PushResult { get; set; }

        /// <summary>
        /// Depart_id
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "Depart_id", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 39)]
        public System.String Depart_id { get; set; }

        /// <summary>
        /// uploaded
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "uploaded", DbType = DbType.Int32, IsPK = false, IsSeq = false, SerNo = 40)]
        public System.Int32? uploaded { get; set; }

        /// <summary>
        /// AllowanceMoney
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "AllowanceMoney", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 41)]
        public System.Decimal? AllowanceMoney { get; set; }

        /// <summary>
        /// dCollectTime
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "dCollectTime", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 42)]
        public System.DateTime? dCollectTime { get; set; }

        /// <summary>
        /// nRecSeq
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "nRecSeq", DbType = DbType.Int64, IsPK = false, IsSeq = false, SerNo = 43)]
        public System.Int64 nRecSeq { get; set; }

        /// <summary>
        /// dAccDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "dAccDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 44)]
        public System.DateTime? dAccDate { get; set; }

        /// <summary>
        /// bIsInvalid
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bIsInvalid", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 45)]
        public System.Boolean? bIsInvalid { get; set; }

        /// <summary>
        /// bPassCheck
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bPassCheck", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 46)]
        public System.Boolean? bPassCheck { get; set; }

        /// <summary>
        /// bObsolete
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bObsolete", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 47)]
        public System.Boolean? bObsolete { get; set; }

        /// <summary>
        /// DoAnomaly
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "DoAnomaly", DbType = DbType.Boolean, IsPK = false, IsSeq = false, SerNo = 48)]
        public System.Boolean? DoAnomaly { get; set; }

        /// <summary>
        /// bakFiled
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bakFiled", DbType = DbType.Int16, IsPK = false, IsSeq = false, SerNo = 49)]
        public int bakFiled { get; set; }

        [DataMember]
        public string empName { get; set; }

        [DataMember]
        public string dteBegin { get; set; }
        [DataMember]
        public string dteEnd { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string emp_id = "emp_id";
            public string card_id = "card_id";
            public string WalletFlag = "WalletFlag";
            public string card_sequ = "card_sequ";
            public string clock_id = "clock_id";
            public string pos_sequ = "pos_sequ";
            public string opcard_id = "opcard_id";
            public string ver_id = "ver_id";
            public string card_times = "card_times";
            public string card_consume = "card_consume";
            public string card_balance = "card_balance";
            public string card_oldbalance = "card_oldbalance";
            public string kind = "kind";
            public string CardTypeCode = "CardTypeCode";
            public string AreaCode = "AreaCode";
            public string PSAMAppSN = "PSAMAppSN";
            public string TAC = "TAC";
            public string CompanyID = "CompanyID";
            public string CommonEmpID = "CommonEmpID";
            public string CompanyEmpID = "CompanyEmpID";
            public string BusiType = "BusiType";
            public string BusiStyle = "BusiStyle";
            public string CardFormat = "CardFormat";
            public string sign_time = "sign_time";
            public string mark = "mark";
            public string flag = "flag";
            public string passed = "passed";
            public string mealtype = "mealtype";
            public string op_ymd = "op_ymd";
            public string difine_sequ = "difine_sequ";
            public string op_user = "op_user";
            public string op_date = "op_date";
            public string AllowanceBalance = "AllowanceBalance";
            public string MoneyType = "MoneyType";
            public string Modify_balance = "Modify_balance";
            public string OnLineId = "OnLineId";
            public string other_id = "other_id";
            public string PushResult = "PushResult";
            public string Depart_id = "Depart_id";
            public string uploaded = "uploaded";
            public string AllowanceMoney = "AllowanceMoney";
            public string dCollectTime = "dCollectTime";
            public string nRecSeq = "nRecSeq";
            public string dAccDate = "dAccDate";
            public string bIsInvalid = "bIsInvalid";
            public string bPassCheck = "bPassCheck";
            public string bObsolete = "bObsolete";
            public string DoAnomaly = "DoAnomaly";
            public string bakFiled = "bakFiled";
        }
    }


    public class EntityGenerMeal
    {
        public string card_id { get; set; }
        public string emp_fname { get; set; }
        public string dteBegin { get; set; }
        public string dteEnd { get; set; }

        public string kind { get; set; }


    }

}
