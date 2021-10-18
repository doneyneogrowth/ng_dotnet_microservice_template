FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
LABEL servicename="ngtemplate"
LABEL buildstage="ngtemplate-build"
WORKDIR /app

# Copy files
COPY *.sln .
COPY ./src ./src
COPY ./tests ./tests
RUN dotnet restore --disable-parallel

# Test
FROM build AS test
LABEL servicename=ngtemplate
LABEL buildstage=ngtemplate-test
RUN dotnet test -c Release --results-directory /testresults/ --logger "trx;LogFileName=testresults.trx" 

# Build
COPY . ./
FROM build AS publish
ARG APP_VERSION=0.0.0
ARG BUILD_NUMBER=0
ARG COMMIT_HASH=""
LABEL servicename=ngtemplate
LABEL buildstage=ngtemplate-publish
RUN dotnet publish ""src/NgTemplate.API/NgTemplate.API.csproj"" -c Release -o out

# Build image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
LABEL servicename=ngtemplate
LABEL buildstage=ngtemplate-release
WORKDIR /app
COPY --from=publish /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "NgTemplate.API.dll"]