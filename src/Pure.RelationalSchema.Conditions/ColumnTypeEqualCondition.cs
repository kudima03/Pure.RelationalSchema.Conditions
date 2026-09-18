using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.String.Operations;
using Pure.RelationalSchema.Abstractions.ColumnType;

namespace Pure.RelationalSchema.Conditions;

public sealed record ColumnTypeEqualCondition : IBool
{
    private readonly IEnumerable<IColumnType> _columnTypes;

    public ColumnTypeEqualCondition(params IEnumerable<IColumnType> columnTypes)
    {
        _columnTypes = columnTypes;
    }

    public bool BoolValue =>
        new EqualCondition(_columnTypes.Select(columnType => columnType.Name)).BoolValue;
}
