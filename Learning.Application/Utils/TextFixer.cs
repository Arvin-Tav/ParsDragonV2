using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Learning.Application.Utils
{
    public static class TextFixer
    {

        public static string FixText(this string text) => text?.Trim().Replace("  ", " ");
        public static string FixTextBetween(this string text)
        {
            return string.Join(" ", text.Split(new char[0],
                StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => x.Trim()));
        }
        public static string FixEmail(string email) => email.Trim().ToLower().Replace(" ", "");

        public static string RemoveHtmlTagsExceptBreak(string text) => Regex.Replace(text, @"<(?!br[\x20/>])[^<>]+>", string.Empty);
        public static string ReplaceNewLineTextArea(string text) => text?.Replace(Environment.NewLine, "<br />");
        public static string ReplaceBrToNewLine(string text) => text?.Replace("<br />", Environment.NewLine);
        public static string GenerateUniqCodeString(string name)
        {
            return Guid.NewGuid().ToString("N") + name.Replace(" ", "-");
        }
        public static string GenerateUniqCodeString()
        {
            return Guid.NewGuid().ToString("N");
        }
        public static string GenerateUniqCodeNumber()
        {
            string randomNumber = new Random().Next(10000, 999999).ToString();
            return randomNumber;
        }
        public static string FixTextForUrl(this string text)
        {
            return text.Replace(" ", "-");
        }

        public static string ConvertBrToNewLine(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Replace("<br/>", Environment.NewLine);
        }

        public static string ConvertNewLineToBr(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Replace(Environment.NewLine, "<br/>");
        }

        public static string FixedEmail(this string email)
        {
            return email.Trim().ToLower();
        }

        public static string[] SplitTags(this string tags)
        {
            return tags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }


        public static string FixTitleForUrl(this string url)
        {
            return url.Replace(" ", "-").Replace("+", "").Replace("#", "");
        }

        public static string FixUrlToTitle(this string title)
        {
            return title.Replace("-", " ");
        }

        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string GetFirstSectionOfString(this string text, int length = 360)
        {
            if (text.Length >= length)
            {
                return text.Substring(0, length) + "...";
            }

            return text;
        }
    }
}
