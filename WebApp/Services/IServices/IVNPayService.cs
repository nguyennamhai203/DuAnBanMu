using Shop_Models.ViewModels.VNPay;

namespace WebApp.Services.IServices
{
    public interface IVNPayService
    {
        string CreatePaymentUrl(HttpContext context, VNPaymentRequestModel requestModel);
        VnPaymentResponseModel PaymentExcute(IQueryCollection collections);
    }
}
