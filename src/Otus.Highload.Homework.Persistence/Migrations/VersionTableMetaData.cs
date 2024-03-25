using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.Options;

namespace Otus.Highload.Homework.Persistence.Migrations;

[VersionTableMetaData]
public class VersionTableMetaData : DefaultVersionTableMetaData
{
    public VersionTableMetaData(IConventionSet conventionSet, IOptions<RunnerOptions> runnerOptions)
        : base(conventionSet, runnerOptions)
    {
    }

    public override string TableName => "version_info";

    public override string ColumnName => "version";

    public override string UniqueIndexName => "version_info_version_idx";

    public override string AppliedOnColumnName => "applied_on";

    public override string DescriptionColumnName => "description";
}
