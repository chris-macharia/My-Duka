# 📦 My-Duka App

My-Duka App is a simple inventory management application built with **.NET MAUI**.  
It allows shop owners to track items, sales, and stock levels across devices.

## 🚀 Prerequisites

Before running the app, make sure you have:

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (17.3 or later)  
  - Workloads required:  
    - **.NET Multi-platform App UI development (.NET MAUI)**  
    - **Desktop development with .NET**  
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download) or newer  
- Windows 10 (build 19041+) or Windows 11

## 📥 Download

Clone this repository:

```
git clone https://github.com/chris-macharia/My-Duka.git
cd DukaInventory
```
Or download the ZIP from GitHub and extract it.

## 🛠️ Build

To build the project from the terminal:

```
dotnet build -f net9.0-windows10.0.19041.0
```

## ▶️ Run

Run the app directly from the command line:

```
dotnet run -f net9.0-windows10.0.19041.0
```

Or open the solution in Visual Studio 2022:

- Open DukaInventory.sln

- Select Windows Machine as the target

- Press F5 to run with debugging or Ctrl + F5 to run without debugging

## 📂 Executable

After building, the .exe can be found here:

```
bin/Debug/net9.0-windows10.0.19041.0/DukaInventory.exe
```

You can run it directly by double-clicking.

## 📖 Features
- Add and manage shop items

- Record sales and update stock

- View sales reports

- Local database storage with SQLite

## ⚠️ Notes
Currently only Windows builds are supported without extra setup.

To run on Android or iOS, install the Android/iOS workloads in Visual Studio.

## 📝 License
This project is licensed under the MIT License. See the LICENSE file for details.
