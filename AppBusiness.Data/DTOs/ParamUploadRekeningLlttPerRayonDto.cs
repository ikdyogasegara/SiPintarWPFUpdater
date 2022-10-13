using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamUploadRekeningLlttPerRayonDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdRayon { get; set; }
        public DateTime? WaktuUpload { get; set; }
    }
}
