using System;
using System.Collections.Generic;
using System.Text;
using Lands.Domain;
namespace Lands.Helpers
{
    using System;
    using Domain;
    using Models;

    public static class Converter
    {
        public static UserLocal ToUserLocal(User user)
        {
            return new UserLocal
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName= user.LastName,
                Telephone = user.Telephone,
                UserId = user.UserId,
                UserTypeId = user.UserTypeId,
            };
        }
    }
}
