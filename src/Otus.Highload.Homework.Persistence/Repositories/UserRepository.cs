using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Npgsql;
using Otus.Highload.Homework.Persistence.Npgsql;
using Otus.Highload.Homework.Persistence.Options;
using Otus.Highload.Homework.Users;

namespace Otus.Highload.Homework.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IOptions<DbOptions> dbOptions)
    {
        _connectionString = dbOptions.Value.ConnectionString;
    }

    public async Task<long> AddAsync(User user, CancellationToken cancellationToken)
    {
        const string Query = """
                             INSERT INTO users(password, name, surname, birth_date, gender, interests, city)
                             VALUES (:password,
                                     :name,
                                     :surname,
                                     :birth_date,
                                     :gender,
                                     :interests,
                                     :city)
                             RETURNING id;
                             """;

        await using var connection = new NpgsqlConnection(_connectionString);
        await using var command = new NpgsqlCommand(Query, connection)
        {
            Parameters =
            {
                { "password", user.Password },
                { "name", user.Name },
                { "surname", user.Surname },
                { "birth_date", user.BirthDate },
                { "gender", user.Gender },
                { "interests", user.Interests },
                { "city", user.City },
            }
        };

        await connection.OpenAsync(cancellationToken);

        var id = (long)(await command.ExecuteScalarAsync(cancellationToken))!;

        return id;
    }

    public async Task<User?> GetAsync(long id, CancellationToken cancellationToken)
    {
        const string Query = """
                             SELECT id, password, name, surname, birth_date, gender, interests, city
                             FROM users
                             WHERE id = :id;
                             """;

        await using var connection = new NpgsqlConnection(_connectionString);
        await using var command = new NpgsqlCommand(Query, connection)
        {
            Parameters =
            {
                { "id", id }
            }
        };
        await connection.OpenAsync(cancellationToken);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var idOrdinal = reader.GetOrdinal("id");
        var passwordOrdinal = reader.GetOrdinal("password");
        var nameOrdinal = reader.GetOrdinal("name");
        var surnameOrdinal = reader.GetOrdinal("surname");
        var birthDateOrdinal = reader.GetOrdinal("birth_date");
        var genderOrdinal = reader.GetOrdinal("gender");
        var interestsOrdinal = reader.GetOrdinal("interests");
        var cityOrdinal = reader.GetOrdinal("city");

        var user = await reader.ReadAsync(cancellationToken)
            ? new User(
                reader.GetInt64(idOrdinal),
                reader.GetString(passwordOrdinal),
                reader.GetString(nameOrdinal),
                reader.GetString(surnameOrdinal),
                DateOnly.FromDateTime(reader.GetDateTime(birthDateOrdinal)),
                await reader.GetFieldValueAsync<Gender>(genderOrdinal, cancellationToken),
                await reader.GetFieldValueAsync<string[]>(interestsOrdinal, cancellationToken),
                reader.GetString(cityOrdinal))
            : null;

        return user;
    }
}
