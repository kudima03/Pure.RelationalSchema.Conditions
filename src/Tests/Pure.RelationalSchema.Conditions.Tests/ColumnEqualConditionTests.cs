using Pure.Primitives.Abstractions.Bool;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Conditions.Tests;

using Column = Column.Column;

public sealed record ColumnEqualConditionTests
{
    [Fact]
    public void ReturnsTrueWhenColumnsAreStructurallyEqual()
    {
        IColumn first = new Column(new String("Id"), new IntColumnType());
        IColumn second = new Column(new String("Id"), new IntColumnType());

        IBool condition = new ColumnEqualCondition(first, second);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ReturnsFalseWhenColumnsDiffer()
    {
        IColumn first = new Column(new String("Id"), new IntColumnType());
        IColumn second = new Column(new String("Id"), new StringColumnType());

        IBool condition = new ColumnEqualCondition(first, second);

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void ReturnsTrueOnSingleColumn()
    {
        IColumn column = new Column(new String("Id"), new IntColumnType());

        IBool condition = new ColumnEqualCondition(column);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool condition = new ColumnEqualCondition();

        _ = Assert.Throws<ArgumentException>(() => condition.BoolValue);
    }
}
