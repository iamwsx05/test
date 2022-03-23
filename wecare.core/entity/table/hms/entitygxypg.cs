using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace weCare.Core.Entity
{
    /// <summary>
    /// EntityGxyPg
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "gxyPg")]
    public class EntityGxyPg : BaseDataContract
    {
        /// <summary>
        /// pgId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "pgId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.Decimal pgId { get; set; }

        /// <summary>
        /// recId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.Decimal recId { get; set; }

        /// <summary>
        /// bloodPressLevel
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "bloodPressLevel", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String bloodPressLevel { get; set; }

        /// <summary>
        /// riskLevel
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "dangerLevel", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String dangerLevel { get; set; }

        /// <summary>
        /// manageLevel
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "manageLevel", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.String manageLevel { get; set; }

        /// <summary>
        /// evaluator
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "evaluator", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String evaluator { get; set; }

        /// <summary>
        /// evaDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "evaDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.DateTime evaDate { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "status", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 8)]
        public System.Int32 status { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string pgId = "pgId";
            public string patId = "patId";
            public string bloodPressLevel = "bloodPressLevel";
            public string dangerLevel = "dangerLevel";
            public string manageLevel = "manageLevel";
            public string evaluator = "evaluator";
            public string evaDate = "evaDate";
            public string status = "status";
        }
    }
}
