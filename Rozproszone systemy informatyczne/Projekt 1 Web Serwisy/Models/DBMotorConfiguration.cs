using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Projekt_1_Web_Serwisy.Models;

public class DBMotorConfiguration : IEntityTypeConfiguration<DBMotor>
{
    public void Configure(EntityTypeBuilder<DBMotor> builder)
    {
        builder.HasData(new DBMotor
        {
            Id = 1,
            Name = "a",
            SpriteURL = null,
            RentPrice = 1000,
            RentTo = null
        },
        new DBMotor
        {
            Id = 2,
            Name = "b",
            SpriteURL = null,
            RentPrice = 1000,
            RentTo = null
        },
        new DBMotor
        {
            Id = 3,
            Name = "c",
            SpriteURL = null,
            RentPrice = 1000,
            RentTo = null
        },
        new DBMotor
        {
            Id = 4,
            Name = "d",
            SpriteURL = null,
            RentPrice = 1000,
            RentTo = null
        },
        new DBMotor
        {
            Id = 5,
            Name = "e",
            SpriteURL = null,
            RentPrice = 1000,
            RentTo = null
        });
    }
}
