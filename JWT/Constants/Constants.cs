using JWT.Models;
using System.Net.Mail;

namespace JWT.Constants
{
    public class Constants
    {
        public static List<UserModel> user = new List<UserModel>
        {
           new UserModel(){UserName="JD123456",Pass="J123456",Rol="Administrador",EmailAddress="J@gmail.com",FirtsName="Josue",LastName="Cruz"},

           new UserModel(){ UserName="DJ",Pass="D123",Rol="Vendedor",EmailAddress="D@gmail.com",FirtsName="Luis",LastName="Lopez"}
        };

    }
}
