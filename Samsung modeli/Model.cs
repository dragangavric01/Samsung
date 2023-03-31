using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsung_modeli {
    [Serializable]
    public class Model {
        public string Name { get; set; }
        public int ProductionStartYear { get; set; }
        public string AddDate { get; set; }
        public string ImagePath { get; set; }
        public string RtfPath { get; set; }

        public Model() { }

        public Model(string name, int productionStartYear, string imagePath, string rtfPath) {
            Name = name;
            ProductionStartYear = productionStartYear;
            ImagePath = imagePath;
            RtfPath = rtfPath;

            AddDate = DateTime.Now.Date.ToString("dd.MM.yyyy.");
        }

        public override bool Equals(object obj) {
            return obj is Model model &&
                   Name == model.Name &&
                   ProductionStartYear == model.ProductionStartYear &&
                   AddDate == model.AddDate &&
                   ImagePath == model.ImagePath &&
                   RtfPath == model.RtfPath;
        }
    }
}
