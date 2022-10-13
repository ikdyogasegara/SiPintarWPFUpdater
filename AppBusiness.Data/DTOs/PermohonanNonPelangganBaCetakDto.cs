using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanNonPelangganBaCetakDto
    {
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public string NomorPermohonan { get; set; }
        public DateTime WaktuPermohonan { get; set; } = DateTime.Now;
        public string NamaTipePendaftaranSambungan { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public string NamaBlok { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public string NoKtp { get; set; }
        public string NoKk { get; set; }
        public string KodePost { get; set; }
        public decimal DayaListrik { get; set; } = 0;
        public decimal LuasTanah { get; set; } = 0;
        public decimal LuasRumah { get; set; } = 0;
        public string NamaPeruntukan { get; set; }
        public string NamaJenisBangunan { get; set; }
        public string NamaKepemilikan { get; set; }
        public string NamaPekerjaan { get; set; }
        public string Keterangan { get; set; }
        public string NosambDepan { get; set; }
        public string NosambBelakang { get; set; }
        public string NosambKiri { get; set; }
        public string NosambKanan { get; set; }
        public int Penghuni { get; set; }
        public string NamaUser { get; set; }

        public string KodeMerekMeter { get; set; }
        public string NamaMerekMeter { get; set; }
        public string NoSeriMeter { get; set; }
        public decimal StanAwalPasang { get; set; } = 0;

        public DateTime TglMeter { get; set; } = DateTime.Now;
        public int UrutanBaca { get; set; }
        public string NomorSpk { get; set; }
        public DateTime TanggalSpk { get; set; } = DateTime.Now;
        public string NomorBa { get; set; }
        public DateTime TanggalBa { get; set; } = DateTime.Now;
        public string NomorRab { get; set; }
        public DateTime TanggalRab { get; set; } = DateTime.Now;
    }
}
