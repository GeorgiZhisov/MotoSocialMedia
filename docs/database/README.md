# База данни на MotoGrid

Базата данни е MS SQL Server. Схемата се създава от EF Core миграциите в
`src/MotoGrid.Data/Migrations`. Генерираният SQL скрипт е в [`schema.sql`](schema.sql).

Освен таблиците по-долу има и стандартните таблици на ASP.NET Core Identity
(`AspNetRoles`, `AspNetUserRoles`, `AspNetUserClaims` и др.).

## ER диаграма

```mermaid
erDiagram
    AspNetUsers ||--o{ Motorcycles : "owns (garage)"
    AspNetUsers ||--o{ Posts : writes
    AspNetUsers ||--o{ Stories : publishes
    AspNetUsers ||--o{ Follows : "follower / followee"
    AspNetUsers ||--o{ Comments : writes
    AspNetUsers ||--o{ PostLikes : likes
    Posts ||--o{ Media : contains
    Posts ||--o{ Comments : has
    Posts ||--o{ PostLikes : has
    Motorcycles |o--o{ Posts : "is about"

    AspNetUsers ||--o{ Groups : creates
    Groups ||--o{ GroupMembers : has
    AspNetUsers ||--o{ GroupMembers : "member of"
    Groups |o--o{ Posts : contains
    Groups |o--o{ Events : organizes
    AspNetUsers ||--o{ Events : organizes
    Events ||--o{ EventParticipants : has
    AspNetUsers ||--o{ EventParticipants : "signs up"
    AspNetUsers ||--o{ Routes : shares
    Routes |o--o{ Events : "is ridden at"

    ListingCategories ||--o{ Listings : groups
    AspNetUsers ||--o{ Listings : sells
    Listings ||--o{ ListingImages : has
    AspNetUsers ||--o{ Reviews : "writes / receives"
    Listings |o--o{ Reviews : "is reviewed in"
    AspNetUsers ||--o{ Conversations : "buyer / seller"
    Listings |o--o{ Conversations : "is discussed in"
    Conversations ||--o{ Messages : contains
    AspNetUsers ||--o{ Messages : sends

    AspNetUsers {
        string Id PK
        string Email
        string DisplayName
        string Bio
        string City
        string AvatarUrl
    }
    Motorcycles {
        int Id PK
        string OwnerId FK
        string Make
        string Model
        int Year
        int EngineCc
        int Category
    }
    Posts {
        int Id PK
        string AuthorId FK
        int MotorcycleId FK
        int GroupId FK
        string Content
        string Location
    }
    Media {
        int Id PK
        int PostId FK
        string Url
        int Type
    }
    Comments {
        int Id PK
        int PostId FK
        string AuthorId FK
        string Content
    }
    PostLikes {
        int PostId PK
        string UserId PK
    }
    Follows {
        string FollowerId PK
        string FolloweeId PK
    }
    Stories {
        int Id PK
        string AuthorId FK
        string MediaUrl
        datetime ExpiresOn
    }
    Groups {
        int Id PK
        string CreatorId FK
        string Name
        bool IsPrivate
    }
    GroupMembers {
        int GroupId PK
        string UserId PK
        int Role
    }
    Events {
        int Id PK
        string OrganizerId FK
        int GroupId FK
        int RouteId FK
        string Title
        datetime StartsOn
        string Location
    }
    EventParticipants {
        int EventId PK
        string UserId PK
    }
    Routes {
        int Id PK
        string AuthorId FK
        string Name
        double DistanceKm
        int Difficulty
    }
    ListingCategories {
        int Id PK
        string Name
        bool IsVehicle
    }
    Listings {
        int Id PK
        string SellerId FK
        int CategoryId FK
        string Title
        decimal Price
        int Condition
        int Status
        string City
        string Make
        string Model
        int Year
        int MileageKm
        int EngineCc
    }
    ListingImages {
        int Id PK
        int ListingId FK
        string Url
        bool IsCover
    }
    Reviews {
        int Id PK
        string ReviewerId FK
        string SellerId FK
        int ListingId FK
        int Rating
    }
    Conversations {
        int Id PK
        string BuyerId FK
        string SellerId FK
        int ListingId FK
    }
    Messages {
        int Id PK
        int ConversationId FK
        string SenderId FK
        string Content
        bool IsRead
    }
```

## Основни решения

- **Soft delete** – записите не се изтриват физически (`IsDeleted`, `DeletedOn`).
- **Одит** – `CreatedOn` и `ModifiedOn` се попълват автоматично в `ApplicationDbContext`.
- **Без каскадно изтриване от потребителите** – връзките към `AspNetUsers` са `NO ACTION`,
  което предотвратява грешката на SQL Server за няколко каскадни пътя.
- **Съставни ключове** за свързващите таблици (`PostLikes`, `Follows`, `GroupMembers`,
  `EventParticipants`) – потребител не може да хареса една публикация два пъти и т.н.
- **Check constraints** – оценката е между 1 и 5, потребител не може да последва себе си
  или да оцени себе си.
- **Индекси** за най-честите заявки: лента с публикации по дата, търсене на обяви по
  категория, статус, цена, кубатура и град.
