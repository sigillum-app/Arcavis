using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.Options;

namespace Sigillum.Arcavis.Database.Migrations.Common;

[VersionTableMetaData]
public class CustomVersionTableMetaData : DefaultVersionTableMetaData
{
    public CustomVersionTableMetaData(
        IConventionSet conventionSet,
        IOptions<RunnerOptions> runnerOptions) : base(conventionSet, runnerOptions)
    { }

    public override string TableName => "Z_VERSION_INFO";
    public override string ColumnName => "VERSION";
    public override string DescriptionColumnName => "DESCRIPTION";
    public override string AppliedOnColumnName => "APPLIED_ON";
}