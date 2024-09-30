using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Application.Generator
{
    public class NameGenerator
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        public static string GenerateUniqCodeNumber()
        {
            string randomNumber = new Random().Next(10000, 999999).ToString();
            return randomNumber;
        }
    }
}
