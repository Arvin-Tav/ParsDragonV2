using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface ISmsService
    {
        Task<bool> SendVerificationSms(string mobile, string activationCode);
        Task<bool> SendUserPasswordSms(string mobile, string password);
    }
}
