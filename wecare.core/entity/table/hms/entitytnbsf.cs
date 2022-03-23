using System;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace weCare.Core.Entity
{
    /// <summary>
    /// EntityTnbSf
    /// </summary>
    [DataContract, Serializable]
    [EntityAttribute(TableName = "tnbSf")]
    public class EntityTnbSf : BaseDataContract
    {
        /// <summary>
        /// sfId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfId", DbType = DbType.Decimal, IsPK = true, IsSeq = false, SerNo = 1)]
        public System.Decimal sfId { get; set; }

        /// <summary>
        /// recId
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "recId", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 2)]
        public System.Decimal recId { get; set; }

        /// <summary>
        /// sfMethod
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfMethod", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 3)]
        public System.String sfMethod { get; set; }

        /// <summary>
        /// sfClass
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfClass", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 4)]
        public System.String sfClass { get; set; }

        /// <summary>
        /// sfDate
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfDate", DbType = DbType.DateTime, IsPK = false, IsSeq = false, SerNo = 5)]
        public System.DateTime sfDate { get; set; }

        /// <summary>
        /// sfRecorder
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfRecorder", DbType = DbType.AnsiString, IsPK = false, IsSeq = false, SerNo = 6)]
        public System.String sfRecorder { get; set; }

        /// <summary>
        /// sfStatus
        /// </summary>
        [DataMember]
        [EntityAttribute(FieldName = "sfStatus", DbType = DbType.Decimal, IsPK = false, IsSeq = false, SerNo = 7)]
        public System.Int32 sfStatus { get; set; }

        /// <summary>
        /// Columns
        /// </summary>
        public static EnumCols Columns = new EnumCols();

        /// <summary>
        /// EnumCols
        /// </summary>
        public class EnumCols
        {
            public string sfId = "sfId";
            public string patId = "patId";
            public string sfMethod = "sfMethod";
            public string sfClass = "sfClass";
            public string sfDate = "sfDate";
            public string sfRecorder = "sfRecorder";
            public string sfStatus = "sfStatus";
        }
    }
}
