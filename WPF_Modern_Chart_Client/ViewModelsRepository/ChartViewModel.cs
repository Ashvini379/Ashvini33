using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Modern_Chart_Client.HelperClasses;
using WPF_Modern_Chart_Client.ServiceAdapter;
using WPF_Modern_Chart_Client.Commands;
using WPF_Modern_Chart_Client.ModelClasses;
using System.ComponentModel;
using System.Windows.Controls;
using WPF_Modern_Chart_Client.ChartControls;
using System.Windows;

namespace WPF_Modern_Chart_Client.ViewModelsRepository
{
    public class ChartViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<ChartNameStore> _chartsInfo;

        public ObservableCollection<ChartNameStore> ChartsInfo
        {
            get
            {
                return _chartsInfo;
            }

            set
            {
                _chartsInfo = value;
                OnPropertyChanged("ChartsInfo");
            }
        }

        public ObservableCollection<SalesInfo> SaleData
        {
            get
            {
                return _saleData;
            }

            set
            {
                _saleData = value;
            }
        }

        public ObservableCollection<CountryRegionCode> CountryRegionCode
        {
            get
            {
                return _countryRegionCode;
            }

            set
            {
                _countryRegionCode = value;
            }
        }

        private ObservableCollection<SalesInfo> _saleData;

        private ObservableCollection<CountryRegionCode> _countryRegionCode;

        private CountryRegionCode _countryRegionName;

        private bool _isRadioButtonEnabled;
        ChartTypeHelper helper;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertName));
            }
        }
        ProxyAdapter adapter;
        public ChartViewModel()
        {
            adapter = new ProxyAdapter();
            helper = new ChartTypeHelper();
            ChartsInfo = helper.GetChartName();
            SaleData = new ObservableCollection<SalesInfo>();

            CountryRegionCode = new ObservableCollection<ModelClasses.CountryRegionCode>();
            GetCountryRegionCodeGroup();
            SalesDetailYTDCommand = new RelayCommand(SalesDetailYTD);
            SalesDetailLastYearCommand = new RelayCommand(SalesDetaiLastYear);
            IsRadioButtonEnabled = true;
        }

        private async void SalesDetaiLastYear()
        {
            if (CountryRegionName != null)
            {
                SaleData.Clear();
                var Res = (from sale in await adapter.GetSalesInformation()
                           where sale.CountryRegionCode == CountryRegionName.CountryRegion
                           select new
                           {
                               Name = sale.Name,
                               SalesLastYear = sale.SalesLastYear
                           }
                           ).ToList();
                foreach (var item in Res)
                {
                    SaleData.Add(new SalesInfo { Name = item.Name, Sales = item.SalesLastYear });
                }

            }
        }

        private async void SalesDetailYTD()
        {
            SaleData.Clear();
            var Res = (from sale in await adapter.GetSalesInformation()
                       where sale.CountryRegionCode == CountryRegionName.CountryRegion
                       select new
                       {
                           Name = sale.Name,
                           SaleYTD = sale.SalesYTD
                       }).ToList();

            foreach (var item in Res)
            {
                SaleData.Add(new SalesInfo { Name = item.Name, Sales = item.SaleYTD });
            }

        }

       
        private async void GetCountryRegionCodeGroup()
        {
            var Res = (from sale in await adapter.GetSalesInformation()
                       group sale by sale.CountryRegionCode into saleg
                       select new CountryRegionCode
                       {
                           CountryRegion = saleg.Key
                       });

            foreach (var item in Res)
            {
                CountryRegionCode.Add(item);
            }
        }

        public CountryRegionCode CountryRegionName
        {
            get { return _countryRegionName; }
            set
            {
                _countryRegionName = value;
                IsRadioButtonEnabled = true;
                OnPropertyChanged("CountryRegionName");
            }
        }
        
        public bool IsRadioButtonEnabled
        {
            get
            {
                return _isRadioButtonEnabled;
            }

            set
            {
                _isRadioButtonEnabled = value;
                OnPropertyChanged("IsRadioButtonEnabled");
            }
        }

        public RelayCommand SalesDetailYTDCommand { get; private set; }
        public RelayCommand SalesDetailLastYearCommand { get; private set; }

        private ChartNameStore _chartNameStore;

        public ChartNameStore ChartNameStore
        {
            get { return _chartNameStore; }
            set {
                _chartNameStore = value;

                DisplayChart();
                OnPropertyChanged("ChartNameStore");
            }
        }

        private FrameworkElement _content;
        public FrameworkElement ContentWindow
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged("ContentWindow");
            }
        }
        private ObservableCollection<FrameworkElement> windows;

        public ObservableCollection<FrameworkElement> Windows
        {
            get { return windows; }
            set { windows = value; }
        }


        public void DisplayChart()
        {
            //ChartsInfo.Clear();

            Windows = new ObservableCollection<FrameworkElement>();
            var chartType = ChartNameStore;

            switch (chartType.Name)
            {
                case "PieChart":
                    PieChartUserControl pie = new PieChartUserControl();
                    pie.DataContext = SaleData;
                    ContentWindow = pie;

                    OnPropertyChanged("ContentWindow");
                    break;
                case "ClusteredColumnChart":
                    ClusteredColumnChartUserControl ccchart = new ClusteredColumnChartUserControl();
                    ccchart.DataContext = SaleData;
                    ContentWindow = ccchart;
                    break;
                case "ClusteredBarChart":
                    ClusteredBarChartUserControl cbchart = new ClusteredBarChartUserControl();
                    cbchart.DataContext = SaleData;
                    ContentWindow = cbchart;
                    break;
                case "DoughnutChart":
                    DoughnutChartUserControl dnchart = new DoughnutChartUserControl();
                    dnchart.DataContext = SaleData;
                    ContentWindow = dnchart;
                    break;
                case "StackedColumnChart":
                    StackedColumnChartUserControl stcchart = new StackedColumnChartUserControl();
                    stcchart.DataContext = SaleData;
                    ContentWindow = stcchart;
                    break;
                case "StackedBarChart":
                    StackedBarChartUserControl stbchart = new StackedBarChartUserControl();
                    stbchart.DataContext = SaleData;
                    ContentWindow = stbchart;
                    break;
                case "RadialGaugeChart":
                    RadialGaugeChartUserControl rgchart = new RadialGaugeChartUserControl();
                    rgchart.DataContext = SaleData;
                    ContentWindow = rgchart;
                    break;
            }

        }
    }
}
