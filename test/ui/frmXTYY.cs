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
    public partial class frmXTYY : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmXTYY()
        {
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");
        }

        string filePath = string.Empty;
        List<EntityPeItem> dataPeItem = null;
        List<EntityLisItem> lstResult = null;
        List<EntityDyTjyjymxxm> lstDyTjjy = null;

        public Dictionary<string, List<string>> dicBmks = new Dictionary<string, List<string>>();
        List<EntityZdzh> lstZdzh = null;
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

        #region Init
        void Init()
        {
            GetDyTjyjymxxm();
            lstZdzh = GetZdzh();
        }
        #endregion

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {
                QueryZh();
            }
            else if (tabPane1.SelectedPageIndex == 1)
            {
                QueryTc();
            }
            else if (tabPane1.SelectedPageIndex == 2)
            {
                QueryTcZh();
            }
            else if (tabPane1.SelectedPageIndex == 4)
            {
                InitTjHis();
            }
            else if (tabPane1.SelectedPageIndex == 5)
            {
                InitJb();
            }
        }

        #endregion

        #region 
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
            catch (Exception ex)
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
                        vo.price = Function.Dec(dr["price"].ToString());
                        vo.combCode = dr["comb_code"].ToString();
                        vo.combName = dr["comb_name"].ToString();
                        string instFlg = dr["inst_flag"].ToString().Trim();
                        if (instFlg == "F")
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
                string sql = @"select a.GKHM as operCode ,
                                        a.XM as operName ,
                                        a.XB as sex ,
                                        a.SRF,
                                        a.GKMM as pwd,
                                        a.PYJM as pyCode,
                                        a.WBJM as wbCode
                                        from futian_user.GZRY a";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

                SqlHelper svcIft = new SqlHelper(EnumBiz.interfaceDB);
                DataTable dt = svcIft.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityCzy vo = new EntityCzy();
                        vo.operCode = dr["operCode"].ToString();
                        vo.operName = dr["operName"].ToString();
                        vo.sex = dr["sex"].ToString();
                        vo.pwd = dr["pwd"].ToString();
                        vo.pyCode = dr["pyCode"].ToString();
                        vo.wbCode = dr["wbCode"].ToString();
                        vo.userId = vo.operCode;

                        sql = @"delete from zdCzy where oper_code = '" + vo.operCode + "'";
                        svc.ExecSql(sql);

                        sql = @"insert into zdCzy values (oper_code,oper_name,sex,pwd,user_id,py_code,wb_code,disable)
                                 values (?, ?, ?, ?, ?, ?, ?, ?)";

                        IDataParameter[] param = svc.CreateParm(8);
                        param[0].Value = vo.operCode;
                        param[1].Value = vo.operName;
                        param[2].Value = vo.sex;
                        param[3].Value = vo.pwd;
                        param[4].Value = vo.userId;
                        param[5].Value = vo.pyCode;
                        param[6].Value = vo.wbCode;
                        param[7].Value = "T";

                        int affect = svc.ExecSql(sql, param);

                        if (affect <= 0)
                        {
                            MessageBox.Show("FAIL..................");
                            break;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 5
        List<EntityZdzhDlt> data = new List<EntityZdzhDlt>();
        List<EntityHisTj> dataHis = new List<EntityHisTj>();
        List<EntityDyTjHis> dataDy = new List<EntityDyTjHis>();
        internal void InitTjHis()
        {
            try
            {
                data.Clear();
                dataHis.Clear();
                dataDy.Clear();

                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    String filepath = f.FileName;//G:\新建文件夹\新建文本文档.txt
                    String filename = f.SafeFileName;//新建文本文档.txt
                    filePath = filepath;
                }

                lblFile.Text = filePath;


                if (!File.Exists(filePath))
                {
                    MessageBox.Show("请选择文件！");
                    return;
                }
                DataTable dtHis = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);
                if (dtHis != null && dtHis.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtHis.Rows)
                    {
                        EntityHisTj vo = new EntityHisTj();
                        vo.combCode = dr["组合代码"].ToString();
                        vo.combName = dr["组合名称"].ToString();
                        vo.hisCode = dr["对应HIS收费代码"].ToString();
                        vo.price = Function.Dec(dr["单价"]);

                        if (dataHis.Any(r => r.combCode == vo.combCode))
                            continue;
                        dataHis.Add(vo);
                    }
                }



                this.gcHiszh.DataSource = dataHis;
                this.gcHiszh.RefreshDataSource();



                string sql = @"select z.cls_name,  c.comb_code,
                                    c.comb_name,
                                    c.price,
                                    h.HIS_ITEM_CODE,
                                    f.HIS_NAME,f.HIS_PRICE 
                                    from zdZhxm c
                                    left join dyZhxmHis h
                                    on c.comb_code  = h.PE_COMB_CODE
                                    left join HIS_FEE_ITEM f
                                    on h.HIS_ITEM_CODE = f.HIS_CODE
                                    left join zdXmfl z
                                    on c.cls_code = z.cls_code
                                   
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
                        vo.price = Function.Dec(dr["HIS_PRICE"].ToString());
                        vo.hisPrice = Function.Dec(dr["HIS_PRICE"].ToString());
                        data.Add(vo);
                    }

                }

                this.gcTjzh.DataSource = data;
                this.gcTjzh.RefreshDataSource();


                sql = "select * from dyzhxmhis";
                dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDyTjHis vo = new EntityDyTjHis();
                        vo.hisItemCode = dr["his_item_code"].ToString();
                        vo.peCombCode = dr["pe_comb_code"].ToString();
                        dataDy.Add(vo);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        internal void InitTj(EntityZdzhDlt voTj)
        {
            try
            {
                data.Clear();
                dataDy.Clear();
                string sql = @"select z.cls_name,  c.comb_code,
                                    c.comb_name,
                                    c.price,
                                    h.HIS_ITEM_CODE,
                                    f.HIS_NAME,f.HIS_PRICE 
                                    from zdZhxm c
                                    left join dyZhxmHis h
                                    on c.comb_code  = h.PE_COMB_CODE
                                    left join HIS_FEE_ITEM f
                                    on h.HIS_ITEM_CODE = f.HIS_CODE
                                    left join zdXmfl z
                                    on c.cls_code = z.cls_code
                                   
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
                        vo.price = Function.Dec(dr["price"].ToString());
                        vo.hisPrice = Function.Dec(dr["HIS_PRICE"].ToString());
                        data.Add(vo);
                    }

                }

                this.gcTjzh.DataSource = data;
                this.gcTjzh.RefreshDataSource();


                sql = "select * from dyzhxmhis";
                dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDyTjHis vo = new EntityDyTjHis();
                        vo.hisItemCode = dr["his_item_code"].ToString();
                        vo.peCombCode = dr["pe_comb_code"].ToString();
                        dataDy.Add(vo);
                    }

                }

                for (int i = 0; i < this.gvTjzh.RowCount; i++)
                {
                    this.gvTjzh.UnselectRow(i);
                }


                for (int i = 0; i < this.gvTjzh.RowCount; i++)
                {
                    EntityZdzhDlt vo = this.gvTjzh.GetRow(i) as EntityZdzhDlt;

                    if (vo.combCode == voTj.combCode)
                    {
                        this.gvTjzh.FocusedRowHandle = i;
                        this.gvTjzh.SelectRow(i);
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public EntityZdzhDlt GetRowObjectTjzh()
        {
            if (this.gvTjzh.FocusedRowHandle < 0)
                return null;
            else
            {
                EntityZdzhDlt his = this.gvTjzh.GetRow(this.gvTjzh.FocusedRowHandle) as EntityZdzhDlt;
                return his;
            }

        }

        public EntityHisTj GetRowObjectHisTj()
        {
            if (this.gvHiszh.FocusedRowHandle < 0)
                return null;
            else
            {
                EntityHisTj his = this.gvHiszh.GetRow(this.gvHiszh.FocusedRowHandle) as EntityHisTj;
                return his;
            }

        }

        private void gvTjzh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            EntityZdzhDlt tjVo = GetRowObjectTjzh();

            if (tjVo == null)
                return;

            List<EntityHisTj> dataTmp = dataHis.FindAll(r => r.combName == tjVo.combName);

            if (dataTmp != null)
            {
                this.gcHiszh.DataSource = dataTmp;
                this.gcHiszh.RefreshDataSource();
            }
        }

        #endregion

        #region GetYwdjxx
        internal EntityYwDjxx GetYwdjxx(string regNo)
        {
            EntityYwDjxx vo = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = @"select * from ywdjxx where reg_no = '{0}'";
            sql = string.Format(sql, regNo);
            DataTable dt = svc.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                vo = new EntityYwDjxx();
                //vo = Function.Row2Model<EntityYwDjxx>(dt.Rows[0]);
                vo.reg_no = dt.Rows[0]["reg_no"].ToString();
                vo.reg_date = dt.Rows[0]["reg_date"].ToString();
            }


            return vo;
        }
        #endregion

        #region GetZdzh
        internal List<EntityZdzh> GetZdzh()
        {
            List<EntityZdzh> data = new List<EntityZdzh>();
            try
            {
                string sql = "select * from zdzhxm where inst_flag = 'T'";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                DataTable dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityZdzh vo = Function.Row2Model<EntityZdzh>(dr);
                        vo.comb_code = vo.comb_code.Trim();
                        data.Add(vo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return data;
        }
        #endregion

        #region GetDyTjyjymxxm
        internal void GetDyTjyjymxxm()
        {
            lstDyTjjy = new List<EntityDyTjyjymxxm>();
            lstDyTjjy.Clear();

            try
            {
                string sql = "select * from dyTjyjymxxm ";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                DataTable dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDyTjyjymxxm vo = Function.Row2Model<EntityDyTjyjymxxm>(dr);
                        lstDyTjjy.Add(vo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region GetRowObj
        public EntityPeItem GetRowObjPeItem()
        {
            if (this.gvPeResult.FocusedRowHandle < 0)
                return null;
            else
            {
                EntityPeItem vo = this.gvPeResult.GetRow(this.gvPeResult.FocusedRowHandle) as EntityPeItem;
                return vo;
            }
        }

        public EntityLisItem GetRowObjLisItem()
        {
            if (this.gvLisResult.FocusedRowHandle < 0)
                return null;
            else
            {
                EntityLisItem vo = this.gvLisResult.GetRow(this.gvLisResult.FocusedRowHandle) as EntityLisItem;
                return vo;
            }
        }
        #endregion

        #region event

        #region tab5
        private void gvHiszh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            EntityHisTj tjVo = GetRowObjectHisTj();

            if (tjVo == null)
                return;

            List<EntityZdzhDlt> dataTmp = data.FindAll(r => r.combName == tjVo.combName);

            if (dataTmp != null)
            {
                this.gcTjzh.DataSource = dataTmp;
                this.gcTjzh.RefreshDataSource();
            }
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            List<DacParm> lstParm = new List<DacParm>();
            EntityHisTj voHis = GetRowObjectHisTj();
            EntityZdzhDlt voTj = GetRowObjectTjzh();
            int affect = -1;
            if (voHis == null || voTj == null)
                return;
            if (dataDy.Any(r => r.peCombCode == voTj.combCode))
                return;

            try
            {
                string sql = "insert into dyzhxmhis values(?,?,?)";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] param = svc.CreateParm(3);
                param[0].Value = voTj.combCode;
                param[1].Value = voHis.hisCode;
                param[2].Value = 1;

                lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param));

                sql = "update zdzhxm set inst_flag = 'T',his_feecode = ?,price = ? where comb_code = ?";

                IDataParameter[] param2 = svc.CreateParm(3);
                param2[0].Value = voHis.hisCode;
                param2[1].Value = voHis.price;
                param2[2].Value = voTj.combCode;

                lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql, param2));

                if (lstParm.Count > 0)
                {
                    affect = svc.Commit(lstParm);
                }

                if (affect > 0)
                {
                    //MessageBox.Show("删除成功 ！");

                    //nitTjJyZh();
                    InitTj(voTj);
                }
                else
                {
                    MessageBox.Show("FAIL......................");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"select * from zdzhxm ";
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                DataTable dt = svc.GetDataTable(sql);
                int affect = -1;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string combName = dr["comb_name"].ToString();
                        string byCode = new SpellAndWbCodeTookit().GetSpellCode(combName);
                        string wbCode = new SpellAndWbCodeTookit().GetWBCode(combName);

                        if (byCode.Length > 4)
                        {
                            byCode = byCode.Substring(0, 4);
                        }
                        if (wbCode.Length > 4)
                        {
                            wbCode = wbCode.Substring(0, 4);
                        }


                        string byCodeTj = dr["py_code"].ToString();
                        string wbCodeTj = dr["wb_code"].ToString();

                        string combCode = dr["comb_Code"].ToString();
                        if (byCodeTj.Trim() != byCode)
                        {
                            sql = @"update zdzhxm set py_code = ?,wb_code = ?  where comb_code = '" + combCode + "'";

                            IDataParameter[] parm = svc.CreateParm(2);
                            parm[0].Value = byCode;
                            parm[1].Value = wbCode;

                            affect = svc.ExecSql(sql, parm);
                        }


                    }

                    if (affect <= 0)
                    {
                        MessageBox.Show("FAIL..................");
                    }
                    else
                    {
                        MessageBox.Show("SUCCESS!!!!!!!!!!!!!!!!!!!!!!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 检验结果对应
        private void btnSearchPeitem_Click(object sender, EventArgs e)
        {
            try
            {


                lstResult = new List<EntityLisItem>();
                #region 体检
                dataPeItem = new List<EntityPeItem>();
                dataPeItem.Clear();
                string regNo = txtRegNo.Text;
                if (string.IsNullOrEmpty(regNo))
                    return;
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                string sql = @"exec dbo.proc_get_recitem_query '{0}'";
                sql = string.Format(sql, regNo);
                DataTable dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityPeItem vo = Function.Row2Model<EntityPeItem>(dr);
                        if (vo == null)
                            return;
                        vo.refLowUp = vo.ref_lower + "-" + vo.ref_uppper;
                        EntityDyTjyjymxxm voDy = lstDyTjjy.Find(r => r.pe_item == vo.item_code);
                        if (voDy != null)
                            vo.lisCode = voDy.as_item;
                        dataPeItem.Add(vo);
                    }
                }
                #endregion

                #region 检验
                EntityYwDjxx voDjxx = GetYwdjxx(regNo);
                if (voDjxx == null)
                    return;
                string dteFrom = voDjxx.reg_date.Replace('.', '-') + " 00:00:00";
                string dteTo = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"; ;
                Service proxyXtsvc = new Service();
                string res = proxyXtsvc.GetPatientsInfo(regNo, dteFrom, dteTo, "109");
                if (res == "-1")
                {
                    MessageBox.Show("获取报告失败！");
                    return;
                }

                XmlDocument document = new XmlDocument();
                document.LoadXml(res);
                DataTable dtSource = null;
                DataSet ds = Function.ReadXml(res);
                dt = ds.Tables["PatInfo"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dtSource == null) dtSource = dt.Clone();
                    dtSource.Merge(dt);
                    dtSource.AcceptChanges();
                }
                List<EntityPatinfo> lisPat = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    lisPat = new List<EntityPatinfo>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityPatinfo patInfo = Function.Row2Model<EntityPatinfo>(dr);
                        if (lisPat.Any(r => r.lismain_repno == patInfo.lismain_repno))
                            continue;
                        lisPat.Add(patInfo);
                    }
                }

                if (lisPat == null)
                    return;

                foreach (var vo in lisPat)
                {
                    res = proxyXtsvc.GetResultInfo(vo.lismain_repno);
                    if (res == "-1")
                    {
                        MessageBox.Show("获取报告失败！");
                        return;
                    }
                    Log.Output("GetResultInfo-->" + Environment.NewLine + res);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(res);
                    dtSource = null;
                    DataSet dsLis = Function.ReadXml(res);
                    dt = dsLis.Tables["ResultInfo"];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dtSource == null) dtSource = dt.Clone();
                        dtSource.Merge(dt);
                        dtSource.AcceptChanges();
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            EntityLisItem voLis = new EntityLisItem();
                            voLis.lisCode = dr["lisresult_order_id"].ToString().Split('_')[1];
                            EntityZdzh voZdzh = lstZdzh.Find(r => r.comb_code == voLis.lisCode);
                            if (voZdzh == null)
                                MessageBox.Show("无项目-->" + voLis.lisCode);
                            else
                                voLis.lisName = voZdzh.comb_name;
                            voLis.itemCode = dr["lisresult_item_id"].ToString();
                            voLis.itemName = dr["lisresult_item_cname"].ToString();
                            voLis.result = dr["llisresult_result"].ToString();
                            voLis.refLow = dr["lisresult_ref_min"].ToString();
                            voLis.refUp = dr["lisresult_ref_max"].ToString();
                            voLis.unit = dr["lisresult_unit"].ToString();
                            voLis.resultTime = dr["lisresult_time"].ToString();
                            voLis.refLowUp = dr["lisresult_ref_range"].ToString();
                            lstResult.Add(voLis);
                        }
                    }
                }
                #endregion

                this.gcPeResult.DataSource = dataPeItem;
                this.gcPeResult.RefreshDataSource();
                this.gcLisResult.DataSource = lstResult.OrderBy(t => t.lisCode);
                this.gcLisResult.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPe2Lis_Click(object sender, EventArgs e)
        {
            EntityLisItem lisVo = GetRowObjLisItem();
            EntityPeItem peVo = GetRowObjPeItem();
            if (lisVo == null || peVo == null)
                return;

            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                string sql = @"insert into dyTjyjymxxm values (?, ?, ?,?,'001','检验科')";
                IDataParameter[] parm = svc.CreateParm(4);
                parm[0].Value = peVo.item_code;
                parm[1].Value = peVo.item_name;
                parm[2].Value = lisVo.itemCode;
                parm[3].Value = lisVo.itemName;
                int affect = svc.ExecSql(sql, parm);
                if (affect > 0)
                {
                    MessageBox.Show("对应成功");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelDy_Click(object sender, EventArgs e)
        {
            EntityPeItem peVo = GetRowObjPeItem();
            if (peVo == null)
                return;

            try
            {
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                string sql = @"delete from dyTjyjymxxm where pe_item = ? ";
                IDataParameter parm = svc.CreateParm();
                parm.Value = peVo.item_code;
                int affect = svc.ExecSql(sql, parm);
                if (affect > 0)
                {
                    MessageBox.Show("删除成功！！");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #endregion

        List<EntityZdjb> dataZdjb = null;
        List<EntityOldJb> dataOldJb = null;
        internal void InitJb()
        {
            dataZdjb = new List<EntityZdjb>();
            dataOldJb = new List<EntityOldJb>();
            dataZdjb.Clear();
            dataOldJb.Clear();

            try
            {
                string filepath = string.Empty;
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    filepath = f.FileName;//G:\新建文件夹\新建文本文档.txt
                    string filename = f.SafeFileName;//新建文本文档.txt
                    lblFile.Text = filepath;
                }

                if (!File.Exists(filepath))
                {
                    MessageBox.Show("请选择文件！");
                    return;
                }
                DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filepath, 0);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityOldJb vo = Function.Row2Model<EntityOldJb>(dr);
                        dataOldJb.Add(vo);
                    }
                }


                string sql = "select * from zdjb ";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityZdjb vo = Function.Row2Model<EntityZdjb>(dr);
                        dataZdjb.Add(vo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.gcOldJb.DataSource = dataOldJb;
            this.gcOldJb.RefreshDataSource();
            this.gcZdjb.DataSource = dataZdjb;
            this.gcZdjb.RefreshDataSource();
        }
       
        private void btnDelSy_Click(object sender, EventArgs e)
        {
            string deaCode = string.Empty;
            try
            {
                string sugStr = string.Empty;
                int starIndex = 0;
                int endIndex = 0;
                int affect = -1;
                string sql = string.Empty;
               
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                if (dataZdjb == null)
                    return;

                foreach(var vo in dataZdjb)
                {
                    sugStr = string.Empty;
                    deaCode = vo.dea_code;
                    string sugTag = vo.sug_tag;
                    if (deaCode == "01140")
                        deaCode = "01140";
                    if (sugTag.Contains("【释义】") && sugTag.Contains("【建议】"))
                    {
                        starIndex = sugTag.IndexOf("【释义】");
                        endIndex = sugTag.IndexOf("【建议】");
                        sugStr = sugTag.Remove(starIndex, endIndex);
                    }

                    if (sugTag.Contains("【释义】") && !sugTag.Contains("【建议】"))
                    {
                        starIndex = sugTag.IndexOf("【释义】");
                        endIndex = sugTag.Length - 1;
                        sugStr = sugTag.Remove(starIndex, endIndex);
                    }

                    if (sugStr.Contains("【保健】"))
                    {
                        starIndex = sugStr.IndexOf("【保健】");
                        endIndex = sugStr.Length - 1 - starIndex;
                        sugStr = sugStr.Remove(starIndex, endIndex);
                    }
                     
                    if(!string.IsNullOrEmpty(sugStr))
                    {
                        sql = @"update zdjb set sug_tag = ? where dea_code = ?";
                        IDataParameter[] parm = svc.CreateParm(2);

                        parm[0].Value = sugStr;
                        parm[1].Value = vo.dea_code;

                        affect  = svc.ExecSql(sql,parm);
                    }
                }
                if (affect >= 0)
                    MessageBox.Show("success !!!!!!!!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(deaCode + Environment.NewLine  +ex.ToString());
            }
        }

        private void btnAddJy_Click(object sender, EventArgs e)
        {
            if (dataOldJb == null)
                return;

            int affect = -1;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = @"select max(dea_code)+1 as deaCode from zdjb";
            DataTable dt = svc.GetDataTable(sql);
            int deaCode = 0;
            if (dt != null && dt.Rows.Count > 0)
                deaCode = Function.Int(dt.Rows[0]["deaCode"]);

            foreach (var vo in dataOldJb)
            {
                EntityZdjb voZd = new EntityZdjb();
                deaCode++;
                voZd.dea_code = deaCode.ToString();
                voZd.dea_name = vo.jbmc;
                voZd.cls_code = "90";
                if (vo.jbfl.Contains("B超") || vo.jbfl.Contains("彩超检查室") )
                    voZd.cls_code = "08";
                if (vo.jbfl.Contains("CT室"))
                    voZd.cls_code = "15";
                if (vo.jbfl.Contains("耳鼻喉科"))
                    voZd.cls_code = "05";
                if (vo.jbfl.Contains("肺功能检查"))
                    voZd.cls_code = "35";
                if (vo.jbfl.Contains("普通放射DR"))
                    voZd.cls_code = "07";
                if (vo.jbfl.Contains("外科"))
                    voZd.cls_code = "03";
                if (vo.jbfl.Contains("心电图检查"))
                    voZd.cls_code = "11";
                if (vo.jbfl.Contains("眼科"))
                    voZd.cls_code = "04";
                voZd.res_tag = vo.jbmc;
                voZd.sug_tag = vo.jbjy;
                string pyCode = new SpellAndWbCodeTookit().GetSpellCode(voZd.dea_name);
                string wbCode = new SpellAndWbCodeTookit().GetWBCode(voZd.dea_name);

                if (pyCode.Length > 4)
                {
                    pyCode = pyCode.Substring(0, 4);
                }
                if (wbCode.Length > 4)
                {
                    wbCode = wbCode.Substring(0, 4);
                }

                voZd.py_code = pyCode;
                voZd.wb_code = wbCode;
                voZd.stat_flag = "F";
                voZd.res_flag = "T";

                sql = @"INSERT INTO zdJb (dea_code, dea_name, 
                                                    cls_code, res_tag, 
                                                    sug_tag, py_code, wb_code,  
                                                    stat_flag, res_flag) 
                                                    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                int n = -1;
                IDataParameter[] parm = svc.CreateParm(9);
                parm[++n].Value = voZd.dea_code;
                parm[++n].Value = voZd.dea_name;
                parm[++n].Value = voZd.cls_code;
                parm[++n].Value = voZd.res_tag;
                parm[++n].Value = voZd.sug_tag;
                parm[++n].Value = voZd.py_code;
                parm[++n].Value = voZd.wb_code;
                parm[++n].Value = voZd.stat_flag;
                parm[++n].Value = voZd.res_flag;

                affect = svc.ExecSql(sql, parm);
            }

            if(affect >= 0)
            {
                MessageBox.Show("success !!!!!!!!!!!!!!!!");
            }
            else
                MessageBox.Show("fail !!!!!!!!!!!!!!!!");
        }

        private void btnAddPacsDy_Click(object sender, EventArgs e)
        {
            int affect = -1;
            try
            {
                dataZdjb = new List<EntityZdjb>();
                dataZdjb.Clear();
                string sql = "select *  from zdJb a where a.dea_code  >= 9647 ";

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                DataTable dt = svc.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityZdjb vo = Function.Row2Model<EntityZdjb>(dr);
                        dataZdjb.Add(vo);
                    }
                }

               foreach(var vo in dataOldJb)
               {
                    sql = @"INSERT INTO dyPacsZf (for_values, for_content, flag,  dea_code) VALUES (?, ?, 'T', ?)";

                    IDataParameter[] parm = svc.CreateParm(3);
                    parm[0].Value = vo.jbmc;

                    EntityOldJb voOld = dataOldJb.Find(r=>r.jbmc == vo.jbmc);
                    if (voOld == null)
                        continue;
                    if (string.IsNullOrEmpty(voOld.jbzgjz.Trim()))
                        continue;
                    if (voOld.jbzgjz != voOld.jbcgjz && !string.IsNullOrEmpty(voOld.jbcgjz.Trim()))
                        parm[1].Value = voOld.jbzgjz + "+" + voOld.jbcgjz;
                    else
                        parm[1].Value = voOld.jbzgjz;
                    parm[2].Value = dataZdjb.Find(t => t.dea_name == vo.jbmc).dea_code;

                    affect = svc.ExecSql(sql,parm);

                }

                if (affect > 0)
                    MessageBox.Show("sucess !!!!!!!!!!!!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            List<EntitySl> data = new List<EntitySl>();
            List<string> lstReg = new List<string>();
            decimal zysl = 0;
            decimal yysl = 0;
            string regNo = string.Empty;
            string restag = string.Empty;
            try
            {
                string sql = @"select a.reg_no,a.rec_no,b.item_code,b.rec_result,a.res_tag from ywTjbgzx a 
                                        left join ywTjbgjgmxzx b
                                        on a.rec_no = b.rec_no 
                                        where a.comb_code ='10006' and b.item_code in ('040003','040002')  ";
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                DataTable dt = svc.GetDataTable(sql);
                int affect = -1;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntitySl vo = new EntitySl();
                        vo.reg_no = dr["reg_no"].ToString();
                        vo.rec_no = dr["rec_no"].ToString();
                        vo.item_code = dr["item_code"].ToString();
                        vo.rec_result = dr["rec_result"].ToString();
                        vo.res_tag = dr["res_tag"].ToString();
                        if (!lstReg.Contains(vo.reg_no))
                            lstReg.Add(vo.reg_no);

                        data.Add(vo);
                    }
                }

                foreach (var reg in lstReg)
                {
                    restag = string.Empty;
                    zysl = 0;
                    yysl = 0;
                    regNo = string.Empty;

                    List<EntitySl> tmp = data.FindAll(r=>r.reg_no == reg);
                    if (tmp == null)
                        return;

                    foreach (var vo in tmp)
                    {
                        regNo = vo.reg_no;
                        if (vo.rec_result.Contains("-") || string.IsNullOrEmpty(vo.rec_result))
                        {
                            restag = "未见异常";
                            break;
                        }
                        if (vo.rec_result.Contains("失明") )
                        {
                            restag = "失明";
                            break;
                        }

                        if (vo.item_code == "040003")
                            zysl = Function.Dec(vo.rec_result);

                        if (vo.item_code == "040002")
                            yysl = Function.Dec(vo.rec_result);

                         
                    }

                    if (restag == "未见异常" || restag == "失明")
                    {
                    }
                    else
                    {
                        if (zysl >= 5 && yysl >= 5)
                        {
                            restag = "未见异常";
                        }
                        if (zysl < 5 && yysl < 5)
                        {
                            restag = "双眼屈光不正";
                        }

                        if (zysl >= 5 && yysl < 5)
                        {
                            restag = "右眼屈光不正";
                        }

                        if (zysl < 5 && yysl >= 5)
                        {
                            restag = "左眼屈光不正";
                        }
                    }

                    if (!string.IsNullOrEmpty(restag))
                    {
                        sql = @"update ywTjbgzx set res_tag = ?  where comb_code = '10006' and reg_no = ?";
                        IDataParameter[] parm = svc.CreateParm(2);
                        parm[0].Value = restag;
                        parm[1].Value = regNo;

                        affect = svc.ExecSql(sql, parm);
                    }
                    else
                        affect = 0;
                }
                if (affect < 0)
                {
                    MessageBox.Show(regNo  + "--" + restag);
                }
                else
                {
                    MessageBox.Show("SUCCESS!!!!!!!!!!!!!!!!!!!!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
