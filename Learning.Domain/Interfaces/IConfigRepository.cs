using Learning.Domain.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IConfigRepository
    {
        void UpdateConfig(Config config);
        Task<Config> GetConfigKey(string key);
        Task Save();
    }
}
