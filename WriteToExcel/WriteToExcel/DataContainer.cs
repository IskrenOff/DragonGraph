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
        public List<double> Velocity { get; set; }
        public List<double> CushionForce { get; set; }
        public List<double> CushionPosition { get; set; }
        public List<double> PunchForce { get; set; }
        public List<double> TimeStamp { get; set; }

        // Private constructor to prevent external instantiation
        private DataContainer()
        {
            SlideForce = new List<double>();
            Velocity = new List<double>();
            CushionForce = new List<double>();
            CushionPosition = new List<double>();
            PunchForce = new List<double>();
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
