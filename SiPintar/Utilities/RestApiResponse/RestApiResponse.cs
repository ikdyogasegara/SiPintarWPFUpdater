using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

namespace SiPintar.Utilities
{
    [ExcludeFromCodeCoverage]
    public class RestApiResponse : ServiceResponse<Response, RestApiError>
    {
        public RestApiResponse(Response response) : base(response) { }

        public RestApiResponse(RestApiError error) : base(error) { }
    }

    [ExcludeFromCodeCoverage]
    public class Response
    {
        public bool Status { get; set; }
        public string? System_msg { get; set; }
        public string? Ui_msg { get; set; }
        public int TotalPage { get; set; }
        public long Record { get; set; }
        public JArray Data { get; set; }
        public long? Execution_time { get; set; }
    }
}
