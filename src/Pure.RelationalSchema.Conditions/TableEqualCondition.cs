using Pure.Linq.Conditions;
using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Bool.Operations;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Index;
using Pure.RelationalSchema.Abstractions.Table;
using StringEqualCondition = Pure.Primitives.String.Operations.EqualCondition;

namespace Pure.RelationalSchema.Conditions;

public sealed record TableEqualCondition : IBool
{
    private readonly IEnumerable<ITable> _tables;

    public TableEqualCondition(params IEnumerable<ITable> tables)
    {
        _tables = tables;
    }

    public bool BoolValue =>
        new And(
            new StringEqualCondition(_tables.Select(table => table.Name)),
            new EqualCondition<IColumn>(
                (left, right) => new ColumnEqualCondition(left, right),
                _tables.Select(table => table.Columns)
            ),
            new EqualCondition<IIndex>(
                (left, right) => new IndexEqualCondition(left, right),
                _tables.Select(table => table.Indexes)
            )
        ).BoolValue;
}
