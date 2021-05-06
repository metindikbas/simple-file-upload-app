# Simple File Upload Application

Angular 11 and .NET 5 powered file upload web application.

Backend API is running with In-Memory database. It can be changed easily by changing appsettings file.  

## Installation

Use `npm install` for frontend, `dotnet restore` for backend.

## Usage

Run the following command to start both frontend and backend app inside docker containers.

```
docker compose up
```

# Upload Settings
To configure maximum single file size and allowed mime types configuration look at the `UploadSettingsSeed` class which contains default values inserted into database to be used as filtering in both backend and frontend.

## Current Seed Values
Default maximum file size is **~20KB**

Default allowed file content types: **image/png,image/jpeg**