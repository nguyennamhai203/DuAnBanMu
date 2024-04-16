using Newtonsoft.Json;
using Shop_Models.Dto;

namespace WebApp.Services
{
    public static class SessionService
    {
        // 1 Đọc dl từ session =>  trả về 1 list
        public static List<GioHangChiTietViewModel> GetObjFromSession(ISession session, string key)
        {
            string jsonData = session.GetString(key);
            if (jsonData == null)
            {

                return new List<GioHangChiTietViewModel>();

            }
            else
            {
                var CartItemDtos = JsonConvert.DeserializeObject<List<GioHangChiTietViewModel>>(jsonData);
                return CartItemDtos;
            }
        }
        // 2. Ghi đè dữ liệu vào session từ 1 list
        public static void SetObjToSession(ISession session, string key, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);// chuyển đổi dữ liệu jsonData
            session.SetString(key, jsonData);// Ghi đè vào Session
        }
        // 3. Kiểm tra xem đối tượng có nằm trong 1 list hay không
        public static bool CheckObjInList(string code, List<GioHangChiTietViewModel> CartItemDtos)
        {
            return CartItemDtos.Any(x => x.MaSPCT == code);
            // Any : một trong những , All : Tất cả
        }

        public static List<GioHangChiTietViewModel> GetFromSession(ISession session, string key)
        {
            string jsonData = session.GetString(key);
            if (jsonData == null)
            {

                return new List<GioHangChiTietViewModel>();
            }
            else
            {

                var ProductDetailViews = JsonConvert.DeserializeObject<List<GioHangChiTietViewModel>>(jsonData);
                return ProductDetailViews;
            }
        }
        // 2. Ghi đè dữ liệu vào session từ 1 list
        public static void SetToSession(ISession session, string key, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);// chuyển đổi dữ liệu jsonData
            session.SetString(key, jsonData);// Ghi đè vào Session
        }
    }
}
