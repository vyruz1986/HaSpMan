FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ./*.sln ./
COPY ./Directory.Build.props ./
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*} && mv $file ${file%.*}; done

RUN dotnet restore

COPY . ./
WORKDIR /src/Web
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV LC_ALL=en_US.UTF-8 \
    LANG=en_US.UTF-8 \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Web.dll"]
EXPOSE 443