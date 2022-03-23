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
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace test
{
    public partial class frmEastRiver : Form
    {
        public frmEastRiver()
        {
            InitializeComponent();
        }

        List<EntityEmployee> lstEmployee { get; set; }
        List<EntityMealRecords> lstMealRecords { get; set; }
        EntityEmployee employee { get; set; }
        private void frmEastRiver_Load(object sender, EventArgs e)
        {
            Init();
        }


        void Init()
        {
            this.chklstMeal.SetItemChecked(0,true);
            this.dteBegin.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
            this.dteEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lstEmployee = GetEmployeeDatasource();
            this.gcEmployee.DataSource = lstEmployee;
            this.gcEmployee.RefreshDataSource();
        }

        public List<EntityEmployee> GetEmployeeDatasource()
        {
            List<EntityEmployee> data = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = @"select card_id,
                                    emp_id,
                                  emp_fname 
                                   from Employee ";
            DataTable dt = svc.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityEmployee>();
                EntityEmployee vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityEmployee();
                    vo.emp_id = dr["emp_id"].ToString();
                    vo.card_id = dr["card_id"].ToString();
                    vo.emp_fname = dr["emp_fname"].ToString();
                    data.Add(vo);
                }
            }

            return data;
        }


        public EntityEmployee GetEmployee(string card_id = null)
        {
            EntityEmployee vo = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = @"select card_id,
                                    emp_id,
                                  emp_fname 
                                   from Employee ";

            if (!string.IsNullOrEmpty(card_id))
                sql += " where card_id = '" + card_id + "'";
            DataTable dt = svc.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            { 
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityEmployee();
                    vo.emp_id = dr["emp_id"].ToString();
                    vo.card_id = dr["card_id"].ToString();
                    vo.emp_fname = dr["emp_fname"].ToString();
                }
            }

            return vo;
        }

        void Query()
        {
            
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            List<EntityParm> dicParm = new List<EntityParm>();
            string beginDate = this.dteBegin.Text.Trim();
            string endDate = this.dteEnd.Text.Trim();

            if (beginDate != string.Empty && endDate != string.Empty)
            {
                if (Function.Datetime(beginDate + " 00:00:00") > Function.Datetime(endDate + " 00:00:00"))
                {
                    DialogBox.Msg("开始时间不能大于结束时间。");
                    return;
                }

                dicParm.Add(Function.GetParm("signTime", beginDate + "|" + endDate));
            }
            else
            {
                DialogBox.Msg("开始或结束时间不能为空。");
            }

            if (employee !=null)
            {
                dicParm.Add(Function.GetParm("cardId", employee.card_id));
            }

            string kind = chklstMeal.SelectedIndex.ToString();

            if(!string.IsNullOrEmpty(kind))
            {
                dicParm.Add(Function.GetParm("kind",kind));
            }

            string sql = @"SELECT
	                        b.emp_fname,
	                        a.*
                        FROM MealRecords a
                        LEFT JOIN Employee b 
                            ON a.card_id = b.card_id
                        WHERE a.sign_time BETWEEN ? AND ?";

            List<IDataParameter> lstParm = new List<IDataParameter>();
            if (dicParm != null)
            {
                foreach (EntityParm po in dicParm)
                {

                    switch (po.key)
                    {
                        case "signTime":
                            IDataParameter parm1 = svc.CreateParm();
                            parm1.Value = beginDate + " 00:00:00";
                            lstParm.Add(parm1);
                            IDataParameter parm2 = svc.CreateParm();
                            parm2.Value = endDate + " 23:59:59";
                            lstParm.Add(parm2);
                            break;
                        case "cardId":
                            sql += " and b.card_id = '" + po.value + "'";
                            break;
                        case "kind":
                            if (kind == "-1" || kind == "0")
                            {

                            }
                            else 
                                sql += " and kind = '" + po.value + "'";
                            break;
                        default:
                            break;
                    }
                }
            }

            sql += " order by a.sign_time";
            DataTable dt = svc.GetDataTable(sql, lstParm.ToArray());
            if (dt != null)
            {
                lstMealRecords = new List<EntityMealRecords>();
                EntityMealRecords vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityMealRecords();
                    vo.empName = dr["emp_fname"].ToString();
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
                    vo.bakFiled = Function.Int(dr["bakFiled"]);
                    vo.signTimeStr = Function.Datetime(vo.sign_time).ToString("yyyy-MM-dd HH:mm");
                    lstMealRecords.Add(vo);
                }
            }

            this.gcData.DataSource = lstMealRecords;
            this.gcData.RefreshDataSource();
        }
            

        #region events
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }
        #endregion

        public void SingleSelectCheckedListBoxControls(CheckedListBoxControl chkControl, int index)
        {
            if (chkControl.CheckedItems.Count > 0)
            {
                for (int i = 0; i < chkControl.Items.Count; i++)
                {
                    if (i != index)
                    {
                        chkControl.SetItemCheckState(i, System.Windows.Forms.CheckState.Unchecked);
                    }
                }
            }
        }

        private void chklstMeal_ItemChecking(object sender, DevExpress.XtraEditors.Controls.ItemCheckingEventArgs e)
        {
            SingleSelectCheckedListBoxControls(this.chklstMeal, e.Index);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(lstEmployee != null)
            {
                frmMeals frm = new frmMeals(lstEmployee);
                frm.beginDate = this.dteBegin.Text;
                frm.endDate = this.dteEnd.Text;
                frm.ShowDialog();
            }
        }

        private void gvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == this.gvData.FocusedColumn && e.RowHandle == this.gvData.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.FromArgb(251, 165, 8);
                e.Appearance.BackColor2 = Color.White;
            }

            int hand = e.RowHandle;
            if (hand < 0) return;
            EntityMealRecords vo = gvData.GetRow(hand) as EntityMealRecords;
            if (vo.bakFiled == 1)
                e.Appearance.ForeColor = Color.Red;
            gvData.Invalidate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<EntityMealRecords> data = new List<EntityMealRecords>();
            if (this.gcData.DataSource != null)
            {
                for (int i = 0; i < this.gvData.RowCount; i++)
                {
                    if (this.gvData.IsRowSelected(i))
                    {
                        EntityMealRecords vo = this.gvData.GetRow(i) as EntityMealRecords;
                        if (vo.bakFiled == 1)
                            data.Add(vo);
                    }
                }
            }

            if (data != null)
            {
                if (DelMealRecord(data) > 0)
                {
                    this.Query();
                    MessageBox.Show("删除成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("删除失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        int DelMealRecord(List<EntityMealRecords> lstMealRecords)
        {
            int affect = -1;
            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
                if (lstMealRecords != null)
                {
                    foreach (var vo in lstMealRecords)
                    {
                        lstParm.Add(svc.GetDelParmByPk(vo));
                    }
                }
                if (lstParm.Count > 0)
                    affect = svc.Commit(lstParm);
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }

            return affect;
        }

        private void gvEmployee_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            employee = this.gvEmployee.GetRow(e.RowHandle) as EntityEmployee;
        }

        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {
            string search = this.txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(search))
            {
                List<EntityEmployee> lstTmp = lstEmployee.FindAll(r => r.emp_fname.Contains(search));
                this.gcEmployee.DataSource = lstTmp;
                this.gcEmployee.RefreshDataSource();
            }
            else
            {
                this.gcEmployee.DataSource = lstEmployee;
                this.gcEmployee.RefreshDataSource();
            }
        }

        private void btnInsertDay_Click(object sender, EventArgs e)
        {
            if (lstEmployee != null)
            {
                frmMealsDay frm = new frmMealsDay(lstEmployee);
                frm.beginDate = this.dteBegin.Text;
                frm.endDate = this.dteEnd.Text;
                frm.ShowDialog();
            }
        }
    }
}
