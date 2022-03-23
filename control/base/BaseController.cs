using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace control
{
    public class BaseController : IDisposable
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public BaseController()
        { }
        #endregion


        /// <summary>
        /// ParentViewer
        /// </summary>
        public frmBase ParentViewer { get; set; }

        /// <summary>
        /// 权限对象
        /// </summary>
        protected System.Security.Principal.IPrincipal objPrincipal = null;

        #region 释放
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            objPrincipal = null;
            GC.SuppressFinalize(this);
        }
        #endregion

        #region 设置UI
        /// <summary>
        /// 设置UI
        /// </summary>
        /// <param name="child"></param>
        public virtual void SetUI(frmBase child)
        {
            ParentViewer = child;
        }
        #endregion
    }
}
