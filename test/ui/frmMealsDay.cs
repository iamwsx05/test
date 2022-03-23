using Common.Controls;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Dac;
using weCare.Core.Utils;

namespace test
{
    public partial class frmMealsDay : Form
    {
        public frmMealsDay(List<EntityEmployee> _lstEmployee)
        {
            InitializeComponent();
            lstEmployee = _lstEmployee;
        }
        List<EntityEmployee> lstSelectEmp { get; set; }
        List<EntityEmployee> lstEmployee { get; set; }
        List<DevExpress.XtraEditors.CheckEdit> lstCheck = new List<DevExpress.XtraEditors.CheckEdit>();
        public string beginDate { get; set; }
        public string endDate { get; set; }

        void Init()
        {
            lstCheck.Add(chkBref);
            lstCheck.Add(chkLunch);
            lstCheck.Add(chkDinner);
            this.gcEmployee.DataSource = lstEmployee;
            string dteNow = DateTime.Now.ToString("yyyy-MM-dd");
            if (dteNow == endDate)
                this.dteEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            else
                this.dteEnd.Text = endDate;
            this.dteBegin.Text = beginDate;  
            if (beginDate != string.Empty && endDate != string.Empty)
            {
                if (Function.Datetime(beginDate + " 00:00:00") > Function.Datetime(endDate + " 00:00:00"))
                {
                    DialogBox.Msg("开始时间不能大于结束时间。");
                    return;
                }
            }
            else
            {
                DialogBox.Msg("开始或结束时间不能为空。");
            }
        }

        public DateTime GetDateTime(string date,string hour)
        {
            Random random = new Random((int)(DateTime.Now.Ticks));
            DateTime rTime = Function.Datetime(date) ;
            int i = 0;
            while (i < 100)
            {
                int minute = random.Next(30, 59);
                int second = random.Next(0, 59);
                string tempStr = string.Format("{0} {1}:{2}:{3}", date, hour, minute, second);
                rTime = Convert.ToDateTime(tempStr);
                i++;
            }

            return rTime;
        }


        #region iChk_CheckedChanged
        /// <summary>
        /// iChk_CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iChk_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit iChk = sender as DevExpress.XtraEditors.CheckEdit;
            // 同时勾选
            if (iChk.Checked)
            {
                // 根据分组属性控制只选一个
                foreach (DevExpress.XtraEditors.CheckEdit chk in lstCheck)
                {
                    if (chk != iChk && chk.Properties.AccessibleName == iChk.Properties.AccessibleName)
                    {
                        chk.Checked = false;
                        ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                        ((DevExpress.XtraEditors.CheckEdit)sender).Update();
                    }
                }
                ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                ((DevExpress.XtraEditors.CheckEdit)sender).Update();
            }
            else
            {
                ((DevExpress.XtraEditors.CheckEdit)sender).Invalidate();
                ((DevExpress.XtraEditors.CheckEdit)sender).Update();
            }
        }
        #endregion

