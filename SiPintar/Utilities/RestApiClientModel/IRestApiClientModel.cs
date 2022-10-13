using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiPintar.Utilities
{
    public interface IRestApiClientModel
    {
        public string GetApiVersion();
        public string GetUrlCloud(bool isReport = false);
        public string GetUrlLocal(bool isReport = false);
        public void SetupConfig();
        public Task<RestApiResponse> GetAsync(string targetUrl, Dictionary<string, dynamic> uriParameter = null, bool ignoreToken = false, bool useTimeOut = true, TimeSpan? timeOut = null, bool isReport = false);
        public Task<RestApiResponse> PostAsync(string targetUrl, Dictionary<string, dynamic> body, bool ignoreToken = false, bool useTimeOut = true, TimeSpan? timeOut = null, bool isReport = false);
        public Task<RestApiResponse> PatchAsync(string targetUrl, string entityId, Dictionary<string, dynamic> body, bool ignoreToken = false, bool useTimeOut = true, TimeSpan? timeOut = null, bool isReport = false);
        public Task<RestApiResponse> DeleteAsync(string targetUrl, Dictionary<string, dynamic> body, bool ignoreToken = false, bool useTimeOut = true, TimeSpan? timeOut = null, bool isReport = false);
        public Task<RestApiResponse> UploadAsync(string targetUrl, Dictionary<string, dynamic> body, bool ignoreToken = false, bool useTimeOut = true, TimeSpan? timeOut = null, bool isReport = false);
        public void SetToken(string token);
    }
}
