using System;

namespace AppBusiness.Data.DTOs
{
    public class PengecualianTunjanganDetailDto : IEquatable<PengecualianTunjanganDetailDto>
    {
        public int? IdJenisTunjangan { get; set; }
        public string KodeJenisTunjangan { get; set; }
        public string NamaJenisTunjangan { get; set; }
        public string Tipe { get; set; }

        public bool Equals(PengecualianTunjanganDetailDto other)
        {
            if (other == null)
            {
                return false;
            }

            return IdJenisTunjangan == other.IdJenisTunjangan;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PengecualianTunjanganDetailDto);
        }

        public override int GetHashCode()
        {
            return IdJenisTunjangan.GetHashCode();
        }
    }
}
