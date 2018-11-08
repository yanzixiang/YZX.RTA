namespace YZX.WINCC.Controls
{

  partial class WlanSignalQuality
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
      base.Dispose(disposing);
    }

    #region 组件设计器生成的代码

    /// <summary> 
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
      this.SignalProgress = new System.Windows.Forms.ProgressBar();
      this.SignalLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // SignalProgress
      // 
      this.SignalProgress.Location = new System.Drawing.Point(0, 0);
      this.SignalProgress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.SignalProgress.Name = "SignalProgress";
      this.SignalProgress.Size = new System.Drawing.Size(200, 188);
      this.SignalProgress.TabIndex = 0;
      // 
      // SignalLabel
      // 
      this.SignalLabel.AutoSize = true;
      this.SignalLabel.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.SignalLabel.Location = new System.Drawing.Point(12, 60);
      this.SignalLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.SignalLabel.Name = "SignalLabel";
      this.SignalLabel.Size = new System.Drawing.Size(219, 83);
      this.SignalLabel.TabIndex = 1;
      this.SignalLabel.Text = "label1";
      // 
      // WlanSignalQuality
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.SignalLabel);
      this.Controls.Add(this.SignalProgress);
      this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.Name = "WlanSignalQuality";
      this.Size = new System.Drawing.Size(200, 188);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar SignalProgress;
    private System.Windows.Forms.Label SignalLabel;

  }
}
