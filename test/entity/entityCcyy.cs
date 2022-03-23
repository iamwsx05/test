using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CCYY
{
    [Serializable]
    public class EntityFromJosn
    {
        [DataMember]
        public EntityRes Response { get; set; }
    }

    [Serializable]
    public class EntityRes
    {
        [DataMember]
        public string Result { get; set; }
        [DataMember]
        public string Error { get; set; }
        [DataMember]
        public List<EntityHisItem> DataTable { get; set; }
    }

    [Serializable]
    public class EntityHisItem
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string py_code { get; set; }
        [DataMember]
        public string wb_code { get; set; }
        [DataMember]
        public string audit_code { get; set; }
        [DataMember]
        public string audit_name { get; set; }
        [DataMember]
        public string charge_price { get; set; }
        [DataMember]
        public string charge_unit { get; set; }
        [DataMember]
        public string specifications { get; set; }
    }



    public class EntityHisFee
    {
        public string peCode { get; set; }
        public string peName { get; set; }
        public string hisCode { get; set; }
        public string hisName { get; set; }
        public string amount { get; set; }

    }


    public class EntityZdzhxm
    {
        public string comb_code { get; set; }
        public string comb_name { get; set; }
        public decimal price { get; set; }
    }

    public class EntityPeHis
    {
        public string comb_code { get; set; }
        public string comb_name { get; set; }
        public string his_code { get; set; }
        public string his_name { get; set; }
        public string addType { get; set; }
        public string amount { get; set; }
    }
}
