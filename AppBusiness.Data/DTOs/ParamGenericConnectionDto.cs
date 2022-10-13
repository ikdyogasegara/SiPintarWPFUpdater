namespace AppBusiness.Data.DTOs
{
    public class ParamGenericConnectionDto
    {
        public int? Id { get; set; }
        public string ConnectionName { get; set; }
        public string KeyToken { get; set; }
        public string Host { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Port { get; set; }
        public string Db { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
