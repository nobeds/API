using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public partial class hotel
    {
        [Key]
        public int hotel_id { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string postal_code { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        //public string password { get; set; }
        public string type { get; set; }
        public int? min_stay { get; set; }

        //public string checkin_time { get; set; }
        //public string checkout_time { get; set; }

        public string languages { get; set; }
        public string currencies { get; set; }
        public string coordinates { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> last_login { get; set; }
        public Nullable<System.DateTime> deleted { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string company_details { get; set; }
        public double? vat { get; set; }
        public string invoice_currency { get; set; }
        public string other_info { get; set; }
        public string logo { get; set; }
        public string review_link { get; set; }
        public Nullable<int> promo_discount { get; set; }
        public Nullable<int> direct_discount { get; set; }
        public string direct_link { get; set; }
        public string invoice_prefix { get; set; }
        public string invoice_text { get; set; }
        public string invoice_footer { get; set; }
        public string signature { get; set; }
        public string approval_text { get; set; }
        public string terms_conditions { get; set; }
        public Nullable<int> require_cc { get; set; }
        public Nullable<int> require_invoice { get; set; }
        public Nullable<int> require_approval { get; set; }
        public string subscription { get; set; }

        public Nullable<int> require_prepay { get; set; }
        public Nullable<int> send_success { get; set; }
        public Nullable<int> send_failed { get; set; }
        public string prepay_text { get; set; }
        public string prepay_success { get; set; }
        public string prepay_fail { get; set; }
        public Nullable<int> prepay_more7d { get; set; }
        public Nullable<int> prepay_less7d { get; set; }

        public string text2reach_api { get; set; }
        public string text2reach_sender { get; set; }
        public Nullable<int> order_sms_required { get; set; }
        public string order_sms_text { get; set; }
        public Nullable<int> paid_sms_required { get; set; }
        public string paid_sms_text { get; set; }
        public string referral { get; set; }
        public string hear { get; set; }
        public string website { get; set; }
        public int? reminder_sms_required { get; set; }
        public string reminder_sms_text { get; set; }
        public int? reminder_email_required { get; set; }
        public string reminder_email_text { get; set; }
        public int? booking_percent { get; set; }
        public int? booking_amount { get; set; }
        public int? airbnb_percent { get; set; }
        public int? airbnb_amount { get; set; }
        public int? expedia_percent { get; set; }
        public int? expedia_amount { get; set; }
        public int? hostelworld_percent { get; set; }
        public int? hostelworld_amount { get; set; }
        public string contract { get; set; }
        public string registration { get; set; }
        public string api_token { get; set; }
    }

    public partial class order
    {
        [Key]
        public int order_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public int invoice { get; set; }
        public int hotel_id { get; set; }
        public int room_id { get; set; }
        public int? vroom_id { get; set; }
        public string referral { get; set; }
        public string referral_order_id { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        [DataType(DataType.EmailAddress)]
        public string emailas { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }
        public int? guests { get; set; }
        public Nullable<short> nights { get; set; }
        public float? price { get; set; }
        public float? total { get; set; }
        public Nullable<float> prepay { get; set; }
        public Nullable<float> comission { get; set; }
        public Nullable<float> balance { get; set; }
        public string status { get; set; }
        public string comment { get; set; }
        public string staff { get; set; }
        public Nullable<int> rating { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> inserted { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public Nullable<System.DateTime> Canceled { get; set; }
        public Nullable<System.DateTime> deleted { get; set; }

        public string roomreservation_id { get; set; }
        public string ruid { get; set; }
        public string genius { get; set; }
        public int? cleaned_id { get; set; }
        public Nullable<float> review { get; set; }
        public Nullable<float> cleanness { get; set; }
        public string blob { get; set; }
        public string pid { get; set; }
        public string b_name { get; set; }
        public string b_price { get; set; }
        public string b_extras { get; set; }
        public string b_smoking { get; set; }
        public string b_remarks { get; set; }
        public string b_info { get; set; }
        public string b_meal { get; set; }
        public string photo_url { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public string company_code { get; set; }
        public string coupon { get; set; }
        public int? hour { get; set; }
    }

    public partial class rentals
    {
        [Key]
        public int room_id { get; set; }
        public int? hotel_id { get; set; }
        public int? order_nr { get; set; }
        public Nullable<short> quantity { get; set; }
        public Nullable<float> price_day { get; set; }
        public int? guests { get; set; }
        public Nullable<short> children { get; set; }
        [RegularExpression("([A-Za-z0-9 ]*)", ErrorMessage = "Can contain only EN letters, space and numbers")]
        public string title { get; set; }
        [RegularExpression("([A-Za-z0-9 ]*)", ErrorMessage = "Can contain only EN letters, space and numbers")]
        public string type { get; set; }
        public string description { get; set; }
        public string photo { get; set; }
        public string safebox_text { get; set; }
        public string booking_hotel_id { get; set; }
        public string booking_room_id { get; set; }
        public string booking_rate_id { get; set; }
        public string airbnb_import { get; set; }
        public string airbnb_export { get; set; }
        public string myallocator_room_id { get; set; }
        public Nullable<byte> active { get; set; }
        public string myallocator_property_id { get; set; }
        public string myallocator_user { get; set; }
        public string myallocator_pasw { get; set; }
        public string expedia_hotel_id { get; set; }
        public string expedia_room_id { get; set; }
        public string expedia_rate_id { get; set; }
        public string hostelworld_hotel_id { get; set; }
        public string hostelworld_room_id { get; set; }
        public string hostelworld_rate_id { get; set; }
        public string hostelworld_pasw { get; set; }
        public Nullable<int> booking_errors { get; set; }
        public Nullable<byte> booking_check { get; set; }
        public Nullable<int> expedia_errors { get; set; }
        public Nullable<byte> expedia_check { get; set; }
        public Nullable<int> airbnb_errors { get; set; }
        public Nullable<byte> airbnb_check { get; set; }
        public Nullable<int> airbnb_listing_id { get; set; }
        public string airbnb_token { get; set; }
        public Nullable<byte> matrix_check { get; set; }
        public Nullable<byte> hostelworld_check { get; set; }
        public string sumup_email { get; set; }
        public string sumup_client_id { get; set; }
        public string sumup_client_secret { get; set; }
        public string url { get; set; }
        public string stripe_secret { get; set; }
        public string subtype { get; set; }
        public string price_type { get; set; }
        public int? checkin_time { get; set; }
        public int? checkout_time { get; set; }
        public string images { get; set; }
        public string facilities_en { get; set; }
    }

    public partial class airbnb
    {
        [Key]
        public int room_id { get; set; }
        public string airbnb_import { get; set; }
        public Nullable<int> airbnb_listing_id { get; set; }
        public string airbnb_token { get; set; }
    }

    public partial class booking
    {
        [Key]
        public int room_id { get; set; }
        public string booking_hotel_id { get; set; }
        public string booking_room_id { get; set; }
        public string booking_rate_id { get; set; }
    }

    public partial class expedia
    {
        [Key]
        public int room_id { get; set; }
        public string expedia_hotel_id { get; set; }
        public string expedia_room_id { get; set; }
        public string expedia_rate_id { get; set; }
    }

    public partial class hostelworld
    {
        [Key]
        public int room_id { get; set; }
        public string hostelworld_hotel_id { get; set; }
        public string hostelworld_room_id { get; set; }
        public string hostelworld_rate_id { get; set; }
        public string hostelworld_pasw { get; set; }
    }

    public partial class accounting
    {
        [Key]
        public int rid { get; set; }
        public int hotel_id { get; set; }
        public int manager_id { get; set; }
        public string title { get; set; }
        public float value { get; set; }
        public Nullable<byte> @checked { get; set; }
        public Nullable<int> room_id { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> timestamp { get; set; }
        public Nullable<int> order_id { get; set; }
    }

    public partial class review
    {
        [Key]
        public int rid { get; set; }
        public int hotel_id { get; set; }
        public int room_id { get; set; }
        public string refferal_order_id { get; set; }
        public Nullable<System.DateTime> timestamp { get; set; }
        public Nullable<System.DateTime> checkin { get; set; }
        public string positive { get; set; }
        public string negative { get; set; }
        public double score { get; set; }
        public double clean { get; set; }
    }

    public partial class availability
    {
        [Key]
        public int availability_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public int hotel_id { get; set; }
        public int room_id { get; set; }
        public System.DateTime date { get; set; }
        public System.TimeSpan time { get; set; }
        public float price { get; set; }
        public short quantity { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public Nullable<byte> last_minute { get; set; }
        public Nullable<int> min_stay { get; set; }
        public Nullable<int> max_stay { get; set; }
        public Nullable<int> closed { get; set; }
        public Nullable<int> coa { get; set; }
        public Nullable<int> cod { get; set; }
        public Nullable<int> mah { get; set; }
        public float? one { get; set; }
        public float? two { get; set; }
        public float? three { get; set; }
        public float? four { get; set; }
        public float? five { get; set; }
        public float? six { get; set; }
        public float? seven { get; set; }
        public float? eight { get; set; }
        public float? nine { get; set; }
        public float? ten { get; set; }

        public string airbnb { get; set; }
        public string booking { get; set; }
        public string expedia { get; set; }
        public string hostelworld { get; set; }
        public string agoda { get; set; }
    }

    public class Tasks
    {
        [Key]
        public int rid { get; set; }
        public int hotel_id { get; set; }
        public int? manager_id { get; set; }
        public int? room_id { get; set; }
        public string task_title { get; set; }
        public string task_desc { get; set; }
        public Byte done { get; set; }
        public string status { get; set; }
        public DateTime? updated { get; set; }
        public int? responsible_manager_id { get; set; }
        public string room { get; set; }
        public string responsible { get; set; }
        public string blob { get; set; }
    }
}