using System;
using Otus.Highload.Homework.Users;

namespace Otus.Highload.Homework.Api.Controllers.Users.Contracts;

public record UserDto(
    long Id,
    string Name,
    string Surname,
    DateOnly BirthDate,
    Gender Gender,
    string[] Interests,
    string City);
