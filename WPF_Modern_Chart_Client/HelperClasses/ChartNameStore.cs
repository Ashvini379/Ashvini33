using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WPF_Modern_Chart_Client.HelperClasses
{
    public class ChartNameStore
    {
        public int Number { get; set; }
        public string Name { get; set; }
    }

   public class ChartTypeHelper
    {
        public ObservableCollection<ChartNameStore> _ChartsNames;

        public ObservableCollection<ChartNameStore> GetChartName()
        {
            _ChartsNames = new ObservableCollection<ChartNameStore>();

            XDocument xDoc = XDocument.Load("ChartNames.xml");

            var Charts = from c in xDoc.Descendants("charttypes").Elements("chart")
                         select c;

            foreach (var item in Charts)
            {
                _ChartsNames.Add(new ChartNameStore()
                {
                    Name = item.Attribute("name").Value,
                    Number = Convert.ToInt32(item.Attribute("number").Value)
                });
            }

            return _ChartsNames;

        }
    }
}
