using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteToExcel.extractData
{
    public class ReadExtractedData
    {
        public required List<double> SlideForce { get; set; }
        public required List<double> Velocity { get; set; }
        public required List<double> CushionForce { get; set; }
        public required List<double> CushionPosition { get; set; }
        public required List<double> TimeStamp { get; set; }

        
    }

    
}
