using Lanpuda.Client.Mvvm;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.DataAnnotations;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Kernel;
using Lanpuda.Lims.InspectionTasks.Dtos;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.UI.InspectionTasks.Dashboards;
using System.Drawing;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Client.Theme.Services.SettingsServices;
using System.Windows;
using Lanpuda.Lims.Samples;
using NUglify.Helpers;

namespace Lanpuda.Lims.UI.Home
{
    public class HomeViewModel : RootViewModelBase
    {
        private readonly IInspectionTaskAppService _inspectionTaskAppService;
        private readonly ISettingsService _settingsService;
        private readonly ISampleAppService _sampleAppService;
        public string Surname
        {
            get { return GetProperty(() => Surname); }
            set { SetProperty(() => Surname, value); }
        }
        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }
        public string WellKnownSaying
        {
            get { return GetProperty(() => WellKnownSaying); }
            set { SetProperty(() => WellKnownSaying, value); }
        }
        public string Hello
        {
            get { return GetProperty(() => Hello); }
            set { SetProperty(() => Hello, value); }
        }

        public BitmapImage Avatar
        {
            get { return GetProperty(() => Avatar); }
            set { SetProperty(() => Avatar, value); }
        }


        public ObservableCollection<ColumnSeries<double>> SampleCountSeries
        {
            get { return GetProperty(() => SampleCountSeries); }
            set { SetProperty(() => SampleCountSeries, value); }
        }

        public ObservableCollection<Axis> SampleCountXAxes
        {
            get { return GetProperty(() => SampleCountXAxes); }
            set { SetProperty(() => SampleCountXAxes, value); }
        }

        public ObservableCollection<ISeries> SeriesProcess
        {
            get { return GetProperty(() => SeriesProcess); }
            set { SetProperty(() => SeriesProcess, value); }
        }

        public ObservableCollection<InspectionTaskDto> InspectionTasks { get; set; }


