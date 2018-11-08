namespace YZX.WINCC.Controls
{
  partial class BatteryView
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
      this.BatteryProgress = new System.Windows.Forms.ProgressBar();
      this.BatteryLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // BatteryProgress
      // 
      this.BatteryProgress.Location = new System.Drawing.Point(0, 0);
      this.BatteryProgress.Name = "BatteryProgress";
      this.BatteryProgress.Size = new System.Drawing.Size(150, 150);
      this.BatteryProgress.TabIndex = 0;
      // 
      // BatteryLabel
      // 
      this.BatteryLabel.AutoSize = true;
      this.BatteryLabel.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.BatteryLabel.Location = new System.Drawing.Point(9, 48);
      this.BatteryLabel.Name = "BatteryLabel";
      this.BatteryLabel.Size = new System.Drawing.Size(219, 83);
      this.BatteryLabel.TabIndex = 1;
      this.BatteryLabel.Text = "label1";
      // 
      // BatteryView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.BatteryLabel);
      this.Controls.Add(this.BatteryProgress);
      this.Name = "BatteryView";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar BatteryProgress;
    private System.Windows.Forms.Label BatteryLabel;
  }
}
