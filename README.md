# BiLL Here

BiLL Here is a modern .NET billing workspace built with ASP.NET Core Razor Pages, EF Core, and SQLite. It is designed as a polished billing software project with a dashboard, invoice workflow, customer ledger, product catalog, unit-tested billing math, and print-ready invoice views.

## Features

- Mall-style retail POS checkout with SKU/search entry, quick product tiles, editable cart, GST/tax preview, payment mode, cash change, and thermal receipt print view
- Dashboard with collected revenue, outstanding balance, open invoice, customer, product, and monthly billing trend metrics
- Invoice list with search, status filtering, derived overdue status, and balance tracking
- Invoice builder with customer selection, catalog-backed line entry, discount, tax, and live total preview
- Print-ready invoice detail page with mark sent, mark paid, and void actions
- Customer management with contact details, active/inactive state, billed totals, and receivable totals
- Product catalog with SKU, unit, price, tax defaults, and active/inactive state
- SQLite persistence with seeded demo data
- Unit tests for money calculations and invoice status resolution

## Tech Stack

- .NET 9 / C# 13
- ASP.NET Core Razor Pages
- Entity Framework Core SQLite
- Bootstrap assets from the ASP.NET template plus custom responsive CSS
- xUnit test project

## Open in VS Code

1. Open VS Code.
2. Choose **File > Open Folder** and select `C:\Users\user\Desktop\LuminaBill`.
3. Install the recommended C# extensions if VS Code prompts you.
4. Press `F5` and choose **Launch BiLL Here Web**.

The VS Code setup includes `.vscode/launch.json` for debugging at `http://localhost:5274`, `.vscode/tasks.json` for build/test/run, and `global.json` pinned to the installed .NET SDK line.


Open the local URL printed by `dotnet run`.

Retail checkout opens at `http://localhost:5274/Checkout`.


## Project Structure

```text
src/LuminaBill.Web       ASP.NET Core Razor Pages app
tests/LuminaBill.Tests   Billing calculation and status tests
```

## Data

The app creates a SQLite database at `src/LuminaBill.Web/App_Data/luminabill.db` on first run and seeds demo customers, products, and invoices. Delete that database file to recreate the demo data.

## Upgrade Note

This workspace currently has .NET SDK `9.0.200` installed, so the project targets `net9.0` and builds immediately here. To target the newer official .NET 10 SDK, install .NET 10, update the target framework to `net10.0`, and update EF Core packages to the matching major version.
