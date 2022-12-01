namespace SplitBackDotnet.Extentions;


public static class StringExtentions
{
  public static decimal ToDecimal(this string str)
  {
    bool successfullyParsed = decimal.TryParse(str, out decimal x);

    if (successfullyParsed)
    {
      return x;
    }
    else
    {
      throw new Exception();
    }
  }
}