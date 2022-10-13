using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SiPintar.State.Navigators;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Background;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels;
using SiPintar.Views;
using SiPintar.Views.Global.Other;

namespace SiPintar.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ViewModelExtension
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IRestApiClientModel, RestApiClientModel>();
            services.AddSingleton<IFilterService, FilterService>();
            services.AddSingleton<IBackgroundService, BackgroundService>();

            services.AddScoped<RestApiClientModel>();

            services = ExtendMainComponent(services);
            services = ExtendBacameter(services);
            services = ExtendBilling(services);
            services = ExtendHublang(services);
            services = ExtendLoket(services);
            services = ExtendDistribusi(services);
            services = ExtendPerencanaan(services);
            services = ExtendGudang(services);
            services = ExtendPersonalia(services);
            services = ExtendAkuntansi(services);
            services = ExtendLaporan(services);

            services.AddScoped<LoginViewModel>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<AppConfigViewModel>();

            services.AddScoped<LoginView>(s => new LoginView(s.GetRequiredService<LoginViewModel>()));
            services.AddScoped<MainView>(s => new MainView(s.GetRequiredService<MainViewModel>()));
            services.AddScoped<AppConfigView>(s => new AppConfigView(s.GetRequiredService<AppConfigViewModel>()));

            return services;
        }

        private static IServiceCollection ExtendMainComponent(IServiceCollection services)
        {
            services.AddSingleton<CreateViewModel<ViewModels.Main.DesktopViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Main.DesktopViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Main.WebViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Main.WebViewModel>();
            });

            services.AddScoped<ViewModels.Main.DesktopViewModel>();
            services.AddScoped<ViewModels.Main.WebViewModel>();

            return services;
        }

        private static IServiceCollection ExtendBacameter(IServiceCollection services)
        {
            services.AddSingleton<ViewModels.Bacameter.Factories.IViewModelFactory, ViewModels.Bacameter.Factories.ViewModelFactory>();

            services.AddSingleton<CreateViewModel<ViewModels.Bacameter.SupervisiViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Bacameter.SupervisiViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Bacameter.ProduktivitasViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Bacameter.ProduktivitasViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Bacameter.PemetaanPelangganViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Bacameter.PemetaanPelangganViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Bacameter.SistemKontrolViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Bacameter.SistemKontrolViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Bacameter.BantuanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Bacameter.BantuanViewModel>();
            });

            services.AddScoped<ViewModels.Bacameter.SupervisiViewModel>();
            services.AddScoped<ViewModels.Bacameter.ProduktivitasViewModel>();
            services.AddScoped<ViewModels.Bacameter.PemetaanPelangganViewModel>();
            services.AddScoped<ViewModels.Bacameter.SistemKontrolViewModel>();
            services.AddScoped<ViewModels.Bacameter.BantuanViewModel>();

            return services;
        }

        private static IServiceCollection ExtendBilling(IServiceCollection services)
        {
            services.AddSingleton<ViewModels.Billing.Factories.IViewModelFactory, ViewModels.Billing.Factories.ViewModelFactory>();

            services.AddSingleton<CreateViewModel<ViewModels.Billing.SupervisiViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Billing.SupervisiViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Billing.AtributViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Billing.AtributViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Billing.ProduktivitasViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Billing.ProduktivitasViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Billing.BantuanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Billing.BantuanViewModel>();
            });

            services.AddScoped<ViewModels.Billing.SupervisiViewModel>();
            services.AddScoped<ViewModels.Billing.AtributViewModel>();
            services.AddScoped<ViewModels.Billing.ProduktivitasViewModel>();
            services.AddScoped<ViewModels.Billing.BantuanViewModel>();

            return services;
        }

        private static IServiceCollection ExtendHublang(IServiceCollection services)
        {
            services.AddSingleton<ViewModels.Hublang.Factories.IViewModelFactory, ViewModels.Hublang.Factories.ViewModelFactory>();

            services.AddSingleton<CreateViewModel<ViewModels.Hublang.PelayananViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Hublang.PelayananViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Hublang.VerifikasiViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Hublang.VerifikasiViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Hublang.AtributViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Hublang.AtributViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Hublang.BantuanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Hublang.BantuanViewModel>();
            });

            services.AddScoped<ViewModels.Hublang.PelayananViewModel>();
            services.AddScoped<ViewModels.Hublang.VerifikasiViewModel>();
            services.AddScoped<ViewModels.Hublang.AtributViewModel>();
            services.AddScoped<ViewModels.Hublang.BantuanViewModel>();

            return services;
        }

        private static IServiceCollection ExtendLoket(IServiceCollection services)
        {
            services.AddSingleton<ViewModels.Loket.Factories.IViewModelFactory, ViewModels.Loket.Factories.ViewModelFactory>();

            services.AddSingleton<CreateViewModel<ViewModels.Loket.TagihanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Loket.TagihanViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Loket.AngsuranViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Loket.AngsuranViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Loket.LaporanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Loket.LaporanViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Loket.TutupLoketViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Loket.TutupLoketViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Loket.SetoranViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Loket.SetoranViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Loket.BantuanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Loket.BantuanViewModel>();
            });

            services.AddScoped<ViewModels.Loket.TagihanViewModel>();
            services.AddScoped<ViewModels.Loket.AngsuranViewModel>();
            services.AddScoped<ViewModels.Loket.LaporanViewModel>();
            services.AddScoped<ViewModels.Loket.TutupLoketViewModel>();
            services.AddScoped<ViewModels.Loket.SetoranViewModel>();
            services.AddScoped<ViewModels.Loket.BantuanViewModel>();

            return services;
        }

        private static IServiceCollection ExtendDistribusi(IServiceCollection services)
        {
            services.AddSingleton<ViewModels.Distribusi.Factories.IViewModelFactory, ViewModels.Distribusi.Factories.ViewModelFactory>();

            services.AddSingleton<CreateViewModel<ViewModels.Distribusi.DistribusiViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Distribusi.DistribusiViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Distribusi.NotifikasiViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Distribusi.NotifikasiViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Distribusi.AtributViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Distribusi.AtributViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Distribusi.BantuanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Distribusi.BantuanViewModel>();
            });

            services.AddScoped<ViewModels.Distribusi.DistribusiViewModel>();
            services.AddScoped<ViewModels.Distribusi.NotifikasiViewModel>();
            services.AddScoped<ViewModels.Distribusi.AtributViewModel>();
            services.AddScoped<ViewModels.Distribusi.BantuanViewModel>();

            return services;
        }
        private static IServiceCollection ExtendPerencanaan(IServiceCollection services)
        {
            services.AddSingleton<ViewModels.Perencanaan.Factories.IViewModelFactory, ViewModels.Perencanaan.Factories.ViewModelFactory>();

            services.AddSingleton<CreateViewModel<ViewModels.Perencanaan.PerencanaanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Perencanaan.PerencanaanViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Perencanaan.AtributViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Perencanaan.AtributViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Perencanaan.BantuanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Perencanaan.BantuanViewModel>();
            });

            services.AddScoped<ViewModels.Perencanaan.PerencanaanViewModel>();
            services.AddScoped<ViewModels.Perencanaan.AtributViewModel>();
            services.AddScoped<ViewModels.Perencanaan.BantuanViewModel>();

            return services;
        }

        private static IServiceCollection ExtendGudang(IServiceCollection services)
        {
            services.AddSingleton<ViewModels.Gudang.Factories.IViewModelFactory, ViewModels.Gudang.Factories.ViewModelFactory>();

            services.AddSingleton<CreateViewModel<ViewModels.Gudang.MasterDataViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Gudang.MasterDataViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Gudang.ProsesTransaksiViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Gudang.ProsesTransaksiViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Gudang.PengolahanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Gudang.PengolahanViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Gudang.BantuanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Gudang.BantuanViewModel>();
            });

            services.AddScoped<ViewModels.Gudang.MasterDataViewModel>();
            services.AddScoped<ViewModels.Gudang.ProsesTransaksiViewModel>();
            services.AddScoped<ViewModels.Gudang.PengolahanViewModel>();
            services.AddScoped<ViewModels.Gudang.BantuanViewModel>();

            return services;
        }

        private static IServiceCollection ExtendPersonalia(IServiceCollection services)
        {
            services.AddSingleton<ViewModels.Personalia.Factories.IViewModelFactory, ViewModels.Personalia.Factories.ViewModelFactory>();

            services.AddSingleton<CreateViewModel<ViewModels.Personalia.DataMasterViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Personalia.DataMasterViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Personalia.KepegawaianViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Personalia.KepegawaianViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Personalia.GajiPokokViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Personalia.GajiPokokViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Personalia.TunjanganViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Personalia.TunjanganViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Personalia.PotonganViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Personalia.PotonganViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Personalia.LainnyaViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Personalia.LainnyaViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Personalia.SupervisiGajiViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Personalia.SupervisiGajiViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Personalia.BantuanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Personalia.BantuanViewModel>();
            });

            services.AddScoped<ViewModels.Personalia.DataMasterViewModel>();
            services.AddScoped<ViewModels.Personalia.KepegawaianViewModel>();
            services.AddScoped<ViewModels.Personalia.GajiPokokViewModel>();
            services.AddScoped<ViewModels.Personalia.TunjanganViewModel>();
            services.AddScoped<ViewModels.Personalia.PotonganViewModel>();
            services.AddScoped<ViewModels.Personalia.LainnyaViewModel>();
            services.AddScoped<ViewModels.Personalia.SupervisiGajiViewModel>();
            services.AddScoped<ViewModels.Personalia.BantuanViewModel>();

            return services;
        }

        private static IServiceCollection ExtendAkuntansi(IServiceCollection services)
        {
            services.AddSingleton<ViewModels.Akuntansi.Factories.IViewModelFactory, ViewModels.Akuntansi.Factories.ViewModelFactory>();

            services.AddSingleton<CreateViewModel<ViewModels.Akuntansi.VoucherViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Akuntansi.VoucherViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Akuntansi.PostingAkuntansiViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Akuntansi.PostingAkuntansiViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Akuntansi.PostingKeuanganViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Akuntansi.PostingKeuanganViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Akuntansi.JurnalViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Akuntansi.JurnalViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Akuntansi.PenyusutanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Akuntansi.PenyusutanViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Akuntansi.MasterDataViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Akuntansi.MasterDataViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Akuntansi.BantuanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Akuntansi.BantuanViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Akuntansi.PengaturanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Akuntansi.PengaturanViewModel>();
            });

            services.AddScoped<ViewModels.Akuntansi.VoucherViewModel>();
            services.AddScoped<ViewModels.Akuntansi.PostingAkuntansiViewModel>();
            services.AddScoped<ViewModels.Akuntansi.PostingKeuanganViewModel>();
            services.AddScoped<ViewModels.Akuntansi.JurnalViewModel>();
            services.AddScoped<ViewModels.Akuntansi.PenyusutanViewModel>();
            services.AddScoped<ViewModels.Akuntansi.MasterDataViewModel>();
            services.AddScoped<ViewModels.Akuntansi.BantuanViewModel>();
            services.AddScoped<ViewModels.Akuntansi.PengaturanViewModel>();

            return services;
        }

        private static IServiceCollection ExtendLaporan(IServiceCollection services)
        {
            services.AddSingleton<ViewModels.Laporan.Factories.IViewModelFactory, ViewModels.Laporan.Factories.ViewModelFactory>();

            services.AddSingleton<CreateViewModel<ViewModels.Laporan.LaporanModuleViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Laporan.LaporanModuleViewModel>();
            });
            services.AddSingleton<CreateViewModel<ViewModels.Laporan.BantuanViewModel>>(services =>
            {
                return () => services.GetRequiredService<ViewModels.Laporan.BantuanViewModel>();
            });

            services.AddScoped<ViewModels.Laporan.LaporanModuleViewModel>();
            services.AddScoped<ViewModels.Laporan.BantuanViewModel>();

            return services;
        }
    }
}
