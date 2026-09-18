using Pure.Primitives.Abstractions.Bool;
using Pure.RelationalSchema.Abstractions.ForeignKey;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.ColumnType;
using ColumnRecord = Pure.RelationalSchema.Column.Column;
using ForeignKeyRecord = Pure.RelationalSchema.ForeignKey.ForeignKey;
using String = Pure.Primitives.String.String;
using TableRecord = Pure.RelationalSchema.Table.Table;

namespace Pure.RelationalSchema.Conditions.Tests;

public sealed record ForeignKeyEqualConditionTests
{
    [Fact]
    public void ReturnsTrueWhenForeignKeysAreStructurallyEqual()
    {
        ITable orders = new TableRecord(
            new String("Orders"),
            [new ColumnRecord(new String("UserId"), new IntColumnType())],
            []
        );

        ITable users = new TableRecord(
            new String("Users"),
            [new ColumnRecord(new String("Id"), new IntColumnType())],
            []
        );

        IForeignKey first = new ForeignKeyRecord(
            orders,
            orders.Columns,
            users,
            users.Columns
        );
        IForeignKey second = new ForeignKeyRecord(
            orders,
            orders.Columns,
            users,
            users.Columns
        );

        IBool condition = new ForeignKeyEqualCondition(first, second);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ReturnsFalseWhenForeignKeysDiffer()
    {
        ITable orders = new TableRecord(
            new String("Orders"),
            [new ColumnRecord(new String("UserId"), new IntColumnType())],
            []
        );

        ITable payments = new TableRecord(
            new String("Payments"),
            [new ColumnRecord(new String("UserId"), new IntColumnType())],
            []
        );

        ITable users = new TableRecord(
            new String("Users"),
            [new ColumnRecord(new String("Id"), new IntColumnType())],
            []
        );

        IForeignKey first = new ForeignKeyRecord(
            orders,
            orders.Columns,
            users,
            users.Columns
        );
        IForeignKey second = new ForeignKeyRecord(
            payments,
            payments.Columns,
            users,
            users.Columns
        );

        IBool condition = new ForeignKeyEqualCondition(first, second);

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void ReturnsTrueOnSingleForeignKey()
    {
        ITable orders = new TableRecord(
            new String("Orders"),
            [new ColumnRecord(new String("UserId"), new IntColumnType())],
            []
        );

        ITable users = new TableRecord(
            new String("Users"),
            [new ColumnRecord(new String("Id"), new IntColumnType())],
            []
        );

        IForeignKey foreignKey = new ForeignKeyRecord(
            orders,
            orders.Columns,
            users,
            users.Columns
        );

        IBool condition = new ForeignKeyEqualCondition(foreignKey);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool condition = new ForeignKeyEqualCondition();

        _ = Assert.Throws<ArgumentException>(() => condition.BoolValue);
    }
}
