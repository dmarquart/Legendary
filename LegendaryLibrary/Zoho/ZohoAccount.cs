using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryLibrary.Zoho
{
    class ZohoAccount
    {
        public ZohoAccount(string accountID, string name, ZohoConstants.enumZohoCompanyTypes companyType,
                           string street, string street2, string city, string state, string postalCode, string country,
                           string phone, string email, string website, string magicKey )
        {
            AccountID = accountID;
            Name = name;
            CompanyType = companyType;
            Street = street;
            Street2 = street2;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;
            Email = email;
            Website = website;
            MagicKey = magicKey;
        }

        public string AccountID { get; } = "";
        public string Name { get; } = "";
        public ZohoConstants.enumZohoCompanyTypes CompanyType { get; } = ZohoConstants.enumZohoCompanyTypes.NotSet;
        public string Street { get; } = "";
        public string Street2 { get; } = "";
        public string City { get; } = "";
        public string State { get; } = "";
        public string Country { get; } = "";
        public string PostalCode { get; } = "";
        public string Phone { get; } = "";
        public string Email { get; } = "";
        public string Website { get; } = "";
        public string MagicKey { get; } = "";
    }
}
