# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Configure o diretório de trabalho
WORKDIR /app

# Copie apenas o arquivo de solução para o contêiner
COPY ["ConectaSys.sln", "./"]

# Copie os arquivos de projeto relacionados
COPY ["ConectaSys.Application/ConectaSys.Application.csproj", "ConectaSys.Application/"]
COPY ["ConectaSys.Core/ConectaSys.Core.csproj", "ConectaSys.Core/"]
COPY ["ConectaSys.Infrastructure/ConectaSys.Infrastructure.csproj", "ConectaSys.Infrastructure/"]
COPY ["ConectaSys.Presentation/ConectaSys.Presentation.csproj", "ConectaSys.Presentation/"]

# Restaurar as dependências (sem cache para garantir atualização)
RUN dotnet restore --no-cache

# Copiar todos os arquivos para o contêiner
COPY . .

# Build e publicação do projeto
RUN dotnet publish "ConectaSys.Presentation/ConectaSys.Presentation.csproj" -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Configure o diretório de trabalho
WORKDIR /app

# Copie os arquivos publicados para a imagem de execução
COPY --from=build /app/out .

# Definir variável de ambiente para indicar o ambiente de produção
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Configurar um loop para restart automático caso o serviço caia
CMD while :; do dotnet ConectaSys.Presentation.dll; sleep 5; done
