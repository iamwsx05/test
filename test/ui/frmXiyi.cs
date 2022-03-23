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
using System.Xml.Serialization;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace test
{
    public partial class frmXiyi : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmXiyi()
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
            else if(tabPane1.SelectedPageIndex == 3)
            {
                GetList();
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
        public static T DESerializer<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region 递归读取文件

        /// <summary>
        /// 文件列表
        /// </summary>
        List<string> lstFile = new List<string>();

        /// <summary>
        /// 递归读取
        /// </summary>
        /// <param name="today"></param>
        /// <param name="currDir"></param>
        public void RecuRead(string currDir)
        {
            DirectoryInfo dir = new DirectoryInfo(currDir);
            DirectoryInfo[] subDirs = dir.GetDirectories();
            // 本级目录文件
            foreach (FileInfo f in dir.GetFiles("*.xml", SearchOption.TopDirectoryOnly))
            {
                lstFile.Add(f.FullName);
            }
        }
        #endregion

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var f = new OpenFileDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = f.FileName;//G:\新建文件夹\新建文本文档.txt
                string filename = f.SafeFileName;//新建文本文档.txt
                filePath = filepath;
            }

            lblFile.Text = filePath;
        }


        internal List<EntityList> GetList()
        {
            XmlDocument doc = new XmlDocument();
            
            List<EntityList> data = new List<EntityList>();

            try
            {
                RecuRead("C:\\Users\\Administrator\\Desktop\\XinYi.Doc\\国家平台数据上传\\20211202\\xml\\1");

                foreach (string str in lstFile)
                {
                    doc.Load(str);
                    string xmlStr = doc.InnerXml;
                    xmlStr = xmlStr.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
                    Header vo =  XmlUtil.XmlDeserialize<Header>(xmlStr);
                    if (vo != null)
                    {
                         MessageBox.Show(vo.DocumentId.ToString() + vo.DocumentId);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



            return data;
        }

        private void btnSearch5_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityTjxmmx> data = new List<EntityTjxmmx>();
                string sql = @" SELECT DISTINCT b.lnc_code,   
                                  lnc_name = (select lnc_name from zddw where b.lnc_code = lnc_code) ,
                                   a.reg_no,
                                  b.pat_name,  
                                  b.sex,
                                  b.age,
                                  b.idcard,  
                                  a.p_flag,
                                  a.c_flag,
                                  a.reg_date,  
                                  c.comb_code,
                                  comb_name = (select comb_name from zdZhxm where comb_code = c.comb_code),
                                  totalSum = cast((select sum(isnull(rb_total,0.00)) from ywTjxm where reg_no = a.reg_no ) as decimal(10,2)),  
                                  c.price1
                             FROM  ywDjxx a,  
                               ywBrxx b,
                               ywTjxm c
                               where a.pat_code= b.pat_code and a.reg_times = b.reg_times
                               and a.reg_no = c.reg_no 
                               and a.reg_date between ?  and  ?  order by a.reg_no,c.comb_code";

                string dteStart = this.dteStart.Text;
                string dteEnd = this.dteEnd.Text;

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] parm = svc.CreateParm(2);
                parm[0].Value = dteStart;
                parm[1].Value = dteEnd;
                DataTable dt = svc.GetDataTable(sql, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityTjxmmx vo = new EntityTjxmmx();
                        vo = Function.Row2Model<EntityTjxmmx>(dr);
                        if (vo.sex == "1")
                            vo.sex = "男";
                        else
                            vo.sex = "女";

                        if (vo.p_flag == "T")
                            vo.tjType = "职业健康";
                        else
                            vo.tjType = "普通健康";

                        if (vo.c_flag == "1")
                            vo.regType = "团检";
                        else
                            vo.regType = "个检";
                        data.Add(vo);
                    }
                }

                this.gcTjxmmx.DataSource = data;
                this.gcTjxmmx.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gvTjxmmx_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string reg_no1 = gvTjxmmx.GetRowCellValue(e.RowHandle1, "reg_no").ToString();
            string reg_no2 = gvTjxmmx.GetRowCellValue(e.RowHandle2, "reg_no").ToString();

            if ( e.Column.FieldName == "totalSum" || e.Column.FieldName == "reg_date" || e.Column.FieldName == "lnc_name" || e.Column.FieldName == "reg_no" || e.Column.FieldName == "sex" || e.Column.FieldName == "age" || e.Column.FieldName == "regType" || e.Column.FieldName == "tjType")
            {
                e.Merge =  reg_no1.Equals(reg_no2);
                e.Handled = true;
            }
        }

        private void btnExport5_Click(object sender, EventArgs e)
        {
            uiHelper.ExportToXls(this.gvTjxmmx);
        }
    }
}
