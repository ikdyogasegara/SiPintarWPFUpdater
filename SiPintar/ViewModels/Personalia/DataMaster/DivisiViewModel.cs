﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.DataMaster.Divisi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.DataMaster
{
    public class DivisiViewModel : ViewModelBase
    {
        public DivisiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }

        private bool _isLoading = true;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isEdit = true;
        public bool IsEdit
        {
            get { return _isEdit; }
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterDivisiDto> _divisiList;
        public ObservableCollection<MasterDivisiDto> DivisiList
        {
            get { return _divisiList; }
            set { _divisiList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterDivisiDto> _divisiAtasList;
        public ObservableCollection<MasterDivisiDto> DivisiAtasList
        {
            get { return _divisiAtasList; }
            set { _divisiAtasList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterDepartemenDto> _departemenList;
        public ObservableCollection<MasterDepartemenDto> DepartemenList
        {
            get { return _departemenList; }
            set { _departemenList = value; OnPropertyChanged(); }
        }

        private MasterDivisiDto _selectedData;
        public MasterDivisiDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private ObservableCollection<int> _urutanList;
        public ObservableCollection<int> UrutanList
        {
            get { return _urutanList; }
            set { _urutanList = value; OnPropertyChanged(); }
        }

        private string _formDivisi;
        public string FormDivisi
        {
            get { return _formDivisi; }
            set { _formDivisi = value; OnPropertyChanged(); }
        }

        private int? _formDivisiAtas;
        public int? FormDivisiAtas
        {
            get { return _formDivisiAtas; }
            set { _formDivisiAtas = value; OnPropertyChanged(); }
        }

        private int? _formDepartemen;
        public int? FormDepartemen
        {
            get { return _formDepartemen; }
            set { _formDepartemen = value; OnPropertyChanged(); }
        }

        private int? _formUrutan;
        public int? FormUrutan
        {
            get { return _formUrutan; }
            set { _formUrutan = value; OnPropertyChanged(); }
        }

    }
}
