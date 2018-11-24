FROM microsoft/dotnet:2.1-sdk-alpine as build
WORKDIR /src
COPY . .
WORKDIR /src/MyCompany.Api
RUN dotnet publish -c Release -o /dist

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine as final
WORKDIR /app
COPY --from=build /dist .
EXPOSE 80
ENTRYPOINT [ "dotnet", "MyCompany.Api.dll" ]