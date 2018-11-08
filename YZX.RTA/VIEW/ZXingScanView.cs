using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

using ZXing;
using AForge.Video;
using AForge.Video.DirectShow;

#if TIA
using System.ComponentModel;
using Siemens.Simatic.Hmi.Utah.Common.Base;
#endif

using Extensions;
using Common;

namespace YZX.WINCC.Controls
{
  [ToolboxBitmap(typeof(ZXingScanView), "Toolbox.ZXingScanView.bmp")]
  public class ZXingScanView : CControl
  {
    private CameraDevices camDevices;
    #if RTA
    private Bitmap currentBitmapForDecoding;
    private Thread decodingThread;
    private Result currentResult;
    private Pen resultRectPen;
    #endif
    public ZXingScanView()
    {
      this.SuspendLayout();

      Controls.Add(bv);

      this.ResumeLayout();

#if RTA
      Load += ZXingScanView_Load;
      HandleDestroyed += ZXingScanView_HandleDestroyed;
#endif
    }


#if RTA
    private void ZXingScanView_Load(object sender, EventArgs e)
    {
      camDevices = new CameraDevices();

      decodingThread = new Thread(DecodeBarcode);
      decodingThread.Start();

      bv.Paint += pictureBox1_Paint;
      resultRectPen = new Pen(Color.Green, 10);

      camDevices.SelectCamera(0);
      camDevices.Current.NewFrame += Current_NewFrame;
      camDevices.Current.Start();
      CodeDecoded += ZXingScanView_CodeDecoded;
    }
    private void ZXingScanView_CodeDecoded(object sender, ZXingResultEventArgs e)
    {
      if (e.result != null)
      {
        TryWriteTag("ResultString", e.result.Text);
        TryWriteTag("ScanTime", e.result.Timestamp);
        TryWriteTag("BarcodeFormat", e.result.BarcodeFormat);
      }
    }

    private void ZXingScanView_HandleDestroyed(object sender, EventArgs e)
    {
      DestroyZXingScanView();
    }

    public void DestroyZXingScanView()
    {
      decodingThread.Abort();
      if (camDevices.Current != null)
      {
        camDevices.Current.NewFrame -= Current_NewFrame;
        if (camDevices.Current.IsRunning)
        {
          camDevices.Current.SignalToStop();
        }
      }
    }
#endif

    #region 属性
#if TIA
    [Category("YZX")]
    [DisplayName("编码")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public BarcodeFormat BarcodeFormat { set; get; }

#if TIA
    [Category("YZX")]
    [DisplayName("扫描结果")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public string ResultString{ get; set; }

#if TIA
    [Category("YZX")]
    [DisplayName("扫描时间")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public long ScanTime { get; set; }

#if TIA
    [Category("YZX")]
    [DisplayName("扫描时间")]
    [SupportedDynamicTypes(SupportedDynamicTypes.All)]
#endif
    public VideoCapabilities VideoResolution
    {
      get
      {
        return camDevices.Current.VideoResolution;
      }
      set
      {
        camDevices.Current.VideoResolution = value;
      }
    }


    #endregion 属性

    public PictureBox bv = new PictureBox();
    public override Control InnerControl
    {
      get
      {
        return bv;
      }
    }

#if RTA
    private void Current_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
      if (IsDisposed)
      {
        return;
      }

      try
      {
        if (currentBitmapForDecoding == null)
        {
          currentBitmapForDecoding = (Bitmap)eventArgs.Frame.Clone();
        }
        Invoke(new Action<Bitmap>(ShowFrame), eventArgs.Frame.Clone());
      }
      catch (ObjectDisposedException)
      {
        // not sure, why....
      }
    }

    private void ShowFrame(Bitmap frame)
    {
      Bitmap resized =  frame.Resize(Width, Height);
      bv.Image = resized;
      frame = null;
      resized = null;
    }

    private void DecodeBarcode()
    {
      var reader = new BarcodeReader();
      IList<BarcodeFormat> pf = new List<BarcodeFormat>()
      {
        BarcodeFormat.All_1D,
        BarcodeFormat.DATA_MATRIX,
        BarcodeFormat.QR_CODE,
        BarcodeFormat.PDF_417,
        BarcodeFormat.MAXICODE,
      };
      reader.Options.PossibleFormats = pf;
      while (true)
      {
        if (!IsHandleCreated)
        {
          return;
        }
        if (currentBitmapForDecoding != null)
        {
          var result = reader.Decode(currentBitmapForDecoding);
          if (result != null)
          {
            Invoke(new Action<Result>(ShowResult), result);
            ZXingResultEventArgs zrea = new ZXingResultEventArgs(result);
            CodeDecoded?.Invoke(this, zrea);
          }
          currentBitmapForDecoding.Dispose();
          currentBitmapForDecoding = null;
        }
        if (Thread.CurrentThread.IsAlive)
        {
          Thread.Sleep(200);
        }
        else
        {
          Thread.CurrentThread.Abort();
        }
      }
    }

    private void ShowResult(Result result)
    {
      currentResult = result;
      BarcodeFormat = result.BarcodeFormat;
      ResultString = result.Text;
    }

    void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
      if (currentResult == null)
        return;

      if (currentResult.ResultPoints != null && currentResult.ResultPoints.Length > 0)
      {
        var resultPoints = currentResult.ResultPoints;
        var rect = new Rectangle((int)resultPoints[0].X, (int)resultPoints[0].Y, 1, 1);
        foreach (var point in resultPoints)
        {
          if (point.X < rect.Left)
            rect = new Rectangle((int)point.X, rect.Y, rect.Width + rect.X - (int)point.X, rect.Height);
          if (point.X > rect.Right)
            rect = new Rectangle(rect.X, rect.Y, rect.Width + (int)point.X - rect.X, rect.Height);
          if (point.Y < rect.Top)
            rect = new Rectangle(rect.X, (int)point.Y, rect.Width, rect.Height + rect.Y - (int)point.Y);
          if (point.Y > rect.Bottom)
            rect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height + (int)point.Y - rect.Y);
        }
        using (var g = bv.CreateGraphics())
        {
          g.DrawRectangle(resultRectPen, rect);
        }
      }
    }


#region RX
    public override List<string> RXTags
    {
      get
      {
        return new List<string>()
        {
          "ResultString"
        };
      }
    }
#endregion
#endif


#region 事件

#if TIA
    [Description("扫描到条码")]
    [Browsable(true)]
    [ScriptEvent]
#endif
    public event ZXingResultEventHandler CodeDecoded;
    #endregion
  }

  public delegate void ZXingResultEventHandler(object sender, ZXingResultEventArgs e);

  public class ZXingResultEventArgs:EventArgs
  {
    public Result result { get; private set; }
    public DateTime ScanTime { get; private set; }

    public ZXingResultEventArgs(Result r)
    {
      result = r;
      ScanTime = DateTime.Now;
    }
  }
}
