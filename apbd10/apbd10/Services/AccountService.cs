using apbd10.Contexts;
using apbd10.Exceptions;
using apbd10.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Services;


public interface IAccountService
{
    Task<GetAccountResponseModel> GetAccountByIdAsync(int id);
}



public class AccountService(DatabaseContext context) :IAccountService
{
    public async Task<GetAccountResponseModel> GetAccountByIdAsync(int id)
    {
        var res = await context.Accounts
            .Where(e => e.AccountId == id)
            .Select(e => new GetAccountResponseModel
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Role = e.Role.RoleName,
                //shopping cart get response

            }).FirstOrDefaultAsync();

        if (res is null)
        {
            throw new NotFoundException($"Account with id:{id} does not exist");
        }

        return res;
    }
}