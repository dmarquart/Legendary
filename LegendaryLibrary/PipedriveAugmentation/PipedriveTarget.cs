using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryLibrary.PipedriveAugmentation
{
    public class PipedriveTarget
    {
        public PipedriveTarget(string contactOrigin, string contactBaseType, string contactType, long contactOriginId, 
                               string wholeName, string firstName, string lastName, string nickName, long crd, string email, string category, string phone,
                               string address, string street, string street2, string city, string state, string zip,
                               string firmName, long firmId, string firmHasLOFReitSA, string firmLOFReitCoding, string firmHasLFReitIIISA, string firmLFReitIIICoding,
                               string rvp, string avp, string distributionRegion,
                               string contactability, string doNotEmail, 
                               string repHasSoldLOFReit, string repLOFReitCoding, string repHasSoldLFReitIII, string repLFReitIIICoding)
        {
            ContactBaseType = contactBaseType;
            ContactOrigin = contactOrigin;
            ContactOriginId = contactOriginId;
            ContactType = contactType;
            WholeName = wholeName;
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
            CRD = crd;
            Email = email;
            Address = address;
            Street = street;
            Street2 = street2;
            City = city;
            State = state;
            Zip = zip;
            Category = category;
            Phone = phone;
            FirmName = firmName;
            FirmId = firmId;
            RVP = rvp;
            AVP = avp;
            DistributionRegion = distributionRegion;
            Contactability = contactability;
            DoNotEmail = doNotEmail;
            FirmLOFReitHasSA = firmHasLOFReitSA;
            FirmLOFReitCoding = firmLOFReitCoding;
            FirmLFReitIIIHasSA = firmHasLFReitIIISA;
            FirmLFReitIIICoding = firmLFReitIIICoding;
            RepHasSoldLOFReit = repHasSoldLOFReit;
            RepLOFReitCoding = repLOFReitCoding;
            RepHasSoldLFReitIII = repHasSoldLFReitIII;
            RepLFReitIIICoding = repLFReitIIICoding;
        }
        public string ContactOrigin { get; }
        public string ContactBaseType { get; }
        public long ContactOriginId { get; }
        public string ContactType { get; }
        public string WholeName { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string NickName { get; }
        public long CRD { get; }
        public string Email { get; }
        public string Address { get; }
        public string Street { get; }
        public string Street2 { get; }
        public string City { get; }
        public string State { get; }
        public string Zip { get; }
        public string Category { get; }
        public string Phone { get; }
        public string FirmName { get; }
        public long FirmId { get; }
        public string RVP { get; }
        public string AVP { get; }
        public string DistributionRegion { get; }
        public string Contactability { get; }
        public string DoNotEmail { get; }
        public string FirmLOFReitHasSA { get; }
        public string FirmLOFReitCoding { get; }
        public string FirmLFReitIIIHasSA { get; }
        public string FirmLFReitIIICoding { get; }
        public string RepHasSoldLOFReit { get; }
        public string RepLOFReitCoding { get; }
        public string RepHasSoldLFReitIII { get; }
        public string RepLFReitIIICoding { get; }
    }

    public class DistinctEmailPipedriveTargetComparer : IEqualityComparer<PipedriveTarget>
    {
        static long count = 0;
        public bool Equals(PipedriveTarget x, PipedriveTarget y)
        {
            return (x.Email ?? "junk1@junk1.com") == (y.Email ?? "junk2@junk2.com");
        }

        public int GetHashCode(PipedriveTarget x)
        {
            count++;
            return (string.IsNullOrWhiteSpace(x.Email) ? $"junk{count}@junk{count}.com".GetHashCode() : x.Email.GetHashCode()) ;
        }
    }

    public class DistinctFullPipedriveTargetComparer : IEqualityComparer<PipedriveTarget>
    {
        public bool Equals(PipedriveTarget x, PipedriveTarget y)
        {
            return (x.LastName ?? "") == (y.LastName ?? "") &&
                   (x.FirstName ?? "") == (y.FirstName ?? "") &&
                   (x.FirmName ?? "") == (y.FirmName ?? "") &&
                   (x.Email ?? "") == (y.Email ?? "");
        }

        public int GetHashCode(PipedriveTarget x)
        {
            return (x.LastName ?? "").GetHashCode() ^
                   (x.FirstName ?? "").GetHashCode() ^
                   (x.FirmName ?? "").GetHashCode() ^
                   (x.Email ?? "").GetHashCode();
        }
    }

    public class PipedriveTargetComparer : IComparer<PipedriveTarget>
    {
        public int Compare(PipedriveTarget x, PipedriveTarget y)
        {
            int result = 0;

            if (((result = string.Compare(x.LastName, y.LastName, true)) != 0) ||
                ((result = string.Compare(x.FirstName, y.FirstName, true)) != 0) ||
                ((result = string.Compare(x.FirmName, y.FirmName, true)) != 0) ||
                ((result = string.Compare(x.Email, y.Email, true)) != 0)) return result;

            return result;
        }
    }
}
