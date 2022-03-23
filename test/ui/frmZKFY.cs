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
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace test
{
    public partial class frmZKFY : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmZKFY()
        {
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");
        }



        string filePath = string.Empty;

        public Dictionary<string, List<string>> dicBmks = new Dictionary<string, List<string>>();

        private void Form1_Load(object sender, EventArgs e)
        {
            ribbonControl.ForceInitialize();
            GalleryDropDown skins = new GalleryDropDown();
            skins.Ribbon = ribbonControl;
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGalleryDropDown(skins);
            iPaintStyle.DropDownControl = skins;
            this.lblFile.Text = string.Empty;

            this.cboItem.Properties.Items.AddRange(new object[] { "产后康复", "母乳分析" ,"套房","心电图", "中晚期妊娠彩超套组", "(妇科)腹部彩超套组", "阴道彩超" , "胎儿生物评分", "健康咨询", "婴幼儿健康体检", "建立健康档案", "一般健康体检" });
        }


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
            else if (tabPane1.SelectedPageIndex == 4)
            {
                LoadFy();
            }
        }

        private void iExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {

            }
            else if (tabPane1.SelectedPageIndex == 1)
            {

            }
            else if (tabPane1.SelectedPageIndex == 2)
            {
                uiHelper.ExportToXls(this.gvCodItem);
            }
            else if(tabPane1.SelectedPageIndex == 3)
            {
                uiHelper.ExportToXls(this.gvBdjg);
                //uiHelper.ExportToXls(this.gvTczh);
            }
            else if(tabPane1.SelectedPageIndex == 4)
            {
                uiHelper.ExportToXls(this.gvIpFee);
            }
            else if (tabPane1.SelectedPageIndex == 7)
            {
                uiHelper.ExportToXls(this.gvJyMzRpt);
            }
            else if (tabPane1.SelectedPageIndex == 8)
            {
                uiHelper.ExportToXls(this.gvYm);
            }
            else if (tabPane1.SelectedPageIndex == 9)
            {
                uiHelper.ExportToXls(this.gvChkf);
            }
        }

        private void btnICD10_Click(object sender, EventArgs e)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("请选择文件！");
                return;
            }

            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.emrDB);
                string wbCode = string.Empty;
                string pyCode = string.Empty;
                string ficdm = string.Empty;
                string fjbname = string.Empty;
                string sql = string.Empty;
                List<EntitySs> data = new List<EntitySs>();

                sql = "select icdcode_vchr, icdcnname_vchr from t_dic_icd10 ";
                DataTable dtIcd = svc.GetDataTable(sql);


                DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);
                int affect = -1;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        pyCode = string.Empty;
                        ficdm = dr["ficdm"].ToString();
                        fjbname = dr["fjbname"].ToString();
                        DataRow[] drr = dtIcd.Select("icdcode_vchr = '" + ficdm + "'");
                        if (drr != null && drr.Length > 0)
                            continue;
                        pyCode = new SpellAndWbCodeTookit().GetSpellCode(fjbname);
                        if (pyCode.Length > 4)
                            pyCode = pyCode.Substring(0, 4);
                        wbCode = new SpellAndWbCodeTookit().GetSpellCode(fjbname);
                        if (wbCode.Length > 4)
                            wbCode = wbCode.Substring(0, 4);
                        sql = @"insert into t_dic_icd10 (icdcode_vchr, icdcnname_vchr, icdpycode_vchr, icdwbcode_vchr) values (?,?,?,?)";
                        IDataParameter[] parm = svc.CreateParm(4);
                        parm[0].Value = ficdm;
                        parm[1].Value = fjbname;
                        parm[2].Value = pyCode;
                        parm[3].Value = wbCode;
                        affect = svc.ExecSql(sql, parm);
                    }
                }

                if (affect > 0)
                    MessageBox.Show("success!!!!!!!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        #region tab 3

        private void btnInit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                string wbCode = string.Empty;
                string pyCode = string.Empty;
                string ficdm = string.Empty;
                string fjbname = string.Empty;
                string sql = string.Empty;
                List<EntityCodeItem> data = new List<EntityCodeItem>();

                sql = @"select  b.ITEM_CODE,a.ITEM_NAME,b.TYPE,a.ITEM_CLS, b.STANDARD,
                                b.dose_rate,
                                b.bcl_flag, --T
                                b.lcj_price,b.PACK_RATE,
                                b.RET_PRICE,
                                b.TRA_PRICE ,
                                b.BIG_UNIT,
                                b.SMALL_UNIT,
                                from CODE_ITEM a 
                                left join plUs_IteM b
                                on a.ITEM_CODE = b.ITEM_CODE
                                where b.TYPE in (2, 3)
                                and a.ITEM_CLS IN('1','2') 
                                --and a.PACK_CODE = '10'
                                --and b.STANDARD like '%*%' 
                                --and b.STANDARD  like '%u:%'";
                //and b.ITEM_CODE = 'X00000432' 
                DataTable dt = svc.GetDataTable(sql);

                if(dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        EntityCodeItem vo = new EntityCodeItem();

                        vo.itemCode = dr["ITEM_CODE"].ToString();
                        vo.itemName = dr["ITEM_NAME"].ToString();
                        vo.type = dr["TYPE"].ToString();
                        vo.lcjPrice = Function.Dec(dr["lcj_price"]) ;
                        vo.traPrice = Function.Dec(dr["RET_PRICE"]);
                        vo.retPrice = Function.Dec(dr["TRA_PRICE"]);
                        vo.packRat = Function.Dec(dr["PACK_RATE"]);
                        vo.itemCls = dr["ITEM_CLS"].ToString();
                        vo.standard = dr["STANDARD"].ToString().Replace("支", "").Replace("袋", "");
                        vo.doseRate = dr["dose_rate"].ToString();
                        vo.bclFlag = dr["bcl_flag"].ToString();
                        vo.BIG_UNIT = dr["BIG_UNIT"].ToString();
                        vo.SMALL_UNIT = dr["SMALL_UNIT"].ToString();

                        string standTmp = vo.standard.Split('*')[1];
                        if (vo.SMALL_UNIT == vo.BIG_UNIT)
                            continue;

                        data.Add(vo);
                    }
                }

                this.gcCodeItem.DataSource = data;
                this.gcCodeItem.RefreshDataSource();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        
        private void btnAj_Click(object sender, EventArgs e)
        {
            AjRate();
        }

        void AjRate()
        {
            try
            {
                string sql = string.Empty;
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                int affect = -1;
                if (this.gvCodItem.RowCount > 0)
                {
                    for (int i = 0; i < gvCodItem.RowCount; i++)
                    {
                        EntityCodeItem vo = this.gvCodItem.GetRow(i) as EntityCodeItem;

                        sql = @"update plUs_IteM set dose_rate= ?,bcl_flag = ? where ITEM_CODE = ? and TYPE = ? ";

                        IDataParameter[] parm = svc.CreateParm(4);

                        parm[0].Value = 1;
                        parm[1].Value = "T";
                        parm[2].Value = vo.itemCode;
                        parm[3].Value = vo.type;

                        affect = svc.ExecSql(sql, parm);
                    }
                }

                if (affect > 0)
                    MessageBox.Show("success !!!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




        #endregion

        #region tab4
        private void btnOpen1_Click(object sender, EventArgs e)
        {
            var f = new OpenFileDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                String filepath = f.FileName;//G:\新建文件夹\新建文本文档.txt
                String filename = f.SafeFileName;//新建文本文档.txt
                filePath = filepath;
            }

            lblOpen1.Text = filePath;
        }

        private void btnOpen2_Click(object sender, EventArgs e)
        {
            var f = new OpenFileDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                String filepath = f.FileName;//G:\新建文件夹\新建文本文档.txt
                String filename = f.SafeFileName;//新建文本文档.txt
                filePath = filepath;
            }

            lblOpen2.Text = filePath;
        }

        List<EntityFyHiszh> dataHis = new List<EntityFyHiszh>();
        List<EntityTczh> data = new List<EntityTczh>();
        private void btnJz_Click(object sender, EventArgs e)
        {
          
           
            try
            {
                Dictionary<string, string> dicTczh = new Dictionary<string, string>();
                DataTable dt2 = new ExcelHelper().ExcelToDataTableFormPath(true, lblOpen1.Text, 0);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {
                        dicTczh.Add(dr["TCZH"].ToString(), dr["TCMC"].ToString());
                    }
                }

                DataTable dt1 = new ExcelHelper().ExcelToDataTableFormPath(true, lblOpen2.Text, 0);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        EntityTczh vo = new EntityTczh();
                        vo.TCZH = dr["TCZH"].ToString();
                        vo.YPMC = dr["YPMC"].ToString();
                        if(dicTczh.ContainsKey(vo.TCZH))
                            vo.TCMC = dicTczh[vo.TCZH];
                        data.Add(vo);
                    }
                }

                
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

                string sql = @"select a.TEMP_NO,a.TEMP_NAME,c.ITEM_NAME,c.item_code from CL_DEF_RECTEMP a 
                                        left join CL_DEF_RECTEMPENTRY b
                                        on a.TEMP_NO = b.TEMP_NO
                                        left join CODE_ITEM c
                                        on b.ITEM_CODE = c.ITEM_CODE order by a.TEMP_NO";

                DataTable dt3 = svc.GetDataTable(sql);

                if(dt3 != null && dt3.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt3.Rows)
                    {
                        EntityFyHiszh vo = new EntityFyHiszh();
                        vo.tempNo = dr["TEMP_NO"].ToString();
                        vo.tempName = dr["TEMP_NAME"].ToString();
                        vo.itemName = dr["ITEM_NAME"].ToString();
                        vo.itemCode = dr["item_code"].ToString();
                        dataHis.Add(vo);
                    }
                }
                
                foreach(EntityTczh vo in data)
                {
                    EntityFyHiszh voH = dataHis.Find(r => r.itemName == vo.YPMC);
                    if(voH != null)
                    {
                        vo.hisItemCode = voH.itemCode;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.gcTczh.DataSource = data;
            this.gcTczh.RefreshDataSource();
            this.gcHiszh.DataSource = dataHis;
            this.gcHiszh.RefreshDataSource();

        }


        #endregion

        private void gvTczh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            this.gcHiszh.DataSource = null;
            this.gcHiszh.RefreshDataSource();
            EntityTczh voTc = this.gvTczh.GetRow(e.RowHandle) as EntityTczh;
            List <EntityFyHiszh> dataTmp = dataHis.FindAll(r => r.tempName == voTc.TCMC);
            if(dataTmp != null)
            {
                this.gcHiszh.DataSource = dataTmp;
                this.gcHiszh.RefreshDataSource();
            }
           

            for (int i = 0; i < this.gvHiszh.RowCount; i++)
            {
                this.gvHiszh.UnselectRow(i);
            }


            for (int i = 0; i < this.gvHiszh.RowCount; i++)
            {
                EntityFyHiszh vo = this.gvHiszh.GetRow(i) as EntityFyHiszh;

                if (vo.itemName == voTc.YPMC)
                {
                    this.gvHiszh.FocusedRowHandle = i;
                    this.gvHiszh.SelectRow(i);
                   
                    break;
                }
            }
        }

        private void gvHiszh_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == this.gvHiszh.FocusedRowHandle)
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void btnBD_Click(object sender, EventArgs e)
        {
            List<EntityTczh> data2 = new List<EntityTczh>();
            foreach(EntityTczh vo in data)
            {
                EntityFyHiszh fyVo = dataHis.Find(r => r.tempName == vo.TCMC && r.itemName == vo.YPMC);

                if(fyVo == null)
                {
                    Log.Output(vo.TCMC + "-->" + vo.YPMC);
                    data2.Add(vo);
                }
            }

            //if()
            this.gcBdjg.DataSource = data2;
            this.gcBdjg.RefreshDataSource();
        }

        #region tab5

        List<EntityFy> dataFy = new List<EntityFy>();
        public void LoadFy()
        {
            try
            {
                DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int n = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityFy vo = new EntityFy();
                        vo.fyxh = dr["fyxh"].ToString();
                        vo.fymc = dr["fymc"].ToString();
                        vo.sl = Function.Dec(dr["sl"]);
                        vo.fydj = Function.Dec(dr["fydj"]);
                        vo.fyzj = Function.Dec(dr["fyzj"]);
                        //vo.sort = (n++);
                        dataFy.Add(vo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.gcFy.DataSource = dataFy.OrderByDescending(r=>r.fyzj);
            this.gcFy.RefreshDataSource();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            //if(dataIpBill != null && dataFy != null)
            //{
            //    foreach(EntityIpFee vo in dataIpBill)
            //    {
            //        string name = vo.item_name;
            //        EntityFy fyVo = dataFy.Find(r=>r.fymc.Contains(name));
            //        if (fyVo != null)
            //            fyVo.sort = vo.sort;
            //    }

            //    this.gcFy.DataSource = dataFy.OrderByDescending(r => r.sort);
            //    this.gcFy.RefreshDataSource();
            //}

            uiHelper.ExportToXls(this.gvFy);
        }


        List<EntityIpFee> dataIpBill = new List<EntityIpFee>();
        private void btnLoadFee_Click(object sender, EventArgs e)
        {
            // List<EntityIpFee> data = new List<EntityIpFee>();
            dataIpBill.Clear();
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

            string sql = @"select a.PRE_NO,b.ITEM_CODE,
                                    b.item_name,
                                    sum(a.qty) as qty,
                                    a.PRICE,
                                     sum(a.TOTAL) as TOTAL
                            from IP_BILL a 
                            left join code_item b
                            on a.item_code = b.item_code
                            where a.REG_NO = {0} group by a.PRE_NO,b.ITEM_CODE,b.item_name,a.PRICE";

            if (!string.IsNullOrEmpty(txtRegNo.Text))
                sql = string.Format(sql, "'" + txtRegNo.Text + "'");
            else
                return; 

            DataTable dt = svc.GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                int n = 1;
                foreach (DataRow dr in dt.Rows)
                {


                    EntityIpFee vo = new EntityIpFee();
                    vo.PRE_NO =dr["PRE_NO"].ToString();
                    vo.item_name = dr["item_name"].ToString();
                    vo.itemCode = dr["ITEM_CODE"].ToString();
                    vo.PRICE = Function.Dec(dr["PRICE"]);
                    vo.qty = Function.Dec(dr["qty"]);
                    vo.TOTAL = Function.Dec(dr["TOTAL"]);
                    if (dataIpBill.Any(r => r.itemCode == vo.itemCode && r.PRICE == vo.PRICE))
                    {
                        EntityIpFee voClone = dataIpBill.Find(r => r.itemCode == vo.itemCode && r.PRICE == vo.PRICE);

                        voClone.qty += vo.qty;
                        voClone.TOTAL += vo.TOTAL;
                    }
                    else
                        dataIpBill.Add(vo);
                }
            }

            this.gcIpFee.DataSource = dataIpBill.OrderByDescending(r=>r.TOTAL);
            this.gcIpFee.RefreshDataSource();
        }


        private void btnSearchBill_Click(object sender, EventArgs e)
        {
            List<EntityIpFee> temp = null;
           // List<EntityIpFee> data = this.gcIpFee.DataSource as List<EntityIpFee>;
            string ipBill = txtIpBill.Text;
            if (!string.IsNullOrEmpty(ipBill))
            {
                temp = dataIpBill.FindAll(r=>r.item_name.Contains(ipBill));
            }
            
            if(temp != null)
            {
                this.gcIpFee.DataSource = temp;
                this.gcIpFee.RefreshDataSource();
            }
            else
            {
                this.gcIpFee.DataSource = dataIpBill;
                this.gcIpFee.RefreshDataSource();
            }

        }
        #endregion


        #region 调整信息
        private void btnSearchPat_Click(object sender, EventArgs e)
        {
            QueryPat();
        }


        void QueryPat()
        {
            List<EntityPatInfo> data = new List<EntityPatInfo>();
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

            string sql = @"select a.reg_no,
                                a.mc_id,
                                c.FEE_NAME as fee_code,
                                a.name,
                                a.sex,
                                a.ip_no,
                                a.ip_date,
                                a.op_date,
                                b.DIAG_NAME as CL_EDIAG,
                                e.DIAG_NAME as outDiag
                                 from IP_REGISTER a 
                                left join CODE_DIAGNOSE b
                                on a.CL_EDIAG = b.DIAG_CODE
                                left join CODE_FEE c
                                on a.FEE_CODE = c.FEE_CODE
                                left join IP_DIAGNOSE d
                                on a.REG_NO = d.REG_NO and d.TYPE = 2
                                left join CODE_DIAGNOSE e
                                on d.DIAG_CODE = e.DIAG_CODE
                                where (a.NAME like {0} or a.REG_NO like {1}) ";

            if (!string.IsNullOrEmpty(txtPatInfo.Text))
                sql = string.Format(sql, "'%" + txtPatInfo.Text + "%'", "'%" + txtPatInfo.Text + "%'");
            else
                return;

            DataTable dt = svc.GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EntityPatInfo vo = new EntityPatInfo();
                    vo.reg_no = dr["reg_no"].ToString();
                    vo.mc_id = dr["mc_id"].ToString();
                    vo.fee_code = dr["fee_code"].ToString();
                    vo.name = dr["name"].ToString();
                    vo.sex = dr["sex"].ToString();
                    vo.cl_ediag = dr["CL_EDIAG"].ToString();
                    vo.ip_no = dr["ip_no"].ToString();
                    vo.outDiag = dr["outDiag"].ToString();
                    vo.ip_date = dr["ip_date"].ToString();
                    vo.op_date = dr["op_date"].ToString();
                    data.Add(vo);
                }
            }

            this.gcPatInfo.DataSource = data;
            this.gcPatInfo.RefreshDataSource();
        }


        private void btnPatClear_Click(object sender, EventArgs e)
        {
            txtRegNo2.Text = "";
            txtNewIpno.Text = "";
            dteIpTime.Text = "";
            dteOpTime.Text = "";
        }




        private void btnAjPat_Click(object sender, EventArgs e)
        {
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string reg_no = txtRegNo2.Text;
            string new_ipno = txtNewIpno.Text;
            string ip_time = Function.Datetime(dteIpTime.Text).ToString("yyyy.MM.dd HH:mm:ss") ;
            string op_time = Function.Datetime(dteOpTime.Text).ToString("yyyy.MM.dd HH:mm:ss");

            if (string.IsNullOrEmpty(reg_no) || string.IsNullOrEmpty(new_ipno) || string.IsNullOrEmpty(ip_time) || string.IsNullOrEmpty(op_time))
            {
                MessageBox.Show("信息有空值，请检查！");
                return;
            }

            string sql = string.Empty;
            List<DacParm> lstParm = new List<DacParm>();

            sql = @"update PATIENTINFO set IP_NO = ? where PID in (select PID from IP_REGISTER where REG_NO =? )";

            IDataParameter[] parm1 = svc.CreateParm(2);
            parm1[0].Value = new_ipno;
            parm1[1].Value = reg_no;
            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, parm1));

            sql = @"update IP_REGISTER set IP_NO =?, IP_CNT = 1 where REG_NO = ?";
            IDataParameter[] parm5 = svc.CreateParm(2);
            parm5[0].Value = new_ipno;
            parm5[1].Value = reg_no;
            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, parm5));

            sql = @"update IP_REGISTER set  IP_DATE = LEFT(?,10), IP_TIME = ? where REG_NO = ?";
            IDataParameter[] parm2 = svc.CreateParm(3);
            parm2[0].Value = ip_time;
            parm2[1].Value = ip_time;
            parm2[2].Value = reg_no;
            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, parm2));

            sql = @"update IP_REGISTER set OP_DATE = LEFT(?,10), OP_TIME = ? where REG_NO = ?";
            IDataParameter[] parm3 = svc.CreateParm(3);
            parm3[0].Value = op_time;
            parm3[1].Value = op_time;
            parm3[2].Value = reg_no;
            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, parm3));

            sql = @"update IP_PATIENT set IN_TIME = ?, IN_DATE = LEFT(?,10), OUT_DATE = LEFT(?,10) where REG_NO =?";
            IDataParameter[] parm4 = svc.CreateParm(4);
            parm4[0].Value = ip_time;
            parm4[1].Value = op_time;
            parm4[2].Value = op_time;
            parm4[3].Value = reg_no;
            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, parm4));

            int affect = -1;
            if (lstParm.Count > 0)
                affect = svc.Commit(lstParm);

            if (affect > 0)
                MessageBox.Show("保存成功 ！");
            else
                MessageBox.Show("保存失败 ！");

            QueryPat();
        }

        private void btnAjRyzd_Click(object sender, EventArgs e)
        {
            //SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            //string ryzd = txtClEdiag.Text;
            //string reg_no = txtRegNo2.Text;

            //if (string.IsNullOrEmpty(ryzd))
            //{
            //    MessageBox.Show("信息有空值，请检查！");
            //    return;
            //}

            //if (string.IsNullOrEmpty(reg_no))
            //{
            //    MessageBox.Show("reg_no 为空，请检查！");
            //    return;
            //}
            //int affect = -1;
            //string sql = string.Empty;
            //List<DacParm> lstParm = new List<DacParm>();

            //sql = @"update IP_REGISTER set CL_EDIAG = ? where PID in (select PID from IP_REGISTER where REG_NO =? )";

            //IDataParameter[] parm1 = svc.CreateParm(2);
            //parm1[0].Value = ryzd;
            //parm1[1].Value = reg_no;
            //affect = svc.ExecSql(sql, parm1);

            //if (affect > 0)
            //    MessageBox.Show("保存成功 ！");
            //else
            //    MessageBox.Show("保存失败 ！");

            //QueryPat();
        }

        private void btnAjCyzd_Click(object sender, EventArgs e)
        {
            //SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            //string cyzd = txtOutDiag.Text;
            //string reg_no = txtRegNo2.Text;

            //if (string.IsNullOrEmpty(cyzd) )
            //{
            //    MessageBox.Show("信息有空值，请检查！");
            //    return;
            //}

            //if (string.IsNullOrEmpty(reg_no))
            //{
            //    MessageBox.Show("reg_no 为空，请检查！");
            //    return;
            //}
            //int affect = -1;
            //string sql = string.Empty;
            //List<DacParm> lstParm = new List<DacParm>();

            //sql = @"update IP_DIAGNOSE set DIAG_CODE = ? where reg_no in (select REG_NO from IP_REGISTER where REG_NO =? ) and type = 2";

            //IDataParameter[] parm1 = svc.CreateParm(2);
            //parm1[0].Value = cyzd;
            //parm1[1].Value = reg_no;
            //affect = svc.ExecSql(sql,parm1);

            //if (affect > 0)
            //    MessageBox.Show("保存成功 ！");
            //else
            //    MessageBox.Show("保存失败 ！");

            //QueryPat();
        }

        #endregion

        #region

        private void btnSearchPreInfo_Click(object sender, EventArgs e)
        {
            QueryPreInfo();
        }

        List<EntityPreInfo> dataPreInfo = new List<EntityPreInfo>();
        void QueryPreInfo()
        {
            dataPreInfo.Clear();
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

            string sql = @"select  e.NAME,a.REG_NO,
                                    a.ITEM_CODE,
                                    d.ITEM_NAME,
                                    b.PRE_NO,
                                    b.GROUP_NO, 
                                    b.BEGIN_DATE,
                                    b.BEGIN_TIME,
                                    b.END_DATE,
                                    b.END_TIME,
                                    c.EXEC_DATE,
                                    c.EXEC_TIME,
                                    c.CHECK_TIME 
                                    from IP_BILL a 
                                    left join IP_PRESCRIPTION b 
                                    on a.REG_NO = b.REG_NO and a.PRE_NO = b.PRE_NO
                                    left join IP_PREXEC c
                                    on b.PRE_NO = c.PRE_NO
                                    left join CODE_ITEM d
                                    on a.ITEM_CODE = d.ITEM_CODE
                                    left join IP_REGISTER e
                                    on a.REG_NO = e.REG_NO
                                    where a.REG_NO = {0}  order by b.GROUP_NO; ";

            if (!string.IsNullOrEmpty(txtSearchPreInfo.Text))
                sql = string.Format(sql, "'" + txtSearchPreInfo.Text + "'");
            else
                return;

            DataTable dt = svc.GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EntityPreInfo vo = new EntityPreInfo();
                    vo.NAME  = dr["NAME"].ToString();
                    vo.REG_NO = dr["REG_NO"].ToString();
                    vo.ITEM_CODE = dr["ITEM_CODE"].ToString();
                    vo.ITEM_NAME = dr["ITEM_NAME"].ToString();
                    vo.GROUP_NO = Function.Dec(dr["GROUP_NO"].ToString()) ;
                    vo.BEGIN_DATE = dr["BEGIN_DATE"].ToString();
                    vo.BEGIN_TIME = dr["BEGIN_TIME"].ToString();
                    vo.END_DATE = dr["END_DATE"].ToString();
                    vo.END_TIME = dr["END_TIME"].ToString();
                    vo.EXEC_DATE = dr["EXEC_DATE"].ToString();
                    vo.EXEC_TIME = dr["EXEC_TIME"].ToString();
                    vo.PRE_NO = dr["PRE_NO"].ToString();
                    vo.CHECK_TIME = dr["CHECK_TIME"].ToString();
                    dataPreInfo.Add(vo);
                }
            }

            this.gcPreInfo.DataSource = dataPreInfo;
            this.gcPreInfo.RefreshDataSource();
        }

        #endregion

        private void btnAjPreInfo_Click(object sender, EventArgs e)
        {
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string reg_no = txtRegNo3.Text;
            string dteRreTime = Function.Datetime(dteRre.Text).ToString("yyyy.MM.dd HH:mm:ss");
            string dtePreDate = Function.Datetime(dteRre.Text).ToString("yyyy.MM.dd");
            decimal group1 = Function.Dec(this.txtGroup1.Text);
            decimal group2 = Function.Dec(this.txtGroup2.Text); ;
            if (string.IsNullOrEmpty(reg_no) || string.IsNullOrEmpty(dteRreTime))
            {
                MessageBox.Show("信息有空值，请检查！");
                return;
            }

            List<EntityPreInfo> lstTemp = dataPreInfo.FindAll(r=>r.GROUP_NO >= group1 && r.GROUP_NO <= group2);

            if (lstTemp == null)
                return;
            string preNo = string.Empty;
            foreach (EntityPreInfo vo in lstTemp)
            {
                preNo += "'" + vo.PRE_NO + "',";
            }

            if (!string.IsNullOrEmpty(preNo))
                preNo = "(" + preNo.TrimEnd(',') + ")";

            string sql = string.Empty;
            List<DacParm> lstParm = new List<DacParm>();

            sql = @"update IP_PRESCRIPTION 
                                set BEGIN_DATE = ?, 
                                BEGIN_TIME = ? ,
                                CHECK_DATE = ?, 
                                EXEC_DATE = ?,
                                END_DATE = ?,
                                END_TIME = ?, 
                                FILL_DATE = ?
                                where PRE_NO in {0}";
            sql = string.Format(sql, preNo);
            IDataParameter[] parm1 = svc.CreateParm(7);
            parm1[0].Value = dtePreDate;
            parm1[1].Value = dteRreTime;
            parm1[2].Value = dtePreDate;
            parm1[3].Value = dtePreDate;
            parm1[4].Value = dtePreDate;
            parm1[5].Value = dteRreTime;
            parm1[6].Value = dtePreDate;
            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, parm1));

            sql = @"update IP_PREXEC 
                                set EXEC_DATE =?,
                                CHECK_TIME = ?,
                                EXEC_TIME = ?
                                where PRE_NO in {0}";
            sql = string.Format(sql, preNo);
            IDataParameter[] parm2 = svc.CreateParm(3);
            parm2[0].Value = dtePreDate;
            parm2[1].Value = dteRreTime;
            parm2[2].Value = dteRreTime;
            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, parm2));


            sql = @"update IP_PREXEC 
                                set EXEC_DATE =?,
                                CHECK_TIME = ?,
                                EXEC_TIME = ?
                                where PRE_NO in {0}";
            sql = string.Format(sql, preNo);
            IDataParameter[] parm3 = svc.CreateParm(3);
            parm3[0].Value = dtePreDate;
            parm3[1].Value = dteRreTime;
            parm3[2].Value = dteRreTime;
            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, parm3));

            sql = @"update IP_BILL 
                            set BILL_DATE = ?,
                            BILL_TIME = ? 
                            where PRE_NO in {0}";
            sql = string.Format(sql, preNo);
            IDataParameter[] parm4 = svc.CreateParm(2);
            parm4[0].Value = dtePreDate;
            parm4[1].Value = dteRreTime;
            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, parm4));

            int affect = -1;
            if (lstParm.Count > 0)
                affect = svc.Commit(lstParm);

            if (affect > 0)
                MessageBox.Show("保存成功 ！");
            else
                MessageBox.Show("保存失败 ！");

            QueryPreInfo();
        }

        private void gvIpFee_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void gvFy_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }


        #region tab8

        private void btnPg8Query_Click(object sender, EventArgs e)
        {
            List<EntityJyfy> data = new List<EntityJyfy>();
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

            string sql = @"select * from (select a.REG_NO,
                                               h.name,
                                               year(getdate())-year(h.BIRTH) age ,
                                               REC_DATE,
                                               e.oper_name,
                                               dept.DEPT_NAME,
                                               c.CHRG_NO,
                                               f.invo_no,
                                               d.group_name,
                                               '采血费' as fl,
                                               sum(b.TOTAL) as total
                                          from cl_recipe a
                                          join CL_RECENTRY b
                                            on a.REC_NO = b.REC_NO
                                          join CL_CHARGE_RECIPE c
                                            on a.REC_NO = c.REC_NO
                                          join AS_CODE_REQITEM d
                                            on b.group_code = d.GROUP_CODE
                                          left join CODE_OPERATOR e
                                            on a.dr_code = e.oper_code
                                          left join CL_CHARGE_INVOICE f
                                            on (c.chrg_no = f.chrg_no)
                                          left join cl_register g
                                            on a.reg_no = g.reg_no
                                          left join PATIENTINFO h
                                            on g.pid = h.pid
                                          left join CODE_DEPARTMENT dept
                                            on a.DEPT_CODE = dept.DEPT_CODE
                                         where a.CHRG_FLAG = 'T'
                                           and a.REC_DATE >= ?
                                           and a.REC_DATE <= ?
                                           and b.item_code in
                                               (select ITEM_CODE from CODE_ITEM where item_name like '%采%')
                                         group by a.REG_NO,
                                                  h.name,h.BIRTH,
                                                  REC_DATE,
                                                  e.oper_name,
                                                  dept.DEPT_NAME,
                                                  c.CHRG_NO,
                                                  d.group_name,
                                                  f.invo_no
                                        union all
                                        select a.REG_NO,
                                               h.name,
                                                year(getdate())-year(h.BIRTH) age ,
                                               REC_DATE,
                                               e.oper_name,
                                              dept.DEPT_NAME,
                                               c.CHRG_NO,
                                               f.invo_no,
                                               d.group_name,
                                               '化验费' as fl,
                                               sum(b.TOTAL) as total
                                          from cl_recipe a
                                          join CL_RECENTRY b
                                            on a.REC_NO = b.REC_NO
                                          join CL_CHARGE_RECIPE c
                                            on a.REC_NO = c.REC_NO
                                          join AS_CODE_REQITEM d
                                            on b.group_code = d.GROUP_CODE
                                          left join CODE_OPERATOR e
                                            on a.dr_code = e.oper_code
                                          left join CL_CHARGE_INVOICE f
                                            on (c.chrg_no = f.chrg_no)
                                          left join cl_register g
                                            on a.reg_no = g.reg_no
                                          left join PATIENTINFO h
                                            on g.pid = h.pid
                                          left join CODE_DEPARTMENT dept
                                            on a.DEPT_CODE = dept.DEPT_CODE
                                         where a.CHRG_FLAG = 'T'
                                           and a.REC_DATE >= ?
                                           and a.REC_DATE <= ?
                                           and b.item_code not in
                                               (select ITEM_CODE from CODE_ITEM where item_name like '%采%')
                                         group by a.REG_NO,
                                                  h.name,h.BIRTH,
                                                  REC_DATE,
                                                  e.oper_name,
                                                  dept.DEPT_NAME,
                                                  c.CHRG_NO,
                                                  d.group_name,
                                                  f.invo_no) tt
                                                    order by tt.reg_no ";

            IDataParameter[] parm = svc.CreateParm(4);
            parm[0].Value = this.dtePg8Begin.Text;
            parm[1].Value = this.dtePg8End.Text;
            parm[2].Value = this.dtePg8Begin.Text;
            parm[3].Value = this.dtePg8End.Text;

            DataTable dt = svc.GetDataTable(sql,parm);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EntityJyfy vo = new EntityJyfy();
                    vo.REG_NO = dr["REG_NO"].ToString();
                    vo.name = dr["name"].ToString();
                    vo.age = dr["age"].ToString();
                    vo.REC_DATE = dr["REC_DATE"].ToString();
                    vo.oper_name = dr["oper_name"].ToString();
                    vo.DEPT_NAME = dr["DEPT_NAME"].ToString();
                    vo.CHRG_NO = dr["CHRG_NO"].ToString();
                    vo.invo_no = dr["invo_no"].ToString();
                    vo.group_name = dr["group_name"].ToString();
                    vo.fl = dr["fl"].ToString();
                    vo.total = Function.Dec(dr["total"].ToString()) ;
                    if (vo.fl == "采血费")
                        vo.cxf = Function.Dec(dr["total"].ToString());
                    if (vo.fl == "化验费")
                        vo.hyf = Function.Dec(dr["total"].ToString());
                    if (data.Any(r=>r.REG_NO == vo.REG_NO && r.invo_no == vo.invo_no))
                    {
                        EntityJyfy voClone = data.Find(r => r.REG_NO == vo.REG_NO && r.invo_no == vo.invo_no);

                        if (vo.fl == "采血费")
                            voClone.cxf = Function.Dec(dr["total"].ToString());
                        if(vo.fl == "化验费")
                            voClone.hyf = Function.Dec(dr["total"].ToString());
                    }
                    else
                    {
                        data.Add(vo);
                    }

                    
                }
            }

            this.gcJyMzRpt.DataSource = data;
            this.gcJyMzRpt.RefreshDataSource();
        }


        #endregion

        #region tab9
        private void btnPg9Query_Click(object sender, EventArgs e)
        {
            List<EntityYm> data = new List<EntityYm>();
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

            string sql = @" select a.REG_NO,
                                h.CARD_NO,
                                       h.name,
                                       REC_DATE,
                                       e.oper_name,
                                       c.CHRG_NO,
                                       f.invo_no,
                                       i.ITEM_CODE,i.ITEM_NAME,b.TOTAL
                                  from cl_recipe a
                                  join CL_RECENTRY b
                                    on a.REC_NO = b.REC_NO
                                  join CL_CHARGE_RECIPE c
                                    on a.REC_NO = c.REC_NO
                                  left join CODE_OPERATOR e
                                    on a.dr_code = e.oper_code
                                  left join CL_CHARGE_INVOICE f
                                    on (c.chrg_no = f.chrg_no)
                                  left join cl_register g
                                    on a.reg_no = g.reg_no
                                  left join PATIENTINFO h
                                    on g.pid = h.pid
                                    left join CODE_ITEM i
                                    on b.ITEM_CODE = i.ITEM_CODE
                                 where a.CHRG_FLAG = 'T'
                                   and a.REC_DATE >= ?
                                   and a.REC_DATE <= ?
                                 and (i.PH_CODE = '70' and i.ITEM_CLS = '6' or i.ITEM_CODE in ('Q00000449','99999000010','Q00000635'))  
                                    order by a.REC_DATE,h.CARD_NO,a.REG_NO";

            IDataParameter[] parm = svc.CreateParm(2);
            parm[0].Value = this.dtePg9Begin.Text;
            parm[1].Value = this.dtePg9End.Text;


            DataTable dt = svc.GetDataTable(sql, parm);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EntityYm vo = new EntityYm();
                    vo.REG_NO = dr["REG_NO"].ToString();
                    vo.CARD_NO = dr["CARD_NO"].ToString();
                    vo.name = dr["name"].ToString();
                    vo.REC_DATE = dr["REC_DATE"].ToString();
                    vo.oper_name = dr["oper_name"].ToString();
                    vo.CHRG_NO = dr["CHRG_NO"].ToString();
                    vo.invo_no = dr["invo_no"].ToString();
                    vo.ITEM_CODE = dr["ITEM_CODE"].ToString();
                    vo.ITEM_NAME = dr["ITEM_NAME"].ToString();
                    if (vo.ITEM_CODE == "99999000010" || vo.ITEM_CODE == "Q00000449")
                    {
                       
                    }
                    else
                        vo.itemName = vo.ITEM_NAME;

                    if (vo.ITEM_NAME.Contains("服务费"))
                    {
                        vo.fwf = Function.Dec(dr["total"].ToString());
                    }
                    else if (vo.ITEM_NAME.Contains("冷链费"))
                    {
                        vo.llf = Function.Dec(dr["total"].ToString());
                    }
                    else  
                    {
                        vo.xmje = Function.Dec(dr["total"].ToString());
                    }

                    if (data.Any(r => r.REG_NO == vo.REG_NO && r.invo_no == vo.invo_no))
                    {
                        EntityYm voClone = data.Find(r => r.REG_NO == vo.REG_NO && r.invo_no == vo.invo_no);

                        if (vo.ITEM_NAME.Contains("服务费"))
                        {
                            voClone.fwf = Function.Dec(dr["total"].ToString());
                        }
                        else if (vo.ITEM_NAME.Contains("冷链费"))
                        {
                            voClone.llf = Function.Dec(dr["total"].ToString());
                        }
                        else
                        {
                            voClone.xmje = Function.Dec(dr["total"].ToString());
                        }

                        if (vo.ITEM_CODE == "99999000010" || vo.ITEM_CODE == "Q00000449")
                        {

                        }
                        else
                            voClone.itemName = vo.ITEM_NAME;
                    }
                    else
                    {
                        data.Add(vo);
                    }

                }
            }

            this.gcYm.DataSource = data;
            this.gcYm.RefreshDataSource();
        }
        #endregion

        #region tab10

        Dictionary<string, string> dicSearch;
        private void btnPg10Query_Click(object sender, EventArgs e)
        {
            string search = this.cboItem.Text;
            if (string.IsNullOrEmpty(search))
                return;
            dicSearch = new Dictionary<string, string>();
            dicSearch.Add("母乳分析", " ( '71200000015')");
            dicSearch.Add("健康咨询", " ( 'Q00000560')");
            dicSearch.Add("婴幼儿健康体检", " ( 'Q00000458')");
            dicSearch.Add("建立健康档案", " ( 'Q00000445','Q00000558')");
            dicSearch.Add("一般健康体检", " ( 'Q00000577')");
            List<EntityChkf> data = new List<EntityChkf>();
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            IDataParameter[] parm = null;
            string sql = string.Empty;

            if (search == "套房")
            {
                sql = @"select 
                                c.CARD_NO,
								'门诊' as flg,
                                a.REG_NO,
                                c.NAME,
                                a.REC_DATE,
                                a.DR_CODE,
                                d.OPER_NAME,
                                e.ITEM_NAME as NOTE ,
                                b.PRICE,
                                b.QTY,
                                b.TOTAL
                                from CL_RECIPE a, 
                                CL_RECENTRY b, 
                                PATIENTINFO c,
                                CODE_OPERATOR d,
                                code_item e
                               where a.REC_NO = b.REC_NO 
                                and a.PID = c.PID
                                and a.DR_CODE = d.OPER_CODE
                                and a.CHRG_FLAG = 'T' 
                                and b.ITEM_CODE = e.ITEM_CODE
                                and b.ITEM_CODE in('91200000001','91200000002')
                                and a.REC_DATE >= ? and a.REC_DATE <=  ?";
                parm = svc.CreateParm(2);
                parm[0].Value = this.dtePg10Begin.Text;
                parm[1].Value = this.dtePg10End.Text;
            }
            else if (dicSearch.ContainsKey(search))
            {
                sql = @" select 
                                c.CARD_NO,
								'门诊' as flg,
                                a.REG_NO,
                                c.NAME,
                                a.REC_DATE,
                                a.DR_CODE,
                                d.OPER_NAME,
                                b.NOTE ,
                                b.PRICE,
                                b.QTY,
                                b.TOTAL
                                from CL_RECIPE a, 
                                CL_RECENTRY b, 
                                PATIENTINFO c,
                                CODE_OPERATOR d
                                where a.REC_NO = b.REC_NO 
                                and a.PID = c.PID
                                and a.DR_CODE = d.OPER_CODE
                                and a.CHRG_FLAG = 'T' 
                                and b.ITEM_CODE in {0}
                                and a.REC_DATE >= ? and a.REC_DATE <= ? 
                                union all
                          select b.IP_NO as CARD_NO,
                                '住院' as flg,
                                B.REG_NO,
                                b.NAME, 
                                c.FILL_DATE as REC_DATE,
                                c.DR_CODE,
                                c.OPER_CODE,
                                c.NOTE, 
                                c.PRICE,
                                a.QTY,
                                a.TOTAL  
                                from IP_BILL a 
                                left join IP_REGISTER b
                                on a.REG_NO = b.REG_NO
                                left join IP_PRESCRIPTION c
                                on a.PRE_NO = c.PRE_NO and a.ITEM_CODE = c.ITEM_CODE
                                where c.ITEM_CODE in {0}
                                and a.BILL_DATE >= ? and a.BILL_DATE <= ?";
                sql = string.Format(sql,dicSearch[search]);
                parm = svc.CreateParm(4);
                parm[0].Value = this.dtePg10Begin.Text;
                parm[1].Value = this.dtePg10End.Text;
                parm[2].Value = this.dtePg10Begin.Text;
                parm[3].Value = this.dtePg10End.Text;
            }
            else
            {
                sql = @" select 
                                c.CARD_NO,
								'门诊' as flg,
                                a.REG_NO,
                                c.NAME,
                                a.REC_DATE,
                                a.DR_CODE,
                                d.OPER_NAME,
                                b.NOTE ,
                                b.PRICE,
                                b.QTY,
                                b.TOTAL
                                from CL_RECIPE a, 
                                CL_RECENTRY b, 
                                PATIENTINFO c,
                                CODE_OPERATOR d
                                where a.REC_NO = b.REC_NO 
                                and a.PID = c.PID
                                and a.DR_CODE = d.OPER_CODE
                                and a.CHRG_FLAG = 'T' 
                                and b.note like '%{0}%'
                                and a.REC_DATE >= ? and a.REC_DATE <= ? 
                                union all
                         select b.IP_NO as CARD_NO,
                                '住院' as flg,
                                B.REG_NO,
                                b.NAME, 
                                c.FILL_DATE as REC_DATE,
                                c.DR_CODE,
                                c.OPER_CODE,
                                c.NOTE, 
                                c.PRICE,
                                a.QTY,
                                a.TOTAL  
                                from IP_BILL a 
                                left join IP_REGISTER b
                                on a.REG_NO = b.REG_NO
                                left join IP_PRESCRIPTION c
                                on a.PRE_NO = c.PRE_NO and a.ITEM_CODE = c.ITEM_CODE
                                where c.NOTE like '%{0}%' 
                                and a.BILL_DATE >= ? and a.BILL_DATE <= ? ";

                sql = string.Format(sql, search);
                parm = svc.CreateParm(4);
                parm[0].Value = this.dtePg10Begin.Text;
                parm[1].Value = this.dtePg10End.Text;
                parm[2].Value = this.dtePg10Begin.Text;
                parm[3].Value = this.dtePg10End.Text;
            }

           
           
            DataTable dt = svc.GetDataTable(sql, parm);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EntityChkf vo = new EntityChkf();
                    vo.REG_NO = dr["REG_NO"].ToString();
                    vo.CARD_NO = dr["CARD_NO"].ToString();
                    vo.NAME = dr["NAME"].ToString();
                    vo.REC_DATE = dr["REC_DATE"].ToString();
                    vo.OPER_NAME = dr["OPER_NAME"].ToString();
                    if(search == "母乳分析")
                        vo.NOTE = "母乳分析";
                    else
                        vo.NOTE = dr["NOTE"].ToString();
                    vo.TOTAL = Function.Dec(dr["TOTAL"].ToString());
                    vo.flg = dr["flg"].ToString();
                    if (vo.TOTAL <= 0)
                        continue;

                    if (chkType.SelectedIndex == 2)
                    {
                        if (vo.flg == "住院")
                            continue;
                    }

                    if (chkType.SelectedIndex == 1)
                    {
                        if (vo.flg == "门诊")
                            continue;
                    }
                     
                    

                    if (data.Any(r => r.REG_NO == vo.REG_NO && r.NOTE == vo.NOTE))
                    {
                        EntityChkf voClone = data.Find(r => r.REG_NO == vo.REG_NO && r.NOTE == vo.NOTE);

                        voClone.TOTAL += vo.TOTAL;
                    }
                    else
                    {
                        data.Add(vo);
                    }

                }
            }

            this.gcChkf.DataSource = data;
            this.gcChkf.RefreshDataSource();
        }
        #endregion


        List<EntitySmItem> lstSmItem = null;
        private void btnPg11Search_Click(object sender, EventArgs e)
        {
            try
            {
                lstSmItem = new List<EntitySmItem>();
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

                string sql = @" select * from CODE_ITEM a where a.ShuoM is not null ";

                DataTable dt = svc.GetDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntitySmItem vo = new EntitySmItem();
                        vo.item_code = dr["item_code"].ToString();
                        vo.item_name = dr["item_name"].ToString();
                        vo.mc_code = dr["mc_code"].ToString();
                        vo.shouM = dr["shuoM"].ToString();
                        lstSmItem.Add(vo);
                    }
                }

                //foreach (var vo in lstSmItem)
                //{
                //    sql = @"update CODE_ITEM set mc_code = ? , shuoM = ? where item_code = ? ";

                //    IDataParameter[] param = svc.CreateParm(3);
                //    param[0].Value = vo.shouM;
                //    param[1].Value = vo.mc_code;
                //    param[2].Value = vo.item_code;

                //    svc.ExecSql(sql, param);
                //}

                this.gridControl1.DataSource = lstSmItem;
                this.gridControl1.RefreshDataSource();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }




        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("请选择文件！");
                return;
            }
            SqlHelper svcEmr = new SqlHelper(EnumBiz.emrDB);
            //SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);


            string sql = @"select *
                                from t_dic_opicd a";
            List<EntityOpcode> data = new List<EntityOpcode>();
            DataTable dtFirst = svcEmr.GetDataTable(sql);

            if (dtFirst != null && dtFirst.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFirst.Rows)
                {
                    EntityOpcode vo = new EntityOpcode();
                    vo.opcode = dr["icdcode_vchr"].ToString();
                    vo.opname = dr["icdname_vchr"].ToString();

                    data.Add(vo);
                }
            }

            int i = 7533;
            DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);
            List<EntityOpcode> lstFvo = new List<EntityOpcode>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EntityOpcode vo = new EntityOpcode();
                    vo.serno_int = i++;
                    vo.opcode = dr["FOPCODE"].ToString();
                    vo.opname = dr["FOPNAME"].ToString();
                    vo.pycode = dr["FZJC"].ToString();
                    vo.level = 1;
                    vo.status = 1;
                    lstFvo.Add(vo);
                }
            }
            int affect = -1;


            List<EntityOpcode> lstFICD = new List<EntityOpcode>();

            foreach (var vo in lstFvo)
            {
                if (data.Any(r => r.opcode == vo.opcode))
                    continue;

                if (lstFICD.Any(r => r.opcode == vo.opcode))
                    continue;

                string pyCode = new SpellAndWbCodeTookit().GetSpellCode(vo.opname);
                if (pyCode.Length > 4)
                    pyCode = pyCode.Substring(0, 4);
                sql = @"insert into t_dic_opicd (icdcode_vchr,icdname_vchr,level_int,icdpycode_vchr) values (?,?,?,?)";

                IDataParameter[] param = svcEmr.CreateParm(4);
                param[0].Value = vo.opcode;
                param[1].Value = vo.opname;
                param[2].Value = 1;
                param[3].Value = pyCode;
                lstFICD.Add(vo);

                 affect = svcEmr.ExecSql(sql,param);



            }

            if (affect >= 0)
                MessageBox.Show("success !!!!!!!!!!!!!!");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("请选择文件！");
                return;
            }

            List<EntityJgitem> lstJg = new List<EntityJgitem>();

            DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EntityJgitem vo = new EntityJgitem();
                    vo.item_code = dr["项目代码"].ToString().Trim();
                    vo.item_name = dr["项目名称"].ToString();
                    vo.mzPrice = Function.Dec(dr["门诊单价"]);
                    vo.zyPrice = Function.Dec(dr["住院单价"]);
                    vo.mc_code = dr["医保码"].ToString();
                    vo.sdm = dr["市代码"].ToString();
                    vo.smc = dr["市名称"].ToString();
                    lstJg.Add(vo);
                }
            }
            this.gcJg.DataSource = lstJg;
            this.gcJg.RefreshDataSource();

            string sql = @"select b.TYPE, a.ITEM_CODE,
                                    a.ITEM_NAME,
                                    a.XTBM,
                                    a.XTMC,
                                    a.ITEM_CLS, 
                                    b.BASIC_CLS, 
                                    b.ITEM_CODE,
                                    b.RET_PRICE from code_item a 
                                    left join plus_item b
                                    on a.ITEM_CODE = b.ITEM_CODE
                                     where b.TYPE  in (2,3) 
                                     and a.ITEM_CLS not in (1,2,3)
                                     --and a.ITEM_CODE in('40400000033','Q00000138') 
                                     order by b.ITEM_CODE ";

            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string type = string.Empty;
            List<EntityItemPrice> lstItemprice = new List<EntityItemPrice>();
            DataTable dt2 = svc.GetDataTable(sql);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    EntityItemPrice vo = new EntityItemPrice();
                    vo.item_code = dr["ITEM_CODE"].ToString().Trim();
                    vo.XTBM = dr["XTBM"].ToString();
                    vo.XTMC = dr["XTMC"].ToString();
                    type = dr["type"].ToString();
                    if (type == "2")
                        vo.mzPrice = Function.Dec(dr["RET_PRICE"]);
                    if (type == "3")
                        vo.zyPrice = Function.Dec(dr["RET_PRICE"]);

                    if (lstItemprice.Any(r => r.item_code == vo.item_code))
                    {
                        EntityItemPrice voClone = lstItemprice.Find(r => r.item_code == vo.item_code);

                        if (type == "2")
                            voClone.mzPrice = Function.Dec(dr["RET_PRICE"]);
                        if (type == "3")
                            voClone.zyPrice = Function.Dec(dr["RET_PRICE"]);
                        continue;
                    }
                        
                    
                    lstItemprice.Add(vo);
                }

                this.gcItem2.DataSource = lstItemprice;
                this.gcItem2.RefreshDataSource();

                foreach (var vo in lstJg)
                {
                    EntityItemPrice voClone = lstItemprice.Find(r => r.item_code == vo.item_code);
                    if (voClone == null)
                        continue;
                    if (voClone.mzPrice != vo.mzPrice || !string.IsNullOrEmpty(vo.sdm))
                    {
                        Log.Output("原价格-->" + voClone.mzPrice.ToString() + "  现价格-->" + vo.mzPrice + " 市代码-->" + vo.sdm);

                        sql = @"update plus_item set RET_PRICE = ?,tra_price=?,lcj_price= ? where item_code = ?";

                        IDataParameter[] param = svc.CreateParm(4);
                        param[0].Value = vo.mzPrice;
                        param[1].Value = vo.mzPrice;
                        param[2].Value = vo.mzPrice;
                        param[3].Value = vo.item_code;
                        svc.ExecSql(sql,param);

                        sql = @"update code_item set XTBM = ? ,XTMC = ? where item_code = ?";

                        IDataParameter[] param2 = svc.CreateParm(3);
                        param2[0].Value = vo.sdm;
                        param2[1].Value = vo.smc;
                        param2[2].Value = vo.item_code;
                        svc.ExecSql(sql, param2);
                    }
                  
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            uiHelper.ExportToXls(this.gvChkf);
        }

        private void chkType_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            for (int i = 0; i < chkType.Items.Count; i++)
            {
                if (i != e.Index) // 不是单击的项
                {
                    chkType.SetItemChecked(i, false); //这一句也可以              
                }
            }
        }
    }
}
