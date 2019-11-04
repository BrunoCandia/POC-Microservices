#Base image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

#Build image
#Specify image
#For content, the first thing we need to define is an image we want to base it on.
#We also need to set a working directory where we want the files to end up on the container.
#We do that with the command FROM and WORKDIR, like so:
		#What we are saying here is to go grab an image with a small OS image made for .Net Core
		#We also say that our working directory is /src

#Copy project file and restore
#Next, we need to copy the project file ending in .csproj.
#Additionally, we also need to call dotnet restore, to ensure we install all specified dependencies
#We do that with the commands:
		#COPY ["Users.API/Users.API.csproj", "Users.API/"]
		#RUN dotnet restore "Users.API/Users.API.csproj"

#Copy, Set a working directory and Build
#Next, we need to copy our app files and build our app
#We do that with the commands:
		#COPY . .
		#WORKDIR "/src/Users.API"
		#RUN dotnet build "Users.API.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Users.API/Users.API.csproj", "Users.API/"]
RUN dotnet restore "Users.API/Users.API.csproj"
COPY . .
WORKDIR "/src/Users.API"
RUN dotnet build "Users.API.csproj" -c Release -o /app/build

#Publish image
FROM build AS publish
RUN dotnet publish "Users.API.csproj" -c Release -o /app/publish

#Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.API.dll"]