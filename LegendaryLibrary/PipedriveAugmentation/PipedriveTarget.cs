using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryLibrary.PipedriveAugmentation
{
    public class PipedriveTarget
    {
        public PipedriveTarget(long personId, string firstName, string lastName, long crd, string email, string address,
                               string category, string firmName, long firmId, string contactability)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            CRD = crd;
            Email = email;
            Address = address;
            Category = category;
            FirmName = firmName;
            FirmId = firmId;
            Contactability = contactability;
        }
        public long PersonId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public long CRD { get; }
        public string Email { get; }
        public string Address { get; }
        public string Category { get; }
        public string FirmName { get; }
        public long FirmId { get; }
        public string Contactability { get; }
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
