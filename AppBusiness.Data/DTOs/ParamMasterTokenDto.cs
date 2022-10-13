namespace AppBusiness.Data.DTOs
{
    public class ParamMasterTokenAuthenticateDto
    {
        public int? IdPdam { get; set; }
        public int? IdModule { get; set; }
        public string IdComputer { get; set; }
        public string NamaUser { get; set; }
        public string Password { get; set; }
        public bool IsPetugasBaca { get; set; }
    }

    public class ParamMasterTokenRefreshRevokeDto
    {
        public string RefreshToken { get; set; }
    }
}
