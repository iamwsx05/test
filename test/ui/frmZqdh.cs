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
    public partial class frmZqdh : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmZqdh()
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
                InitTjJyZh();
                GetTjJyZh();
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

            }
            else if (tabPane1.SelectedPageIndex == 1)
            {

            }
            else if (tabPane1.SelectedPageIndex == 2)
            {
            }
        }


        #region tab4
        List<EntityHisxm> lstHis = new List<EntityHisxm>();
        List<EntityTjJyZh> lstTjzh = new List<EntityTjJyZh>();
        List<EntityTjJyZh> lstJyzh = new List<EntityTjJyZh>();
        List<EntityZhmx> lstZhxm = new List<EntityZhmx>();
        List<EntityZhmx> lstALLxm = new List<EntityZhmx>();
        void InitTjJyZh()
        {

            if (!File.Exists(filePath))
            {
                MessageBox.Show("请选择文件！");
                return;
            }
            DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EntityHisxm vo = new EntityHisxm();
                    vo.hisName = dr["名称"].ToString();
                    vo.hisCode = dr["编码"].ToString();
                    vo.hisJg = dr["门诊价格"].ToString();
                    lstHis.Add(vo);
                }
            }


            lstTjzh.Clear();
            lstJyzh.Clear();

            string sql = @"SELECT
	                            a.comb_code,
	                            a.comb_name,
	                            b.cls_code,
	                            b.cls_name,
	                            a.his_feecode,
	                            a.price,
                              c.as_group
                            FROM
	                            zdZhxm a
                            LEFT JOIN zdXmfl b ON a.cls_code = b.cls_code
                            left join dyTjjyxm c on a.comb_code = c.comb_code
                            WHERE
	                            b.cls_code = '06'
                            AND a.inst_flag = 'T'   ";

            SqlHelper svcTj = new SqlHelper(EnumBiz.onlineDB);
            DataTable dtTj = svcTj.GetDataTable(sql);
            if (dtTj != null && dtTj.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTj.Rows)
                {
                    EntityTjJyZh vo = new EntityTjJyZh();
                    vo.tjzhbm = dr["comb_code"].ToString();
                    vo.tjzhmc = dr["comb_name"].ToString();
                    vo.tjhis = dr["his_feecode"].ToString().Trim();
                    vo.tjjg = dr["price"].ToString();
                    lstTjzh.Add(vo);
                }
            }

            this.gcTjzh.DataSource = lstTjzh;
            this.gcTjzh.RefreshDataSource();


            sql = @"select * from zdxm ";
            DataTable dtXm = svcTj.GetDataTable(sql);
            if (dtXm != null && dtXm.Rows.Count > 0)
            {
                foreach (DataRow dr in dtXm.Rows)
                {
                    EntityZhmx vo = new EntityZhmx();
                    vo.xmbm = dr["item_code"].ToString();
                    vo.xmmc = dr["item_name"].ToString();

                    lstALLxm.Add(vo);
                }
            }


            List<EntityTjJyZh> lstYh = GetTjJyZh();
            string combCodeStr = string.Empty;
            if (lstYh != null && lstYh.Count > 0)
            {
                foreach (EntityTjJyZh vo in lstYh)
                {
                    combCodeStr += "'" + vo.jyzhbm + "',";
                }
            }


            sql = @"select a.GROUP_CODE,a.GROUP_NAME,a.HIS_CODE from DMB_ZH a";

            SqlHelper svcLis = new SqlHelper(EnumBiz.lisDB);
            DataTable dtLis = svcLis.GetDataTable(sql);
            if (dtLis != null && dtLis.Rows.Count > 0)
            {
                foreach (DataRow dr in dtLis.Rows)
                {
                    EntityTjJyZh vo = new EntityTjJyZh();
                    vo.jyzhbm = dr["GROUP_CODE"].ToString();
                    vo.jyzhmc = dr["GROUP_NAME"].ToString();
                    vo.jyhis = dr["HIS_CODE"].ToString();
                    if (!string.IsNullOrEmpty(vo.jyhis))
                    {
                        EntityHisxm his = lstHis.Find(r => r.hisCode == vo.jyhis);
                        if (his != null)
                        {
                            vo.jyzhmc = his.hisName;
                            vo.jyjg = his.hisJg;
                        }

                    }

                    lstJyzh.Add(vo);
                }
            }

            this.gcJyzh.DataSource = lstJyzh;
            this.gcJyzh.RefreshDataSource();

            this.gcTjxm.DataSource = null;
            this.gcTjxm.RefreshDataSource();
            this.gcJyxm.DataSource = null;
            this.gcJyxm.RefreshDataSource();

        }

        public EntityTjJyZh GetRowObjectTjzh()
        {
            if (this.gvTjzh.FocusedRowHandle < 0)
                return null;
            else
            {
                EntityTjJyZh his = this.gvTjzh.GetRow(this.gvTjzh.FocusedRowHandle) as EntityTjJyZh;
                return his;
            }

        }

        public EntityTjJyZh GetRowObjectJyzh()
        {
            if (this.gvJyzh.FocusedRowHandle < 0)
                return null;
            else
            {
                EntityTjJyZh tjzh = this.gvJyzh.GetRow(this.gvJyzh.FocusedRowHandle) as EntityTjJyZh;
                return tjzh;
            }
        }

        List<EntityTjJyZh> GetTjzh(EntityTjJyZh vo = null)
        {
            List<EntityTjJyZh> data = new List<EntityTjJyZh>();
            string sql = string.Empty;
            if (vo == null)
            {
                sql = @"SELECT
	                            a.comb_code,
	                            a.comb_name,
	                            b.cls_code,
	                            b.cls_name,
	                            a.his_feecode,
	                            a.price,
                              c.as_group
                            FROM
	                            zdZhxm a
                            LEFT JOIN zdXmfl b ON a.cls_code = b.cls_code
                            left join dyTjjyxm c on a.comb_code = c.comb_code
                            WHERE
	                            b.cls_code = '06'
                            AND a.inst_flag = 'T'";
            }
            else
            {
                sql = @"SELECT
	                            a.comb_code,
	                            a.comb_name,
	                            b.cls_code,
	                            b.cls_name,
	                            a.his_feecode,
	                            a.price,
                              c.as_group
                            FROM
	                            zdZhxm a
                            LEFT JOIN zdXmfl b ON a.cls_code = b.cls_code
                            left join dyTjjyxm c on a.comb_code = c.comb_code
                            WHERE
	                            b.cls_code = '06'
                            AND a.inst_flag = 'T'";

                sql += " and c.as_group = '" + vo.jyzhbm + "'";
            }

            SqlHelper svcTj = new SqlHelper(EnumBiz.onlineDB);
            DataTable dtTj = svcTj.GetDataTable(sql);
            if (dtTj != null && dtTj.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTj.Rows)
                {
                    EntityTjJyZh vo1 = new EntityTjJyZh();
                    vo1.tjzhbm = dr["comb_code"].ToString();
                    vo1.tjzhmc = dr["comb_name"].ToString();
                    vo1.tjhis = dr["his_feecode"].ToString().Trim();
                    vo1.tjjg = dr["price"].ToString();
                    data.Add(vo1);
                }
            }
            return data;
        }

        List<EntityTjJyZh> GetTjJyZh(EntityTjJyZh voTj = null)
        {
            List<EntityTjJyZh> data = new List<EntityTjJyZh>();
            string sql = string.Empty;
            if (voTj == null)
            {
                sql = @"SELECT
	                            zdXmfl.cls_code,
	                            zdXmfl.cls_name,
	                            dyTjjyxm.comb_code,
	                            dyTjjyxm.comb_name,
	                            dyTjjyxm.as_group,
	                            dyTjjyxm.as_group_name,
	                            dyTjjyxm.as_room,
	                            dyTjjyxm.as_room_name
                            FROM
	                            dyTjjyxm,
	                            zdZhxm,
	                            zdXmfl
                            WHERE
	                            (
		                            dyTjjyxm.comb_code = zdZhxm.comb_code
	                            )
                            AND (
	                            zdZhxm.cls_code = zdXmfl.cls_code
                            )
                            AND (zdZhxm.inst_flag = 'T')";
            }
            else
            {
                sql = @"SELECT
	                            zdXmfl.cls_code,
	                            zdXmfl.cls_name,
	                            dyTjjyxm.comb_code,
	                            dyTjjyxm.comb_name,
	                            dyTjjyxm.as_group,
	                            dyTjjyxm.as_group_name,
	                            dyTjjyxm.as_room,
	                            dyTjjyxm.as_room_name
                            FROM
	                            dyTjjyxm,
	                            zdZhxm,
	                            zdXmfl
                            WHERE
	                            (
		                            dyTjjyxm.comb_code = zdZhxm.comb_code
	                            )
                            AND (
	                            zdZhxm.cls_code = zdXmfl.cls_code
                            )
                            AND (zdZhxm.inst_flag = 'T')";
                sql += " and dyTjjyxm.comb_code = '" + voTj.tjzhbm + "'";

            }


            SqlHelper svcTj = new SqlHelper(EnumBiz.onlineDB);
            svcTj.ExecSql(sql);
            DataTable dtTj = svcTj.GetDataTable(sql);
            if (dtTj != null && dtTj.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTj.Rows)
                {
                    EntityTjJyZh vo = new EntityTjJyZh();
                    vo.tjzhbm = dr["comb_code"].ToString();
                    vo.tjzhmc = dr["comb_name"].ToString();
                    vo.jyzhbm = dr["as_group"].ToString();
                    vo.jyzhmc = dr["as_group_name"].ToString();

                    data.Add(vo);
                }
            }

            this.gcTjJyZh.DataSource = data;
            this.gcTjJyZh.RefreshDataSource();

            return data;
        }

        private void btnAjtjjy_Click(object sender, EventArgs e)
        {
            //AjJt2Tjzh();
            AjTjzh();
        }

        void AjTjzh()
        {
            EntityTjJyZh tjVo = GetRowObjectTjzh();
            EntityTjJyZh jyVo = GetRowObjectJyzh();
            if (tjVo == null || jyVo == null)
                return;

            try
            {
                string sql = @"update zdzhxm set comb_name = ?, price = ?,his_feecode = ?  where comb_code = ?";

                SqlHelper svcTj = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] parm = svcTj.CreateParm(4);
                parm[0].Value = jyVo.jyzhmc;
                parm[1].Value = Function.Dec(jyVo.jyjg);
                parm[2].Value = jyVo.jyhis;
                parm[3].Value = tjVo.tjzhbm;
                int affect = -1;
                affect = svcTj.ExecSql(sql, parm);

                if (affect > 0)
                {
                    GetTjJyZh();

                    for (int i = 0; i < this.gvTjJyZh.RowCount; i++)
                    {
                        this.gvTjJyZh.UnselectRow(i);
                    }


                    for (int i = 0; i < this.gvTjJyZh.RowCount; i++)
                    {
                        EntityTjJyZh vo = this.gvTjJyZh.GetRow(i) as EntityTjJyZh;

                        if (vo.tjzhbm == tjVo.tjzhbm)
                        {
                            this.gvTjJyZh.FocusedRowHandle = i;
                            this.gvTjJyZh.SelectRow(i);
                            break;
                        }
                    }

                    for (int i = 0; i < this.gvTjzh.RowCount; i++)
                    {
                        this.gvTjzh.UnselectRow(i);
                    }


                    for (int i = 0; i < this.gvTjzh.RowCount; i++)
                    {
                        EntityTjJyZh vo = this.gvTjzh.GetRow(i) as EntityTjJyZh;

                        if (vo.tjzhbm == tjVo.tjzhbm)
                        {
                            this.gvTjzh.FocusedRowHandle = i;
                            this.gvTjzh.SelectRow(i);
                            break;
                        }
                    }

                    this.gcTjzh.DataSource = GetTjzh(jyVo);
                    this.gcTjzh.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }

        void AjJt2Tjzh()
        {
            EntityTjJyZh tjVo = GetRowObjectTjzh();
            EntityTjJyZh jyVo = GetRowObjectJyzh();
            if (tjVo == null || jyVo == null)
                return;

            string sql = @"select * from dyTjjyxm a where a.comb_code = ?";

            SqlHelper svcTj = new SqlHelper(EnumBiz.onlineDB);
            IDataParameter parm = svcTj.CreateParm();
            parm.Value = tjVo.tjzhbm;
            int affect = -1;
            DataTable dtTj = svcTj.GetDataTable(sql, parm);
            if (dtTj != null && dtTj.Rows.Count > 0)
            {
                sql = @"update dyTjjyxm set  as_group= ?, as_group_name= ? where comb_code = ?";
                IDataParameter[] parmTj = svcTj.CreateParm(3);
                parmTj[0].Value = jyVo.jyzhbm;
                parmTj[1].Value = jyVo.jyzhmc;
                parmTj[2].Value = tjVo.tjzhbm;
                affect = svcTj.ExecSql(sql, parmTj);


            }
            else
            {
                sql = @"INSERT INTO dyTjjyxm (comb_code, comb_name, as_group, as_group_name, as_room, as_room_name) VALUES (?, ?, ?, ?, '001 ', '检验科')";
                IDataParameter[] parmTj = svcTj.CreateParm(4);
                parmTj[0].Value = tjVo.tjzhbm;
                parmTj[1].Value = tjVo.tjzhmc;
                parmTj[2].Value = jyVo.jyzhbm;
                parmTj[3].Value = jyVo.jyzhmc;
                affect = svcTj.ExecSql(sql, parmTj);
            }

            if (affect > 0)
            {
                GetTjJyZh();

                for (int i = 0; i < this.gvTjJyZh.RowCount; i++)
                {
                    this.gvTjJyZh.UnselectRow(i);
                }


                for (int i = 0; i < this.gvTjJyZh.RowCount; i++)
                {
                    EntityTjJyZh vo = this.gvTjJyZh.GetRow(i) as EntityTjJyZh;

                    if (vo.tjzhbm == tjVo.tjzhbm)
                    {
                        this.gvTjJyZh.FocusedRowHandle = i;
                        this.gvTjJyZh.SelectRow(i);
                        break;
                    }
                }
            }
        }

        private void btnStjzh_Click(object sender, EventArgs e)
        {
            string search = txtStj.Text;
            if (string.IsNullOrEmpty(search))
            {
                this.gcTjzh.DataSource = lstTjzh;
                this.gcTjzh.RefreshDataSource();
            }
            else
            {
                this.gcTjzh.DataSource = lstTjzh.FindAll(t => t.tjzhmc.Contains(search));
                this.gcTjzh.RefreshDataSource();
            }

        }
        private void btnSjyzh_Click(object sender, EventArgs e)
        {
            string search = txtSjy.Text;
            if (string.IsNullOrEmpty(search))
            {
                this.gcJyzh.DataSource = lstJyzh;
                this.gcJyzh.RefreshDataSource();
            }
            else
            {
                this.gcJyzh.DataSource = lstJyzh.FindAll(t => t.jyzhmc.Contains(search));
                this.gcJyzh.RefreshDataSource();
            }
        }

        private void gvTjzh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            EntityTjJyZh tjVo = GetRowObjectTjzh();
            List<EntityZhmx> data = new List<EntityZhmx>();
            if (tjVo == null)
                return;

            string sql = @"select c.item_code,c.item_name from zdZhxm a 
                                    left join zdZhxmmx b
                                    on a.comb_code = b.comb_code
                                    left join zdXm c
                                    on b.item_code = c.item_code where a.comb_code = ? ";


            SqlHelper svcTj = new SqlHelper(EnumBiz.onlineDB);
            IDataParameter parm = svcTj.CreateParm();
            parm.Value = tjVo.tjzhbm;

            DataTable dtTj = svcTj.GetDataTable(sql, parm);
            if (dtTj != null && dtTj.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTj.Rows)
                {
                    EntityZhmx vo = new EntityZhmx();
                    vo.xmbm = dr["item_code"].ToString();
                    vo.xmmc = dr["item_name"].ToString();

                    data.Add(vo);
                }
            }

            this.gcTjxm.DataSource = data;
            this.gcTjxm.RefreshDataSource();

            for (int i = 0; i < this.gvTjJyZh.RowCount; i++)
            {
                this.gvTjJyZh.UnselectRow(i);
            }

            bool isMatch = false;
            EntityTjJyZh voTjjy = null;
            for (int i = 0; i < this.gvTjJyZh.RowCount; i++)
            {
                voTjjy = this.gvTjJyZh.GetRow(i) as EntityTjJyZh;

                if (voTjjy.tjzhbm == tjVo.tjzhbm)
                {
                    isMatch = true;
                    this.gvTjJyZh.FocusedRowHandle = i;
                    this.gvTjJyZh.SelectRow(i);

                    break;
                }
            }
            if (voTjjy == null)
                return;

            for (int i = 0; i < this.gvJyzh.RowCount; i++)
            {
                this.gvJyzh.UnselectRow(i);
            }
            for (int i = 0; i < this.gvJyzh.RowCount; i++)
            {
                EntityTjJyZh vo = this.gvJyzh.GetRow(i) as EntityTjJyZh;

                if (vo.jyzhbm == voTjjy.jyzhbm)
                {
                    isMatch = true;
                    this.gvJyzh.FocusedRowHandle = i;
                    this.gvJyzh.SelectRow(i);

                    GetJyxm(vo);

                    break;
                }
            }


            if (!isMatch)
            {
                this.gcJyzh.DataSource = null;
                this.gcJyzh.RefreshDataSource();
                this.gcJyxm.DataSource = null;
                this.gcJyxm.RefreshDataSource();
            }
        }



        private void gvJyzh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            EntityTjJyZh jyVo = GetRowObjectJyzh();
            GetJyxm(jyVo);

            this.gcTjzh.DataSource = GetTjzh(jyVo);
            this.gcTjzh.RefreshDataSource();

            EntityTjJyZh tjVo = GetRowObjectTjzh();
            GetTjJyZh(tjVo);
        }

        void GetJyxm(EntityTjJyZh jyvo)
        {
            List<EntityZhmx> data = new List<EntityZhmx>();
            string sql = @"select distinct c.ITEM_CODE,
                            c.ITEM_NAME,
                            c.UNIT,
                            d.UPBOUND,
                            d.DNBOUND  
                            from DMB_ZH a 
                            left join DYB_XMZH b
                            on a.GROUP_CODE = b.GROUP_CODE
                            left join DMB_XM c
                            on b.ITEM_CODE = c.ITEM_CODE
                            left join DYB_XMCKFW d
                            on c.ITEM_CODE = d.ITEM_CODE
                            where a.GROUP_CODE = ?  ";

            SqlHelper svcLis = new SqlHelper(EnumBiz.lisDB);
            IDataParameter parm = svcLis.CreateParm();
            parm.Value = jyvo.jyzhbm;
            DataTable dtLis = svcLis.GetDataTable(sql, parm);
            if (dtLis != null && dtLis.Rows.Count > 0)
            {
                foreach (DataRow dr in dtLis.Rows)
                {
                    EntityZhmx vo = new EntityZhmx();
                    vo.xmbm = dr["ITEM_CODE"].ToString();
                    vo.xmmc = dr["ITEM_NAME"].ToString();
                    vo.unit = dr["UNIT"].ToString();
                    vo.refUp = dr["UPBOUND"].ToString();
                    vo.refDn = dr["DNBOUND"].ToString();
                    if (vo.refDn.Contains("阴"))
                        vo.refResult = "阴性";

                    if (vo.refDn.Contains("阴") || vo.refDn.Contains("阳"))
                    {
                    }
                    else
                    {
                        vo.hint_lower = "2";
                        vo.hint_lower = "2";
                    }

                    if (data.Any(r => r.xmbm == vo.xmbm))
                        continue;
                    data.Add(vo);
                }
            }
            this.gcJyxm.DataSource = data;
            this.gcJyxm.RefreshDataSource();
        }

        private void txtSjy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string search = txtSjy.Text;
                if (string.IsNullOrEmpty(search))
                {
                    this.gcJyzh.DataSource = lstJyzh;
                    this.gcJyzh.RefreshDataSource();
                }
                else
                {
                    this.gcJyzh.DataSource = lstJyzh.FindAll(t => t.jyzhmc.Contains(search));
                    this.gcJyzh.RefreshDataSource();
                }
            }
        }

        private void txtStj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string search = txtStj.Text;
                if (string.IsNullOrEmpty(search))
                {
                    this.gcTjzh.DataSource = lstTjzh;
                    this.gcTjzh.RefreshDataSource();
                }
                else
                {
                    this.gcTjzh.DataSource = lstTjzh.FindAll(t => t.tjzhmc.Contains(search));
                    this.gcTjzh.RefreshDataSource();
                }
            }
        }

        private void btnDelTjzh_Click(object sender, EventArgs e)
        {

            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                EntityTjJyZh tjVo = GetRowObjectTjzh();

                if (tjVo == null)
                    return;
                string sql = @"select * from zdTcmx a where a.comb_code = ?";

                SqlHelper svcTj = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter parm = svcTj.CreateParm();
                parm.Value = tjVo.tjzhbm;
                int affect = -1;
                DataTable dtTj = svcTj.GetDataTable(sql, parm);
                if (dtTj != null && dtTj.Rows.Count > 0)
                {
                    MessageBox.Show("该组已添加到套餐不能删除！");
                }
                else
                {
                    sql = @"delete from zdZhxm  where comb_code = ?";
                    IDataParameter[] parmTj = svcTj.CreateParm(1);
                    parmTj[0].Value = tjVo.tjzhbm;

                    lstParm.Add(svcTj.GetDacParm(EnumExecType.ExecSql, sql, parmTj));

                    sql = @"delete from zdZhxmmx  where comb_code = ?";
                    IDataParameter[] parmTj2 = svcTj.CreateParm(1);
                    parmTj2[0].Value = tjVo.tjzhbm;

                    lstParm.Add(svcTj.GetDacParm(EnumExecType.ExecSql, sql, parmTj2));
                    if (lstParm.Count > 0)
                    {
                        affect = svcTj.Commit(lstParm);
                    }

                    if (affect > 0)
                    {
                        MessageBox.Show("删除成功 ！");

                        InitTjJyZh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<EntityZdzhXm> data = new List<EntityZdzhXm>();
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("请选择文件！");
                    return;
                }
                DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityZdzhXm vo = new EntityZdzhXm();
                        vo.comb_code = dr["组合代码"].ToString();
                        if (vo.comb_code.Contains("项目分类"))
                            continue;
                        vo.comb_name = dr["组合名称"].ToString();
                        vo.price = Function.Dec(dr["单价"].ToString());
                        vo.hint_info = dr["提示信息"].ToString();

                        string inst_flag = dr["启用标志"].ToString().Trim();
                        if (inst_flag == "启用")
                            vo.inst_flag = "T";
                        if (inst_flag == "禁用")
                            vo.inst_flag = "F";
                        vo.his_feecode = dr["HIS收费代码"].ToString();
                        data.Add(vo);
                    }
                }
                int affect = -1;
                foreach (EntityZdzhXm vo in data)
                {
                    affect = -1;
                    string sql = @"update zdzhxm set comb_name = ?, price= ?, hint_info = ? ,inst_flag = ?, his_feecode= ?  where comb_code =  ? ";

                    SqlHelper svcTj = new SqlHelper(EnumBiz.onlineDB);
                    IDataParameter[] parm = svcTj.CreateParm(6);
                    parm[0].Value = vo.comb_name;
                    parm[1].Value = vo.price;
                    parm[2].Value = vo.hint_info;
                    parm[3].Value = vo.inst_flag;
                    parm[4].Value = vo.his_feecode;
                    parm[5].Value = vo.comb_code;
                    affect = svcTj.ExecSql(sql, parm);
                    if (affect < 0)
                        Log.Output("comb_code->" + vo.comb_code);
                }

                if (affect > 0)
                    MessageBox.Show("success !!!!!");
                               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        public class EntityZhmx
        {
            public string zhmc { get; set; }
            public string zhbm { get; set; }
            public string zhdj { get; set; }
            public string xmbm { get; set; }
            public string xmmc { get; set; }
            public string unit { get; set; }
            public string refUp { get; set; }
            public string refDn { get; set; }
            public string refResult { get; set; }
            public string hint_lower { get; set; }
            public string hint_upper { get; set; }
        }



        public class EntityTjJyZh
        {
            public string tjzhbm { get; set; }

            public string tjzhmc { get; set; }
            public string jyzhbm { get; set; }
            public string jyzhmc { get; set; }

            public string jyhis { get; set; }
            public string jyjg { get; set; }
            public string tjhis { get; set; }
            public string tjjg { get; set; }
        }

        public class EntityTjJyXm
        {
            public string tjxmbm { get; set; }
            public string tjxmmc { get; set; }
            public string jyxmbm { get; set; }
            public string jyxmmc { get; set; }

            public string unit { get; set; }
            public string refUp { get; set; }
            public string refDn { get; set; }
            public string refResult { get; set; }
            public string hint_lower { get; set; }
            public string hint_upper { get; set; }

        }


        public class EntityHisxm
        {
            public string hisCode { get; set; }
            public string hisJg { get; set; }
            public string hisName { get; set; }
        }

        public class EntityZdzhXm
        {
            public string comb_code { get; set; }
            public string comb_name { get; set; }
            public decimal price { get; set; }
            public string hint_info { get; set; }
            public string inst_flag { get; set; }
            public string his_feecode { get; set; }
        }

        private void btnAjTjJyXm_Click(object sender, EventArgs e)
        {

        }
    } 
}
