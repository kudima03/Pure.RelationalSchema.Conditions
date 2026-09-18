using Pure.Primitives.Abstractions.Bool;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.ColumnType;
using ColumnRecord = Pure.RelationalSchema.Column.Column;
using SchemaRecord = Pure.RelationalSchema.Schema.Schema;
using String = Pure.Primitives.String.String;
using TableRecord = Pure.RelationalSchema.Table.Table;

namespace Pure.RelationalSchema.Conditions.Tests;

public sealed record SchemaEqualConditionTests
{
    [Fact]
    public void ReturnsTrueWhenSchemasAreStructurallyEqual()
    {
        ITable table = new TableRecord(
            new String("Users"),
            [new ColumnRecord(new String("Id"), new IntColumnType())],
            []
        );

        ISchema first = new SchemaRecord(new String("Schema1"), [table], []);
        ISchema second = new SchemaRecord(new String("Schema1"), [table], []);

        IBool condition = new SchemaEqualCondition(first, second);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ReturnsFalseWhenSchemasDiffer()
    {
        ITable table = new TableRecord(
            new String("Users"),
            [new ColumnRecord(new String("Id"), new IntColumnType())],
            []
        );

        ISchema first = new SchemaRecord(new String("Schema1"), [table], []);
        ISchema second = new SchemaRecord(new String("Schema2"), [table], []);

        IBool condition = new SchemaEqualCondition(first, second);

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void ReturnsTrueOnSingleSchema()
    {
        ISchema schema = new SchemaRecord(new String("Schema1"), [], []);

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
