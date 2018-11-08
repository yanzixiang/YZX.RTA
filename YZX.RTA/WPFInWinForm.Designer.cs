using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace YZX.WINCC.Controls
{
  partial class WPFInWinForm
  {
    /// <summary> 
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      //base.Dispose(disposing);
    }

    #region 组件设计器生成的代码

    /// <summary> 
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    protected void InitializeComponent()
    {
      this.WPF = new ElementHost();
      this.info = new TextBox();
      this.SuspendLayout();
      // 
      // WPF
      // 
      this.WPF.Dock = DockStyle.Fill;
      this.WPF.Location = new System.Drawing.Point(0, 0);
      this.WPF.Name = "WPF";
      this.WPF.Size = new System.Drawing.Size(150, 150);
      this.WPF.TabIndex = 0;
      this.WPF.Child = null;
      // 
      // info
      // 
      this.info.Location = new System.Drawing.Point(0, 0);
      this.info.Name = "info";
      this.info.Size = new System.Drawing.Size(150, 21);
      this.info.TabIndex = 1;
      
      // 
      // WPFInWinForm
      // 
      this.Controls.Add(this.info);
      this.Controls.Add(this.WPF);
      this.WPF.BringToFront();
      this.Name = "WPFInWinForm";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    protected ElementHost WPF;
    protected TextBox info;
  }
}
