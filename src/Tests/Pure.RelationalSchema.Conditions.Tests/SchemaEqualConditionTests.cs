using Pure.Primitives.Abstractions.Bool;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.ColumnType;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Conditions.Tests;

using Column = Column.Column;
using Schema = Schema.Schema;
using Table = Table.Table;

public sealed record SchemaEqualConditionTests
{
    [Fact]
    public void ReturnsTrueWhenSchemasAreStructurallyEqual()
    {
        ITable table = new Table(
            new String("Users"),
            [new Column(new String("Id"), new IntColumnType())],
            []
        );

        ISchema first = new Schema(new String("Schema1"), [table], []);
        ISchema second = new Schema(new String("Schema1"), [table], []);

        IBool condition = new SchemaEqualCondition(first, second);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ReturnsFalseWhenSchemasDiffer()
    {
        ITable table = new Table(
            new String("Users"),
            [new Column(new String("Id"), new IntColumnType())],
            []
        );

        ISchema first = new Schema(new String("Schema1"), [table], []);
        ISchema second = new Schema(new String("Schema2"), [table], []);

        IBool condition = new SchemaEqualCondition(first, second);

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void ReturnsTrueOnSingleSchema()
    {
        ISchema schema = new Schema(new String("Schema1"), [], []);

        IBool condition = new SchemaEqualCondition(schema);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool condition = new SchemaEqualCondition();

        _ = Assert.Throws<ArgumentException>(() => condition.BoolValue);
    }
}
