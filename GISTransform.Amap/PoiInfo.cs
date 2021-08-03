using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GISTransform.Amap
{

    public class POIInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SearchOpt searchOpt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
    }


    //如果好用，请收藏地址，帮忙分享。
    public class Magicbox_data
    {        
    }

    public class Domain_listItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }

        public string value { get; set; }
    }

    public class Poi_listItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string recommend_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string distance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string group_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rating { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cinemazuo_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Domain_listItem> domain_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string review_total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string areacode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string typecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string newtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> exits { get; set; }
        /// <summary>
        /// 湖南中模绿建科技有限公司员工宿舍
        /// </summary>
        public string disp_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shape_region { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> entrances { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string diner_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cpdata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string discount_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adcode { get; set; }
        /// <summary>
        /// 株洲市
        /// </summary>
        public string cityname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string display_brand { get; set; }
        /// <summary>
        /// 湖南中模绿建科技有限公司员工宿舍
        /// </summary>
        public string name { get; set; }
    }

    public class Suggestion
    {
    }

    public class Query_intent
    {
        /// <summary>
        /// 
        /// </summary>
        public string cate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cate_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cate_ext { get; set; }
    }

    public class New_year
    {
        /// <summary>
        /// 
        /// </summary>
        public string flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tip { get; set; }
    }

    public class Activity
    {
        /// <summary>
        /// 
        /// </summary>
        public New_year new_year { get; set; }
    }

    public class Cache_all
    {
        /// <summary>
        /// 
        /// </summary>
        public string flag { get; set; }
    }

    public class Cache_filter
    {
        /// <summary>
        /// 
        /// </summary>
        public string expires { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string flag { get; set; }
    }

    public class Cache_directive
    {
        /// <summary>
        /// 
        /// </summary>
        public Cache_all cache_all { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Cache_filter cache_filter { get; set; }
    }

    public class Tips_info
    {
        /// <summary>
        /// 
        /// </summary>
        public string tip_rule { get; set; }
        /// <summary>
        /// 株洲市
        /// </summary>
        public string result_city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> target_keywords_list { get; set; }
    }

    public class Rec_pos
    {
        /// <summary>
        /// 
        /// </summary>
        public string latlng { get; set; }
    }

    public class Magic_box
    {
    }

    public class Suggest_query
    {
        /// <summary>
        /// 
        /// </summary>
        public string col { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string row { get; set; }
    }

    public class Ww
    {
        /// <summary>
        /// 
        /// </summary>
        public string t_tag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string poiid { get; set; }
    }

    public class Ww_info
    {
        /// <summary>
        /// 
        /// </summary>
        public Ww ww { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_ww { get; set; }
    }

    public class City_info_attr
    {
        /// <summary>
        /// 
        /// </summary>
        public string target_city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_loc_city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string map_view_city { get; set; }
    }

    public class Brand_intent
    {
        /// <summary>
        /// 
        /// </summary>
        public string t_tag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hierarchy_idx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string query_brand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hierarchy_strategy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
    }

    public class Lqii
    {
        /// <summary>
        /// 房产=0.994000;地名=0.001000房产|宿舍=0.994000;地名|门牌地址=0.001000
        /// </summary>
        public string auto_query_cate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Query_intent query_intent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string render_name_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Activity activity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Cache_directive cache_directive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string change_query_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string category_brand_recognition_result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string general_query_result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string expand_range_tip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string preload_next_page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string suggestcontent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string show_hand_drawing { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int category_brand_recognition_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ppinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Tips_info tips_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string general_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string show_pic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Rec_pos rec_pos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Magic_box magic_box { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string querytype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_view_city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string view_region { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_current_city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string self_navigation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string slayer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string business { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showaoi { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string has_recommend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string car_icon_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pdheatmap { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Suggest_query suggest_query { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_tupu_sug { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Ww_info ww_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string smartspot { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string target_view_city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string distance_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public City_info_attr city_info_attr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string suggestionview { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string slayer_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string call_taxi { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string utd_sceneid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string change_query_tip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string specialclassify { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Brand_intent brand_intent { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public Magicbox_data magicbox_data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Poi_listItem> poi_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int codepoint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 湖南中模绿建科技有限公司员工宿舍
        /// </summary>
        public string keywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Suggestion suggestion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> bus_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string interior_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Lqii lqii { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string general_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bounds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string busline_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> busline_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_general_search { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string timestamp { get; set; }
    }

    public class SearchOpt
    {
        /// <summary>
        /// 
        /// </summary>
        public string ac { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pageIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pageSize { get; set; }
        /// <summary>
        /// 湖南中模绿建科技有限公司员工宿舍
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
    }

 
}
