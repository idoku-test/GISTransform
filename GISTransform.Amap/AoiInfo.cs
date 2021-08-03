using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GISTransform.Amap
{
    public class AoiInfo
    {
        public string aoi_id { get; set; }

        public string pca_code { get; set; }

        public string address { get; set; }

        public string aoi_name { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public double aoi_lat { get; set; }

        public double aoi_lng { get; set; }

        public string aoi_pointer
        {
            get
            {
                return aoi_lat + "," + aoi_lng;
            }
            private set { }
        }
    }
}
