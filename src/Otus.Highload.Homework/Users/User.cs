using System;

namespace Otus.Highload.Homework.Users;

public sealed record User(
    long Id,
    string Password,
    string Name,
    string Surname,
    DateOnly BirthDate,
    Gender Gender,
    string[] Interests,
    string City);
