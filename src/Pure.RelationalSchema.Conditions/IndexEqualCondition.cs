using Pure.Linq.Conditions;
using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Bool.Operations;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;

namespace Pure.RelationalSchema.Conditions;

public sealed record IndexEqualCondition : IBool
{
    private readonly IEnumerable<IIndex> _indexes;

    public IndexEqualCondition(params IEnumerable<IIndex> indexes)
    {
        _indexes = indexes;
    }

    public bool BoolValue =>
        new And(
            new EqualCondition(_indexes.Select(index => index.IsUnique)),
            new EqualCondition<IColumn>(
                (left, right) => new ColumnEqualCondition(left, right),
                _indexes.Select(index => index.Columns)
            )
        ).BoolValue;
}
