using Learning.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class SmsService : ISmsService
    {


        public async Task<bool> SendUserPasswordSms(string mobile, string password)
        {
            //try
            //{
            //    Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);
            //    api.VerifyLookup(mobile, password, "MoneyMagnetForgetPassword");
            //    return true;
            //}
            //catch
            //{
            //}
                return false;
        }

        public async Task<bool> SendVerificationSms(string mobile, string activationCode)
        {
            //try
            //{
            //    Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);
            //    api.VerifyLookup(mobile, activationCode, "MoneyMagnetRegister");
            //    return true;
            //}
            //catch
            //{
            //}
                return false;
        }
    }
}
