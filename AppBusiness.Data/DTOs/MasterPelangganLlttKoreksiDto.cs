using System;
using System.Collections.ObjectModel;

namespace AppBusiness.Data.DTOs
{
    public class MasterPelangganLlttKoreksiDto
    {
        public int? IdPdam { get; set; }
        public int? IdKoreksi { get; set; }
        public string SumberPerubahan { get; set; }
        public DateTime? WaktuKoreksi { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public int? IdPelangganLltt { get; set; }
        public string NomorLltt { get; set; }
        public string NomorLltt_Baru { get; set; }
        public string Nama { get; set; }
        public string Nama_Baru { get; set; }
        public string Alamat { get; set; }
        public string Alamat_Baru { get; set; }
        public string Rt { get; set; }
        public string Rt_Baru { get; set; }
        public string Rw { get; set; }
        public string Rw_Baru { get; set; }
        public int? IdTarifLltt { get; set; }
        public string KodeTarifLltt { get; set; }
        public string NamaTarifLltt { get; set; }
        public int? IdTarifLltt_Baru { get; set; }
        public string KodeTarifLltt_Baru { get; set; }
        public string NamaTarifLltt_Baru { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public int? IdRayon_Baru { get; set; }
        public string KodeRayon_Baru { get; set; }
        public string NamaRayon_Baru { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int? IdKelurahan_Baru { get; set; }
        public string KodeKelurahan_Baru { get; set; }
        public string NamaKelurahan_Baru { get; set; }
        public int? IdKolektif { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public int? IdKolektif_Baru { get; set; }
        public string KodeKolektif_Baru { get; set; }
        public string NamaKolektif_Baru { get; set; }
        public int? IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public int? IdStatus_Baru { get; set; }
        public string NamaStatus_Baru { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
        public int? IdFlag_Baru { get; set; }
        public string NamaFlag_Baru { get; set; }

        public string NoHp { get; set; }
        public string NoHp_Baru { get; set; }
        public string NoTelp { get; set; }
        public string NoTelp_Baru { get; set; }
        public string NoKtp { get; set; }
        public string NoKtp_Baru { get; set; }
        public string Email { get; set; }
        public string Email_Baru { get; set; }

        public string Latitude { get; set; }
        public string Latitude_Baru { get; set; }
        public string Longitude { get; set; }
        public string Longitude_Baru { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
