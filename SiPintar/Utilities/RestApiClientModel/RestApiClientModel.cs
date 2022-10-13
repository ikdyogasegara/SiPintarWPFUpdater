using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using SiPintar.Commands.Authentication;
using SiPintar.Utilities.Services.Filter;

namespace SiPintar.Utilities
{
    [ExcludeFromCodeCoverage]
    public class RestApiClientModel : IRestApiClientModel
    {
        private readonly string _configPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\config.env";
        private string _baseUriCloud;
        private string _baseUriLocal;
        private string _baseUriCloudReport;
        private string _baseUriLocalReport;
        private readonly string _contentType = "application/json";
        private int _currentProcess;
        private readonly int _maxRetry = 3;
        private string _token;

        private static readonly string ApiVersion = Application.Current.Resources["api_version"] is null ? "v1" : Application.Current.Resources["api_version"].ToString();

        public RestApiClientModel()
        {
            SetupConfig();
        }

        public void SetupConfig()
        {
            DotNetEnv.Env.Load(_configPath);
            var baseUriCloud = DotNetEnv.Env.GetString("CLOUD_API_URL");
            var baseUriLocal = DotNetEnv.Env.GetString("LOCAL_API_URL");
            var baseUriCloudReport = DotNetEnv.Env.GetString("CLOUD_REPORT_API_URL");
            var baseUriLocalReport = DotNetEnv.Env.GetString("LOCAL_REPORT_API_URL");

            _baseUriCloud = baseUriCloud ?? "";
            if (_baseUriCloud[^1] == '/')
                _baseUriCloud = _baseUriCloud[0..^1];

            _baseUriLocal = baseUriLocal ?? "";
            if (_baseUriLocal[^1] == '/')
                _baseUriLocal = _baseUriLocal[0..^1];

            _baseUriCloudReport = baseUriCloudReport ?? "";
            if (_baseUriCloudReport[^1] == '/')
                _baseUriCloudReport = _baseUriCloudReport[0..^1];

            _baseUriLocalReport = baseUriLocalReport ?? "";
            if (_baseUriLocalReport[^1] == '/')
                _baseUriLocalReport = _baseUriLocalReport[0..^1];

        }

        // GET function
        public async Task<RestApiResponse> GetAsync(string targetUrl, Dictionary<string, dynamic> uriParameter = null, bool ignoreToken = false, bool useTimeOut = true, TimeSpan? timeOut = null, bool isReport = false)
        {
            // inject idPdam + idUser
            uriParameter = InjectRequiredParam(uriParameter);
            CheckForIgnoreToken(ignoreToken);
            var url = UrlBuilder(targetUrl, uriParameter, isReport);
            ConsolePayload("GET", url);
            var client = new HttpClient();
            SetTimeOut(client, useTimeOut, timeOut);

            try
            {
                var response = await client.GetAsync(url);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.RequestTimeout:
                        return new RestApiResponse(new RestApiError($"Error Request Timeout. Please try again.", ResponseErrorType.RequestTimeOut));
                    case System.Net.HttpStatusCode.TooManyRequests:
                        string messageTooManyRequests = null;
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                            messageTooManyRequests = res.GetValue("Ui_msg", StringComparison.OrdinalIgnoreCase)?.ToString();
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                        }
                        return new RestApiResponse(new RestApiError($"{messageTooManyRequests}", ResponseErrorType.TooManyRequests));
                    case System.Net.HttpStatusCode.OK:
                    case System.Net.HttpStatusCode.BadRequest:
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());

                            JArray data = null;
                            try
                            {
                                data = !string.IsNullOrEmpty(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString()) ? JArray.Parse(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString() ?? string.Empty) : null;
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    data = !string.IsNullOrEmpty(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString()) ? new JArray(JObject.Parse(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString())) : null;
                                }
                                catch (Exception)
                                {
                                    Debug.WriteLine("no data served");
                                }
                            }

