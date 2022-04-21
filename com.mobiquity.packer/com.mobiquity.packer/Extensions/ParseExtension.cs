using System.Globalization;

namespace com.mobiquity.packer.Extensions;

public static class ParseExtension
{
    // round to integer, works only with two decimal
    public static int ToInt(this string number)
    {
        return (int)(decimal.Parse(number, new NumberFormatInfo {NumberDecimalSeparator = "."}) * 100);
    }
}