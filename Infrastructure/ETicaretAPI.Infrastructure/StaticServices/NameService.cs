using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Infrastructure.StaticServices
{
    public static class NameService
    {
        public static string CharacterRegulatory(string name)
        =>
            name.Replace("\"", "")
                .Replace("!", "")
                .Replace("'", "")
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
                .Replace("ß", "b")
                .Replace("", "b");
    }
}
