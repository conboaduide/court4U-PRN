using DataAccess.Repository.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Interface
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(RequestCreateOrderModel model);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryable<KeyValuePair<string, string>> collection);
    }
}
