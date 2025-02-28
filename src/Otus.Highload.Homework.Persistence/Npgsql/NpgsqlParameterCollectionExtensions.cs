using System;
using System.Data.Common;
using Npgsql;
using NpgsqlTypes;

namespace Otus.Highload.Homework.Persistence.Npgsql;

public static class NpgsqlParameterCollectionExtensions
{
    public static void Add<T>(this DbParameterCollection parameters, string name, T? value) =>
        parameters.Add(
            new NpgsqlParameter<T>
            {
                ParameterName = name,
                TypedValue = value
            });

    public static void Add<T>(this DbParameterCollection parameters, string name, T? value, NpgsqlDbType npgsqlDbType) =>
        parameters.Add(
            new NpgsqlParameter<T>(name, npgsqlDbType)
            {
                TypedValue = value
            });

    public static void Add<T>(this DbParameterCollection parameters, string name, T? value)
        where T : struct =>
        parameters.Add(
            value.HasValue
                ? new NpgsqlParameter<T>(name, value.Value)
                : new NpgsqlParameter<DBNull>(name, DBNull.Value));

    public static void Add<T>(this DbParameterCollection parameters, string name, T? value, NpgsqlDbType npgsqlDbType)
        where T : struct =>
        parameters.Add(
            value.HasValue
                ? new NpgsqlParameter<T>(name, npgsqlDbType) { TypedValue = value.Value }
                : new NpgsqlParameter<DBNull>(name, npgsqlDbType) { TypedValue = DBNull.Value });
}
