using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly PersentaseTarifPajakViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(PersentaseTarifPajakViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _ = _restApi;

            GetDummyData();

            await Task.FromResult(_isTest);
        }


        public void GetDummyData()
        {
            _viewModel.DataList = new List<dummydatakeuangan>()
            {
                new dummydatakeuangan()
                {
                    Id = 1,
                    IdPdam = 1,
                    KodePersen ="01",
                    NamaPersen ="%Utang%pajak%Ppn",
                    JumlahPersen = 10

                },
                new dummydatakeuangan()
                {
                    Id = 2,
                    IdPdam = 1,
                    KodePersen ="01",
                    NamaPersen ="%Utang%PPh%pasal 21%",
                    JumlahPersen = 10
                },
                new dummydatakeuangan()
                {
                    Id = 3,
                    IdPdam = 1,
                    KodePersen ="01",
                    NamaPersen ="%Utang%PPh%pasal 22%",
                    JumlahPersen = 10
                }
            };
        }
    }
}
