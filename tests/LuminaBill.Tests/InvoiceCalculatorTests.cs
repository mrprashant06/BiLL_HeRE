using LuminaBill.Web.Models;
using LuminaBill.Web.Services;

namespace LuminaBill.Tests;

public sealed class InvoiceCalculatorTests
{
    [Fact]
    public void Calculate_AppliesLineDiscountInvoiceDiscountTaxAndPayments()
    {
        var calculator = new InvoiceCalculator();
        var items = new[]
        {
            new InvoiceLineItem
            {
                Description = "Implementation",
                Quantity = 2,
                UnitPrice = 100,
                TaxRate = 10,
                Discount = 20
            }
        };

        var totals = calculator.Calculate(items, invoiceDiscount: 10, paidAmount: 50);

        Assert.Equal(200, totals.Subtotal);
        Assert.Equal(20, totals.LineDiscount);
        Assert.Equal(10, totals.InvoiceDiscount);
        Assert.Equal(18, totals.Tax);
        Assert.Equal(188, totals.GrandTotal);
        Assert.Equal(50, totals.PaidAmount);
        Assert.Equal(138, totals.BalanceDue);
    }

    [Fact]
    public void Calculate_ClampsDiscountsAndPaymentsToValidMoneyValues()
    {
        var calculator = new InvoiceCalculator();
        var items = new[]
        {
            new InvoiceLineItem
            {
                Description = "Support",
                Quantity = 1,
                UnitPrice = 100,
                TaxRate = 0,
                Discount = 150
            }
        };

        var totals = calculator.Calculate(items, invoiceDiscount: 100, paidAmount: 200);

        Assert.Equal(100, totals.Subtotal);
        Assert.Equal(100, totals.LineDiscount);
        Assert.Equal(0, totals.InvoiceDiscount);
        Assert.Equal(0, totals.GrandTotal);
        Assert.Equal(0, totals.PaidAmount);
        Assert.Equal(0, totals.BalanceDue);
    }

    [Fact]
    public void Resolve_ReturnsOverdueOnlyForSentUnpaidInvoicesPastDue()
    {
        var today = new DateTime(2026, 6, 24);
        var calculator = new InvoiceCalculator();
        var invoice = new Invoice
        {
            Status = InvoiceStatus.Sent,
            DueDate = today.AddDays(-1),
            LineItems = new List<InvoiceLineItem>
            {
                new()
                {
                    Description = "Billing terminal",
                    Quantity = 1,
                    UnitPrice = 500
                }
            }
        };

        var totals = calculator.Calculate(invoice);
        var status = InvoiceStatusResolver.Resolve(invoice, totals, today);

        Assert.Equal(InvoiceStatus.Overdue, status);
    }

    [Fact]
    public void Resolve_KeepsDraftInvoicesInDraftEvenWhenPastDue()
    {
        var today = new DateTime(2026, 6, 24);
        var calculator = new InvoiceCalculator();
        var invoice = new Invoice
        {
            Status = InvoiceStatus.Draft,
            DueDate = today.AddDays(-3),
            LineItems = new List<InvoiceLineItem>
            {
                new()
                {
                    Description = "Draft item",
                    Quantity = 1,
                    UnitPrice = 250
                }
            }
        };

        var totals = calculator.Calculate(invoice);
        var status = InvoiceStatusResolver.Resolve(invoice, totals, today);

        Assert.Equal(InvoiceStatus.Draft, status);
    }
}
