using CargoPay.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace CargoPay.Application.Services
{
    public class UniversalFeesExchange : IPaymentFeeService
    {
        private readonly FeeUpdateService _feeUpdateService;

        public UniversalFeesExchange(FeeUpdateService feeUpdateService)
        {
            _feeUpdateService = feeUpdateService;
        }

        public Task<decimal> GetCurrentFeeRateAsync()
        {
            return Task.FromResult(_feeUpdateService.GetCurrentFeeRate());
        }
    }
}
