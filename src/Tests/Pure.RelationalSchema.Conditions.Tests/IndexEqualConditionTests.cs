using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Bool;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.ColumnType;
using ColumnRecord = Pure.RelationalSchema.Column.Column;
using IndexRecord = Pure.RelationalSchema.Index.Index;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Conditions.Tests;

public sealed record IndexEqualConditionTests
{
    [Fact]
    public void ReturnsTrueWhenIndexesAreStructurallyEqual()
    {
        IColumn column = new ColumnRecord(new String("Id"), new IntColumnType());

        IIndex first = new IndexRecord(new True(), [column]);
        IIndex second = new IndexRecord(new True(), [column]);

        IBool condition = new IndexEqualCondition(first, second);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ReturnsFalseWhenIndexesDiffer()
    {
        IColumn column = new ColumnRecord(new String("Id"), new IntColumnType());

        IIndex first = new IndexRecord(new True(), [column]);
        IIndex second = new IndexRecord(new False(), [column]);

        IBool condition = new IndexEqualCondition(first, second);

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void ReturnsTrueOnSingleIndex()
    {
        IColumn column = new ColumnRecord(new String("Id"), new IntColumnType());

        IIndex index = new IndexRecord(new True(), [column]);

        IBool condition = new IndexEqualCondition(index);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool condition = new IndexEqualCondition();

        _ = Assert.Throws<ArgumentException>(() => condition.BoolValue);
    }
}
