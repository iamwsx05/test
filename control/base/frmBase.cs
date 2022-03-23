using control.entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace control
{
    public partial class frmBase : Form
    {
        public frmBase()
        {
            GlobalParm.IsPopupOpening = true;
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.ImeMode = System.Windows.Forms.ImeMode.OnHalf;
            this.AutoScaleMode = AutoScaleMode.None;
            if (!DesignMode) this.CreateController();
        }
        /// <summary>
        /// 窗体控制器
        /// </summary>
        protected BaseController Controller;

        /// <summary>
        /// 窗体打开方式
        /// </summary>
        public string FormOperName { get; set; }

        private void ResizeMar()
        {
           
        }

        /// <summary>
        /// 创建窗体控制器
        /// </summary>
        protected virtual void CreateController()
        { }

        private void frmBase_Activated(object sender, EventArgs e)
        {
            uiHelper.frmCurr = this;
        }

        private void frmBase_Load(object sender, EventArgs e)
        {

        }
    }
}