        public List<EntityMealRecords> GeneralMeals(EntityEmployee employee)
        {
            if (employee == null)
                return null;
            List<EntityMealRecords> lstGeneralMeal = new List<EntityMealRecords>();
            DateTime dteBegein = Function.Datetime(this.dteBegin.Text);
            DateTime dteEnd = Function.Datetime(this.dteEnd.Text);
            string hour = string.Empty;
            string kind = string.Empty;
            if (chkBref.Checked == true)
            {
                kind = "1";
                hour = "07";
            }
            if (chkLunch.Checked == true)
            {
                kind = "2";
                hour = "12";
            }
            if (chkDinner.Checked == true)
            {
                kind = "3";
                hour = "17";
            }
            EntityMealRecords lastVo = GetLastRecord(employee,dteEnd.ToString("yyyy-MM-dd"), kind);
            if (lastVo == null)
                return null;

            for (; dteBegein <= dteEnd; dteBegein = dteBegein.AddDays(1))
            {
                EntityMealRecords vo = new EntityMealRecords();
                vo.emp_id = employee.emp_id;
                vo.card_id = employee.card_id;
                vo.empName = employee.emp_fname;
                vo.sign_time = GetDateTime(dteBegein.ToString("yyyy-MM-dd"), hour);
                vo.signTimeStr = vo.sign_time.ToString("yyyy-MM-dd HH:mm");
                vo.kind = kind;
                vo.WalletFlag = lastVo.WalletFlag;
                vo.card_sequ = lastVo.card_sequ;
                vo.clock_id = lastVo.clock_id;
                vo.pos_sequ = lastVo.pos_sequ;
                vo.card_times = lastVo.card_times;
                vo.card_consume = lastVo.card_consume;
                vo.card_balance = lastVo.card_balance;
                vo.AreaCode = lastVo.AreaCode;
                vo.BusiStyle = lastVo.BusiStyle;
                vo.BusiType = lastVo.BusiType;
                vo.CardFormat = lastVo.CardFormat;
                vo.mark = lastVo.mark;
                vo.flag = lastVo.flag;
                vo.passed = lastVo.passed;
                vo.mealtype = lastVo.mealtype;
                vo.op_ymd = dteBegein;
                vo.AllowanceBalance = lastVo.AllowanceBalance;
                vo.other_id = lastVo.other_id;
                vo.PushResult = lastVo.PushResult;
                vo.Depart_id = lastVo.Depart_id;
                vo.uploaded = lastVo.uploaded;
                vo.AllowanceMoney = lastVo.AllowanceMoney;
                vo.dCollectTime = vo.sign_time.AddMinutes(8);
                vo.nRecSeq = lastVo.nRecSeq;
                vo.bakFiled = 1;
                if (!CheckMealRecords(vo))
                    lstGeneralMeal.Add(vo);
            }

            return lstGeneralMeal;
        }

        bool CheckMealRecords(EntityMealRecords mealRecord)
        {
            bool isAllow = false;
            if (mealRecord == null)
                return false;

            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

            string sql = @"select 1 from MealRecords a 
                                    where a.card_id = ? 
                                    and a.sign_time BETWEEN ? AND ?
                                    and a.kind = ?";

            IDataParameter[] param = svc.CreateParm(4);
            param[0].Value = mealRecord.card_id;
            param[1].Value = Function.Datetime(mealRecord.sign_time).ToString("yyyy-MM-dd") + " 00:00:00" ;
            param[2].Value = Function.Datetime(mealRecord.sign_time).ToString("yyyy-MM-dd") + " 23:59:59";
            param[3].Value = mealRecord.kind;

            DataTable dt = svc.GetDataTable(sql, param);
            if (dt != null && dt.Rows.Count > 0)
                isAllow = true;

            return isAllow;

        }

