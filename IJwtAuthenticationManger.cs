namespace WebApiWithToken;
public interface IJwtAuthenticationManger{

    string Authenticate(string userName, string password);

}