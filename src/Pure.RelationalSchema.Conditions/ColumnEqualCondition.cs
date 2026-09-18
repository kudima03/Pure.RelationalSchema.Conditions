using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Bool.Operations;
using Pure.RelationalSchema.Abstractions.Column;
using StringEqualCondition = Pure.Primitives.String.Operations.EqualCondition;

namespace Pure.RelationalSchema.Conditions;

public sealed record ColumnEqualCondition : IBool
{
    private readonly IEnumerable<IColumn> _columns;

    public ColumnEqualCondition(params IEnumerable<IColumn> columns)
    {
        _columns = columns;
    }

    public bool BoolValue =>
        new And(
            new StringEqualCondition(_columns.Select(column => column.Name)),
            new ColumnTypeEqualCondition(_columns.Select(column => column.Type))
        ).BoolValue;
}
