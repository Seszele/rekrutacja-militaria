# GUS application

GUS application is a simple MVVM .NET 6.0 application that takes data from the GUS API (https://api-dbw.stat.gov.pl) and displays it on the user interface using virtualization, sorting and editing on the DataGrid.

## Requirements

- .NET SDK 6.0 (https://dotnet.microsoft.com/download/dotnet/6.0)
- Windows

## How to run

1 Clone the repository on your local computer:

```bash
git clone https://github.com/Seszele/rekrutacja-militaria.git
```

2. navigate to the project directory:

```bash
cd zad2/GusApp
```

3. launch the application:

```bash
dotnet run --project GusApp/GusApp.csproj
```

After these steps, the application should run and display a window with the downloaded data from the GUS API.

## Functionality

- Downloading subject areas from the GUS API
- Displaying data on DataGrid with virtualization, sorting and editing
- Rows are colored according to the "name-level" value: "Subject" (green), "Information scope" (red), "Field" (yellow).
