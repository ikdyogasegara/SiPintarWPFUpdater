using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Serilog;
using SiPintar.ViewModels.Hublang.Bantuan;
using static SiPintar.ViewModels.Hublang.Bantuan.FaqViewModel;

namespace SiPintar.Commands.Hublang.Bantuan.FAQ
{
    [ExcludeFromCodeCoverage]
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly FaqViewModel _viewModel;
        private readonly string DirectoryToScan = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\FAQ\\Hublang";

        public OnLoadCommand(FaqViewModel ViewModel)
        {
            _viewModel = ViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.IsFullPage = false;
            _viewModel.IsDetail = false;
            _viewModel.CurrentContent = null;

            GetListFaq();

            await Task.FromResult(true);
        }

        private void GetListFaq()
        {
            if (!Directory.Exists(DirectoryToScan))
                return;

            var Result = new ObservableCollection<FaqListDto>();

            string[] directory = Directory.GetDirectories(DirectoryToScan);
            for (int iDir = 0; iDir < directory.Length; iDir++)
            {
                string foldername = new FileInfo(directory[iDir]).Name;

                if (foldername.ToLower() != "assets" && foldername.ToLower() != "images")
                {
                    string[] folder = foldername.Split('.');
                    string menuFirstLevel = folder.Length > 1 ? folder[1] : folder[0];

                    menuFirstLevel = menuFirstLevel.Replace('_', '&');

                    // Check if have child
                    List<KeyValuePair<string, string>> ChildComponent = GetChildComponent(DirectoryToScan, foldername);

                    if (ChildComponent.Count == 0)
                        ChildComponent.Add(new KeyValuePair<string, string>(foldername, menuFirstLevel));

                    var ContentList = new List<FaqContentListDto>();
                    foreach (var item in ChildComponent)
                    {
                        ContentList.Add(new FaqContentListDto()
                        {
                            ContentPath = item.Key,
                            ContentTitle = item.Value,
                            ContentList = GetFaqContent(item.Key)
                        });
                    }

                    Result.Add(new FaqListDto()
                    {
                        MainTitle = menuFirstLevel,
                        DataList = ContentList
                    });
                }
            }

            _viewModel.ListContent = Result;
            _viewModel.IsEmptyContent = _viewModel.ListContent.Count == 0;
        }

        private List<KeyValuePair<string, string>> GetChildComponent(string DirectoryToScan, string foldername)
        {
            var ChildComponent = new List<KeyValuePair<string, string>>();

            string DirChild = $"{DirectoryToScan}\\{foldername}";

            string[] dirChildList = Directory.GetDirectories(DirChild);
            for (int iDir2 = 0; iDir2 < dirChildList.Length; iDir2++)
            {
                string folderChild = new FileInfo(dirChildList[iDir2]).Name;
                string[] folderchild = folderChild.Split('.');
                string menuSecondLevel = folderchild.Length > 1 ? folderchild[1] : folderchild[0];

                string keyMenuSecondLevel = foldername + "\\" + folderChild;
                menuSecondLevel = menuSecondLevel.Replace('_', '&');

                ChildComponent.Add(new KeyValuePair<string, string>(keyMenuSecondLevel, menuSecondLevel));
            }

            return ChildComponent;
        }

        public List<FaqContentDetailDto> GetFaqContent(string PageName)
        {
            var Result = new List<FaqContentDetailDto>();
            try
            {
                var currentDirectory = DirectoryToScan;

                if (PageName != "FAQ")
                {
                    string specificDirectory = PageName;
                    currentDirectory += "\\" + specificDirectory;
                }

                string PageNow = PageName.Split('.')[PageName.Split('.').Length - 1].Replace('_', '&');

                string[] files = Directory.GetFiles(currentDirectory, "*.html");

                for (int iFile = 0; iFile < files.Length; iFile++)
                {
                    string filename = new FileInfo(files[iFile]).Name;
                    string htmlpath = $"{currentDirectory}\\{filename}";

                    string fileTitle = filename.Replace(".html", string.Empty);

                    bool IsValid = true;
                    if (!string.IsNullOrEmpty(_viewModel.SearchText) && !fileTitle.ToLower().Contains(_viewModel.SearchText.ToLower()))
                        IsValid = false;

                    if (IsValid)
                    {
                        var doc = new HtmlDocument();
                        doc.Load(htmlpath);
                        var node = doc.DocumentNode.SelectSingleNode("//body");
                        string htmlInnerString = node.InnerHtml;

                        Result.Add(
                            new FaqContentDetailDto()
                            {
                                FileTitle = fileTitle,
                                BreadcrumbInfo = new List<string>()
                                {
                                    fileTitle, PageName, PageNow
                                },
                                HtmlString = htmlInnerString
                            }
                        );
                    }
                }

                // IsEmptyContent = ListContent.Count == 0;
            }

            catch (Exception e)
            {
                Log.Logger.Error(e.Message.ToString());
                Debug.WriteLine(e.Message);
            }

            return Result;
        }
    }
}
