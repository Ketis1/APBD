using apbd10.Models;

namespace apbd10.ResponseModels;

public class GetAccountResponseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
}