using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

public class ComStream : Stream
{
  private IStream originalStream_;

  public IStream UnderlyingStream
  {
    get
    {
      return this.originalStream_;
    }
  }

  public override long Length
  {
    get
    {
      if (this.originalStream_ == null)
        throw new ObjectDisposedException("originalStream_");
      STATSTG pstatstg;
      this.originalStream_.Stat(out pstatstg, 1);
      return pstatstg.cbSize;
    }
  }

  public override long Position
  {
    get
    {
      return this.Seek(0L, SeekOrigin.Current);
    }
    set
    {
      this.Seek(value, SeekOrigin.Begin);
    }
  }

  public override bool CanRead
  {
    get
    {
      return true;
    }
  }

  public override bool CanWrite
  {
    get
    {
      return true;
    }
  }

  public override bool CanSeek
  {
    get
    {
      return true;
    }
  }

  public ComStream(IStream stream)
  {
    if (stream == null)
      throw new ArgumentNullException("stream");
    this.originalStream_ = stream;
  }

  ~ComStream()
  {
    this.Close();
  }

  public override unsafe int Read(byte[] buffer, int offset, int count)
  {
    if (this.originalStream_ == null)
      throw new ObjectDisposedException("originalStream_");
    if (offset != 0)
      throw new NotSupportedException("only 0 offset is supported");
    int num;
    IntPtr pcbRead = new IntPtr((void*) &num);
    this.originalStream_.Read(buffer, count, pcbRead);
    return num;
  }

  public override void Write(byte[] buffer, int offset, int count)
  {
    if (this.originalStream_ == null)
      throw new ObjectDisposedException("originalStream_");
    if (offset != 0)
      throw new NotSupportedException("only 0 offset is supported");
    this.originalStream_.Write(buffer, count, IntPtr.Zero);
  }

  public override unsafe long Seek(long offset, SeekOrigin origin)
  {
    if (this.originalStream_ == null)
      throw new ObjectDisposedException("originalStream_");
    long num = 0L;
    IntPtr plibNewPosition = new IntPtr((void*) &num);
    this.originalStream_.Seek(offset, (int) origin, plibNewPosition);
    return num;
  }

  public override void SetLength(long value)
  {
    if (this.originalStream_ == null)
      throw new ObjectDisposedException("originalStream_");
    this.originalStream_.SetSize(value);
  }

  public override void Close()
  {
    if (this.originalStream_ == null)
      return;
    this.originalStream_.Commit(0);
    this.originalStream_ = null;
    GC.SuppressFinalize((object) this);
  }

  public override void Flush()
  {
    this.originalStream_.Commit(0);
  }
}
