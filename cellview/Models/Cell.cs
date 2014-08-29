using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cellview.Models
{
    public class Cell
    {
        public string CellId { get; set; }
        public decimal lat { get; set; }
        public decimal lng { get; set; }
        public Icon icon { get { return new Icon();} }
        public decimal iconAngle { get; set; }
        public decimal radius { get; set; }
        public decimal range { get; set; }
        public string Area { get; set; }
        public string Street { get; set; }
        public string message { get { return string.Format("{0} {1}",Area,Street); } }
    }

    public class Icon
    {
        public string type { get { return "awesomeMarker"; } }
        public string icon { get { return "arrow-up"; } }
        public string markerColor { get { return "red"; } }
    }
}
