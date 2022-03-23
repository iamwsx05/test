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

namespace DLYY
{
    public partial class frmDLYY : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmDLYY()
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
                InitTjHis();
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

                       int affect =  svc.ExecSql(sql,param);

                        if(affect <= 0)
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


        #region 5
        List<EntityZdzhDlt> data = new List<EntityZdzhDlt>();
        List<EntityHisTj> dataHis = new List<EntityHisTj>();
        internal void InitTjHis()
        {
            try
            {
                
                string sql = @"select z.cls_name,  c.comb_code,
                                    c.comb_name,
                                    h.HIS_ITEM_CODE,
                                    f.HIS_NAME,f.HIS_PRICE 
                                    from zdZhxm c
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
                        data.Add(vo);
                    }

                }

                this.gcTjzh.DataSource = data;
                this.gcTjzh.RefreshDataSource();


                sql = @"select * from tj_zhxmdzb";

                DataTable dtHis = svc.GetDataTable(sql);
                if (dtHis != null && dtHis.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtHis.Rows)
                    {
                        EntityHisTj vo = new EntityHisTj();
                        vo.gjc = dr["gjc"].ToString();
                        vo.gjcmc = dr["gjcmc"].ToString();
                        vo.tjzhxm = dr["tjzhxm"].ToString();
                        vo.tjzhxmmc = dr["tjzhxmmc"].ToString().Trim();

                        dataHis.Add(vo);
                    }

                }

                this.gcHiszh.DataSource = dataHis;
                this.gcHiszh.RefreshDataSource();

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

        private void gvTjzh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            EntityZdzhDlt tjVo = GetRowObjectTjzh();

            if (tjVo == null)
                return;

            List<EntityHisTj> dataTmp = dataHis.FindAll(r=>r.tjzhxmmc == tjVo.combName);

            if(dataTmp != null )
            {
                this.gcHiszh.DataSource = dataTmp;
                this.gcHiszh.RefreshDataSource();
            }
        }

        #endregion
    }
}
