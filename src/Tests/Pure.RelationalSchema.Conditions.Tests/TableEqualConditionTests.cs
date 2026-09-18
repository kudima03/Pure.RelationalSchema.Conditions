using Pure.Primitives.Abstractions.Bool;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.ColumnType;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Conditions.Tests;

using Column = Column.Column;
using Table = Table.Table;

public sealed record TableEqualConditionTests
{
    [Fact]
    public void ReturnsTrueWhenTablesAreStructurallyEqual()
    {
        ITable first = new Table(
            new String("Users"),
            [new Column(new String("Id"), new IntColumnType())],
            []
        );

        ITable second = new Table(
            new String("Users"),
            [new Column(new String("Id"), new IntColumnType())],
            []
        );

        IBool condition = new TableEqualCondition(first, second);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ReturnsFalseWhenTablesDiffer()
    {
        ITable first = new Table(
            new String("Users"),
            [new Column(new String("Id"), new IntColumnType())],
            []
        );

        ITable second = new Table(
            new String("Orders"),
            [new Column(new String("Id"), new IntColumnType())],
            []
        );

        IBool condition = new TableEqualCondition(first, second);

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void ReturnsTrueOnSingleTable()
    {
        ITable table = new Table(new String("Users"), [], []);

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
