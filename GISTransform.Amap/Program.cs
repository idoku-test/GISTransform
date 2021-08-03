using GISTransform.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GISTransform.Amap
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = @"E:\code\fd\GISTransform\GISTransform.Amap\excel\高德地图-株洲-aoi-20210803 - 调整.xlsx";
            string output = @"E:\code\fd\GISTransform\GISTransform.Amap\excel\高德地图-株洲-aoi-20210803 - 输出.xls";
            var fs = new FileStream(input, FileMode.Open);
            DataTable dt = BuildExcel.ReadStreamToDataTable(fs, "sheet1");
            var excel = new BuildExcel();
            double lat,lng, a_lat, a_lng;
            var list = new List<AoiInfo>();
            foreach (DataRow row in dt.Rows)
            {
                var json = row[0].ToString();
                POIInfo info = JsonTools.JsonToObject2<POIInfo>(json);
                if (info != null && info.data.poi_list!=null)
                {
                    AoiInfo aoiinfo = new AoiInfo();
                    var poi = info.data.poi_list.FirstOrDefault();
                    if (poi != null)
                    {
                        GCJ2WGS.GCJ02_to_WGS84(Convert.ToDouble(poi.latitude), Convert.ToDouble(poi.longitude), out lat, out lng);
                        aoiinfo.aoi_id = poi.id;
                        aoiinfo.aoi_name = poi.name;
                        aoiinfo.address = poi.address;
                        aoiinfo.pca_code = poi.adcode;
                        aoiinfo.latitude = lat;
                        aoiinfo.longitude = lng;
                        list.Add(aoiinfo);
                        var aoi = poi.domain_list.FirstOrDefault(a => a.name == "aoi");

                        if (aoi != null && !string.IsNullOrEmpty(aoi.value))
                        {
                            var aoi_list = aoi.value.Split('_');
                            foreach (var item in aoi_list)
                            {
                                var point = item.Split(',').Select(f => Convert.ToDouble(f)).ToArray();

                                GCJ2WGS.GCJ02_to_WGS84(point[1], point[0], out a_lat, out a_lng);
                                AoiInfo aoiInfo2 = new AoiInfo()
                                {
                                    aoi_id = poi.id,
                                    aoi_name = poi.name,
                                    address = poi.address,
                                    pca_code = poi.adcode,
                                    latitude = lat,
                                    longitude = lng,
                                    aoi_lat = a_lat,
                                    aoi_lng = a_lng,
                                };                               
                                list.Add(aoiInfo2);
                            }
                        }

                       
                    }
                }
            

              
            }
            DataTable aoiTb =  BuildExcel.ListToDataTable(list);
            excel.InsertTable(aoiTb, 0, 0);
            excel.SaveAs(output);
        }
    }
}
