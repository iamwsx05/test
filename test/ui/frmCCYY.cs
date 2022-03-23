using Common.Controls;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Ribbon;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using test;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace CCYY
{
    public partial class frmCCYY : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmCCYY()
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(tabPane1.SelectedPageIndex == 0)
            {
                QueryZh();
            }
            else if(tabPane1.SelectedPageIndex == 1)
            {
                QueryTc();
            }
            else if (tabPane1.SelectedPageIndex == 2)
            {
                QueryTcZh();
            }
            else if(tabPane1.SelectedPageIndex == 4)
            {

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {
                uiHelper.ExportToXls(this.gvZh);
            }
            else if (tabPane1.SelectedPageIndex == 1)
            {
                uiHelper.ExportToXls(this.gvTc);
            }
            else if (tabPane1.SelectedPageIndex == 2)
            {
                uiHelper.ExportToXls(this.gvTcZh);
            }
        }

        internal void QueryZh()
        {
            try
            {
                List<EntityZdzhDlt> data = new List<EntityZdzhDlt>();
                string sql = @"select z.cls_name,  c.comb_code,
                                    c.comb_name,
                                    h.HIS_ITEM_CODE,
                                    f.HIS_NAME,f.HIS_PRICE ,
                                    d.item_code,
                                    e.item_name 
                                    from zdZhxm c
                                    left join zdZhxmmx d
                                    on c.comb_code = d.comb_code
                                    left join zdXm e
                                    on d.item_code = e.item_code
                                    left join dyZhxmHis h
                                    on c.comb_code  = h.PE_COMB_CODE
                                    left join HIS_FEE_ITEM f
                                    on h.HIS_ITEM_CODE = f.HIS_CODE
                                    left join zdXmfl z
                                    on c.cls_code = z.cls_code
                                    where c.inst_flag = 'T'
                                    order by c.cls_code, c.comb_code ";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                DataTable dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityZdzhDlt vo = new EntityZdzhDlt();
                        vo.clsName = dr["cls_name"].ToString();
                        vo.combCode = dr["comb_code"].ToString();
                        vo.combName = dr["comb_name"].ToString();
                        vo.hisItemCode = dr["HIS_ITEM_CODE"].ToString().Trim();
                        vo.hisName = dr["HIS_NAME"].ToString();
                        vo.hisPrice = Function.Dec(dr["HIS_PRICE"].ToString());
                        vo.itemCode = dr["item_code"].ToString();
                        vo.itemName = dr["item_name"].ToString();
                        data.Add(vo);
                    }

                }

                this.gcZh.DataSource = data;
                this.gcZh.RefreshDataSource();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        internal void QueryTc()
        {
            try
            {
                List<EntityTcDlt> data = new List<EntityTcDlt>();
                string sql = @"select a.clus_code,a.clus_name,
                                    a.price,b.comb_code,
                                    c.comb_name,
                                    c.inst_flag
                                    from zdTc a 
                                    left join zdTcmx b
                                    on a.clus_code = b.clus_code
                                    left join zdZhxm c
                                    on b.comb_code = c.comb_code
                                    order by a.clus_code,c.cls_code, b.comb_code ";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                DataTable dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityTcDlt vo = new EntityTcDlt();
                        vo.clusCode = dr["clus_code"].ToString();
                        vo.clusName = dr["clus_name"].ToString();
                        vo.price = Function.Dec( dr["price"].ToString());
                        vo.combCode = dr["comb_code"].ToString();
                        vo.combName = dr["comb_name"].ToString();
                        string instFlg = dr["inst_flag"].ToString().Trim();
                        if (instFlg == "F" )
                            vo.instFlg = "禁用";
                        if (instFlg == "T")
                            vo.instFlg = "启用";
                        data.Add(vo);
                    }

                }

                this.gcTc.DataSource = data;
                this.gcTc.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        internal void QueryTcZh()
        {
            try
            {
                List<EntityTcZhDlt> data = new List<EntityTcZhDlt>();
                string sql = @"select a.clus_code,a.clus_name,
                                        a.price,b.comb_code,
                                        c.comb_name,
                                        h.HIS_ITEM_CODE,
                                        f.HIS_NAME,
                                        f.HIS_PRICE, 
                                        d.item_code,
                                        e.item_name from zdTc a 
                                        left join zdTcmx b
                                        on a.clus_code = b.clus_code
                                        left join zdZhxm c
                                        on b.comb_code = c.comb_code
                                        left join zdZhxmmx d
                                        on c.comb_code = d.comb_code
                                        left join zdXm e
                                        on d.item_code = e.item_code
                                        left join dyZhxmHis h
                                        on c.comb_code  = h.PE_COMB_CODE
                                        left join HIS_FEE_ITEM f
                                        on h.HIS_ITEM_CODE = f.HIS_CODE
                                        order by a.clus_code,c.cls_code, b.comb_code,d.item_code ";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                DataTable dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityTcZhDlt vo = new EntityTcZhDlt();
                        vo.clusCode = dr["clus_code"].ToString();
                        vo.clusName = dr["clus_name"].ToString();
                        vo.combCode = dr["comb_code"].ToString();
                        vo.combName = dr["comb_name"].ToString();
                        vo.hisItemCode = dr["HIS_ITEM_CODE"].ToString();
                        vo.hisName = dr["HIS_NAME"].ToString();
                        vo.hisPrice = Function.Dec(dr["HIS_PRICE"].ToString());
                        vo.itemCode = dr["item_code"].ToString();
                        vo.itemName = dr["item_name"].ToString();
                        data.Add(vo);
                    }
                }

                this.gcTcZh.DataSource = data;
                this.gcTcZh.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnSyncCZY_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string filepath = f.FileName;//G:\新建文件夹\新建文本文档.txt
                    string filename = f.SafeFileName;//新建文本文档.txt
                    filePath = filepath;
                }

                lblFile.Text = filePath;

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("请选择文件！");
                    return;
                }

                StreamReader sr = new StreamReader(lblFile.Text, Encoding.Default);
                string line;
                string jsonStr = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    jsonStr += line.ToString();
                }


                JObject jObject = (JObject)JsonConvert.DeserializeObject(jsonStr);
                EntityFromJosn resVo = new EntityFromJosn();
                resVo.Response = new EntityRes();
                resVo.Response.Result = jObject["Response"]["Result"].ToString();
                List<EntityHisItem> lstInsuinfo = JsonConvert.DeserializeObject<List<EntityHisItem>>(jObject["Response"]["DataTable"].ToString());
                if (string.IsNullOrEmpty( resVo.Response.Result) )
                    return;

                this.gcHisItem.DataSource = lstInsuinfo;
                this.gcHisItem.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
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

        List<EntityHisFee> dataHisFee = new List<EntityHisFee>();
        List<EntityZdzhxm> dataZdzhxm = new List<EntityZdzhxm>();
        List<EntityPeHis> dataPeHis = new List<EntityPeHis>();
        internal void Init(int refreshType = 0)
        {
            dataHisFee.Clear();
            dataZdzhxm.Clear();
            dataPeHis.Clear();
            string sql = string.Empty;
            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                sql = @"select * from ccyy_item order by peCode, hisCode";
                DataTable dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityHisFee vo = Function.Row2Model<EntityHisFee>(dr);
                        dataHisFee.Add(vo);
                    }
                }

                if (refreshType == 1)
                {
                    sql = @"select * from zdzhxm a where a.inst_flag = 'T'";

                    dt = svc.GetDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            EntityZdzhxm vo = Function.Row2Model<EntityZdzhxm>(dr);
                            dataZdzhxm.Add(vo);
                        }
                    }

                    this.gcComb.DataSource = dataZdzhxm;
                    this.gcComb.RefreshDataSource();
                    return;
                }
                else
                {
                    sql = @"select * from zdzhxm a where a.inst_flag = 'T'";

                    dt = svc.GetDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            EntityZdzhxm vo = Function.Row2Model<EntityZdzhxm>(dr);
                            dataZdzhxm.Add(vo);
                        }
                    }

                    sql = @"select a.cls_code, a.comb_code,a.comb_name,b.HIS_ITEM_CODE as his_code,c.HIS_NAME,c.HIS_PRICE,b.NUM from zdZhxm a 
                        left join dyZhxmHis b
                        on a.comb_code = b.PE_COMB_CODE
                        left join HIS_FEE_ITEM c
                        on b.HIS_ITEM_CODE = c.HIS_CODE
                        where a.inst_flag = 'T' 
                        union  all
                        select a.cls_code ,a.comb_code,a.comb_name,b.HIS_ITEM_CODE as his_code,c.HIS_NAME,c.HIS_PRICE,b.NUM  from zdZhxm a 
                        left join dyZhxmHis_Add b
                        on a.comb_code = b.PE_COMB_CODE
                        left join HIS_FEE_ITEM c
                        on b.HIS_ITEM_CODE = c.HIS_CODE
                        where a.inst_flag  = 'T'
                        order by cls_code,comb_code";
                    dt = svc.GetDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            EntityPeHis vo = Function.Row2Model<EntityPeHis>(dr);
                            if (string.IsNullOrEmpty(vo.his_code.Trim()))
                                continue;
                            vo.amount = dr["NUM"].ToString();
                            dataPeHis.Add(vo);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            this.gcHisFee.DataSource = dataHisFee ;
            this.gcHisFee.RefreshDataSource();

            this.gcComb.DataSource = dataZdzhxm;
            this.gcComb.RefreshDataSource();

            this.gcPeHis.DataSource = dataPeHis;
            this.gcPeHis.RefreshDataSource();

        }

        internal void RefreshData(string comb_code)
        {

            string sql = @"select a.cls_code, 
                                    a.comb_code,
                                    a.comb_name,
                                    b.HIS_ITEM_CODE as his_code,
                                    c.HIS_NAME,c.HIS_PRICE,
                                    b.NUM ,
                                    '' as add_flg
                        from zdZhxm a 
                        left join dyZhxmHis b
                        on a.comb_code = b.PE_COMB_CODE
                        left join HIS_FEE_ITEM c
                        on b.HIS_ITEM_CODE = c.HIS_CODE
                        where a.inst_flag = 'T' {0}
                        union  all
                        select a.cls_code ,
                                a.comb_code,
                                a.comb_name,
                                b.HIS_ITEM_CODE as his_code,
                                c.HIS_NAME,c.HIS_PRICE,
                                b.NUM  ,
                                b.add_flag
                        from zdZhxm a 
                        left join dyZhxmHis_Add b
                        on a.comb_code = b.PE_COMB_CODE
                        left join HIS_FEE_ITEM c
                        on b.HIS_ITEM_CODE = c.HIS_CODE
                        where a.inst_flag  = 'T' {0}";
            if (!string.IsNullOrEmpty(comb_code))
            {
                string str = " and a.comb_code in " + comb_code + "";
                sql = string.Format(sql,str);
            }
                
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            DataTable dt = svc.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                dataPeHis.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    EntityPeHis vo = Function.Row2Model<EntityPeHis>(dr);
                    vo.amount = dr["NUM"].ToString();
                    if (string.IsNullOrEmpty(vo.his_code.Trim()))
                        continue;

                    dataPeHis.Add(vo);
                }
            }
            this.gcPeHis.DataSource = dataPeHis;
            this.gcPeHis.RefreshDataSource();
        }

        public EntityHisFee GetRowObjHisFee()
        {
            if (this.gvHisFee.FocusedRowHandle < 0)
                return null;
            else
            {
                EntityHisFee vo = this.gvHisFee.GetRow(this.gvHisFee.FocusedRowHandle) as EntityHisFee;
                return vo;
            }
        }

        public EntityZdzhxm GetRowObjComb()
        {
            if (this.gvComb.FocusedRowHandle < 0)
                return null;
            else
            {
                EntityZdzhxm vo = this.gvComb.GetRow(this.gvComb.FocusedRowHandle) as EntityZdzhxm;
                return vo;
            }
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void btnSearchComb_Click(object sender, EventArgs e)
        {
            List<EntityZdzhxm> temp = null;
            string search = txtSearhComb6.Text;
            if (!string.IsNullOrEmpty(search))
            {
                temp = dataZdzhxm.FindAll(r => r.comb_name.Contains(search));
            }

            if (temp != null)
            {
                this.gcComb.DataSource = temp;
                this.gcComb.RefreshDataSource();
            }
            else
            {
                this.gcComb.DataSource = dataZdzhxm;
                this.gcComb.RefreshDataSource();
            }
        }

        private void btnSearchFee_Click(object sender, EventArgs e)
        {
            List<EntityHisFee> temp = null;
            string search = txtSearhHisFee6.Text;
            if (!string.IsNullOrEmpty(search))
            {
                temp = dataHisFee.FindAll(r => r.hisName.Contains(search));
            }

            if (temp != null)
            {
                this.gcHisFee.DataSource = temp;
                this.gcHisFee.RefreshDataSource();
            }
            else
            {
                this.gcHisFee.DataSource = dataZdzhxm;
                this.gcHisFee.RefreshDataSource();
            }
        }

        private void btnAddHisFee_Click(object sender, EventArgs e)
        {
            EntityHisFee hisFee = GetRowObjHisFee();
            EntityZdzhxm zdZh = GetRowObjComb();
            if (hisFee == null || zdZh == null)
                return;

            if (hisFee.peName != zdZh.comb_name)
            {
                MessageBox.Show("名称不同，请重选！");
                return;
            }

            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                string sql = @"insert into dyZhxmHis values (?, ?, ?)";
                IDataParameter[] parm = svc.CreateParm(3);
                parm[0].Value = zdZh.comb_code;
                parm[1].Value = hisFee.hisCode;
                parm[2].Value = Function.Int(hisFee.amount) ;
                int affect = svc.ExecSql(sql,parm);
                if (affect > 0)
                {
                    string comb_code = "('" + zdZh.comb_code + "')";
                    RefreshData(comb_code);
                }
                    
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }  
        }

        private void btnAddHisFeeAdd_Click(object sender, EventArgs e)
        {
            EntityHisFee hisFee = GetRowObjHisFee();
            EntityZdzhxm zdZh = GetRowObjComb();
            if (hisFee == null || zdZh == null)
                return;


            if (hisFee.peName != zdZh.comb_name)
            {
                MessageBox.Show("名称不同，请重选！");
                return;
            }

            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                string sql = @"insert into dyZhxmHis_Add values (?, ?, ?,?)";
                IDataParameter[] parm = svc.CreateParm(4);
                parm[0].Value = zdZh.comb_code;
                parm[1].Value = hisFee.hisCode;
                parm[2].Value = Function.Int(hisFee.amount);
                parm[3].Value = 0;
                int affect = svc.ExecSql(sql, parm);
                if (affect > 0)
                {
                    string comb_code = "('" + zdZh.comb_code + "')";
                    RefreshData(comb_code);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gvComb_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            EntityZdzhxm zdZh = this.gvComb.GetRow(e.RowHandle) as EntityZdzhxm;
            if (zdZh == null)
                return;

            List<EntityPeHis> lstPeHis = dataPeHis.FindAll(r => r.comb_code.Contains(zdZh.comb_code));
            if (lstPeHis != null)
            {
                this.gcPeHis.DataSource = lstPeHis;
                this.gcPeHis.RefreshDataSource();
            }
        }

        private void gvHisFee_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            EntityHisFee hisFee = this.gvHisFee.GetRow(e.RowHandle) as EntityHisFee;
            if (hisFee == null)
                return;

            List<EntityZdzhxm> lstZdzh = dataZdzhxm.FindAll(r=>r.comb_name.Contains(hisFee.peName));
            if(lstZdzh != null)
            {
                this.gcComb.DataSource = lstZdzh;
                this.gcComb.RefreshDataSource();
            }
            string combCode = string.Empty;
            foreach(EntityZdzhxm vo in lstZdzh)
            {
                combCode += "'" + vo.comb_code + "',";
            }

            if (!string.IsNullOrEmpty(combCode))
            {
                combCode = "(" + combCode.TrimEnd(',') + ")";
                RefreshData(combCode);
            }
        }

        private void btnAddMr_Click(object sender, EventArgs e)
        {
            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                int seq = -1;
                string sql = @"select MAX(comb_code)+1 as seq from zdZhxm";
                DataTable dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                    seq = Function.Int(dt.Rows[0]["seq"]) ;
                if (seq <= 0)
                    return;
                EntityHisFee hisFee = GetRowObjHisFee();
                if (hisFee == null)
                    return;

                sql = @"INSERT INTO zdZhxm (comb_code, comb_name, cls_code, check_cls, price, fee_class, rep_class, hint_info, 
                                                disp_order, dept_code, exam_room, py_code, wb_code, selfdef_code, inst_flag, 
                                                prn_flag, ref_flag, btype, lflag, tab_type, basic_cls, con_info, 
                                                rate_flag, comb_time, sex, txm_sl, zyd_flag, get_room, zyd_prn_flag, zyd_order, 
                                                his_feecode, comb_meaning, web_flag, yg_flag, p_flag, jy_note, jy_note2, pd_flag, ws, 
                                                lis_feecode, pacs_feecode, addr_info, Depname, his_feetype) VALUES 
                                                ('?', '?', '18    ', '1', '850.00', '    ', '  ', '', '99999 ', '18', '115', 
                                                    'XZDM', 'EYFE', '', 'T', 'T', ' ', '  ', ' ', '   ', '06  ', '未见异常', ' ', 
                                                NULL, '0', '', 'MR', '18', 'T', '', '', '', 'F', ' ', '', '', '', 'F', ' ', '', '', '', '?', '')";

                IDataParameter[] parm = svc.CreateParm(3);
                parm[0].Value = seq.ToString();
                parm[1].Value = hisFee.peName;
                parm[2].Value = hisFee.peName;
                int affect = svc.ExecSql(sql, parm);
                if (affect < 0)
                    MessageBox.Show("Fail...................");
                Init(1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAddCt_Click(object sender, EventArgs e)
        {
            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                int seq = -1;
                string sql = @"select MAX(comb_code)+1 as seq from zdZhxm";
                DataTable dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                    seq = Function.Int(dt.Rows[0]["seq"]);
                if (seq <= 0)
                    return;
                EntityHisFee hisFee = GetRowObjHisFee();
                if (hisFee == null)
                    return;

                sql = @"INSERT INTO zdZhxm (comb_code, comb_name, cls_code, check_cls, price, fee_class, rep_class, hint_info, 
                                                disp_order, dept_code, exam_room, py_code, wb_code, selfdef_code, inst_flag, 
                                                prn_flag, ref_flag, btype, lflag, tab_type, basic_cls, con_info, 
                                                rate_flag, comb_time, sex, txm_sl, zyd_flag, get_room, zyd_prn_flag, zyd_order, 
                                                his_feecode, comb_meaning, web_flag, yg_flag, p_flag, jy_note, jy_note2, pd_flag, ws, 
                                                lis_feecode, pacs_feecode, addr_info, Depname, his_feetype) VALUES 
                                                ('?', '?', '15    ', '1', '850.00', '    ', '  ', '', '99999 ', '15', '115', 
                                                    'XZDM', 'EYFE', '', 'T', 'T', ' ', '  ', ' ', '   ', '06  ', '未见异常', ' ', 
                                                NULL, '0', '', 'CT', '15', 'T', '', '', '', 'F', ' ', '', '', '', 'F', ' ', '', '', '', '?', '')";

                IDataParameter[] parm = svc.CreateParm(3);
                parm[0].Value = seq.ToString();
                parm[1].Value = hisFee.peName;
                parm[2].Value = hisFee.peName;
                int affect = svc.ExecSql(sql, parm);
                if (affect < 0)
                    MessageBox.Show("Fail...................");
                Init(1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
