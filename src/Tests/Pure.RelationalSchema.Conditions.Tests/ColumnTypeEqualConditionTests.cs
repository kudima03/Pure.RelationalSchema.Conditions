using Pure.Primitives.Abstractions.Bool;
using Pure.RelationalSchema.Abstractions.ColumnType;
using Pure.RelationalSchema.ColumnType;

namespace Pure.RelationalSchema.Conditions.Tests;

public sealed record ColumnTypeEqualConditionTests
{
    [Fact]
    public void ReturnsTrueWhenColumnTypesAreStructurallyEqual()
    {
        IColumnType first = new IntColumnType();
        IColumnType second = new IntColumnType();

        IBool condition = new ColumnTypeEqualCondition(first, second);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ReturnsFalseWhenColumnTypesDiffer()
    {
        IColumnType first = new IntColumnType();
        IColumnType second = new StringColumnType();

        IBool condition = new ColumnTypeEqualCondition(first, second);

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void ReturnsTrueOnSingleColumnType()
    {
        IColumnType columnType = new IntColumnType();

        IBool condition = new ColumnTypeEqualCondition(columnType);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool condition = new ColumnTypeEqualCondition();

        _ = Assert.Throws<ArgumentException>(() => condition.BoolValue);
    }
}
