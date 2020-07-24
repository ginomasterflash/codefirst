using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


static class ExtensionMethod
{
    public static string CapitalizeFirstLetter(this String input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
    }
    public static string Capitalize(this String input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        string[] result = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result[i]);
        }
         
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Join(" ", result));
    }
}

