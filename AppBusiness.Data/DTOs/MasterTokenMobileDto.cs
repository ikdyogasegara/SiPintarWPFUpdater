using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterTokenMobileDto
    {
        public int? IdPetugasBaca { get; set; }
        public int? IdPdam { get; set; }
        public int IdModule { get; set; }
        public string IdComputer { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiredAt { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiredAt { get; set; }
        public bool IsRefreshTokenExpired { get; set; }
        public bool IsRefreshTokenActive { get; set; }
    }
}
