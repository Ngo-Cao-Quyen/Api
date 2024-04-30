using Api.Dtos.Account;
using static Api.Dtos.Account.ServiceResponses;

namespace Api.Interfaces
{
    public interface IAccountRepository
    {
        Task<GeneralResponse> CreateAccount(RegisterDto registerDto);
        Task<LoginResponse> LoginAccount(LoginDto loginDto);
    }
}
