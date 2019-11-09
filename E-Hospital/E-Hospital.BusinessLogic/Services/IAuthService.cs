using System.ServiceModel;
using E_Hospital.BLL.Data;

namespace E_Hospital.BLL.Services
{
    [ServiceContract]
    public interface IAuthService
    {
        [OperationContract]
        UserDto Login(string login, string password);
    }
}