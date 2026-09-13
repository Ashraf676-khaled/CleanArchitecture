# المرحلة الأولى: بناء المشروع (Build Stage)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملفات الإعدادات والـ Packages لضمان كفاءة التخزين المؤقت (Caching)
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["Jogo.sln", "./"]

# نسخ باقي المشاريع بناءً على مساراتها الحقيقية
COPY ["src/Jogo.Api/Jogo.Api.csproj", "src/Jogo.Api/"]
COPY ["src/Jogo.Application/Jogo.Application.csproj", "src/Jogo.Application/"]
COPY ["src/Jogo.Domain/Jogo.Domain.csproj", "src/Jogo.Domain/"]
COPY ["src/Jogo.Infrastructure/Jogo.Infrastructure.csproj", "src/Jogo.Infrastructure/"]

# استعادة الحزم (Restore NuGet Packages)
RUN dotnet restore "Jogo.sln"

# نسخ كل الكود المصدري وبناء المشروع بنظام Releas
COPY . .
WORKDIR "/src/src/Jogo.Api"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# المرحلة الثانية: التشغيل النهائي (Runtime Stage)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Jogo.Api.dll"]