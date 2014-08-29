using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using cellview.Models;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net;

namespace cellview.Controllers
{
    [RoutePrefix("api/cells")]
    public class CellsController : ApiController
    {
        private List<Cell> cosmote;
        private List<Cell> vodafone;
        private List<Cell> wind;

        public CellsController()
        {
            loadCosmote();
            loadVodafone();
            loadWind();
        }

        private decimal convert(string degrees)
        {
            degrees = Regex.Replace(degrees, "\"", "");
            var d = degrees.Split(' ');
            if (d.Count() < 3) return 0;
            if (string.IsNullOrWhiteSpace(d[2])) d[2] = "00";
            decimal r = decimal.Parse(d[0]) + (decimal.Parse(d[1]) / 60) + (decimal.Parse(d[2]) / 3600);
            return r;//.ToString("#.####").Replace(',','.');
        }

        private void loadCosmote()
        {
            String[] csv = File.ReadAllLines("Data/cosmote.csv");
            cosmote = new List<Cell>();
            foreach (string csvrow in csv)
            {
                var fields = csvrow.Split(';');
                cosmote.Add(new Cell()
                {
                    CellId = fields[0],
                    lat = convert(fields[3]),
                    lng = convert(fields[2]),
                    iconAngle = decimal.Parse(fields[4]),
                    radius = decimal.Parse(fields[5]),
                    range = decimal.Parse(fields[6]) * 1000,
                    Area = fields[7],
                    Street = fields[8],
                });
            }
        }

        private void loadVodafone()
        {
            String[] csv = File.ReadAllLines("Data/vodafone.csv");
            vodafone = new List<Cell>();
            foreach (string csvrow in csv)
            {
                var fields = csvrow.Split(';');
                vodafone.Add(new Cell()
                {
                    CellId = fields[0],
                    lat = convert(fields[9]),
                    lng = convert(fields[10]),
                    Area = fields[8],
                });
            }
        }

        private void loadWind()
        {
            String[] csv = File.ReadAllLines("Data/wind.csv");
            wind = new List<Cell>();
            foreach (string csvrow in csv)
            {
                var fields = csvrow.Split(';');
                wind.Add(new Cell()
                {
                    CellId = fields[0],
                    lat = convert(fields[3]),
                    lng = convert(fields[4]),
                    Area = fields[7],
                });
            }
        }

        [HttpGet]
        [Route("cosmote/{id}")]
        // GET api/values 
        public dynamic GetCosmote(string id)
        {
            try
            {
                string i = int.Parse(id, System.Globalization.NumberStyles.HexNumber).ToString();
                string title = string.Format("-{0}", i);
                var list = from c in cosmote
                           where c.CellId.EndsWith(title)
                           select new {c.CellId, c.lat, c.lng, c.message, c.icon, c.iconAngle,c.radius,c.range };

                return list;
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = "Unable to find record."
                };
                throw new HttpResponseException(resp); 
            }
        }

        [HttpGet]
        [Route("vodafone/{id}")]
        // GET api/values 
        public IEnumerable<Cell> GetVodafone(string id)
        {
            string i = int.Parse(id, System.Globalization.NumberStyles.HexNumber).ToString();
            string title = string.Format("-{0}", i);
            var list = cosmote.Where(c => c.CellId.EndsWith(title));
            return list;
        }

        [HttpGet]
        [Route("wind/{id}")]
        // GET api/values 
        public IEnumerable<Cell> GetWind(string id)
        {
            string i = int.Parse(id, System.Globalization.NumberStyles.HexNumber).ToString();
            string title = string.Format("{0:00000}", i);
            var list = wind.Where(c => c.CellId.EndsWith(title));
            return list;
        }
    }
}
