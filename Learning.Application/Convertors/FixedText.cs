using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Application.Convertors
{
    public static class FixedText
    {
        public static string FixedUserEmail(this string name)
        {
            return name.Trim().ToLower().Replace("<script>", "").Replace("</script>", "");
        }
        public static string FixedNameCourse(this string name)
        {
            return name.Trim().ToLower().Replace(" ", "-").Replace("/", "-");
        }
        public static string ToPrice(this string name,string price="#,0 تومان")
        {
            return name.ToString();
        }
    }
}
