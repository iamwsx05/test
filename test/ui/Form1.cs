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
using weCare.Core.Utils;

namespace test
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");
        }

        //48项编码
        List<string> bm48List = new List<string> {
"019900170",
"019900981",
"010200076",
"019900408",
"019900409",
"019900664",
"019900349",
"019900717",
"019900983",
"019900984",
"019900985",
"019900996",
"019900982",
"019901011",
"011000006",
"019900326",
"019900586",
"019900416",
"019900809",
"019900758",
"011000005",
"019900255",
"019900117",
"019900319",
"011000001",
"019900929",
"019900846",
"019900685",
"019900344",
"019900802",
"019900771",
"019900125",
"019900392",
"019900393",
"019900391",
"019900358",
"019900384",
"019900179",
"019900009",
"019900010",
"010900020",
"019900464",
"019900047",
"019900402",
"019900401",
"019900184",
"010900152",
"010900151",
"019900769",
"019900770"};

        //48项编码
        List<string> bm48List2 = new List<string> {
"19900170",
"19900981",
"10200076",
"19900408",
"19900409",
"19900664",
"19900349",
"19900717",
"19900983",
"19900984",
"19900985",
"19900996",
"19900982",
"19901011",
"11000006",
"19900326",
"19900586",
"19900416",
"19900809",
"19900758",
"11000005",
"19900255",
"19900117",
"19900319",
"11000001",
"19900929",
"19900846",
"19900685",
"19900344",
"19900802",
"19900771",
"19900125",
"19900392",
"19900393",
"19900391",
"19900358",
"19900384",
"19900179",
"19900009",
"19900010",
"10900020",
"19900464",
"19900047",
"19900402",
"19900401",
"19900184",
"10900152",
"10900151",
"19900769",
"19900770"};

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

        internal void stat1()
        {
            List<EntityVo> data = new List<EntityVo>();
            string PathStr = Application.StartupPath + "\\院本部手术室.xls";
            string PathStr2 = Application.StartupPath + "\\院本部手术室2.xls";
            string PathStr3 = Application.StartupPath + "\\院本部手术室3.xls";
            if (!File.Exists(PathStr))
            {
                return;
            }


            DataTable dt1 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr, 0);

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m1 += Function.Dec(dr["数量"].ToString().Trim());

                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;

                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();

                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.m1 = Function.Dec(dr["数量"].ToString().Trim());
                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }

            DataTable dt2 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr, 1);

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m2 += Function.Int(dr["数量"].ToString().Trim());

                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;

                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();

                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.m2 = Function.Int(dr["数量"].ToString().Trim());
                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }

            DataTable dt3 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr, 2);

            if (dt3 != null && dt3.Rows.Count > 0)
            {
                foreach (DataRow dr in dt3.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m3 += Function.Int(dr["数量"].ToString().Trim());
                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;
                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();

                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.m3 = Function.Int(dr["数量"].ToString().Trim());
                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }

            DataTable dt4 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr, 3);

            if (dt4 != null && dt4.Rows.Count > 0)
            {
                foreach (DataRow dr in dt4.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m4 += Function.Int(dr["数量"].ToString().Trim());
                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;
                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();
                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.m4 = Function.Int(dr["数量"].ToString().Trim());
                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }

            DataTable dt5 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr, 4);

            if (dt5 != null && dt5.Rows.Count > 0)
            {
                foreach (DataRow dr in dt5.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m5 += Function.Int(dr["数量"].ToString().Trim());
                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;
                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();
                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.m5 = Function.Int(dr["数量"].ToString().Trim());
                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }

            DataTable dt6 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr2, 0);

            if (dt6 != null && dt6.Rows.Count > 0)
            {
                foreach (DataRow dr in dt6.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m6 += Function.Int(dr["数量"].ToString().Trim());
                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;
                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();
                        vo.m6 = Function.Int(dr["数量"].ToString().Trim());
                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }

            DataTable dt7 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr2, 1);

            if (dt7 != null && dt7.Rows.Count > 0)
            {
                foreach (DataRow dr in dt7.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m7 += Function.Int(dr["数量"].ToString().Trim());
                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;
                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();
                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.m7 = Function.Int(dr["数量"].ToString().Trim());
                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }

            DataTable dt8 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr2, 2);

            if (dt8 != null && dt8.Rows.Count > 0)
            {
                foreach (DataRow dr in dt8.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m8 += Function.Int(dr["数量"].ToString().Trim());
                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;
                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();
                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.m8 = Function.Int(dr["数量"].ToString().Trim());
                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }

            DataTable dt9 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr2, 3);

            if (dt9 != null && dt9.Rows.Count > 0)
            {
                foreach (DataRow dr in dt9.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m9 += Function.Int(dr["数量"].ToString().Trim());
                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;
                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();
                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.m9 = Function.Int(dr["数量"].ToString().Trim());
                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }

            DataTable dt10 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr2, 4);

            if (dt10 != null && dt10.Rows.Count > 0)
            {
                foreach (DataRow dr in dt10.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m10 += Function.Int(dr["数量"].ToString().Trim());
                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;
                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();
                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.m10 = Function.Int(dr["数量"].ToString().Trim());
                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }

            DataTable dt11 = new ExcelHelper().ExcelToDataTableFormPath(true, PathStr3, 0);

            if (dt11 != null && dt11.Rows.Count > 0)
            {
                foreach (DataRow dr in dt11.Rows)
                {
                    string clbm = dr["材料编码"].ToString();

                    if (data.Any(t => t.clbm == clbm))
                    {
                        EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                        voClone.m11 += Function.Int(dr["数量"].ToString().Trim());
                        voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                        voClone.je = Function.Dec(voClone.dj) * voClone.hj;
                    }
                    else
                    {
                        EntityVo vo = new EntityVo();
                        vo.clbm = clbm;
                        vo.clmc = dr["材料名称"].ToString();
                        vo.ggxh = dr["规格型号"].ToString();
                        vo.lyks = dr["领料科室"].ToString();
                        vo.dj = dr["单价"].ToString();
                        vo.dw = dr["单位"].ToString();
                        vo.sccj = dr["生产厂家"].ToString();
                        vo.m11 = Function.Int(dr["数量"].ToString().Trim());

                        vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                        vo.je = Function.Dec(vo.dj) * vo.hj;
                        data.Add(vo);
                    }
                }
            }


            this.gcData.DataSource = data;
        }

        internal void stat2()
        {
            List<EntityVo> data = new List<EntityVo>();
            if (!File.Exists(filePath))
            {
                MessageBox.Show("请选择文件！");
                return;
            }

            try
            {
                DataTable dt1 = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);


                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        string clbm = dr["材料编码"].ToString();
                        int m1 = Function.Int(dr["1月"]);
                        int m2 = Function.Int(dr["2月"]);
                        int m3 = Function.Int(dr["3月"]);
                        int m4 = Function.Int(dr["4月"]);
                        int m5 = Function.Int(dr["5月"]);
                        int m6 = Function.Int(dr["6月"]);
                        int m7 = Function.Int(dr["7月"]);
                        int m8 = Function.Int(dr["8月"]);
                        int m9 = Function.Int(dr["9月"]);
                        int m10 = Function.Int(dr["10月"]);
                        int m11 = Function.Int(dr["11月"]);
                        int m12 = Function.Int(dr["12月"]);

                        if (data.Any(t => t.clbm == clbm))
                        {
                            EntityVo voClone = data.FirstOrDefault(t => t.clbm == clbm);

                            voClone.m1 += m1;
                            voClone.m2 += m2;
                            voClone.m3 += m3;
                            voClone.m4 += m4;
                            voClone.m5 += m5;
                            voClone.m6 += m6;
                            voClone.m7 += m7;
                            voClone.m8 += m8;
                            voClone.m9 += m9;
                            voClone.m10 += m10;
                            voClone.m11 += m11;
                            voClone.m12 += m12;

                            voClone.hj = voClone.m1 + voClone.m2 + voClone.m3 + voClone.m4 + voClone.m5 + voClone.m6 + voClone.m7 + voClone.m8 + voClone.m9 + voClone.m10 + voClone.m11 + voClone.m12;
                            voClone.je = Function.Dec(voClone.dj) * voClone.hj;

                        }
                        else
                        {
                            EntityVo vo = new EntityVo();
                            vo.clbm = clbm;
                            vo.clmc = dr["材料名称"].ToString();
                            vo.ggxh = dr["规格型号"].ToString();
                            vo.lyks = dr["领用科室"].ToString();
                            vo.dj = dr["单价"].ToString();

                            vo.dw = dr["单位"].ToString();
                            vo.sccj = dr["生产厂家"].ToString();
                            vo.m1 = m1;
                            vo.m2 = m2;
                            vo.m3 = m3;
                            vo.m4 = m4;
                            vo.m5 = m5;
                            vo.m6 = m6;
                            vo.m7 = m7;
                            vo.m8 = m8;
                            vo.m9 = m9;
                            vo.m10 = m10;
                            vo.m11 = m11;
                            vo.m12 = m12;
                            vo.hj = vo.m1 + vo.m2 + vo.m3 + vo.m4 + vo.m5 + vo.m6 + vo.m7 + vo.m8 + vo.m9 + vo.m10 + vo.m11 + vo.m12;
                            vo.je = Function.Dec(vo.dj) * vo.hj;
                            data.Add(vo);
                        }
                    }
                }
                this.gcData.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        internal void statBm()
        {
            List<EntityBm> data = new List<EntityBm>();
            if (!File.Exists(filePath))
            {
                MessageBox.Show("请选择文件！");
                return;
            }

            try
            {
                DataTable dt1 = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        string bz = string.Empty;
                        if (dt1.Columns.Contains("备注"))
                            bz = dr["备注"].ToString();
                        string gys = string.Empty;
                        if (dt1.Columns.Contains("供应商"))
                            gys = dr["供应商"].ToString();

                        string bm = dr["编码"].ToString();
                        string ks = dr["科室"].ToString();
                        //if (string.IsNullOrEmpty(ks))
                        //    ks = "无";

                        decimal hjTmp = 0;
                        if (dt1.Columns.Contains("合计"))
                        {
                            hjTmp = Function.Dec(dr["合计"]);
                        }

                        decimal sl = Function.Dec(dr["数量"]);

                        if (data.Any(t => t.bm == bm))
                        {
                            EntityBm voClone = data.FirstOrDefault(t => t.bm == bm);
                            voClone.sl += sl;
                            voClone.hj += sl;

                            if (!string.IsNullOrEmpty(bz))
                            {
                                voClone.bz += "、" + ks + ":" + bz;
                                voClone.bz = voClone.bz.TrimStart('、');
                            }
                            if (bm48List.Contains(bm) || bm48List2.Contains(bm))
                                voClone.isKey = "是";

                            if (dt1.Columns.Contains("合计"))
                            {
                                voClone.hj = hjTmp;
                                continue;
                            }

                            List<string> ksList = dicBmks[bm];

                            if (!ksList.Contains(ks))
                            {
                                dicBmks[bm].Add(ks);
                                DataRow[] drr = dt1.Select("编码 = '" + bm + "' and 科室= '" + ks + "'");
                                decimal count = 0;
                                if (drr != null && drr.Length > 0)
                                {
                                    for (int i = 0; i < drr.Length; i++)
                                    {
                                        count += Function.Dec(drr[i]["数量"]);
                                    }
                                }
                                voClone.ks += "、" + ks + "(" + count.ToString() + ")";
                            }
                        }
                        else
                        {
                            EntityBm vo = new EntityBm();
                            vo.ks = ks;
                            vo.bm = bm;
                            if (bm48List.Contains(bm) || bm48List2.Contains(bm))
                                vo.isKey = "是";

                            vo.mc = dr["名称"].ToString();
                            vo.gg = dr["规格"].ToString();
                            vo.dw = dr["单位"].ToString();
                            vo.gys = gys;
                            vo.cj = dr["厂家"].ToString();
                            vo.ckj = dr["参考价"].ToString();
                            if (!string.IsNullOrEmpty(bz))
                            {
                                vo.bz = ks + ":" + bz;
                            }
                            vo.sl = Function.Dec(dr["数量"]);
                            vo.hj = vo.sl;

                            DataRow[] drr = dt1.Select("编码 = '" + bm + "' and 科室= '" + ks + "'");
                            decimal count = 0;
                            if (dt1.Columns.Contains("合计"))
                            {
                                vo.hj = hjTmp;
                                data.Add(vo);
                                continue;
                            }
                            if (drr != null && drr.Length > 0)
                            {
                                for (int i = 0; i < drr.Length; i++)
                                {
                                    count += Function.Dec(drr[i]["数量"]);
                                }
                            }
                            vo.ks += "(" + count.ToString() + ")";
                            List<string> ksList = new List<string>();
                            ksList.Add(ks);
                            if (dicBmks.ContainsKey(bm))
                                continue;
                            dicBmks.Add(bm, ksList);
                            data.Add(vo);
                        }
                    }
                }
                this.gcBmData.DataSource = data.OrderByDescending(t => t.bm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        internal void statSs()
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("请选择文件！");
                return;
            }

            try
            {
                List<EntitySs> data = new List<EntitySs>();
                DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string ks = dr["科室"].ToString();
                        string clmc = dr["材料名称"].ToString();
                        string ggxh = dr["规格型号"].ToString();
                        string jg = dr["价格"].ToString();
                        string cpph = dr["产品批号"].ToString();
                        string sccj = dr["生产厂家"].ToString();
                        string bz = dr["备注"].ToString().Split(' ')[0];
                        string result = System.Text.RegularExpressions.Regex.Replace(bz, @"[^0-9]+", "");
                        decimal sl = Function.Dec(result);
                        if (data.Any(t => t.ggxh == ggxh))
                        {
                            EntitySs voClone = data.FirstOrDefault(t => t.ggxh == ggxh );
                            voClone.zs += sl;

                            List<string> ksList = dicBmks[ggxh];
                            if (ggxh == "72200752 3.5mm")
                            {
                                sl = 0;
                            }
                            if (!ksList.Contains(ks))
                            //if(!voClone.ks.Contains(ks))
                            {
                                dicBmks[ggxh].Add(ks);
                                DataRow[] drr = dt.Select("规格型号 = '" + ggxh + "' and 科室= '" + ks + "'");
                                decimal count = 0;
                                if (drr != null && drr.Length > 0)
                                {
                                    for (int i = 0; i < drr.Length; i++)
                                    {
                                        string drBz = drr[i]["备注"].ToString().Split(' ')[0];
                                        string drResult = System.Text.RegularExpressions.Regex.Replace(drBz, @"[^0-9]+", "");
                                        decimal drSl = Function.Dec(drResult);

                                        count += drSl;
                                    }
                                }
                                voClone.ks += "、" + ks + "(" + count.ToString() + ")";
                            }
                        }
                        else
                        {
                            EntitySs vo = new EntitySs();
                            vo.ks = dr["科室"].ToString();
                            vo.clmc = dr["材料名称"].ToString();
                            vo.ggxh = dr["规格型号"].ToString();
                            vo.jg = dr["价格"].ToString();
                            vo.cpph = dr["产品批号"].ToString();
                            vo.sccj = dr["生产厂家"].ToString();
                            vo.zczh = dr["注册证号"].ToString();

                            vo.zs = sl;

                            DataRow[] drr = dt.Select("规格型号 = '" + ggxh + "' and 科室= '" + ks + "'");
                            decimal count = 0;
                            if (drr != null && drr.Length > 0)
                            {
                                for (int i = 0; i < drr.Length; i++)
                                {
                                    string drBz = drr[i]["备注"].ToString().Split(' ')[0];
                                    string drResult = System.Text.RegularExpressions.Regex.Replace(drBz, @"[^0-9]+", "");
                                    decimal drSl = Function.Dec(drResult);

                                    count += drSl;
                                }
                            }
                            vo.ks += "(" + count.ToString() + ")";
                            List<string> ksList = new List<string>();
                            ksList.Add(ks);
                            if (dicBmks.ContainsKey(ggxh))
                                continue;
                            dicBmks.Add(ggxh, ksList);

                            data.Add(vo);
                        }
                    }
                }
                this.gcSs.DataSource = data;
                this.gcSs.RefreshDataSource();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                stat2();
            }
            else if (tabPane1.SelectedPageIndex == 1)
            {
                statBm();
            }
            else if(tabPane1.SelectedPageIndex == 2)
            {
                statSs();
            }
        }

        private void iExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 0)
            {
                uiHelper.ExportToXls(this.gvData);
            }
            else if (tabPane1.SelectedPageIndex == 1)
            {
                uiHelper.ExportToXls(this.gvBmData);
            }
            else if(tabPane1.SelectedPageIndex == 2)
            {
                uiHelper.ExportToXls(this.gvSs);
            }
        }


        #region test
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<EntityBm> data = new List<EntityBm>();
            if (!File.Exists(filePath))
            {
                MessageBox.Show("请选择文件！");
                return;
            }
            List<string> lstOp = new List<string>();
            DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);

            string sql = @"select * from t_dic_opicd";

            SqlHelper svc = new SqlHelper(EnumBiz.emrDB);
            DataTable dtOp = svc.GetDataTable(sql);
            if (dtOp != null && dtOp.Rows.Count > 0)
            {
                foreach (DataRow dr in dtOp.Rows)
                {
                    string icdcode_vchr = dr["icdcode_vchr"].ToString().Trim();
                    lstOp.Add(icdcode_vchr);
                }
            }

            sql = "insert into t_dic_opicd (icdcode_vchr,icdname_vchr,level_int,icdpycode_vchr) values(?,?,?,?) ";

            string sqlUpdate = @"update t_dic_opicd set icdname_vchr = ?,level_int = ? where icdcode_vchr = ?";
            IDataParameter[] parm = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string fopcode = dr["fopcode"].ToString().Trim();
                    string fopname = dr["fopname"].ToString();
                    string fopjb = dr["fopjb"].ToString();
                    int fopjbInt = 0;
                    if (fopjb.Trim() == "一级")
                        fopjbInt = 1;
                    if (fopjb.Trim() == "二级")
                        fopjbInt = 2;
                    if (fopjb.Trim() == "三级")
                        fopjbInt = 3;
                    if (fopjb.Trim() == "四级")
                        fopjbInt = 4;
                    string fzjc = dr["fzjc"].ToString().ToUpper();

                    if (lstOp.Contains(fopcode))
                    {
                        parm = svc.CreateParm(3);
                        parm[0].Value = fopname;
                        parm[1].Value = fopjbInt;
                        parm[2].Value = fopcode;
                        svc.ExecSql(sqlUpdate, parm);
                    }
                    else
                    {
                        parm = svc.CreateParm(4);
                        parm[0].Value = fopcode;
                        parm[1].Value = fopname;
                        parm[2].Value = fopjbInt;
                        parm[3].Value = fzjc;
                        svc.ExecSql(sql, parm);
                    }
                }
            }

            MessageBox.Show("success !");
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("请选择文件！");
                return;
            }
            List<entityIcd10> lstIcd = new List<entityIcd10>();
            DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);

            string sql = @"select * from t_dic_icd";

            SqlHelper svc = new SqlHelper(EnumBiz.emrDB);
            DataTable dtOp = svc.GetDataTable(sql);
            if (dtOp != null && dtOp.Rows.Count > 0)
            {
                foreach (DataRow dr in dtOp.Rows)
                {
                    entityIcd10 vo = new entityIcd10();
                    vo.icdCode = dr["icdcode_vchr"].ToString().Trim().ToUpper();
                    vo.icdName = dr["icdcnname_vchr"].ToString().Trim();
                    lstIcd.Add(vo);
                }
            }

            sql = "insert into t_dic_icd (icdcode_vchr,icdcnname_vchr,icdpycode_vchr) values(?,?,?) ";

            string sqlUpdate = @"update t_dic_icd set icdcnname_vchr = ?,icdpycode_vchr = ? where icdcode_vchr = ?";
            IDataParameter[] parm = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string icdCodeUP = dr["ficdm"].ToString().Trim().ToUpper();
                    string icdCode = dr["ficdm"].ToString();
                    string fopname = dr["fjbname"].ToString().Trim();
                    string fzjc = dr["fzjc"].ToString().ToUpper();

                    if (lstIcd.Exists(r => r.icdCode == icdCodeUP))
                    {
                        parm = svc.CreateParm(3);
                        parm[0].Value = fopname;
                        parm[1].Value = fzjc;
                        parm[2].Value = icdCode;
                        svc.ExecSql(sqlUpdate, parm);
                    }
                    else
                    {
                        parm = svc.CreateParm(3);
                        parm[0].Value = icdCode;
                        parm[1].Value = fopname;
                        parm[2].Value = fzjc;
                        svc.ExecSql(sql, parm);
                    }
                }
            }

            MessageBox.Show("success !");
        }



        private void simpleButton3_Click(object sender, EventArgs e)
        {
            List<EntityBm> data = new List<EntityBm>();
            if (!File.Exists(filePath))
            {
                MessageBox.Show("请选择文件！");
                return;
            }
            List<string> lstOp = new List<string>();
            DataTable dt = new ExcelHelper().ExcelToDataTableFormPath(true, filePath, 0);

            string sql = @"delete  from t_dic_operation";

            SqlHelper svc = new SqlHelper(EnumBiz.emrDB);
            svc.ExecSql(sql);

            sql = "insert into t_dic_operation (serno_int,opcode_vchr,opname_vchr,pycode_vchr,level_int,status_int) values(?,?,?,?,?,?) ";

            IDataParameter[] parm = null;
            int i = 1;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string fopcode = dr["fopcode"].ToString().Trim();
                    string fopname = dr["fopname"].ToString();
                    string fopjb = dr["fopjb"].ToString();
                    int fopjbInt = 0;
                    if (fopjb.Trim() == "一级")
                        fopjbInt = 1;
                    if (fopjb.Trim() == "二级")
                        fopjbInt = 2;
                    if (fopjb.Trim() == "三级")
                        fopjbInt = 3;
                    if (fopjb.Trim() == "四级")
                        fopjbInt = 4;
                    string fzjc = dr["fzjc"].ToString().ToUpper();

                    parm = svc.CreateParm(6);
                    parm[0].Value = i;
                    parm[1].Value = fopcode;
                    parm[2].Value = fopname;
                    parm[3].Value = fzjc;
                    parm[4].Value = fopjbInt;
                    parm[5].Value = 1;
                    svc.ExecSql(sql, parm);
                    i++;

                }
            }

            MessageBox.Show("success !");
        }

        #endregion
    }



    public class entityOP
    {
        public string icdcode_vchr { get; set; }
        public string icdname_vchr { get; set; }
    }


    public class entityIcd10
    {
        public string icdCode { get; set; }
        public string icdName { get; set; }
    }

    public class EntityVo
    {
        public string clbm { get; set; }
        public string clmc { get; set; }
        public string ggxh { get; set; }
        public string lyks { get; set; }
        public string dj { get; set; }
        public decimal je { get; set; }
        public string dw { get; set; }
        public string sccj { get; set; }
        public decimal m1 { get; set; }
        public decimal m2 { get; set; }
        public decimal m3 { get; set; }
        public decimal m4 { get; set; }
        public decimal m5 { get; set; }
        public decimal m6 { get; set; }
        public decimal m7 { get; set; }
        public decimal m8 { get; set; }
        public decimal m9 { get; set; }
        public decimal m10 { get; set; }
        public decimal m11 { get; set; }
        public decimal m12 { get; set; }
        public decimal hj { get; set; }
    }


    public class EntityBm
    {
        public string bm { get; set; }
        public string mc { get; set; }
        public string gg { get; set; }
        public string dw { get; set; }
        public string ckj { get; set; }
        public string gys { get; set; }
        public string cj { get; set; }
        public string bz { get; set; }
        public decimal sl { get; set; }
        public string ks { get; set; }
        public decimal hj { get; set; }
        public string isKey { get; set; }
    }


    public class EntitySs
    {
        public string ks { get; set; }
        public string clmc { get; set; }
        public string ggxh { get; set; }
        public string jg { get; set; }
        public string cpph { get; set; }
        public string sccj { get; set; }
        public string bz { get; set; }
        public decimal zs { get; set; }

        public string zczh { get; set; }
    }

    public class EntityKsSl
    {
        public string ks { get; set; }
        public string sl { get; set; }
    }

}
