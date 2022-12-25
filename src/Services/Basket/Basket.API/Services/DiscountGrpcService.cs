using Discount.GRPC.Protos;

namespace Basket.API.Services
{
    /// <summary>
    /// Encapsulate discount proto service on basket.api
    /// </summary>
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetDiscount(string productName) //dto olabilir
        {
            var request = new GetDiscountRequest() { ProductName = productName };
            return await _discountProtoServiceClient.GetDiscountAsync(request);
        }  
    }
}
