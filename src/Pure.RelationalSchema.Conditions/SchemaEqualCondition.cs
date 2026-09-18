using Pure.Linq.Conditions;
using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Bool.Operations;
using Pure.RelationalSchema.Abstractions.ForeignKey;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.Abstractions.Table;
using StringEqualCondition = Pure.Primitives.String.Operations.EqualCondition;

namespace Pure.RelationalSchema.Conditions;

public sealed record SchemaEqualCondition : IBool
{
    private readonly IEnumerable<ISchema> _schemas;

    public SchemaEqualCondition(params IEnumerable<ISchema> schemas)
    {
        _schemas = schemas;
    }

    public bool BoolValue =>
        new And(
            new StringEqualCondition(_schemas.Select(schema => schema.Name)),
            new EqualCondition<ITable>(
                (left, right) => new TableEqualCondition(left, right),
                _schemas.Select(schema => schema.Tables)
            ),
            new EqualCondition<IForeignKey>(
                (left, right) => new ForeignKeyEqualCondition(left, right),
                _schemas.Select(schema => schema.ForeignKeys)
            )
        ).BoolValue;
}
