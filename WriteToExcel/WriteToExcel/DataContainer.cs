using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteToExcel
{
    public class DataContainer
    {
        private static DataContainer _instance;

        public List<double> SlideForce { get; set; }
        public List<double> TimeStamp { get; set; }

        // Private constructor to prevent external instantiation
        private DataContainer()
        {
            SlideForce = new List<double>();
            TimeStamp = new List<double>();
        }

        // Accessor for the singleton instance
        public static DataContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataContainer();
                }
                return _instance;
            }
        }
    }
}
