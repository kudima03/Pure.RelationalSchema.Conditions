using Pure.Linq.Conditions;
using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Bool.Operations;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.ForeignKey;

namespace Pure.RelationalSchema.Conditions;

public sealed record ForeignKeyEqualCondition : IBool
{
    private readonly IEnumerable<IForeignKey> _foreignKeys;

    public ForeignKeyEqualCondition(params IEnumerable<IForeignKey> foreignKeys)
    {
        _foreignKeys = foreignKeys;
    }

    public bool BoolValue =>
        new And(
            new TableEqualCondition(
                _foreignKeys.Select(foreignKey => foreignKey.ReferencingTable)
            ),
            new EqualCondition<IColumn>(
                (left, right) => new ColumnEqualCondition(left, right),
                _foreignKeys.Select(foreignKey => foreignKey.ReferencingColumns)
            ),
            new TableEqualCondition(
                _foreignKeys.Select(foreignKey => foreignKey.ReferencedTable)
            ),
            new EqualCondition<IColumn>(
                (left, right) => new ColumnEqualCondition(left, right),
                _foreignKeys.Select(foreignKey => foreignKey.ReferencedColumns)
            )
        ).BoolValue;
}
