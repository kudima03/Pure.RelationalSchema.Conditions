using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Bool;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.ColumnType;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Conditions.Tests;

using Column = Column.Column;
using Index = Index.Index;

public sealed record IndexEqualConditionTests
{
    [Fact]
    public void ReturnsTrueWhenIndexesAreStructurallyEqual()
    {
        IColumn column = new Column(new String("Id"), new IntColumnType());

        IIndex first = new Index(new True(), [column]);
        IIndex second = new Index(new True(), [column]);

        IBool condition = new IndexEqualCondition(first, second);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ReturnsFalseWhenIndexesDiffer()
    {
        IColumn column = new Column(new String("Id"), new IntColumnType());

        IIndex first = new Index(new True(), [column]);
        IIndex second = new Index(new False(), [column]);

        IBool condition = new IndexEqualCondition(first, second);

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void ReturnsTrueOnSingleIndex()
    {
        IColumn column = new Column(new String("Id"), new IntColumnType());

        IIndex index = new Index(new True(), [column]);

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
