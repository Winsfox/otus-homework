using System;
using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace Otus.Highload.Homework.Persistence.Migrations;

public abstract class SqlMigration : IMigration
{
    public void GetUpExpressions(IMigrationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        context.Expressions.Add(new ExecuteSqlStatementExpression { SqlStatement = GetUpSql(context.ServiceProvider) });
    }

    public void GetDownExpressions(IMigrationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        context.Expressions.Add(new ExecuteSqlStatementExpression { SqlStatement = GetDownSql(context.ServiceProvider) });
    }

    protected abstract string GetUpSql(IServiceProvider services);
    protected abstract string GetDownSql(IServiceProvider services);

    object IMigration.ApplicationContext => throw new NotSupportedException();
    string IMigration.ConnectionString => throw new NotSupportedException();
}
