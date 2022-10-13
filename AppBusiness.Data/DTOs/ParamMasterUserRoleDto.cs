using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterUserRoleDto
    {
        public int? IdPdam { get; set; }
        public int? IdRole { get; set; }
        public string NamaRole { get; set; }
        public List<ParamMasterUserAccessDto> Akses { get; set; }
    }

    public class ParamMasterUserAccessDto
    {
        public int? IdRoleAccess { get; set; }
        public bool? Value { get; set; }
    }

    public class ParamMasterUserRoleFilterDto : ParamMasterUserRoleDto
    {
    }
}
