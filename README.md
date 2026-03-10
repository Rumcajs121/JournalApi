# 📖 JournalApi

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-blue.svg)](https://docs.microsoft.com/en-us/ef/)
[![Azure](https://img.shields.io/badge/Azure-Storage-orange.svg)](https://azure.microsoft.com/)

> REST API dla aplikacji dziennika (journal), zbudowane z wykorzystaniem ASP.NET Core, Clean Architecture i CQRS. Umożliwia zarządzanie wpisami dziennikowymi z obsługą zdjęć, autorów i metadanych.

## 📋 Spis treści
- [Opis projektu](#opis-projektu)
- [Technologie](#technologie)
- [Architektura](#architektura)
- [Funkcjonalności](#funkcjonalności)
- [Autor](#autor)

## 📖 Opis projektu
JournalApi to REST API umożliwiające zarządzanie wpisami w dzienniku elektronicznym. Projekt został zrealizowany jako część portfolio deweloperskiego, demonstrując zastosowanie nowoczesnych technologii i wzorców architektonicznych w rozwoju aplikacji webowych.

Główne cechy:
- **Clean Architecture** - modularna struktura z oddzielonymi warstwami
- **CQRS** - separacja między operacjami zapisu i odczytu
- **Azure Blob Storage** - przechowywanie zdjęć i mediów
- **MediatR** - implementacja wzorca Mediator
- **Swagger/OpenAPI** - automatyczna dokumentacja API

## 🏗 Architektura

Projekt oparty na zasadach Clean Architecture:

### Journal.Domain
- **Encje biznesowe** - klasy reprezentujące domenę (Journal, Author, Picture)
- **Reguły biznesowe** - logika specyficzna dla domeny

### Journal.Application
- **Interfejsy usług** - kontrakty dla operacji biznesowych
- **DTOs** - obiekty transferu danych
- **Komendy i zapytania** - implementacja CQRS z MediatR
- **Handler'y** - przetwarzanie żądań

### Journal.Infrastructure
- **Repozytoria** - dostęp do danych z EF Core
- **Kontekst bazy danych** - konfiguracja SQL Server
- **Migracje** - zarządzanie schematem bazy
- **Usługi zewnętrzne** - integracja z Azure Storage

### JournalApi
- **Kontrolery** - punkty końcowe REST API
- **Middleware** - obsługa błędów, logowanie
- **Konfiguracja** - ustawienia aplikacji
- **Swagger** - dokumentacja interaktywna

### QueueMessageConsumer
- **Konsumer wiadomości** - przetwarzanie kolejek (np. Azure Service Bus)

## 🛠 Technologie

### Backend
- **ASP.NET Core 8.0** - framework do budowy API REST
- **Entity Framework Core** - ORM dla bazy danych
- **MediatR** - implementacja CQRS i Mediator pattern
- **AutoMapper** - mapowanie obiektów
- **SQL Server** - relacyjna baza danych
- **Azure Blob Storage** - przechowywanie plików
- **Azure Table Storage** - dodatkowe przechowywanie danych

### Narzędzia i biblioteki
- **log4net** - logowanie aplikacji
- **Swashbuckle/Swagger** - dokumentacja API
- **Memory Cache** - pamięć podręczna
- **xUnit** - testy jednostkowe

### Narzędzia deweloperskie
- **Visual Studio 2022** - środowisko programistyczne
- **Git** - system kontroli wersji
- **Postman** - testowanie API

## ✨ Funkcjonalności
- ✅ Tworzenie nowych wpisów dziennikowych
- ✅ Pobieranie wszystkich wpisów
- ✅ Pobieranie pojedynczego wpisu po ID
- ✅ Edycja istniejących wpisów
- ✅ Zarządzanie zdjęciami (Azure Blob Storage)
- ✅ Obsługa autorów z metadanymi
- ✅ Middleware do obsługi błędów
- ✅ Logowanie z log4net
- ✅ Dokumentacja Swagger/OpenAPI
- ✅ Testy jednostkowe

## 👨‍💻 Autor
**Rumcajs121** - Student Informatyki, Społeczna Akademia Nauk w Łodzi


