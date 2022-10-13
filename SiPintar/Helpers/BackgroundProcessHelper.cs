using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Utilities;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public class BackgroundProcessHelper
    {
        public BackgroundProcessHelper()
        {
            QueueList = new List<ProcessObject>();
        }

        public List<ProcessObject> QueueList { get; set; }

        public void AddToQueue(ProcessObject data)
        {
            QueueList.Add(data);
        }

        public void ClearAllQueue()
        {
            QueueList.Clear();
        }

        public async Task RunAsync(IRestApiClientModel _restApi)
        {
            try
            {
                bool IsDone = false;
                while (!IsDone)
                {
                    await Task.Delay(5000);

                    var check = QueueList.FirstOrDefault(i => i.Status == 0);
                    if (check == null || QueueList.Count == 0)
                    {
                        IsDone = true;
                        break;
                    }

                    var temp = new List<ProcessObject>(QueueList);
                    foreach (var item in temp)
                    {
                        if (item.Status == 0)
                        {
                            var Response = await Task.Run(() => _restApi.GetAsync(item.UrlToCheck, item.ValueToCheck));

                            if (!Response.IsError)
                            {
                                var Result = Response.Data;

                                if (Result.Status && Result.Data != null && Result.Data.Count > 0)
                                {
                                    Debug.WriteLine("Data ===> " + Result.Data);

                                    item.Status = 1;
                                }
                            }
                        }
                    }
                    QueueList = temp;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        [ExcludeFromCodeCoverage]
        public class ProcessObject
        {
            public DateTime SubmitTime { get; set; }
            public string UrlToCheck { get; set; }
            public Dictionary<string, dynamic> ValueToCheck { get; set; }
            public string ProcessDescription { get; set; }
            public string ModuleSource { get; set; }
            public int Status { get; set; } // 0 = Pending, 1 = Success
        }
    }
}

