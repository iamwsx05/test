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
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace test
{
    public partial class frmNhpk : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmNhpk()
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
            else if(tabPane1.SelectedPageIndex == 3)
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
            else if(tabPane1.SelectedPageIndex == 3)
            {
                
            }
        }


        List<EntityTmpZybZd4> data = new List<EntityTmpZybZd4>();
        List<EntityTmpZybZd4g> datag = new List<EntityTmpZybZd4g>();

        private void btnSearch_Click(object sender, EventArgs e)
        {
            data.Clear();
            datag.Clear();
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select  *  from tmpZybZd4 a where a.lnc_code  in ('0284',   '0283',  '0288 ')";// a where a.reg_no  ='211030880032'";
            DataTable dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityTmpZybZd4>();
                EntityTmpZybZd4 vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityTmpZybZd4();
                    vo = Function.Row2Model<EntityTmpZybZd4>(dr);

                    Sql = @"exec dbo.procZybExamItemResultList '{0}' ";// a where a.reg_no  ='211030880032'";
                    Sql = string.Format(Sql, vo.reg_no);
                    DataTable dt2 = svc.GetDataTable(Sql);
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            EntityTmpZybZd4 vo2 = new EntityTmpZybZd4();
                            vo2 = Function.Row2Model<EntityTmpZybZd4>(dr);
                            vo2.examItemPname = dr2["examItemPname"].ToString();
                            vo2.examItemName = dr2["examItemName"].ToString();
                            vo2.examItemCode = dr2["examItemCode"].ToString();
                            vo2.examResultType = dr2["examResultType"].ToString();
                            vo2.examResult = dr2["examResult"].ToString();
                            vo2.examItemUnitCode = dr2["examItemUnitCode"].ToString();
                            vo2.referenceRangeMin = dr2["referenceRangeMin"].ToString();
                            vo2.referenceRangeMax = dr2["referenceRangeMax"].ToString();
                            vo2.abnormal = dr2["abnormal"].ToString();
                            vo2.depName = dr2["depName"].ToString();
                            vo2.updateTime = dr2["updateTime"].ToString();
                            vo2.stdId = dr2["stdId"].ToString();
                            vo2.xmdm = dr2["xmdm"].ToString();
                            data.Add(vo2);
                        }
                    }

                    data.Add(vo);
                }
            }

            this.gcTmpZybZd.DataSource = data;
            this.gcTmpZybZd.RefreshDataSource();
            Sql = @"select  * from tmpZybZd4g   a where a.reg_no  in ( select reg_no from tmpZybZd4 where lnc_code  in ('0284',   '0283',  '0288 ') ) ";
            dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                datag = new List<EntityTmpZybZd4g>();
                EntityTmpZybZd4g vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityTmpZybZd4g();
                    vo = Function.Row2Model<EntityTmpZybZd4g>(dr);
                    datag.Add(vo);
                }
            }

            this.gcTmpZybZdg.DataSource = datag;
            this.gcTmpZybZdg.RefreshDataSource();

        }

        Dictionary<string, string> dicZyb = new Dictionary<string, string> {
{"1001",    "白云石粉尘"},
{"2117",    "氟及其化合物（不含氟化氢）（按|F|计）"},
{"3007",    "高温"},
{"3008",    "噪声"}};

        private void btnAj_Click(object sender, EventArgs e)
        {
            if (data == null || datag == null)
                return;
            string Sql = string.Empty;
            IDataParameter[] parm = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            foreach (var vo in data)
            {
                List<EntityTmpZybZd4g> lstg = datag.FindAll(r=>r.reg_no ==  vo.reg_no);

                if (lstg == null)
                    return;

                if (vo.C01 == "03")
                    continue;
                foreach (var vog in lstg)
                {
                    if (string.IsNullOrEmpty(vo.C01))
                    {
                        if (vo.C01a.Contains("未发现") && vo.C01f == "-")
                        {
                            vo.C01 = "01";
                        }
                        else if (string.IsNullOrEmpty(vo.C01) && vo.C01f.Contains("职业禁忌证"))
                        {
                            vo.C01 = "04";
                        }

                        Sql = @"update tmpZybZd4 set C01 = ? where   reg_no = ?  ";

                        parm = svc.CreateParm(2);
                        parm[0].Value = vo.C01;
                        parm[1].Value = vo.reg_no;

                        svc.ExecSql(Sql, parm);

                    }


                    vog.examConclusionCode = vo.C01;
                    vog.yszybCode = "";
                    if (vog.examConclusionCode == "01" || vog.examConclusionCode == "05" || vog.examConclusionCode == "02")
                    {
                        vog.zyjjzName = "-";
                        vog.qtjbName = "未发现" + dicZyb[vog.itemCode] + "作业职业禁忌证和疑似职业病。";
                    }
                    else if(vog.examConclusionCode == "04")
                    {
                        if(vo.C01a.Contains("职业禁忌证") && vo.C01f.Contains(dicZyb[vog.itemCode]))
                        {
                            vog.zyjjzName = dicZyb[vog.itemCode] + "职业禁忌证。";
                            vog.qtjbName = dicZyb[vog.itemCode] + "职业禁忌证。";
                        }
                        else
                        {
                            vog.examConclusionCode = "01";
                            vog.yszybCode = "";
                            vog.zyjjzName = "-";
                            vog.qtjbName = "未发现" + dicZyb[vog.itemCode] + "作业职业禁忌证和疑似职业病。";
                        }
                    }

                    Sql = @"update tmpZybZd4g set examConclusionCode = ?, yszybCode = ?,zyjjzName= ?,qtjbName=? where   reg_no = ? and itemCode = ? ";

                    parm = svc.CreateParm(6);
                    parm[0].Value = vog.examConclusionCode;
                    parm[1].Value = vog.yszybCode;
                    parm[2].Value = vog.zyjjzName;
                    parm[3].Value = vog.qtjbName;
                    parm[4].Value = vog.reg_no;
                    parm[5].Value = vog.itemCode;

                    svc.ExecSql(Sql, parm);
                }

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            uiHelper.ExportToXls(this.gvTmpZybZd);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }


        public static string GetClientParm(string node)
        {
            string text = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName + "\\database.xml";
            if (!File.Exists(text))
            {
                try
                {
                    text = AppDomain.CurrentDomain.BaseDirectory + "\\bin\\database.xml";
                    if (!File.Exists(text))
                    {
                        text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bin\\database.xml";
                    }
                }
                catch
                {
                    text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bin\\database.xml";
                }
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(text);
            XmlElement xmlElement = doc["configuration"][node];
            if (xmlElement == null)
            {
                return "";
            }
            return xmlElement.InnerText.Trim();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            data.Clear();
            datag.Clear();
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string Sql = string.Empty;
            Sql = @"select  *  from tmpZybZd4 a where a.reg_no in ('211013880001',
'211013880002',
'211013880003',
'211013880004',
'211013880005',
'211013880006',
'211013880007',
'211013880008',
'211013880009',
'211013880010',
'211014880001',
'211014880002',
'211014880003',
'211014880004',
'211014880005',
'211014880006',
'211014880007',
'211014880008',
'211014880009',
'211014880010',
'211014880011',
'211014880012',
'211014880013',
'211014880014',
'211014880015',
'211014880016',
'211014880017',
'211014880018',
'211014880019',
'211014880020',
'211014880021',
'211014880022',
'211014880023',
'211014880024',
'211014880025',
'211015880015',
'211015880016',
'211015880017',
'211015880018',
'211015880019',
'211015880020',
'211015880021',
'211015880022',
'211015880023',
'211015880024',
'211015880025',
'211015880026',
'211015880027',
'211015880028',
'211021880001',
'211021880002',
'211021880003',
'211021880004',
'211021880005',
'211021880006',
'211021880007',
'211021880008',
'211021880009',
'211021880010',
'211021880011',
'211021880012',
'211106880001',
'211106880002',
'211106880003',
'211106880004',
'211106880005',
'211106880007',
'211106880008',
'211106880010',
'211106880011',
'211109880001',
'211109880002',
'211109880003',
'211109880004',
'211109880005',
'211109880006',
'211109880007',
'211109880008',
'211109880009',
'211109880010',
'211109880011',
'211109880012',
'211120820001',
'211122880001',
'211122880002',
'211122880003',
'211122880004',
'211122880005',
'211122880006',
'211122880007',
'211122880008',
'211122880009',
'211122880010',
'211122880011',
'211122880012',
'211122880014',
'211122880015',
'211122880016',
'211122880017',
'211122880018',
'211122880019',
'211122880020',
'211122880021',
'211122880022',
'211122880023',
'211122880024',
'211122880025',
'211122880026',
'211122880027',
'211122880028',
'211122880029',
'211122880030',
'211122880031',
'211122880032',
'211122880033',
'211122880034',
'211122880035',
'211122880036',
'211122880037',
'211122880038',
'211122880039',
'211122880040',
'211122880041',
'211122880043',
'211122880044',
'211122880045',
'211122880046',
'211122880047',
'211122880048',
'211122880049',
'211122880050',
'211122880051',
'211122880052',
'211122880053',
'211122880054',
'211122880055',
'211122880056',
'211122880057',
'211122880058',
'211122880059',
'211122880060',
'211122880061',
'211122880062',
'211122880063',
'211122880064',
'211122880065',
'211122880066',
'211122880067',
'211122880068',
'211122880069',
'211122880070',
'211122880071',
'211122880072',
'211122880073',
'211122880074',
'211122880075',
'211122880076',
'211122880077',
'211122880078',
'211122880079',
'211122880080',
'211122880081',
'211122880082',
'211122880083',
'211122880084',
'211122880085',
'211122880086',
'211122880087',
'211122880088',
'211122880089',
'211122880090',
'211122880091',
'211122880092',
'211122880093',
'211122880094',
'211122880095',
'211122880096',
'211122880097',
'211122880098',
'211122880099',
'211122880100',
'211122880101',
'211122880102',
'211122880103',
'211122880104',
'211122880105',
'211122880106',
'211122880107',
'211122880108',
'211122880109',
'211122880110',
'211122880111',
'211122880112',
'211122880113',
'211122880114',
'211122880115',
'211124880001',
'211124880002',
'211124880003',
'211124880004',
'211124880005',
'211124880006',
'211124880007',
'211124880008',
'211124880009',
'211124880010',
'211124880011',
'211124880012',
'211124880013',
'211124880014',
'211124880015',
'211124880016'
) ";//a.lnc_code  in ('0284',   '0283',  '0288 ')";// a where a.reg_no  ='211030880032'";
            DataTable dt = svc.GetDataTable(Sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                data = new List<EntityTmpZybZd4>();
                EntityTmpZybZd4 vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityTmpZybZd4();
                    vo = Function.Row2Model<EntityTmpZybZd4>(dr);
                    Sql = @"exec dbo.procZybExamItemResultList '{0}' ";// a where a.reg_no  ='211030880032'";
                    Sql = string.Format(Sql, vo.reg_no);
                    DataTable dt2 = svc.GetDataTable(Sql);
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            string examItemCode = dr2["examItemCode"].ToString();

                            if (examItemCode.Contains("4070500"))
                            {
                                vo.B4070500 = dr2["examResult"].ToString();
                                vo.B4070500_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4070500_min = dr2["referenceRangeMin"].ToString();
                                vo.B4070500_max = dr2["referenceRangeMax"].ToString();
                            }
                            
                            if (examItemCode.Contains("3051700"))
                                
                            {
                                vo.B3051700 = dr2["examResult"].ToString();
                                vo.B3051700_unit = dr2["examItemUnitCode"].ToString();
                                vo.B3051700_min = dr2["referenceRangeMin"].ToString();
                                vo.B3051700_max = dr2["referenceRangeMax"].ToString();
                            }
                            if (examItemCode.Contains("3020100"))
                                
                            {
                                vo.B3020100 = dr2["examResult"].ToString();
                                vo.B3020100_unit = dr2["examItemUnitCode"].ToString();
                                vo.B3020100_min = dr2["referenceRangeMin"].ToString();
                                vo.B3020100_max = dr2["referenceRangeMax"].ToString();
                            }
                            if (examItemCode.Contains("3020200"))

                            {
                                vo.B3020200 = dr2["examResult"].ToString();
                                vo.B3020200_unit = dr2["examItemUnitCode"].ToString();
                                vo.B3020200_min = dr2["referenceRangeMin"].ToString();
                                vo.B3020200_max = dr2["referenceRangeMax"].ToString();
                            }

                            if (examItemCode.Contains("3020300"))

                            {
                                vo.B3020300 = dr2["examResult"].ToString();
                                vo.B3020300_unit = dr2["examItemUnitCode"].ToString();
                                vo.B3020300_min = dr2["referenceRangeMin"].ToString();
                                vo.B3020300_max = dr2["referenceRangeMax"].ToString();
                            }


                            if (examItemCode.Contains("3080200"))
                            {
                                vo.B3080200 = dr2["examResult"].ToString();
                                vo.B3080200_unit = dr2["examItemUnitCode"].ToString();
                                vo.B3080200_min = dr2["referenceRangeMin"].ToString();
                                vo.B3080200_max = dr2["referenceRangeMax"].ToString();
                            }

                            if (examItemCode.Contains("3080200"))
                            {
                                vo.B3080200 = dr2["examResult"].ToString();
                                vo.B3080200_unit = dr2["examItemUnitCode"].ToString();
                                vo.B3080200_min = dr2["referenceRangeMin"].ToString();
                                vo.B3080200_max = dr2["referenceRangeMax"].ToString();
                            }

                            if (examItemCode.Contains("4040900"))
                            {
                                vo.B4040900 = dr2["examResult"].ToString();
                                vo.B4040900_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4040900_min = dr2["referenceRangeMin"].ToString();
                                vo.B4040900_max = dr2["referenceRangeMax"].ToString();
                            }

                            if (examItemCode.Contains("4021100"))
                            {
                                vo.B4021100 = dr2["examResult"].ToString();
                                vo.B4021100_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4021100_min = dr2["referenceRangeMin"].ToString();
                                vo.B4021100_max = dr2["referenceRangeMax"].ToString();
                            }
                            if (examItemCode.Contains("4020300"))
                            {
                                vo.B4020300 = dr2["examResult"].ToString();
                                vo.B4020300_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4020300_min = dr2["referenceRangeMin"].ToString();
                                vo.B4020300_max = dr2["referenceRangeMax"].ToString();
                            }
                            if (examItemCode.Contains("B4020500"))
                            {
                                vo.B4020500 = dr2["examResult"].ToString();
                                vo.B4020500_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4020500_min = dr2["referenceRangeMin"].ToString();
                                vo.B4020500_max = dr2["referenceRangeMax"].ToString();
                            }
                            if (examItemCode.Contains("3080200"))
                            {
                                vo.B3080200 = dr2["examResult"].ToString();
                                vo.B3080200_unit = dr2["examItemUnitCode"].ToString();
                                vo.B3080200_min = dr2["referenceRangeMin"].ToString();
                                vo.B3080200_max = dr2["referenceRangeMax"].ToString();
                            }
                            if (examItemCode.Contains("4040900"))   
                            {
                                vo.B4040900 = dr2["examResult"].ToString();
                                vo.B4040900_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4040900_min = dr2["referenceRangeMin"].ToString();
                                vo.B4040900_max = dr2["referenceRangeMax"].ToString();
                            }
                            if (examItemCode.Contains("4021100"))
                            {
                                vo.B4021100 = dr2["examResult"].ToString();
                                vo.B4021100_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4021100_min = dr2["referenceRangeMin"].ToString();
                                vo.B4021100_max = dr2["referenceRangeMax"].ToString();
                            }
                            
                            if (examItemCode.Contains("4020300"))
                            {
                                vo.B4020300 = dr2["examResult"].ToString();
                                vo.B4020300_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4020300_min = dr2["referenceRangeMin"].ToString();
                                vo.B4020300_max = dr2["referenceRangeMax"].ToString();
                            }
                            
                            if (examItemCode.Contains("4020500"))
                            {
                                vo.B4020500 = dr2["examResult"].ToString();
                                vo.B4020500_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4020500_min = dr2["referenceRangeMin"].ToString();
                                vo.B4020500_max = dr2["referenceRangeMax"].ToString();
                            }
                            
                            if (examItemCode.Contains("4020600"))
                            {
                                vo.B4020600 = dr2["examResult"].ToString();
                                vo.B4020600_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4020600_min = dr2["referenceRangeMin"].ToString();
                                vo.B4020600_max = dr2["referenceRangeMax"].ToString();
                            }
                            
                            if (examItemCode.Contains("4012600"))
                            {
                                vo.B4012600 = dr2["examResult"].ToString();
                                vo.B4012600_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4012600_min = dr2["referenceRangeMin"].ToString();
                                vo.B4012600_max = dr2["referenceRangeMax"].ToString();
                            }
                           
                            if (examItemCode.Contains("4012000"))
                            {
                                vo.B4012000 = dr2["examResult"].ToString();
                                vo.B4012000_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4012000_min = dr2["referenceRangeMin"].ToString();
                                vo.B4012000_max = dr2["referenceRangeMax"].ToString();
                            }
                           
                            if (examItemCode.Contains("4011600"))
                            {
                                vo.B4011600 = dr2["examResult"].ToString();
                                vo.B4011600_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4011600_min = dr2["referenceRangeMin"].ToString();
                                vo.B4011600_max = dr2["referenceRangeMax"].ToString();
                            }
                           
                            if (examItemCode.Contains("4010100"))
                            {
                                vo.B4010100 = dr2["examResult"].ToString();
                                vo.B4010100_unit = dr2["examItemUnitCode"].ToString();
                                vo.B4010100_min = dr2["referenceRangeMin"].ToString();
                                vo.B4010100_max = dr2["referenceRangeMax"].ToString();
                            }
                            
                            if (examItemCode.Contains("2010502"))
                            {
                                vo.B2010502 = dr2["examResult"].ToString();
                                vo.B2010502_unit = dr2["examItemUnitCode"].ToString();
                                vo.B2010502_min = dr2["referenceRangeMin"].ToString();
                                vo.B2010502_max = dr2["referenceRangeMax"].ToString();
                            }
                            
                            if (examItemCode.Contains("2010501"))
                            {
                                vo.B2010501 = dr2["examResult"].ToString();
                                vo.B2010501_unit = dr2["examItemUnitCode"].ToString();
                                vo.B2010501_min = dr2["referenceRangeMin"].ToString();
                                vo.B2010501_max = dr2["referenceRangeMax"].ToString();
                            }
                            
                        }
                    }

                    data.Add(vo);
                }
            }

            this.gcNewZyb.DataSource = data;
            this.gcNewZyb.RefreshDataSource();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            uiHelper.ExportToXls(this.gvNewZyb);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            SetPrintName("普通");
        }


        Dictionary<string, string> ReadXmlNodes(string xml, string nodeName)
        {
            XmlDocument document = new XmlDocument();
            document.Load(xml);
            XmlElement element = document[nodeName];
            document = null;

            if (element == null) return null;
            Dictionary<string, string> dicVal = new Dictionary<string, string>();
            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.Name == "#comment") continue;
                if (!dicVal.ContainsKey(node.Name))
                {
                    dicVal.Add(node.Name.ToUpper(), node.InnerText);
                }
            }
            return dicVal;
        }


        void SetPrintName(string recipeTypeName)
        {
            if (string.IsNullOrEmpty(recipeTypeName)) return;
            string file = Application.StartupPath + "\\RecipePrinter.xml";
            if (File.Exists(file))
            {
                // 普通  儿科  易制毒  麻醉  精神一类  
                Dictionary<string, string> dicPrinter = this.ReadXmlNodes(file, "printer");
                if (dicPrinter.ContainsKey(recipeTypeName) && dicPrinter[recipeTypeName].Trim() != string.Empty)
                {
                    MessageBox.Show("!!!!!!!!!!!!!!!!!!!!!!!");
                    
                }
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
