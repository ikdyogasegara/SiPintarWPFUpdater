using System;
using SiPintar.ViewModels;

namespace SiPintar.State.Navigators
{
    public interface INavigator
    {
        ViewModelBase BacameterCurrentViewModel { get; set; }
        ViewModelBase BillingCurrentViewModel { get; set; }
        ViewModelBase HublangCurrentViewModel { get; set; }
        ViewModelBase LoketCurrentViewModel { get; set; }
        ViewModelBase DistribusiCurrentViewModel { get; set; }
        ViewModelBase PerencanaanCurrentViewModel { get; set; }
        ViewModelBase GudangCurrentViewModel { get; set; }
        ViewModelBase PersonaliaCurrentViewModel { get; set; }
        ViewModelBase AkuntansiCurrentViewModel { get; set; }
        ViewModelBase LaporanCurrentViewModel { get; set; }

        event Action StateChanged;
    }
    public enum BacameterViewType
    {
        Supervisi,
        Produktivitas,
        PemetaanPelanggan,
        SistemKontrol,
        Bantuan,
        Onboarding
    }
    public enum LoketViewType
    {
        Tagihan,
        Angsuran,
        Laporan,
        TutupLoket,
        Bantuan,
        Setoran,
        Pengaturan,
        Onboarding
    }
    public enum BillingViewType
    {
        Supervisi,
        Atribut,
        Produktivitas,
        Bantuan,
        Onboarding
    }
    public enum HublangViewType
    {
        Pelayanan,
        Verifikasi,
        Atribut,
        Bantuan,
        BeritaAcara,
        Onboarding
    }
    public enum DistribusiViewType
    {
        Distribusi,
        Notifikasi,
        Atribut,
        Bantuan,
        Onboarding
    }
    public enum PerencanaanViewType
    {
        Perencanaan,
        Atribut,
        Bantuan,
        Onboarding
    }
    public enum GudangViewType
    {
        ProsesTransaksi,
        Pengolahan,
        MasterData,
        Laporan,
        Bantuan,
        Onboarding
    }
    public enum PersonaliaViewType
    {
        DataMaster,
        Kepegawaian,
        GajiPokok,
        Tunjangan,
        Potongan,
        Lainnya,
        SupervisiGaji,
        Bantuan,
        Onboarding
    }
    public enum AkuntansiViewType
    {
        Voucher,
        PostingAkuntansi,
        PostingKeuangan,
        Jurnal,
        Penyusutan,
        MasterData,
        Bantuan,
        Pengaturan,
        Onboarding
    }

    public enum LaporanViewType
    {
        Laporan,
        Bantuan
    }
}
