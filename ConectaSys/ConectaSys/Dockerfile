# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Configure o diret�rio de trabalho
WORKDIR /app

# Copie apenas o arquivo de solu��o para o cont�iner
COPY ["ConectaSys.sln", "./"]

# Copie os arquivos de projeto relacionados
COPY ["ConectaSys.Application/ConectaSys.Application.csproj", "ConectaSys.Application/"]
COPY ["ConectaSys.Core/ConectaSys.Core.csproj", "ConectaSys.Core/"]
COPY ["ConectaSys.Infrastructure/ConectaSys.Infrastructure.csproj", "ConectaSys.Infrastructure/"]
COPY ["ConectaSys.Presentation/ConectaSys.Presentation.csproj", "ConectaSys.Presentation/"]

# Restaurar as depend�ncias (sem cache para garantir atualiza��o)
RUN dotnet restore --no-cache

# Copiar todos os arquivos para o cont�iner
COPY . .

# Build e publica��o do projeto
RUN dotnet publish "ConectaSys.Presentation/ConectaSys.Presentation.csproj" -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Configure o diret�rio de trabalho
WORKDIR /app

# Copie os arquivos publicados para a imagem de execu��o
COPY --from=build /app/out .

# Definir vari�vel de ambiente para indicar o ambiente de produ��o
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Configurar um loop para restart autom�tico caso o servi�o caia
CMD while :; do dotnet ConectaSys.Presentation.dll; sleep 5; done
