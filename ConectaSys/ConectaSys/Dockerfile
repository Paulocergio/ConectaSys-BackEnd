# Use a imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Configure o diret�rio de trabalho
WORKDIR /app

# Copie apenas o arquivo de solu��o para o cont�iner
COPY ["ConectaSys.sln", "./"]

# Copie os arquivos de projeto relacionados
COPY ["ConectaSys.Application/ConectaSys.Application.csproj", "ConectaSys.Application/"]
COPY ["ConectaSys.Domain/ConectaSys.Domain.csproj", "ConectaSys.Domain/"]
COPY ["ConectaSys.Infrastructure/ConectaSys.Infrastructure.csproj", "ConectaSys.Infrastructure/"]
COPY ["ConectaSys.Presentation/ConectaSys.Presentation.csproj", "ConectaSys.Presentation/"]

# Restaure as depend�ncias do projeto
RUN dotnet restore

# Copie o restante dos arquivos para o cont�iner
COPY . .

# Compile e publique o projeto
RUN dotnet publish "ConectaSys.Presentation/ConectaSys.Presentation.csproj" -c Release -o /app/out

# Use uma imagem base mais leve para a execu��o
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Configure o diret�rio de trabalho para execu��o
WORKDIR /app

# Copie os arquivos publicados para a imagem de execu��o
COPY --from=build /app/out ./

# Defina o comando de inicializa��o
ENTRYPOINT ["dotnet", "ConectaSys.Presentation.dll"]
