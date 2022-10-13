using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiPintar.Utilities.Services.Filter
{
    public interface IFilterService
    {
        public Task<T> GetFilterDataAsync<T>(string name = null, Dictionary<string, dynamic> additionalParam = null);
    }
}
