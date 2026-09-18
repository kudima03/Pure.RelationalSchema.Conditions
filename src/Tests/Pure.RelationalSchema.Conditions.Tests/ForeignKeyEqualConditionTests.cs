using Pure.Primitives.Abstractions.Bool;
using Pure.RelationalSchema.Abstractions.ForeignKey;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.ColumnType;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Conditions.Tests;

using Column = Column.Column;
using ForeignKey = ForeignKey.ForeignKey;
using Table = Table.Table;

public sealed record ForeignKeyEqualConditionTests
{
    [Fact]
    public void ReturnsTrueWhenForeignKeysAreStructurallyEqual()
    {
        ITable orders = new Table(
            new String("Orders"),
            [new Column(new String("UserId"), new IntColumnType())],
            []
        );

        ITable users = new Table(
            new String("Users"),
            [new Column(new String("Id"), new IntColumnType())],
            []
        );

        IForeignKey first = new ForeignKey(orders, orders.Columns, users, users.Columns);
        IForeignKey second = new ForeignKey(orders, orders.Columns, users, users.Columns);

        IBool condition = new ForeignKeyEqualCondition(first, second);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ReturnsFalseWhenForeignKeysDiffer()
    {
        ITable orders = new Table(
            new String("Orders"),
            [new Column(new String("UserId"), new IntColumnType())],
            []
        );

        ITable payments = new Table(
            new String("Payments"),
            [new Column(new String("UserId"), new IntColumnType())],
            []
        );

        ITable users = new Table(
            new String("Users"),
            [new Column(new String("Id"), new IntColumnType())],
            []
        );

        IForeignKey first = new ForeignKey(orders, orders.Columns, users, users.Columns);
        IForeignKey second = new ForeignKey(
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
        ITable orders = new Table(
            new String("Orders"),
            [new Column(new String("UserId"), new IntColumnType())],
            []
        );

        ITable users = new Table(
            new String("Users"),
            [new Column(new String("Id"), new IntColumnType())],
            []
        );

        IForeignKey foreignKey = new ForeignKey(
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
