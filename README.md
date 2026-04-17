![header](https://capsule-render.vercel.app/api?type=waving&color=0:1a1b27,50:24283b,100:414868&height=200&section=header&text=StudyTracker&fontSize=50&fontColor=7aa2f7&animation=fadeIn&fontAlignY=35&desc=Clean%20Architecture%20Web%20API%20%E2%80%94%20.NET%2010&descAlignY=55&descSize=18&descColor=a9b1d6)

<div align="center">

[![.NET](https://img.shields.io/badge/.NET_10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](#)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](#)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](#)
[![MediatR](https://img.shields.io/badge/MediatR-bb9af7?style=for-the-badge)](#)
[![EF Core](https://img.shields.io/badge/EF_Core-512BD4?style=for-the-badge)](#)
[![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)](#)

<br/>

Ett ASP.NET Core Web API byggt enligt Clean Architecture.<br/>
Elever loggar studiepass och motivation före/efter — data lagras via CQRS med MediatR och Repository Pattern mot SQL Server.

</div>

<br/>

## &nbsp;Kör programmet

```bash
# Skapa databasen
dotnet ef database update --project StudyTracker.Infrastructure --startup-project StudyTracker.API

# Starta API:et
dotnet run --project StudyTracker.API
```

> [!NOTE]
> Kräver .NET 10 SDK och SQL Server Express. Anpassa connection string i `StudyTracker.API/appsettings.json` efter din lokala instans.

<br/>

## &nbsp;Under utveckling

Projektet byggs upp i steg enligt Clean Architecture. Fullständig dokumentation fylls i löpande.

<br/>

## &nbsp;Författare

<div align="center">

<a href="https://github.com/klasolsson81">
<img src="https://github.com/klasolsson81.png" alt="Klas Olsson" width="170" style="border-radius: 50%" />
</a>

### Klas Olsson

[![Portfolio](https://img.shields.io/badge/klasolsson.se-7aa2f7?style=for-the-badge&logo=googlechrome&logoColor=white)](https://klasolsson.se)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/in/klasolsson81)
[![Email](https://img.shields.io/badge/Email-bb9af7?style=for-the-badge&logo=gmail&logoColor=white)](mailto:klasolsson81@gmail.com)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/klasolsson81)

</div>

![footer](https://capsule-render.vercel.app/api?type=waving&color=0:1a1b27,50:24283b,100:414868&height=120&section=footer)
