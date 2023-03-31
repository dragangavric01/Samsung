using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsung_modeli {
    public class Common {
        public static ObservableCollection<string> ImageNames { get; set; } = new ObservableCollection<string>() { "Galaxy A02", 
                        "Galaxy A03", "Galaxy S9", "Galaxy S10", "Galaxy S20", "Galaxy S21", "Galaxy S22", "Galaxy S23",
                        "Galaxy Z Fold 3", "Galaxy Z Fold 4" };
        public static ObservableCollection<int> FontSizes { get; set; } = new ObservableCollection<int>() { 10, 12, 14, 16, 18, 20, 22, 24, 26 };
    }
}
