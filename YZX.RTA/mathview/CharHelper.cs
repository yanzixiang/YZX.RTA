namespace mathview
{
  using System;

  /// <summary>
  /// A class for character manipulation.
  /// </summary>
  public static class CharHelper
  {
    /// <summary>
    /// Converts a 32bit char to a string using surrogate pairs.
    /// </summary>
    /// <param name="c">The character.</param>
    /// <returns>The resulting string.</returns>
    public static string ConvertFromUtf32(this uint c)
    {
      if (c < 0x10000)
        return "" + c;

      c -= 0x10000;

      return "" + (char)((c >> 10) + 0xD800) + (char)(c % 0x0400 + 0xDC00);
    }
  }
}