                            return new RestApiResponse(new Response()
                            {
                                Status = res.GetValue("Status", StringComparison.OrdinalIgnoreCase) != null && (bool)res.GetValue("Status", StringComparison.OrdinalIgnoreCase),
                                System_msg = res.GetValue("System_msg", StringComparison.OrdinalIgnoreCase)?.ToString(),
                                Ui_msg = res.GetValue("Ui_msg", StringComparison.OrdinalIgnoreCase)?.ToString(),
                                TotalPage = res.GetValue("TotalPage", StringComparison.OrdinalIgnoreCase) != null ? (int)res.GetValue("TotalPage", StringComparison.OrdinalIgnoreCase) : 0,
                                Record = res.GetValue("TotalRecord", StringComparison.OrdinalIgnoreCase) != null ? (long)res.GetValue("TotalRecord", StringComparison.OrdinalIgnoreCase) : 0,
                                Data = data,
                                Execution_time = (long?)res.GetValue("Execution_time", StringComparison.OrdinalIgnoreCase)
                            });

                        }
                        catch (Exception internalError)
                        {
                            return new RestApiResponse(new RestApiError($"Internal Error {internalError.Message}", ResponseErrorType.UnknownError));
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        return new RestApiResponse(new RestApiError("EndPoint Not Found", ResponseErrorType.ApiNotFound));
                    case System.Net.HttpStatusCode.InternalServerError:
                        string messageInternalServerError = null;
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                            messageInternalServerError = "Server Bermasalah. Hubungi Vendor !";
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                        }
                        return new RestApiResponse(new RestApiError($"{messageInternalServerError}", ResponseErrorType.ServerError));
                    case System.Net.HttpStatusCode.Unauthorized:
                        if (_currentProcess < _maxRetry)
                        {
                            await RefreshTokenAsync();
                            return await GetAsync(targetUrl, uriParameter, ignoreToken);
                        }
                        else
                        {
                            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var value = (JObject)result.GetValue("Value", StringComparison.OrdinalIgnoreCase);
                            return new RestApiResponse(new RestApiError(value.GetValue("System_msg", StringComparison.OrdinalIgnoreCase).ToString(), ResponseErrorType.Unauthorized));
                        }
                    default:
                        string message = null;
                        try
                        {
                            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var value = (JObject)result.GetValue("Value", StringComparison.OrdinalIgnoreCase);
                            message = value.GetValue("System_msg", StringComparison.OrdinalIgnoreCase).ToString();
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                        }

                        return new RestApiResponse(new RestApiError($"Error {message}", ResponseErrorType.UnknownError));
                }
            }
            catch (HttpRequestException e)
            {
                var msg = e.InnerException ?? e;
                Debug.WriteLine(msg.Message);
                Log.Logger.Error(msg.Message);
                return new RestApiResponse(new RestApiError("Koneksi Error. Silahkan periksa API URL atau koneksi internet Anda.", ResponseErrorType.ConnectionError));
            }
            catch (Exception e)
            {
                var msg = e.InnerException ?? e;
                Debug.WriteLine(msg.Message);
                Log.Logger.Error(msg.Message);
                return new RestApiResponse(new RestApiError($"Error {e.Message}", ResponseErrorType.UnknownError));
            }
        }

        // POST function (Create/Post Data)
        public async Task<RestApiResponse> PostAsync(string targetUrl, Dictionary<string, dynamic> body, bool ignoreToken = false, bool useTimeOut = true, TimeSpan? timeOut = null, bool isReport = false)
        {
            // inject idPdam + idUser
            body = InjectRequiredParam(body);
            CheckForIgnoreToken(ignoreToken);
            var content = JsonConvert.SerializeObject(body);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
            var url = UrlBuilder(targetUrl, null, isReport);
            ConsolePayload("POST", url, content);
            var client = new HttpClient();
            SetTimeOut(client, useTimeOut, timeOut);

            try
            {
                var response = await client.PostAsync(url, byteContent);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.RequestTimeout:
                        return new RestApiResponse(new RestApiError($"Error Request Timeout. Please try again.", ResponseErrorType.RequestTimeOut));
                    case System.Net.HttpStatusCode.TooManyRequests:
                        string messageTooManyRequests = null;
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                            messageTooManyRequests = res.GetValue("Ui_msg", StringComparison.OrdinalIgnoreCase)?.ToString();
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }
                        return new RestApiResponse(new RestApiError($"{messageTooManyRequests}", ResponseErrorType.TooManyRequests));
                    case System.Net.HttpStatusCode.OK:
                    case System.Net.HttpStatusCode.Created:
                    case System.Net.HttpStatusCode.BadRequest:
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());

                            JArray data = null;
                            try
                            {
                                data = !string.IsNullOrEmpty(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString()) ? JArray.Parse(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString() ?? string.Empty) : null;
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    data = !string.IsNullOrEmpty(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString()) ? new JArray(JObject.Parse(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString() ?? string.Empty)) : null;
                                }
                                catch (Exception)
                                {
                                    Debug.WriteLine("no data served");
                                }
                            }

                            return new RestApiResponse(new Response()
                            {
                                Status = res.GetValue("Status", StringComparison.OrdinalIgnoreCase) != null && (bool)res.GetValue("Status", StringComparison.OrdinalIgnoreCase),
                                System_msg = res.GetValue("System_msg", StringComparison.OrdinalIgnoreCase)?.ToString(),
                                Ui_msg = res.GetValue("Ui_msg", StringComparison.OrdinalIgnoreCase)?.ToString(),
                                TotalPage = res.GetValue("TotalPage", StringComparison.OrdinalIgnoreCase) != null ? (int)res.GetValue("TotalPage", StringComparison.OrdinalIgnoreCase) : 0,
                                Record = res.GetValue("TotalRecord", StringComparison.OrdinalIgnoreCase) != null ? (long)res.GetValue("TotalRecord", StringComparison.OrdinalIgnoreCase) : 0,
                                Data = data,
                                Execution_time = (long?)res.GetValue("Execution_time", StringComparison.OrdinalIgnoreCase)
                            });
                        }
                        catch (Exception internalError)
                        {
                            return new RestApiResponse(new RestApiError($"Internal Error {internalError.Message}", ResponseErrorType.UnknownError));
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        return new RestApiResponse(new RestApiError("EndPoint Not Found", ResponseErrorType.ApiNotFound));
                    case System.Net.HttpStatusCode.InternalServerError:
                        string messageInternalServerError = null;
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                            messageInternalServerError = "Server Bermasalah. Hubungi Vendor !";
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }
                        return new RestApiResponse(new RestApiError($"{messageInternalServerError}", ResponseErrorType.ServerError));
                    case System.Net.HttpStatusCode.Unauthorized:
                        if (_currentProcess < _maxRetry)
                        {
                            await RefreshTokenAsync();
                            return await PostAsync(targetUrl, body, ignoreToken);
                        }
                        else
                        {
                            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var value = (JObject)result.GetValue("Value");
                            return new RestApiResponse(new RestApiError(value.GetValue("System_msg").ToString(), ResponseErrorType.Unauthorized));
                        }
                    default:
                        string message = null;
                        try
                        {
                            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var value = (JObject)result.GetValue("Value", StringComparison.OrdinalIgnoreCase);
                            message = value.GetValue("System_msg", StringComparison.OrdinalIgnoreCase).ToString();
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }

                        return new RestApiResponse(new RestApiError($"Error {message}", ResponseErrorType.UnknownError));
                }
            }
            catch (HttpRequestException e)
            {
                var msg = e.InnerException ?? e;
                Debug.WriteLine(msg.Message);
                Log.Logger.Error(msg.Message);
                return new RestApiResponse(new RestApiError("Koneksi Error. Silahkan periksa API URL atau koneksi internet Anda.", ResponseErrorType.ConnectionError));
            }
            catch (Exception e)
            {
                var msg = e.InnerException ?? e;
                Debug.WriteLine(msg.Message);
                Log.Logger.Error(msg.Message);
                return new RestApiResponse(new RestApiError($"Error {e.Message}", ResponseErrorType.UnknownError));
            }
        }

        // PATCH function (Update Data)
        public async Task<RestApiResponse> PatchAsync(string targetUrl, string entityId, Dictionary<string, dynamic> body, bool ignoreToken = false, bool useTimeOut = true, TimeSpan? timeOut = null, bool isReport = false)
        {
            // inject idPdam + idUser
            body = InjectRequiredParam(body);
            CheckForIgnoreToken(ignoreToken);
            var content = JsonConvert.SerializeObject(body);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
            var url = entityId != null ? Path.Combine(targetUrl, entityId) : targetUrl;
            url = UrlBuilder(url, null, isReport);
            ConsolePayload("PATCH", url, content);
            var client = new HttpClient();
            SetTimeOut(client, useTimeOut, timeOut);

            try
            {
                var response = await client.PatchAsync(url, byteContent);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.RequestTimeout:
                        return new RestApiResponse(new RestApiError($"Error Request Timeout. Please try again.", ResponseErrorType.RequestTimeOut));
                    case System.Net.HttpStatusCode.TooManyRequests:
                        string messageTooManyRequests = null;
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                            messageTooManyRequests = res.GetValue("Ui_msg", StringComparison.OrdinalIgnoreCase)?.ToString();
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }
                        return new RestApiResponse(new RestApiError($"{messageTooManyRequests}", ResponseErrorType.TooManyRequests));
                    case System.Net.HttpStatusCode.OK:
                    case System.Net.HttpStatusCode.Created:
                    case System.Net.HttpStatusCode.BadRequest:
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());

                            JArray data = null;
                            try
                            {
                                data = !string.IsNullOrEmpty(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString()) ? JArray.Parse(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString() ?? string.Empty) : null;
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    data = !string.IsNullOrEmpty(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString()) ? new JArray(JObject.Parse(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString())) : null;
                                }
                                catch (Exception)
                                {
                                    Debug.WriteLine("no data served");
                                }
                            }

                            return new RestApiResponse(new Response()
                            {
                                Status = res.GetValue("Status", StringComparison.OrdinalIgnoreCase) != null && (bool)res.GetValue("Status", StringComparison.OrdinalIgnoreCase),
                                System_msg = res.GetValue("System_msg", StringComparison.OrdinalIgnoreCase)?.ToString(),
                                Ui_msg = res.GetValue("Ui_msg", StringComparison.OrdinalIgnoreCase)?.ToString(),
                                TotalPage = res.GetValue("TotalPage", StringComparison.OrdinalIgnoreCase) != null ? (int)res.GetValue("TotalPage", StringComparison.OrdinalIgnoreCase) : 0,
                                Record = res.GetValue("TotalRecord", StringComparison.OrdinalIgnoreCase) != null ? (long)res.GetValue("TotalRecord", StringComparison.OrdinalIgnoreCase) : 0,
                                Data = data,
                                Execution_time = (long?)res.GetValue("Execution_time", StringComparison.OrdinalIgnoreCase)
                            });
                        }
                        catch (Exception internalError)
                        {
                            return new RestApiResponse(new RestApiError($"Internal Error {internalError.Message}", ResponseErrorType.UnknownError));
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        return new RestApiResponse(new RestApiError("EndPoint Not Found", ResponseErrorType.ApiNotFound));
                    case System.Net.HttpStatusCode.InternalServerError:
                        string messageInternalServerError = null;
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                            messageInternalServerError = "Server Bermasalah. Hubungi Vendor !";
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }
                        return new RestApiResponse(new RestApiError($"{messageInternalServerError}", ResponseErrorType.ServerError));
                    case System.Net.HttpStatusCode.Unauthorized:
                        if (_currentProcess < _maxRetry)
                        {
                            await RefreshTokenAsync();
                            return await PatchAsync(targetUrl, entityId, body, ignoreToken);
                        }
                        else
                        {
                            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var value = (JObject)result.GetValue("Value");
                            return new RestApiResponse(new RestApiError(value.GetValue("System_msg").ToString(), ResponseErrorType.Unauthorized));
                        }
                    default:
                        string message = null;
                        try
                        {
                            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var value = (JObject)result.GetValue("Value", StringComparison.OrdinalIgnoreCase);
                            message = value.GetValue("System_msg", StringComparison.OrdinalIgnoreCase).ToString();
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }

                        return new RestApiResponse(new RestApiError($"Error {message}", ResponseErrorType.UnknownError));
                }
            }
            catch (HttpRequestException e)
            {
                var msg = e.InnerException ?? e;
                Debug.WriteLine(msg.Message);
                Log.Logger.Error(msg.Message);
                return new RestApiResponse(new RestApiError("Koneksi Error. Silahkan periksa API URL atau koneksi internet Anda.", ResponseErrorType.ConnectionError));
            }
            catch (Exception e)
            {
                var msg = e.InnerException ?? e;
                Debug.WriteLine(msg.Message);
                Log.Logger.Error(msg.Message);
                return new RestApiResponse(new RestApiError($"Error {e.Message}", ResponseErrorType.UnknownError));
            }
        }

        // DELETE function (Delete Entity)
        public async Task<RestApiResponse> DeleteAsync(string targetUrl, Dictionary<string, dynamic> body, bool ignoreToken = false, bool useTimeOut = true, TimeSpan? timeOut = null, bool isReport = false)
        {
            // inject idPdam + idUser
            body = InjectRequiredParam(body);
            CheckForIgnoreToken(ignoreToken);
            var url = UrlBuilder(targetUrl, body, isReport);
            ConsolePayload("DELETE", url);
            var client = new HttpClient();
            SetTimeOut(client, useTimeOut, timeOut);

            try
            {
                var response = await client.DeleteAsync(url);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.RequestTimeout:
                        return new RestApiResponse(new RestApiError($"Error Request Timeout. Please try again.", ResponseErrorType.RequestTimeOut));
                    case System.Net.HttpStatusCode.TooManyRequests:
                        string messageTooManyRequests = null;
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                            messageTooManyRequests = res.GetValue("Ui_msg", StringComparison.OrdinalIgnoreCase)?.ToString();
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }
                        return new RestApiResponse(new RestApiError($"{messageTooManyRequests}", ResponseErrorType.TooManyRequests));
                    case System.Net.HttpStatusCode.OK:
                    case System.Net.HttpStatusCode.Created:
                    case System.Net.HttpStatusCode.BadRequest:
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());

                            JArray data = null;
                            try
                            {
                                data = !string.IsNullOrEmpty(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString()) ? JArray.Parse(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString()) : null;
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    data = !string.IsNullOrEmpty(res.GetValue("Data", StringComparison.OrdinalIgnoreCase)?.ToString()) ? new JArray(JObject.Parse(res.GetValue("Data", StringComparison.OrdinalIgnoreCase).ToString())) : null;
                                }
                                catch (Exception)
                                {
                                    Debug.WriteLine("no data served");
                                }
                            }

                            return new RestApiResponse(new Response()
                            {
                                Status = res.GetValue("Status", StringComparison.OrdinalIgnoreCase) != null && (bool)res.GetValue("Status", StringComparison.OrdinalIgnoreCase),
                                System_msg = res.GetValue("System_msg", StringComparison.OrdinalIgnoreCase)?.ToString(),
                                Ui_msg = res.GetValue("Ui_msg", StringComparison.OrdinalIgnoreCase)?.ToString(),
                                TotalPage = res.GetValue("TotalPage", StringComparison.OrdinalIgnoreCase) != null ? (int)res.GetValue("TotalPage", StringComparison.OrdinalIgnoreCase) : 0,
                                Record = res.GetValue("TotalRecord", StringComparison.OrdinalIgnoreCase) != null ? (long)res.GetValue("TotalRecord", StringComparison.OrdinalIgnoreCase) : 0,
                                Data = data,
                                Execution_time = (long?)res.GetValue("Execution_time", StringComparison.OrdinalIgnoreCase)
                            });
                        }
                        catch (Exception internalError)
                        {
                            return new RestApiResponse(new RestApiError($"Internal Error {internalError.Message}", ResponseErrorType.UnknownError));
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        return new RestApiResponse(new RestApiError("EndPoint Not Found", ResponseErrorType.ApiNotFound));
                    case System.Net.HttpStatusCode.InternalServerError:
                        string messageInternalServerError = null;
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                            messageInternalServerError = "Server Bermasalah. Hubungi Vendor !";
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }
                        return new RestApiResponse(new RestApiError($"{messageInternalServerError}", ResponseErrorType.ServerError));
                    case System.Net.HttpStatusCode.Unauthorized:
                        if (_currentProcess < _maxRetry)
                        {
                            await RefreshTokenAsync();
                            return await DeleteAsync(targetUrl, body, ignoreToken);
                        }
                        else
                        {
                            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var value = (JObject)result.GetValue("Value");
                            return new RestApiResponse(new RestApiError(value.GetValue("System_msg").ToString(), ResponseErrorType.Unauthorized));
                        }
                    default:
                        string message = null;
                        try
                        {
                            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var value = (JObject)result.GetValue("Value", StringComparison.OrdinalIgnoreCase);
                            message = value.GetValue("System_msg", StringComparison.OrdinalIgnoreCase).ToString();
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }

                        return new RestApiResponse(new RestApiError($"Error {message}", ResponseErrorType.UnknownError));
                }
            }
            catch (HttpRequestException e)
            {
                var msg = e.InnerException ?? e;
                Debug.WriteLine(msg.Message);
                Log.Logger.Error(msg.Message);
                return new RestApiResponse(new RestApiError("Koneksi Error. Silahkan periksa API URL atau koneksi internet Anda.", ResponseErrorType.ConnectionError));
            }
            catch (Exception e)
            {
                var msg = e.InnerException ?? e;
                Debug.WriteLine(msg.Message);
                Log.Logger.Error(msg.Message);
                return new RestApiResponse(new RestApiError($"Error {e.Message}", ResponseErrorType.UnknownError));
            }
            finally
            {
                MasterListGlobal.ClearMasterListGlobal();
            }
        }

        // UPLOAD function
        public async Task<RestApiResponse> UploadAsync(string targetUrl, Dictionary<string, dynamic> body, bool ignoreToken = false, bool useTimeOut = true, TimeSpan? timeOut = null, bool isReport = false)
        {
            // inject idPdam + idUser
            body = InjectRequiredParam(body);
            CheckForIgnoreToken(ignoreToken);
            var client = new HttpClient();
            SetTimeOut(client, useTimeOut, timeOut);
            var content = new MultipartFormDataContent();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            foreach (var item in body)
            {
                if (item.Key.ToLower().Contains("file_"))
                {
                    var keyName = item.Key.Replace("file_", string.Empty);
                    content.Add(new StreamContent(File.OpenRead(item.Value)), keyName, new FileInfo(item.Value).Name);
                }
                else
                {
                    content.Add(new StringContent(item.Value.ToString()), item.Key);
                }
            }
            var url = UrlBuilder(targetUrl, null, isReport);
            ConsolePayload("UPLOAD", url, content.ToString());

            try
            {
                var response = await client.PostAsync(url, content);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.RequestTimeout:
                        return new RestApiResponse(new RestApiError($"Error Request Timeout. Please try again.", ResponseErrorType.RequestTimeOut));
                    case System.Net.HttpStatusCode.TooManyRequests:
                        string messageTooManyRequests = null;
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                            messageTooManyRequests = res.GetValue("Ui_msg", StringComparison.OrdinalIgnoreCase)?.ToString();
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }
                        return new RestApiResponse(new RestApiError($"{messageTooManyRequests}", ResponseErrorType.TooManyRequests));
                    case System.Net.HttpStatusCode.OK:
                    case System.Net.HttpStatusCode.Created:
                    case System.Net.HttpStatusCode.BadRequest:
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());

                            // cast JArray of data, if there's object convert it to JArray
                            JArray data = null;
                            try
                            {
                                data = !string.IsNullOrEmpty(res.GetValue("Data")?.ToString()) ? JArray.Parse(res.GetValue("Data")?.ToString()) : null;
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    data = !string.IsNullOrEmpty(res.GetValue("Data")?.ToString()) ? new JArray(JObject.Parse(res.GetValue("Data")?.ToString())) : null;
                                }
                                catch (Exception)
                                {
                                    Debug.WriteLine("no data served");
                                }
                            }

                            return new RestApiResponse(new Response()
                            {
                                Status = res.GetValue("Status") != null && (bool)res.GetValue("Status"),
                                System_msg = res.GetValue("System_msg")?.ToString(),
                                Ui_msg = res.GetValue("Ui_msg")?.ToString(),
                                Data = data,
                                Execution_time = (long?)res.GetValue("Execution_time", StringComparison.OrdinalIgnoreCase)
                            });
                        }
                        catch (Exception internalError)
                        {
                            return new RestApiResponse(new RestApiError($"Internal Error {internalError.Message}", ResponseErrorType.UnknownError));
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        return new RestApiResponse(new RestApiError("EndPoint Not Found", ResponseErrorType.ApiNotFound));
                    case System.Net.HttpStatusCode.InternalServerError:
                        string messageInternalServerError = null;
                        try
                        {
                            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
                            messageInternalServerError = "Server Bermasalah. Hubungi Vendor !";
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }
                        return new RestApiResponse(new RestApiError($"{messageInternalServerError}", ResponseErrorType.ServerError));
                    case System.Net.HttpStatusCode.Unauthorized:
                        if (_currentProcess < _maxRetry)
                        {
                            await RefreshTokenAsync();
                            return await PostAsync(targetUrl, body, ignoreToken);
                        }
                        else
                        {
                            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var value = (JObject)result.GetValue("Value");
                            return new RestApiResponse(new RestApiError(value.GetValue("System_msg").ToString(), ResponseErrorType.Unauthorized));
                        }
                    default:
                        string message = null;
                        try
                        {

                            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var value = (JObject)result.GetValue("Value", StringComparison.OrdinalIgnoreCase);
                            message = value.GetValue("System_msg", StringComparison.OrdinalIgnoreCase).ToString();
                        }
                        catch (Exception e)
                        {
                            var msg = e.InnerException ?? e;
                            Debug.WriteLine(msg.Message);
                            Log.Logger.Error(msg.Message);
                        }

                        return new RestApiResponse(new RestApiError($"Error {message}", ResponseErrorType.UnknownError));
                }
            }
            catch (HttpRequestException e)
            {
                var msg = e.InnerException ?? e;
                Debug.WriteLine(msg.Message);
                Log.Logger.Error(msg.Message);
                return new RestApiResponse(new RestApiError("Koneksi Error. Silahkan periksa API URL atau koneksi internet Anda.", ResponseErrorType.ConnectionError));
            }
            catch (Exception e)
            {
                var msg = e.InnerException ?? e;
                Debug.WriteLine(msg.Message);
                Log.Logger.Error(msg.Message);
                return new RestApiResponse(new RestApiError($"Error {e.Message}", ResponseErrorType.UnknownError));
            }
            finally
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_contentType));
            }
        }

        public void SetToken(string token)
        {
            _token = token;
        }

        private void CheckForIgnoreToken(bool ignoreToken)
        {
            if (!ignoreToken)
                SetToken(Application.Current.Resources["UserToken"]?.ToString());
        }

        private async Task RefreshTokenAsync() { await OnRefreshToken.ExecuteAsync(); _currentProcess++; }

        #region internal function
        private string UrlBuilder(string targetUrl, Dictionary<string, dynamic> uriParameter = null, bool isReport = false)
        {
            var sb = new StringBuilder();

            var module = Application.Current.Resources["CURRENT_MODULE"]?.ToString() ?? "DEFAULT";

            if (module != "DEFAULT")
            {
                var api = Application.Current.Resources[module]?.ToString() ?? "CLOUD";
                var uriTemp1 = isReport ? _baseUriCloudReport : _baseUriCloud;
                var uriTemp2 = isReport ? _baseUriLocalReport : _baseUriLocal;
                sb.Append(api == "CLOUD" ? uriTemp1 : uriTemp2);
            }
            else
            {
                sb.Append(isReport ? _baseUriCloudReport : _baseUriCloud);
            }

            sb.Append(targetUrl);

            if (uriParameter != null)
            {
                sb.Append('?');

                var counter = 0;
                foreach (var item in uriParameter)
                {

                    if (counter > 0 && counter < uriParameter.Count)
                        sb.Append('&');

                    sb.Append(item.Key);
                    sb.Append('=');
                    if (item.Value is DateTime)
                    {
                        string dateString = JsonConvert.SerializeObject(item.Value);
                        sb.Append(dateString.Replace("\"", string.Empty));
                    }
                    else
                    {
                        sb.Append(item.Value);
                    }
                    counter++;
                }
            }
            return sb.ToString();
        }

        private Dictionary<string, dynamic> InjectRequiredParam(Dictionary<string, dynamic> param)
        {
            if (param == null)
                param = new Dictionary<string, dynamic>();

            if (!param.ContainsKey("IdPdam"))
                param.Add("IdPdam", Application.Current.Resources["IdPdam"]?.ToString());

            if (!param.ContainsKey("IdUserRequest") && Application.Current.Resources.Contains("IdUser") && Application.Current.Resources["IdUser"] != null)
                param.Add("IdUserRequest", Application.Current.Resources["IdUser"].ToString());

            return param;
        }

        private void ConsolePayload(string method, string url, string content = null)
        {
            Debug.WriteLine(method + " " + url);
            if (content != null)
                Debug.WriteLine("Content: " + content);
        }
        #endregion

        public string GetApiVersion() => ApiVersion;

        public string GetUrlCloud(bool isReport = false) => isReport ? _baseUriCloudReport : _baseUriCloud;

        public string GetUrlLocal(bool isReport = false) => isReport ? _baseUriLocalReport : _baseUriLocal;

        private void SetTimeOut(HttpClient client, bool useTimeOut = true, TimeSpan? timeOut = null)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_contentType));
            try
            {
                if (client.DefaultRequestHeaders.Contains("bearer"))
                    client.DefaultRequestHeaders.Remove("bearer");
                client.DefaultRequestHeaders.Add("bearer", _token);
            }
            catch (Exception)
            {
                Debug.WriteLine("Already has token!");
            }

            if (!useTimeOut)
            {
                client.Timeout = TimeSpan.FromMilliseconds(-1);
            }
            else
            {
                client.Timeout = timeOut ?? TimeSpan.FromMinutes(3);
            }
        }

    }
}
