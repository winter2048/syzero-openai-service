FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
EXPOSE 5000
COPY . .
ENTRYPOINT ["dotnet", SyZero.OpenAI.Web.dll"]