using System;
using System.Collections.Generic;
using System.Text;

namespace Youtube.Manager.Models.Container.DB_models.Library
{
    public class AppBillingProduct
    {
        //
        // Summary:
        //     Name of the product
        public string Name { get; set; }
        //
        // Summary:
        //     Description of the product
        public string Description { get; set; }
        //
        // Summary:
        //     Product ID or sku
        public string ProductId { get; set; }
        //
        // Summary:
        //     Localized Price (not including tax)
        public string LocalizedPrice { get; set; }
        //
        // Summary:
        //     ISO 4217 currency code for price. For example, if price is specified in British
        //     pounds sterling is "GBP".
        public string CurrencyCode { get; set; }
        //
        // Summary:
        //     Price in micro-units, where 1,000,000 micro-units equal one unit of the currency.
        //     For example, if price is "€7.99", price_amount_micros is "7990000". This value
        //     represents the localized, rounded price for a particular currency.
        public long MicrosPrice { get; set; }
        //
        // Summary:
        //     Gets or sets the localized introductory price.
        public string LocalizedIntroductoryPrice { get; set; }
        //
        // Summary:
        //     Introductory price of the product in micor-units
        public long MicrosIntroductoryPrice { get; set; }
        //
        // Summary:
        //     Gets a value indicating whether this Plugin.InAppBilling.Abstractions.InAppBillingProduct
        //     has introductory price. This is an optional value in the answer from the server,
        //     requires a boolean to check if this exists
        public bool HasIntroductoryPrice { get; set; }

        public long CoinsAmount { get; set; }

        public string Price { get => $"{LocalizedPrice} {CurrencyCode}"; }
    }
}
