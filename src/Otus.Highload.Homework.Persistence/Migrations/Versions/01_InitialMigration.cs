using System;
using FluentMigrator;

namespace Otus.Highload.Homework.Persistence.Migrations.Versions;

[Migration(1, "InitialMigration")]
public sealed class InitialMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider services) => @"
CREATE TYPE gender AS ENUM ('man', 'woman');

CREATE TABLE users
(
    id          BIGINT  GENERATED ALWAYS AS IDENTITY,
    password    TEXT    NOT NULL,
    name        TEXT    NOT NULL,
    surname     TEXT    NOT NULL,
    birth_date  DATE    NOT NULL,
    gender      gender  NOT NULL,
    interests   TEXT[]  NOT NULL,
    city        TEXT    NOT NULL
);
";

    protected override string GetDownSql(IServiceProvider services) => @"
DROP TABLE users;
DROP TYPE gender;
";
}
