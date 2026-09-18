using Pure.Primitives.Abstractions.Bool;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.ColumnType;
using ColumnRecord = Pure.RelationalSchema.Column.Column;
using String = Pure.Primitives.String.String;
using TableRecord = Pure.RelationalSchema.Table.Table;

namespace Pure.RelationalSchema.Conditions.Tests;

public sealed record TableEqualConditionTests
{
    [Fact]
    public void ReturnsTrueWhenTablesAreStructurallyEqual()
    {
        ITable first = new TableRecord(
            new String("Users"),
            [new ColumnRecord(new String("Id"), new IntColumnType())],
            []
        );

        ITable second = new TableRecord(
            new String("Users"),
            [new ColumnRecord(new String("Id"), new IntColumnType())],
            []
        );

        IBool condition = new TableEqualCondition(first, second);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ReturnsFalseWhenTablesDiffer()
    {
        ITable first = new TableRecord(
            new String("Users"),
            [new ColumnRecord(new String("Id"), new IntColumnType())],
            []
        );

        ITable second = new TableRecord(
            new String("Orders"),
            [new ColumnRecord(new String("Id"), new IntColumnType())],
            []
        );

        IBool condition = new TableEqualCondition(first, second);

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void ReturnsTrueOnSingleTable()
    {
        ITable table = new TableRecord(new String("Users"), [], []);

        IBool condition = new TableEqualCondition(table);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool condition = new TableEqualCondition();

        _ = Assert.Throws<ArgumentException>(() => condition.BoolValue);
    }
}