        public HomeViewModel(IInspectionTaskAppService inspectionTaskAppService , ISettingsService settingsService, ISampleAppService sampleAppService)
        {
            _settingsService = settingsService;
            this.PageTitle = "首页";
            Surname = "Surname";
            Name = "Name";
            WellKnownSaying = "WellKnownSaying";
            Hello = "Hello";
            Avatar = new BitmapImage(new Uri("pack://application:,,,/Lanpuda.Client.Theme;component/Assets/Images/DefaultAvatar.png"));
            SampleCountSeries = new ObservableCollection<ColumnSeries<double>>();
            SampleCountXAxes = new ObservableCollection<Axis>();
            SeriesProcess = new ObservableCollection<ISeries>();
            InspectionTasks = new ObservableCollection<InspectionTaskDto>();
            _inspectionTaskAppService = inspectionTaskAppService;
            _sampleAppService = sampleAppService;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                this.Surname = _settingsService.GetSurname();
                this.Name = _settingsService.GetName();
                this.WellKnownSaying = GetWellKnownSaying();
               
                var sampleCountList = await _sampleAppService.GetSampleCountAsync();

                SampleCountSeries.Clear();

                double[] sampleCountArray = new double[sampleCountList.Count];
                for (int i = 0; i < sampleCountList.Count; i++)
                {
                    sampleCountArray[i] = sampleCountList[i].SampleCount;
                }

                ColumnSeries<double> columnSeries = new ColumnSeries<double>
                {
                    Name = "",
                    Values = sampleCountArray
                };
                SampleCountSeries.Add(columnSeries);

                string[] sampleDateArray = new string[sampleCountList.Count];
                for (int i = 0; i < sampleCountList.Count; i++)
                {
                    sampleDateArray[i] = sampleCountList[i].SampleDate.ToShortDateString();
                }
                SampleCountXAxes.Clear();
                var axis = new Axis
                {
                    Labels = sampleDateArray,
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    SeparatorsAtCenter = false,
                    TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                    TicksAtCenter = true
                };
                this.SampleCountXAxes.Add(axis);
                
                
               


               
                InspectionTaskGetListInput input = new InspectionTaskGetListInput();
                var now = DateTime.Now;
                input.InspectionDateStart = new DateTime(now.Year,now.Month,now.Day);
                input.InspectionDateEnd = new DateTime(now.Year, now.Month, now.Day,23,59,59);
                input.MaxResultCount = 500;
                input.SkipCount = 0;
                var result = await _inspectionTaskAppService.GetPagedListAsync(input);
                InspectionTasks.Clear();
                foreach (var item in result.Items)
                {
                    this.InspectionTasks.Add(item);
                }


                double hasValueCount = this.InspectionTasks.Where(m=>m.ResultValue != null).Count();

                double rate = hasValueCount / this.InspectionTasks.Count;
                rate = Math.Round(rate, 2);

                double res = rate * 100;

                var aaa = new GaugeBuilder()
                          .WithMaxColumnWidth(30)
                          .AddValue(rate)
                          .BuildSeries();

                SeriesProcess.Clear();
                foreach (var item in aaa)
                {
                    SeriesProcess.Add(item);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        [Command]
        public void OpenView(string viewKey)
        {
            ModuleManager.DefaultManager.InjectOrNavigate(RegionNames.MainContentRegion, viewKey);
        }


        private string GetWellKnownSaying()
        {
            List<string> list = new List<string>()
            {
                "朝朝又暮暮，日有小暖，岁有小安",
                "去无人的岛摸鲨鱼的角",
                "保持须臾的浪漫 理想的喧嚣 平等的热情-徐自摩",
                "如果你停止努力，那就是谷底；如果你继续攀爬，那就是在上坡。",
                "抱怨身处黑暗，不如提灯前行。愿你在自己存在的地方，成为一束光，照亮世界的一角",
                "细枝末节累加起来，即是生活",
                "人闲桂花落，满身都是秋。                                           ",
                "盛意已山河，山河不及你。                                           ",
                "世界先爱了我，我不能不爱它。                                       ",
                "苦练七十二变，笑对八十一难。                                       ",
                "失落时悄悄伸出手和风击个掌。                                       ",
                "做颗星星，有棱有角，还会发光。                                     ",
                "上了生活的贼船，就做快乐的海盗。                                   ",
                "梦里走了千万里，醒来还是在床上。                                   ",
                "哪里有爱，哪里就有不顾一切的信任。                                 ",
                "人生行路，芬芳之人方能遇到芬芳的心。                               ",
                "交友须带三分侠气，做人要存一点素心。                               ",
                "认真生活就能找到生活里藏起来的糖果。                               ",
                "零星的变好，最后也会和星河一样闪耀。                               ",
                "只要有花可开，就不允许生命与黯淡为伍。                             ",
                "先努力让自己发光，对的人才能迎着光而来。                           ",
                "人生一世，草生一春，来如风雨，去似微尘。                           ",
                "如果你喜欢，它就是喜悦，是意境，是海棠花里寻往昔。                 ",
                "错过落日余晖，请记得还有漫天星辰。                                 ",
                "每个人的裂纹最后都会变成故事的花纹。                               ",
                "要去追寻月亮，即使坠落也是掉进浩瀚星河。                           ",
                "我抓不住这世间的美好，只好装作万事顺遂的模样。                     ",
                "夕阳总会落在你身上，你也会有自己的宇航员和月亮。                   ",
                "努力的最大意义是让自己随时有能力跳出自己厌恶的圈子。               ",
                "尘世是非，躲不开人间风月。人间风月，躲不开情深意长。               ",
                "习惯于绝望的处境比绝望处境本身还要糟。——加缪《鼠疫》               ",
                "最好的生活状态就是，一个人时，安静而丰盛，两个人时，温暖而踏实。   ",
                "在长长的沉默之后说出的话，原本根本就不愿说。——赫塔·米勒《呼吸秋千》",
            };

            Random r = new Random();
            int index = r.Next(0, list.Count);
            return list[index];
        }



     
    }
}
