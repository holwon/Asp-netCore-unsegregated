using System;

namespace Auth.JWT.Models
{
    public class UserConstants
    {
        public static List<User> Users = new()
        {
            new()
            {UserName="John_admin",Email="John_admin@example.com",
            Password="password",GivenName="John",Surname="Bryant",Role="Administrator"},
            new()
            {UserName="elyse_seller",Email="elyse_seller@example.com",
            Password="password",GivenName="Elyse",Surname="Lamp",Role="Seller"}
        };
    }
}
