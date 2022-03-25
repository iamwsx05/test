using Common.Controls;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Ribbon;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using test;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace XTYY
{
    public partial class frmXtyyRpt : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmXtyyRpt()
        {
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");
        }

        string filePath = string.Empty;
        private void Form1_Load(object sender, EventArgs e)
        {
            ribbonControl.ForceInitialize();
            GalleryDropDown skins = new GalleryDropDown();
            skins.Ribbon = ribbonControl;
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGalleryDropDown(skins);
            iPaintStyle.DropDownControl = skins;
            this.lblFile.Text = string.Empty;
            Init();
        }

        List<EntityDw> lstDw = null;
        #region Init
       
        void Init()
        {
            DateTime dteNow = DateTime.Now;
            this.dteBegin.Text = dteNow.ToString("yyyy-MM") + "-01";
            this.dteEnd.Text = dteNow.ToString("yyyy-MM-dd");
            lstDw = new List<EntityDw>();
            lstDw = GetZdDw();
            lueDw.EditValue = "lnc_code";
            lueDw.Properties.ValueMember = "lnc_code";
            lueDw.Properties.DisplayMember = "lnc_name";
            lueDw.Properties.DataSource = lstDw;
            //自适应宽度
            lueDw.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            //填充列
            lueDw.Properties.PopulateColumns();
            //设置列属性
            lueDw.Properties.Columns[0].Caption = "编号";
            lueDw.Properties.Columns[1].Caption = "名称";
            lueDw.Properties.Columns[0].Width = 70;
            lueDw.Properties.Columns[1].Width = 300;
            //控制选择项的总宽度
            lueDw.Properties.PopupWidth = 900;
        }
        #endregion

        internal List<EntityDw> GetZdDw()
        {
            List<EntityDw> data = new List<EntityDw>();

            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                string sql = @"select * from zddw";

                DataTable dt = svc.GetDataTable(sql);

                if(dt != null && dt.Rows.Count > 0)
                {
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDw vo = Function.Row2Model<EntityDw>(dr);
                        data.Add(vo);
                    }
                }    
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return data;
        }

        #region 
        private void iOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new OpenFileDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                String filepath = f.FileName;//G:\新建文件夹\新建文本文档.txt
                String filename = f.SafeFileName;//新建文本文档.txt
                filePath = filepath;
            }

            lblFile.Text = filePath;
        }

        private void iState_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {

            }
            else if (tabPane1.SelectedPageIndex == 1)
            {

            }
            else if (tabPane1.SelectedPageIndex == 2)
            {

            }

        }

        private void iExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {
                //uiHelper.ExportToXls(this.gvTJxm);
            }
            else if (tabPane1.SelectedPageIndex == 1)
            {

            }
            else if (tabPane1.SelectedPageIndex == 2)
            {

            }
        }

        #region Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {

            }
            else if (tabPane1.SelectedPageIndex == 1)
            {

            }
            else if (tabPane1.SelectedPageIndex == 2)
            {

            }
            else if (tabPane1.SelectedPageIndex == 4)
            {

            }
            else if (tabPane1.SelectedPageIndex == 5)
            {

            }
        }
        #endregion

        #endregion

        #region GetRowObj
        //public EntityPeItem GetRowObjPeItem()
        //{
        //    if (this.gvPeResult.FocusedRowHandle < 0)
        //        return null;
        //    else
        //    {
        //        EntityPeItem vo = this.gvPeResult.GetRow(this.gvPeResult.FocusedRowHandle) as EntityPeItem;
        //        return vo;
        //    }
        //}


        #endregion

        #region 项目报表
        internal void QueryItemRpt()
        {
            List<EntityItemRpt> data = new List<EntityItemRpt>();

            string lncCode = this.lueDw.EditValue.ToString();
            string lncName = this.lueDw.Text;
            string beginTime = this.dteBegin.Text;
            string endTime = this.dteEnd.Text;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = @"select * from (select b.reg_date,b.lnc_code, b.reg_no, 
                                     e.comb_code,e.cls_code,
                                     e.comb_name,
                                      deptName= (select dept_name from zdKs where dept_code = e.dept_code),
                                     d.price1, 
                                     d.rate,
                                     d.rb_total
                                     from ywBrxx a,ywDjxx b,ywTjxmzx d,zdZhxm e        
                                     where a.pat_code = b.pat_code and             
                                           a.reg_times = b.reg_times and        
                                           b.reg_no = d.reg_no and           
                                           d.comb_code = e.comb_code            
                                           and  b.active = 'T'
                                     union all 
                                     select  b.reg_date, b.lnc_code, b.reg_no, 
                                     e.comb_code,e.cls_code,
                                     e.comb_name,
                                     deptName= (select dept_name from zdKs where dept_code = e.dept_code),
                                     d.price1, 
                                     d.rate,
                                     d.rb_total     
                                     from ywBrxx a,ywDjxx b,ywTjxm d,zdZhxm e           
                                     where a.pat_code = b.pat_code and             
                                           a.reg_times = b.reg_times and        
                                           b.reg_no = d.reg_no and           
                                           d.comb_code = e.comb_code                   
                                           and  b.active = 'T')  tmp where reg_date between ? and ? ";

            if (!string.IsNullOrEmpty(lncCode))
                sql += " and lnc_code = '" + lncCode.Trim() + "'";
            sql += " order by cls_code;";
            IDataParameter[] parm = svc.CreateParm(2);
            parm[0].Value = beginTime.Replace("-", ".");
            parm[1].Value = endTime.Replace("-", ".");
            DataTable dt = svc.GetDataTable(sql, parm);
            int n = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EntityItemRpt vo = new EntityItemRpt();

                    vo.itemCode = dr["comb_code"].ToString().Trim();
                    vo.itemName = dr["comb_name"].ToString().Trim();
                    vo.zk = (Function.Dec(dr["rate"]) * 100).ToString() + "%";
                    vo.zqdj = Function.Dec(dr["price1"]);
                    vo.zhdj = Function.Dec((vo.zqdj * Function.Dec(dr["rate"])).ToString("0.00"));
                    vo.kdks = "体检中心";
                    vo.zxks = dr["deptName"].ToString();
                    vo.bz = "";

                    if (data.Any(r => r.itemCode == vo.itemCode && r.zhdj == vo.zhdj))
                    {
                        EntityItemRpt voClone = data.Find(r => r.itemCode == vo.itemCode && r.zhdj == vo.zhdj);
                        voClone.tjrc++;
                        voClone.zqje += vo.zqdj;
                        voClone.zhje += vo.zhdj;
                    }
                    else
                    {
                        vo.xh = ++n;
                        vo.tjrc = 1;
                        vo.zqje = vo.zqdj;
                        vo.zhje = vo.zhdj;
                        data.Add(vo);
                    }
                }
            }

            if (!string.IsNullOrEmpty(lncName))
                this.gvItemRpt.GroupPanelText = "体检单位：" + lncName + "    " + beginTime + "~" + endTime;
            else
                this.gvItemRpt.GroupPanelText = beginTime + "~" + endTime;

            this.gcItemRpt.DataSource = data;
            this.gcItemRpt.RefreshDataSource();
        }
        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {
                QueryItemRpt();
            }
            else if (tabPane1.SelectedPageIndex == 1)
            {

            }
            else if (tabPane1.SelectedPageIndex == 2)
            {

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {
                uiHelper.ExportToXls(this.gvItemRpt,true);
            }
            else if (tabPane1.SelectedPageIndex == 1)
            {

            }
            else if (tabPane1.SelectedPageIndex == 2)
            {

            }
        }
    }
}
