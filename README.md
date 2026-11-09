# MotoGrid

[![CI](https://github.com/GeorgiZhisov/MotoSocialMedia/actions/workflows/ci.yml/badge.svg)](https://github.com/GeorgiZhisov/MotoSocialMedia/actions/workflows/ci.yml)

**MotoGrid** е уеб платформа, която обединява социална мрежа и специализиран онлайн пазар за мотоциклетисти.

Дипломен проект – ЧПГДН „СофтУни Будител“, професия „Приложен програмист“, специалност „Приложно програмиране“.

## Предназначение и цел

Мотоциклетистите днес използват няколко различни платформи: социални мрежи за снимки, Facebook групи за клубове, сайтове за обяви за покупко-продажба и навигационни приложения за маршрути. Обявите в общите сайтове нямат характеристики, специфични за мотоциклетите, и често са обект на измами.

Целта на MotoGrid е да събере тези дейности на едно място, в среда, създадена специално за мотоциклетисти:
- социална мрежа с профили, „гараж“ с мотоциклети, публикации, истории, групи, събития и маршрути;
- онлайн пазар за мотоциклети, части, екипировка и аксесоари със структурирани характеристики, разширено търсене, чат и оценки на продавачите.

## Използвани технологии

| Част | Технология |
|---|---|
| Сървърна част | ASP.NET Core MVC (.NET 10), C# |
| Клиентска част | Razor Views, Bootstrap 5, jQuery |
| База данни | Microsoft SQL Server, Entity Framework Core 10 |
| Автентикация и роли | ASP.NET Core Identity |
| Тестове | xUnit, Moq, EF Core InMemory |
| CI | GitHub Actions (build и тестове при всеки push) |
| Среда за разработка | Visual Studio 2026, Git, GitHub |

## Структура на проекта

```
MotoGrid.slnx
├── src
│   ├── MotoGrid.Web        – MVC приложение: контролери, изгледи, конфигурация
│   ├── MotoGrid.Services   – бизнес логика (услуги), напр. съхранение на файлове
│   ├── MotoGrid.Data       – ApplicationDbContext, ентитита, конфигурации, миграции, seed данни
│   └── MotoGrid.Common     – общи константи (роли, ограничения за валидация)
├── tests
│   └── MotoGrid.Tests      – unit тестове
└── docs                    – документация и описание на базата данни
```

## Инструкции за стартиране

### Необходимо
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Microsoft SQL Server (Express или LocalDB)
- Visual Studio 2026 (или `dotnet` CLI)

### Стъпки
1. Клонирайте хранилището:
   ```bash
   git clone https://github.com/GeorgiZhisov/MotoSocialMedia.git
   cd MotoSocialMedia
   ```
2. Настройте връзката към базата данни чрез **User Secrets** (вижте „Необходими настройки“).
3. Стартирайте проекта `MotoGrid.Web` (F5 във Visual Studio или):
   ```bash
   dotnet run --project src/MotoGrid.Web
   ```
   При стартиране базата данни се създава автоматично от миграциите и се попълват началните данни (роли и категории на обявите).

## Необходими настройки

Паролите и връзката към базата данни **не се пазят** в `appsettings.json`, а в User Secrets. По подразбиране се използва LocalDB. За SQL Server Express изпълнете от папката на решението:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.\SQLEXPRESS;Database=MotoGrid;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true" --project src/MotoGrid.Web
```

Във Visual Studio: десен бутон върху `MotoGrid.Web` → **Manage User Secrets**.

По желание може да се създаде и първият администратор:

```bash
dotnet user-secrets set "Seed:AdminEmail" "admin@motogrid.local" --project src/MotoGrid.Web
dotnet user-secrets set "Seed:AdminPassword" "<вашата парола>" --project src/MotoGrid.Web
```

## База данни

Подробно описание и ER диаграма: [docs/database](docs/database/README.md).

Основни таблици: `AspNetUsers`, `Motorcycles`, `Posts`, `Media`, `Comments`, `PostLikes`, `Follows`, `Stories`, `Groups`, `GroupMembers`, `Events`, `EventParticipants`, `Routes`, `ListingCategories`, `Listings`, `ListingImages`, `Reviews`, `Conversations`, `Messages`.

Нова миграция след промяна на моделите (Package Manager Console, Default project: `MotoGrid.Data`):
```
Add-Migration <Име>
Update-Database
```

## Роли и тестови потребители

| Роля | Права |
|---|---|
| `User` | профил, гараж, публикации, групи, събития, обяви, чат |
| `Moderator` | модериране на публикации, коментари и обяви |
| `Administrator` | всичко, включително административния панел и управлението на потребители |

Тестовите потребители се създават локално чрез User Secrets (вижте по-горе). В хранилището няма реални пароли.

## Функционалности

### Етап 1 – готово
- [x] Структура на решението на слоеве (Web, Services, Data, Common, Tests)
- [x] Модел на данните – 19 ентитита и начална миграция
- [x] Регистрация и вход (ASP.NET Core Identity), роли
- [x] Начални данни: роли, категории на обявите, администратор
- [x] Услуга за качване на файлове с проверка на тип и размер
- [x] Unit тестове и CI с GitHub Actions

### Планирано
- [ ] Профил и гараж с мотоциклети
- [ ] Лента с публикации, снимки и видеа, коментари, харесвания, следване
- [ ] Истории, които изчезват след 24 часа
- [ ] Групи и клубове, събития със записване, споделяне на маршрути
- [ ] Обяви със структурирани характеристики (марка, модел, година, пробег, кубатура, състояние)
- [ ] Търсене и филтри по категория, кубатура, цена, град и състояние
- [ ] Чат между купувач и продавач в реално време (SignalR)
- [ ] Оценки и репутация на продавачите
- [ ] Административен панел (Admin area) и модериране
- [ ] Персонализирани страници за грешки (404, 500)

## Автор

**Георги Ивов Жисов**, XII „в“ клас
Ръководител-консултант: Иван Иванов
