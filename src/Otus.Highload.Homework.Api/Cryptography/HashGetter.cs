using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Otus.Highload.Homework.Api.Cryptography;

public static class HashGetter
{
    private static readonly byte[] s_salt = "highload"u8.ToArray();

    public static string Get(string input)
    {
        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: input,
            salt: s_salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hash;
    }
}
