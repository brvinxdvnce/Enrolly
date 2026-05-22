using Enrolly.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Accounts.Infrastructure.Database.Seeders;

public static class CitizenshipSeederExtension
{
    public static ModelBuilder SeedCitizenships(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Citizenship>().HasData(
            new Citizenship { Id = 1, Name = "Россия", IsoCode = "RU" },
            new Citizenship { Id = 2, Name = "США", IsoCode = "US" },
            new Citizenship { Id = 3, Name = "Германия", IsoCode = "DE" },
            new Citizenship { Id = 4, Name = "Франция", IsoCode = "FR" },
            new Citizenship { Id = 5, Name = "Великобритания", IsoCode = "GB" },
            new Citizenship { Id = 6, Name = "Италия", IsoCode = "IT" },
            new Citizenship { Id = 7, Name = "Испания", IsoCode = "ES" },
            new Citizenship { Id = 8, Name = "Китай", IsoCode = "CN" },
            new Citizenship { Id = 9, Name = "Япония", IsoCode = "JP" },
            new Citizenship { Id = 10, Name = "Индия", IsoCode = "IN" },
            new Citizenship { Id = 11, Name = "Бразилия", IsoCode = "BR" },
            new Citizenship { Id = 12, Name = "Канада", IsoCode = "CA" },
            new Citizenship { Id = 13, Name = "Австралия", IsoCode = "AU" },
            new Citizenship { Id = 14, Name = "Мексика", IsoCode = "MX" },
            new Citizenship { Id = 15, Name = "Южная Корея", IsoCode = "KR" },
            new Citizenship { Id = 16, Name = "Нидерланды", IsoCode = "NL" },
            new Citizenship { Id = 17, Name = "Швеция", IsoCode = "SE" },
            new Citizenship { Id = 18, Name = "Норвегия", IsoCode = "NO" },
            new Citizenship { Id = 19, Name = "Швейцария", IsoCode = "CH" },
            new Citizenship { Id = 20, Name = "Бельгия", IsoCode = "BE" },
            new Citizenship { Id = 21, Name = "Австрия", IsoCode = "AT" },
            new Citizenship { Id = 22, Name = "Польша", IsoCode = "PL" },
            new Citizenship { Id = 23, Name = "Турция", IsoCode = "TR" },
            new Citizenship { Id = 24, Name = "Египет", IsoCode = "EG" },
            new Citizenship { Id = 25, Name = "Израиль", IsoCode = "IL" },
            new Citizenship { Id = 26, Name = "ОАЭ", IsoCode = "AE" },
            new Citizenship { Id = 27, Name = "Саудовская Аравия", IsoCode = "SA" },
            new Citizenship { Id = 28, Name = "ЮАР", IsoCode = "ZA" },
            new Citizenship { Id = 29, Name = "Аргентина", IsoCode = "AR" },
            new Citizenship { Id = 30, Name = "Чили", IsoCode = "CL" },
            new Citizenship { Id = 31, Name = "Колумбия", IsoCode = "CO" },
            new Citizenship { Id = 32, Name = "Перу", IsoCode = "PE" },
            new Citizenship { Id = 33, Name = "Венесуэла", IsoCode = "VE" },
            new Citizenship { Id = 34, Name = "Малайзия", IsoCode = "MY" },
            new Citizenship { Id = 35, Name = "Сингапур", IsoCode = "SG" },
            new Citizenship { Id = 36, Name = "Таиланд", IsoCode = "TH" },
            new Citizenship { Id = 37, Name = "Вьетнам", IsoCode = "VN" },
            new Citizenship { Id = 38, Name = "Индонезия", IsoCode = "ID" },
            new Citizenship { Id = 39, Name = "Филиппины", IsoCode = "PH" },
            new Citizenship { Id = 40, Name = "Украина", IsoCode = "UA" },
            new Citizenship { Id = 41, Name = "Казахстан", IsoCode = "KZ" },
            new Citizenship { Id = 42, Name = "Беларусь", IsoCode = "BY" },
            new Citizenship { Id = 43, Name = "Финляндия", IsoCode = "FI" },
            new Citizenship { Id = 44, Name = "Дания", IsoCode = "DK" },
            new Citizenship { Id = 45, Name = "Португалия", IsoCode = "PT" },
            new Citizenship { Id = 46, Name = "Греция", IsoCode = "GR" },
            new Citizenship { Id = 47, Name = "Ирландия", IsoCode = "IE" },
            new Citizenship { Id = 48, Name = "Чехия", IsoCode = "CZ" },
            new Citizenship { Id = 49, Name = "Венгрия", IsoCode = "HU" },
            new Citizenship { Id = 50, Name = "Румыния", IsoCode = "RO" }
        );
        
        return modelBuilder;
    }
}