        public EntityMealRecords GetLastRecord(EntityEmployee employee, string dteEnd,string kind)
        {
            EntityMealRecords vo = null;
            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

                string sql = @"select * from MealRecords a 
                                    where a.card_id = ?
                                    and a.sign_time BETWEEN ? AND ?
                                    and a.kind = ? order by a.sign_time desc";

                IDataParameter[] param = svc.CreateParm(4);
                param[0].Value = employee.card_id;
                param[1].Value = "2018-01-01 00:00:00";
                param[2].Value = dteEnd + " 23:59:59";
                param[3].Value = kind;

                DataTable dt = svc.GetDataTable(sql, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    vo = new EntityMealRecords();
                    DataRow dr = dt.Rows[0];
                    vo.emp_id = dr["emp_id"].ToString();
                    vo.card_id = dr["card_id"].ToString();
                    vo.WalletFlag = Function.Int(dr["WalletFlag"]);
                    vo.card_sequ = Function.Int(dr["card_sequ"]);
                    vo.clock_id = Function.Int(dr["clock_id"]);
                    vo.pos_sequ = Function.Int(dr["pos_sequ"]);
                    vo.opcard_id = dr["opcard_id"].ToString();
                    vo.ver_id = dr["ver_id"].ToString();
                    vo.card_times = Function.Int(dr["card_times"]);
                    vo.card_consume = Function.Dec(dr["card_consume"]);
                    vo.card_balance = Function.Dec(dr["card_balance"]);
                    vo.card_oldbalance = Function.Dec(dr["card_oldbalance"]);
                    vo.kind = dr["kind"].ToString();
                    vo.CardTypeCode = dr["CardTypeCode"].ToString();
                    vo.AreaCode = dr["AreaCode"].ToString();
                    vo.PSAMAppSN = dr["PSAMAppSN"].ToString();
                    vo.sign_time = Function.Datetime(dr["sign_time"]);
                    vo.mark = Function.Int(dr["mark"]);
                    vo.flag = Function.Int(dr["flag"]);
                    vo.mealtype = Function.Int(dr["mealtype"]);
                    vo.op_ymd = Function.Datetime(dr["op_ymd"]);
                    vo.other_id = Function.Int(dr["other_id"]);
                    vo.PushResult = Function.Int(dr["PushResult"]);
                    vo.Depart_id = dr["Depart_id"].ToString();
                    vo.uploaded = Function.Int(dr["uploaded"]);
                    vo.AllowanceBalance = Function.Dec(dr["AllowanceBalance"]);
                    vo.AllowanceMoney = Function.Dec(dr["AllowanceMoney"]);
                    vo.dCollectTime = Function.Datetime(dr["dCollectTime"]);
                    vo.nRecSeq = Function.Int(dr["nRecSeq"]);
                }
            }
            catch(Exception ex)
            {
                ExceptionLog.OutPutException(ex); 
            }
            
            return vo;
        }

        private void frmMeals_Load(object sender, EventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstMealRecords"></param>
        /// <returns></returns>
        public int SaveMealRecords(List<EntityMealRecords>  lstMealRecords)
        {
            int affect = -1;
            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
                if(lstMealRecords != null)
                {
                    foreach(var vo in lstMealRecords)
                    {
                        lstParm.Add(svc.GetInsertParm(vo)); 
                    }
                }
                if (lstParm.Count > 0)
                    affect = svc.Commit(lstParm);
            }
            catch(Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
           
            return affect;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<EntityMealRecords> data = null;
            if (lstSelectEmp != null)
            {
                data = new List<EntityMealRecords>();
                foreach (var vo in lstSelectEmp)
                {
                    List<EntityMealRecords> lstR = GeneralMeals(vo);
                    if (lstR == null || lstR.Count <= 0)
                        continue;
                    else 
                        data.AddRange(lstR);
                }
            }

            if (data == null || data.Count <= 0)
            {
                return;
            }
                
            if (SaveMealRecords(data) > 0)
            {
                MessageBox.Show("保存成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {
            string search = this.txtSearch.Text.Trim();

            if(!string.IsNullOrEmpty(search))
            {
                List<EntityEmployee> lstTmp = lstEmployee.FindAll(r=>r.emp_fname.Contains(search));
                this.gcEmployee.DataSource = lstTmp;
                this.gcEmployee.RefreshDataSource();
            }
            else
            {
                this.gcEmployee.DataSource = lstEmployee;
                this.gcEmployee.RefreshDataSource();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {

                lstSelectEmp = GetSelectData();
                this.gridControl.DataSource = lstSelectEmp;
                this.gridControl.RefreshDataSource();
            
        }

        #region
        List<EntityEmployee> GetSelectData()
        {
            List<EntityEmployee> data = new List<EntityEmployee>();

            for (int i = 0; i < this.gvEmployee.RowCount; i++)
            {
                if (this.gvEmployee.IsRowSelected(i))
                {
                    data.Add((this.gvEmployee.GetRow(i) as EntityEmployee));
                }
            }

            return data;
        }
        #endregion
    }
}
