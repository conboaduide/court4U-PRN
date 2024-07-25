using BusinessLogic.Service.Interface;
using DataAccess.Repository.Request;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using DataAccess.Entity.Data;

namespace BusinessLogic.Service
{
    public class MomoService: IMomoService
    {
        private readonly IOptions<MomoOptionModel> _options;
        private readonly IBillService _billService;
        private readonly IUserService _userService;
        public MomoService(IOptions<MomoOptionModel> options, IBillService billService, IUserService userService)
        {
            _options = options;
            _billService = billService;
            _userService = userService;
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }

        public async Task<MomoCreatePaymentResponseModel> CreateBookSlotPaymentAsync(RequestCreateOrderModel order)
        {
            //var bill = new Bill
            //{
            //    Method = "Momo",
            //    Price = order.Price,
            //    Type = order.Type,
            //};
            var returnUrl = "https://court4u.ddns.net/BookedSlot/PaymentCallBack";

            //var model = await _billService.Create(bill);
            //var model = await _orderService.CreateOrder(order);
            var user = await _userService.Get(order.UserId);
            //var course = await _unitOfWork.CourseRepository.GetSingleById(model.CourseId);
            var orderInfo = "Khách hàng: " + user.FullName + ". Nội dung: Mua hàng tại court4u ";
            var orderId = order.OrderId + DateTime.Now;
            var rawData =
                $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={order.OrderId}&amount={order.Price}&orderId={orderId}&orderInfo={orderInfo}&returnUrl={returnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData=";

            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

            var client = new RestClient(_options.Value.MomoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            // Create an object representing the request data
            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = _options.Value.RequestType,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = returnUrl,
                orderId = orderId,
                amount = order.Price.ToString(),
                orderInfo = orderInfo,
                requestId = order.OrderId,
                extraData = "",
                signature = signature
            };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
        }

        public async Task<MomoCreatePaymentResponseModel> CreateMemberSubscriptionPaymentAsync(RequestCreateOrderModel order, string subscriptionId)
        {
            var bill = new Bill
            {
                Method = "Momo",
                Price = order.Price,
                Type = order.Type,
            };
            var returnUrl = "https://court4u.ddns.net/MemberSubscription/PaymentCallBack";

            var model = await _billService.Create(bill);
            var user = await _userService.Get(order.UserId);
            var orderInfo = "Khách hàng: " + user.FullName + ". Nội dung: Mua gói hội viên tại court4u ";
            var orderId = order.OrderId + DateTime.Now;
            var extraData = $"{subscriptionId};{model.Id};";
            var rawData =
                $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={order.OrderId}&amount={order.Price}&orderId={orderId}&orderInfo={orderInfo}&returnUrl={returnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData={extraData}";

            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

            var client = new RestClient(_options.Value.MomoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            // Create an object representing the request data
            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = _options.Value.RequestType,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = returnUrl,
                orderId = orderId,
                amount = order.Price.ToString(),
                orderInfo = orderInfo,
                requestId = order.OrderId,
                extraData = extraData,
                signature = signature
            };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
        }

        public MomoExecuteResponseModel PaymentExecuteAsync(IQueryable<KeyValuePair<string, string>> collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            return new MomoExecuteResponseModel()
            {
                Amount = amount,
                OrderId = orderId,
                OrderInfo = orderInfo
            };
        }
    }
}
