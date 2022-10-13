using System;
using System.Collections.Generic;
using System.Dynamic;

namespace AppBusiness.Data.DTOs
{
    public class ParamGenericDto
    {
        public int? IdUserRequest { get; set; }
        public Connection Connection { get; set; }
        public Query Query { get; set; }
    }

    public class Connection
    {
        public int? IdConnection { get; set; }
        public string KeyToken { get; set; }
    }

    public class Query
    {
        public int? IdQuery { get; set; }
        public int? PageSize { get; set; } = 10000000;
        public int? CurrentPage { get; set; } = 1;
        public Dictionary<string, object> FilterParameter { get; set; }
    }

    public class ParamGenericDynamicDto
    {
        public int? IdUserRequest { get; set; }
        public ConnectionDynamic Connection { get; set; }
        public QueryDynamic Query { get; set; }
    }

    public class ConnectionDynamic
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public int Port { get; set; }
        public string Db { get; set; }
    }

    public class QueryDynamic
    {
        public string QueryRaw { get; set; }
        public int PageSize { get; set; } = 10000000;
        public int CurrentPage { get; set; } = 1;
    }
}
