#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CriEduc.School.Api/CriEduc.School.Api.csproj", "CriEduc.School.Api/"]
COPY ["CriEduc.School.Repository/CriEduc.School.Repository.csproj", "CriEduc.School.Repository/"]
COPY ["CriEduc.School.Border/CriEduc.School.Border.csproj", "CriEduc.School.Border/"]
COPY ["CriEduc.School.UseCase/CriEduc.School.UseCase.csproj", "CriEduc.School.UseCase/"]
RUN dotnet restore "CriEduc.School.Api/CriEduc.School.Api.csproj"
COPY . .
WORKDIR "/src/CriEduc.School.Api"
RUN dotnet build "CriEduc.School.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CriEduc.School.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CriEduc.School.Api.dll"]