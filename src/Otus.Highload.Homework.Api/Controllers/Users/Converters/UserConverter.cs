using Otus.Highload.Homework.Api.Controllers.Users.Contracts;
using Otus.Highload.Homework.Api.Cryptography;
using Otus.Highload.Homework.Users;

namespace Otus.Highload.Homework.Api.Controllers.Users.Converters;

public static class UserConverter
{
    public static User ToUser(this RegisterReq req) =>
        new(Id: 0, Password: HashGetter.Get(req.Password), req.Name, req.Surname, req.BirthDate, req.Gender, req.Interests, req.City);

    public static UserDto ToUserDto(this User user) =>
        new(user.Id, user.Name, user.Surname, user.BirthDate, user.Gender, user.Interests, user.City);
}
