using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        =>
            name.Trim().Replace("\"", "")
                .Replace("!", "")
                .Replace("'", "")
                .Replace(" ", "-")
                .Replace(".", "-")
                .Replace(",", "")
                .Replace("?", "")
                .Replace("*", "")
                .Replace("\\", "")
                .Replace("+", "")
                .Replace("=", "")
                .Replace("&", "")
                .Replace("%", "")
                .Replace("^", "")
                .Replace("{", "")
                .Replace("}", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("@", "")
                .Replace("#", "")
                .Replace("$", "")
                .Replace("£", "")
                .Replace("€", "")
                .Replace(":", "")
                .Replace(";", "")
                .Replace("|", "")
                .Replace("¨", "")
                .Replace("~", "")
                .Replace("`", "")
                .Replace("½", "")
                .Replace("ı", "i")
                .Replace("ğ", "g")
                .Replace("Ğ", "G")
                .Replace("ü", "u")
                .Replace("Ü", "U")
                .Replace("ş", "s")
                .Replace("Ş", "S")
                .Replace("İ", "i")
                .Replace("ç", "c")
                .Replace("ö", "o")
                .Replace("Ç", "C")
                .Replace("Ö", "O")
                .Replace("æ", "a")
                .Replace("ß", "b");
    }
}
