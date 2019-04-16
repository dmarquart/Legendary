using System;
using System.Collections.Generic;
using System.Linq;
using Pipedrive;
using Pipedrive.CustomFields;
using RepStages = LegendaryLibrary.PipedriveAugmentation.PipelineList.RepActivationStages;
using LOFStages = LegendaryLibrary.PipedriveAugmentation.PipelineList.LOF_REIT_SAsStages;
using ReitIIIStages = LegendaryLibrary.PipedriveAugmentation.PipelineList.LF_REIT_III_SAsStages;
using OrgFields = LegendaryLibrary.PipedriveAugmentation.OrganizationCustomFields;

namespace LegendaryLibrary.PipedriveAugmentation
{
    public enum enumFilterSellingFirms
    {
        NotSet,
        AnyFirm,
        BrokerDealer,
        RIA
    }

    public enum enumFilterSellingFirmsPersons
    {
        NotSet,
        HomeOfficeOther,
        DueDiligence
    }

    public enum enumFilterReps
    {
        NotSet,
        RegisteredRep,
        IAR
    }

    public enum enumFilterCategories
    {
        NotSet,
        Z,
        NC,
        T,
        I,
        P,
        H,
        DD,
        B,
        A,
        SellingRepsOfRecord
    }

    public enum enumFilterRegions
    {
        NotSet,
        West,
        NorthCentral,
        SouthCentral,
        NorthEast,
        SouthEast
    }

    public enum enumFilterYesNoEither
    {
        NotSet,
        Yes,
        No,
        Either
    }

    public enum enumLegendaryProducts
    {
        NotSet,
        LOFReit,
        LFReitIII
    }

    //public sealed class enumFilterLegendaryProducts
    //{
    //    private readonly string Name;
    //    public static readonly enumFilterLegendaryProducts NotSet = new enumFilterLegendaryProducts("NotSet");
    //    public static readonly enumFilterLegendaryProducts LOFReit = new enumFilterLegendaryProducts("LOFReit");
    //    public static readonly enumFilterLegendaryProducts LFReitIII = new enumFilterLegendaryProducts("LFReitIII");
    //    private enumFilterLegendaryProducts(string name) { Name = name; }
    //    public override string ToString() { return Name; }
    //}

    public enum enumFilterMarketingAudiences
    {
        NotSet,
        SellingFirms,
        FundSubscriptions,
        ProServices,
        IndustryGroups,
        TradePress
    };

    class PipedriveTargetFilter
    {
        static public string listDelimeter = "~";
        static public string contactOrigin = "Pipedrive";

        public PipedriveTargetFilter(List<enumLegendaryProducts> legendaryProducts = null,
                                     enumFilterMarketingAudiences marketingAudience = enumFilterMarketingAudiences.NotSet,
                                     enumFilterYesNoEither firmSyndicate = enumFilterYesNoEither.NotSet,
                                     List<enumFilterSellingFirms> sellingFirms = null,
                                     List<enumFilterSellingFirmsPersons> sellingFirmsPersons = null,
                                     List<enumFilterCategories> sellingCategories = null,
                                     enumFilterYesNoEither repSyndicate = enumFilterYesNoEither.NotSet,
                                     List<enumFilterReps> repPersons = null,
                                     List<enumFilterCategories> repCategories = null,
                                     List<enumFilterRegions> regions = null,
                                     bool includeDoNotContact = false,
                                     bool keepZReps = false )
        {
            LegendaryProducts         = legendaryProducts?.ToList();
            MarketingAudience         = marketingAudience;
            FirmSyndicate             = firmSyndicate;
            ListOfSellingFirms        = sellingFirms?.ToList();
            ListOfSellingFirmsPersons = sellingFirmsPersons?.ToList();
            ListOfSellingCategories   = sellingCategories?.ToList();
            RepSyndicate              = repSyndicate;
            ListOfRepPersons          = repPersons?.ToList();
            ListOfRepCategories       = repCategories?.ToList();
            ListOfRegions             = regions?.ToList();
            IncludeDoNotContact       = includeDoNotContact;
            KeepZReps                 = keepZReps;
        }

        public List<enumLegendaryProducts> LegendaryProducts { get; } = null;
        public enumFilterMarketingAudiences MarketingAudience { get; } = enumFilterMarketingAudiences.NotSet;
        public enumFilterYesNoEither FirmSyndicate { get; } = enumFilterYesNoEither.NotSet;
        public List<enumFilterSellingFirms> ListOfSellingFirms { get; } = null;
        public List<enumFilterSellingFirmsPersons> ListOfSellingFirmsPersons { get; } = null;
        public List<enumFilterCategories> ListOfSellingCategories { get; } = null;
        public enumFilterYesNoEither RepSyndicate { get; } = enumFilterYesNoEither.NotSet;
        public List<enumFilterReps> ListOfRepPersons { get; } = null;
        public List<enumFilterCategories> ListOfRepCategories { get; } = null;
        public List<enumFilterRegions> ListOfRegions { get; } = null;
        public bool IncludeDoNotContact { get; } = false;
        public bool KeepZReps { get; } = false;

        public string Description
        {
            get
            {
                string description = "";

                List<string> strings = new List<string>();
                foreach (var product in LegendaryProducts ?? new List<enumLegendaryProducts>())
                    strings.Add(product.ToString());
                if (strings.Count > 0)
                    description += $@"Funds: <{strings.Aggregate((x, y) => x.ToString() + "," + y.ToString())}> ";

                description += "  Firms: ";

                if (FirmSyndicate == enumFilterYesNoEither.Yes)
                    description += "<FirmHasSA> ";
                else if (FirmSyndicate == enumFilterYesNoEither.No)
                    description += "<FirmNoSA> ";
                else if (FirmSyndicate == enumFilterYesNoEither.Either)
                    description += "<FirmSAEither> ";

                strings = new List<string>();
                foreach (var product in ListOfSellingFirms ?? new List<enumFilterSellingFirms>())
                    strings.Add(product.ToString());
                if (strings.Count > 0)
                    description += $@"<{strings.Aggregate((x, y) => x.ToString() + "," + y.ToString())}> ";

                strings = new List<string>();
                foreach (var product in ListOfSellingCategories ?? new List<enumFilterCategories>())
                    strings.Add(product.ToString());
                strings.Reverse();
                if (strings.Count > 0)
                    description += $@"<{strings.Aggregate((x, y) => x.ToString() + "," + y.ToString())}> ";

                strings = new List<string>();
                foreach (var product in ListOfSellingFirmsPersons ?? new List<enumFilterSellingFirmsPersons>())
                    strings.Add(product.ToString());
                if (strings.Count > 0)
                    description += $@"<{strings.Aggregate((x, y) => x.ToString() + "," + y.ToString())}> ";

                description += "  Reps: ";

                if (RepSyndicate == enumFilterYesNoEither.Yes)
                    description += "<RepHasSA> ";
                else if (RepSyndicate == enumFilterYesNoEither.No)
                    description += "<RepNoSA> ";
                else if (RepSyndicate == enumFilterYesNoEither.Either)
                    description += "<RepSAEither> ";

                strings = new List<string>();
                foreach (var product in ListOfRepPersons ?? new List<enumFilterReps>())
                    strings.Add(product.ToString());
                if (strings.Count > 0)
                    description += $@"<{strings.Aggregate((x, y) => x.ToString() + "," + y.ToString())}> ";

                strings = new List<string>();
                foreach (var product in ListOfRepCategories ?? new List<enumFilterCategories>())
                    strings.Add(product.ToString());
                strings.Reverse();
                if (strings.Count > 0)
                    description += $@"<{strings.Aggregate((x, y) => x.ToString() + "," + y.ToString())}> ";

                strings = new List<string>();
                foreach (var product in ListOfRegions ?? new List<enumFilterRegions>())
                    strings.Add(product.ToString());
                if (strings.Count > 0)
                    description += $@"<{strings.Aggregate((x, y) => x.ToString() + "," + y.ToString())}> ";

                strings = new List<string>();
                if (KeepZReps) strings.Add("KeepZReps");
                if (IncludeDoNotContact) strings.Add("IncludeDoNotContact");
                if (strings.Count > 0)
                    description += $@"  Options: <{strings.Aggregate((x, y) => x.ToString() + "," + y.ToString())}> ";

                return description;
            }
        }

        static public List<PipedriveTarget> Filter(List<Deal> deals, List<Person> persons, List<Organization> organizations, PipedriveTargetFilter filter)
        {
            List<PipedriveTarget> result = new List<PipedriveTarget>();
            var allPersons = new List<Person>();
            PipedriveTarget pipedriveTarget = null;

            try
            {
                switch (filter.MarketingAudience)
                {
                    case enumFilterMarketingAudiences.FundSubscriptions:
                        allPersons = FilterAudienceFundSubscriptions(deals, persons, organizations, filter);
                        break;

                    case enumFilterMarketingAudiences.SellingFirms:
                        allPersons = FilterAudienceSellingFirmsReps(deals, persons, organizations, filter);
                        break;

                    default:
                        Legendary.UpdateStatus($@"Audience of that Type is not implemented yet.");
                        break;
                }

                // Final "hard-coded" filtering out of Legendary Capital (2073) and Lodgind Fund REIT (583) persons...
                allPersons = allPersons.Where(x => ((x.OrgId?.Value ?? -1) != 2073) && ((x.OrgId?.Value ?? -1) != 583)).ToList() ?? new List<Person>();

                // Pull out the do not contact persons...
                if (! filter.IncludeDoNotContact)
                    allPersons = allPersons.Where(x => ! (string.Compare((x.CustomFields[PersonCustomFields.DoNotContact] as StringCustomField)?.Value ?? "",
                                                                         ((int)PersonCustomFields.DoNotContactEnum.DoNotContact).ToString(), true) == 0))
                                           .ToList() ?? new List<Person>();

                // Put into pipedrive target list...
                foreach (var person in allPersons)
                {
                    var address = person.CustomFields?[PersonCustomFields.Address] as Pipedrive.CustomFields.AddressCustomField;
                    var email = person.Email?[0]?.Value;
                    var phone = person.Phone?[0]?.Value;
                    string repHasSoldLOFReit = GetPersonCustomFieldEnumStringValue(PersonCustomFields.RepLOFReitHasSold, PersonCustomFields.RepLOFReitHasSolds, person);
                    string repLOFReitCoding = (person.CustomFields[PersonCustomFields.RepLOFReitCoding] as StringCustomField)?.Value ?? "";
                    string repHasSoldLFReitIII = GetPersonCustomFieldEnumStringValue(PersonCustomFields.RepLFReitIIIHasSold, PersonCustomFields.RepLFReitIIIHasSolds, person);
                    string repLFReitIIICoding = (person.CustomFields[PersonCustomFields.RepLFReitIIICoding] as StringCustomField)?.Value ?? "";
                    string firmLOFReitHasSA = GetPersonCustomFieldEnumStringValue(PersonCustomFields.FirmLOFReitHasSA, PersonCustomFields.FirmLOFReitHasSAs, person);
                    string firmLOFReitCoding = (person.CustomFields[PersonCustomFields.FirmLOFReitCoding] as StringCustomField)?.Value ?? "";
                    string firmLFReitIIIHasSA = GetPersonCustomFieldEnumStringValue(PersonCustomFields.FirmLFReitIIIHasSA, PersonCustomFields.FirmLFReitIIIHasSAs, person);
                    string firmLFReitIIICoding = (person.CustomFields[PersonCustomFields.FirmLFReitIIICoding] as StringCustomField)?.Value ?? "";
                    var contactType = GetPersonCustomFieldEnumStringValue(PersonCustomFields.ContactType, PersonCustomFields.ContactTypes, person);
                    string category = GetPersonCustomFieldEnumStringValue(PersonCustomFields.HomeOfficeDistribution, PersonCustomFields.HomeOfficeDistributions, person);
                    string contactability = GetPersonCustomFieldEnumStringValue(PersonCustomFields.DoNotContact, PersonCustomFields.DoNotContacts, person);
                    long crd = (person.CustomFields[PersonCustomFields.CRD] as LongCustomField)?.Value ?? 0;

                    if (!((email != null) && email.Contains("lodgingfund.com")))
                    {
                        pipedriveTarget = new PipedriveTarget(contactOrigin, "Not Set", contactType,
                                                              person.Id, person.Name, person.FirstName, person.LastName, "", crd, person.Email[0]?.Value, category, phone,
                                                              address?.FormattedAddress, address?.StreetNumber, "", address?.Sublocality, address?.Subpremise, address?.PostalCode,
                                                              (person.OrgName ?? ""), (person.OrgId?.Value ?? 0),
                                                              firmLOFReitHasSA, firmLOFReitCoding, firmLFReitIIIHasSA, firmLFReitIIICoding,
                                                              "", "", "",
                                                              contactability, "",
                                                              repHasSoldLOFReit, repLOFReitCoding, repHasSoldLFReitIII, repLFReitIIICoding);
                        result.Add(pipedriveTarget);
                    }
                }

                result = result.Distinct(new DistinctEmailPipedriveTargetComparer()).ToList();

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            return result;
        }

        static private PipedriveTarget GetPipedriveTargetFromPerson(Person person, List<Organization> organizations)
        {
            PipedriveTarget pipedriveTarget = null;

            try
            {
                var address = GetPersonAddress(person, out string street, out string street2, out string city, out string state, out string zip);
                string email = person.Email?[0]?.Value;
                string phone = person.Phone?[0]?.Value;
                long crd = (person.CustomFields[PersonCustomFields.CRD] as LongCustomField)?.Value ?? 0;
                string category = GetPersonCustomFieldEnumStringValue(PersonCustomFields.HomeOfficeDistribution, PersonCustomFields.HomeOfficeDistributions, person);
                string contactability = GetPersonCustomFieldEnumStringValue(PersonCustomFields.DoNotContact, PersonCustomFields.DoNotContacts, person);
                string emailOptedOut = GetPersonCustomFieldEnumStringValue(PersonCustomFields.OptedOutOfEmail, PersonCustomFields.OptedOutOfEmails, person);
                string repHasSoldLOFReit = GetPersonCustomFieldEnumStringValue(PersonCustomFields.RepLOFReitHasSold, PersonCustomFields.RepLOFReitHasSolds, person);
                string repLOFReitCoding = (person.CustomFields[PersonCustomFields.RepLOFReitCoding] as StringCustomField)?.Value ?? "";
                string repHasSoldLFReitIII = GetPersonCustomFieldEnumStringValue(PersonCustomFields.RepLFReitIIIHasSold, PersonCustomFields.RepLFReitIIIHasSolds, person);
                string repLFReitIIICoding = (person.CustomFields[PersonCustomFields.RepLFReitIIICoding] as StringCustomField)?.Value ?? "";
                string rvp = GetPersonCustomFieldEnumStringValue(PersonCustomFields.RVP, PersonCustomFields.RVPs, person);
                string avp = GetPersonCustomFieldEnumStringValue(PersonCustomFields.AVP, PersonCustomFields.AVPs, person);
                string distributionRegion = GetPersonCustomFieldEnumStringValue(PersonCustomFields.DistributionRegion, PersonCustomFields.DistributionRegions, person);
                string contactType = GetPersonCustomFieldEnumStringValue(PersonCustomFields.ContactType, PersonCustomFields.ContactTypes, person);
                string contactBaseType = "Not Set";

                // Get the firm has SA and coding values
                string firmLOFReitHasSA = GetPersonCustomFieldEnumStringValue(PersonCustomFields.FirmLOFReitHasSA, PersonCustomFields.FirmLOFReitHasSAs, person);
                string firmLOFReitCoding = (person.CustomFields[PersonCustomFields.FirmLOFReitCoding] as StringCustomField)?.Value ?? "";
                string firmLFReitIIIHasSA = GetPersonCustomFieldEnumStringValue(PersonCustomFields.FirmLFReitIIIHasSA, PersonCustomFields.FirmLFReitIIIHasSAs, person);
                string firmLFReitIIICoding = (person.CustomFields[PersonCustomFields.FirmLFReitIIICoding] as StringCustomField)?.Value ?? "";
                string firmName = person.OrgName;
                long firmID = (person.OrgId?.Value ?? 0);
                if (contactType == "Not Set")
                {
                    var organizationList = organizations.Where(x => x.Id == (person.OrgId?.Value ?? -2)).ToList() ?? new List<Organization>();
                    var organization = organizationList.Count == 1 ? organizationList[0] : null;
                    string firmType = "";
                    if (organization != null)
                    {
                        firmType = GetOrganizationCustomFieldEnumStringValue(OrganizationCustomFields.OrgType, OrganizationCustomFields.OrgTypes, organization);
                        if (GetCustomFieldEnumValueFromStringValue(OrganizationCustomFields.OrgTypes, firmType) == (int)OrgFields.OrgTypeEnum.RIA)
                            contactType = GetCustomFieldStringValueFromEnumValue(PersonCustomFields.ContactTypes, (int)PersonCustomFields.ContactTypeEnum.RIA);
                        if (GetCustomFieldEnumValueFromStringValue(OrganizationCustomFields.OrgTypes, firmType) == (int)OrgFields.OrgTypeEnum.BrokerDealer)
                            contactType = GetCustomFieldStringValueFromEnumValue(PersonCustomFields.ContactTypes, (int)PersonCustomFields.ContactTypeEnum.BD);
                    }
                }

                pipedriveTarget = new PipedriveTarget(contactOrigin, contactBaseType, contactType,
                                                      person.Id, person.Name, person.FirstName, person.LastName, "", crd, person.Email[0]?.Value, category, phone,
                                                      address?.FormattedAddress, street, street2, city, state, zip,
                                                      firmName, firmID, firmLOFReitHasSA, firmLOFReitCoding, firmLFReitIIIHasSA, firmLFReitIIICoding,
                                                      rvp, avp, distributionRegion,
                                                      contactability, emailOptedOut,
                                                      repHasSoldLOFReit, repLOFReitCoding, repHasSoldLFReitIII, repLFReitIIICoding);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }

            return pipedriveTarget;
        }

        static public List<PipedriveTarget> GetAllPersonsForConstantContact(List<Deal> deals, List<Person> persons, List<Organization> organizations)
        {
            List<PipedriveTarget> result = new List<PipedriveTarget>();
            var allPersons = new List<Person>();
            PipedriveTarget pipedriveTarget = null;

            try
            {
                // Filter out Legendary Capital (2073) and Lodgind Fund REIT (583) persons...
                allPersons = persons.Where(x => ((x.OrgId?.Value ?? -1) != 2073) && ((x.OrgId?.Value ?? -1) != 583)).ToList() ?? new List<Person>();

                foreach (var person in allPersons)
                {
                    var email = person.Email[0]?.Value;
                    if (!((email != null) && email.Contains("lodgingfund.com")))
                    {
                        pipedriveTarget = GetPipedriveTargetFromPerson(person, organizations);
                        result.Add(pipedriveTarget);
                    }
                }

               ////// result = result.Distinct(new DistinctEmailPipedriveTargetComparer()).ToList();

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            return result;
        }

        static private string GetPersonCustomFieldEnumStringValue(string personCustomField, (int, string)[] valueArray, Person person)
        {
            int valueID = 0;
            if (int.TryParse(((person.CustomFields[personCustomField] as StringCustomField)?.Value ?? "0"), out valueID))
                foreach (var tuple in valueArray)
                    if (tuple.Item1 == valueID)
                        return tuple.Item2;
            return "Not Set";
        }

        static private string GetOrganizationCustomFieldEnumStringValue(string organizationCustomField, (int, string)[] valueArray, Organization organization)
        {
            int valueID = 0;
            if (int.TryParse(((organization.CustomFields[organizationCustomField] as StringCustomField)?.Value ?? "0"), out valueID))
                foreach (var tuple in valueArray)
                    if (tuple.Item1 == valueID)
                        return tuple.Item2;
            return "Not Set";
        }

        static private string GetCustomFieldStringValueFromEnumValue((int, string)[] valueArray, int enumValue)
        {
            string result = "";
            foreach (var tuple in valueArray)
                if (tuple.Item1 == enumValue)
                    result = tuple.Item2;
            return result;
        }

        static private int GetCustomFieldEnumValueFromStringValue((int, string)[] valueArray, string stringValue)
        {
            int result = 0;
            foreach (var tuple in valueArray)
                if (stringValue == tuple.Item2)
                    result = tuple.Item1;
            return result;
        }

        static private AddressCustomField GetPersonAddress(Person person, out string street, out string street2, out string city, out string state, out string zip)
        {
            AddressCustomField address = person.CustomFields?[PersonCustomFields.Address] as Pipedrive.CustomFields.AddressCustomField;
            street = $"{address?.StreetNumber ?? ""} { address?.Route ?? ""}";
            street2 = address?.Subpremise ?? "";
            city = address?.Locality ?? ""; ;
            state = address?.AdminAreaLevel1 ?? "";
            zip = address?.PostalCode ?? ""; ;
            return address;
        }

        static public List<PipedriveTarget> UpdateSpecialContactFields(List<Deal> deals, List<Person> persons, List<Organization> organizations)
        {
            List<PipedriveTarget> result = new List<PipedriveTarget>();

            try
            {
                PipedriveTarget pdt = null;
                var allPersons = new List<Person>();
                var lofReitDeals = deals.Where(x => x.PipelineId == PipelineList.LOF_REIT_SAs.ID).ToList();
                var lfReitIIIDeals = deals.Where(x => x.PipelineId == PipelineList.LF_REIT_III_SAs.ID).ToList();
                var repDeals = deals.Where(x => x.PipelineId == PipelineList.RepActivation.ID).ToList();
                var lofReitSubDeals = deals.Where(x => x.PipelineId == PipelineList.LOFSubscriptions.ID).ToList();
                var lfReitIIISubDeals = deals.Where(x => x.PipelineId == PipelineList.LF_REIT_III_Subscriptions.ID).ToList();

                // Filter out of Legendary Capital (2073) and Lodgind Fund REIT (583) persons...
                allPersons = persons.Where(x => ((x.OrgId?.Value ?? -1) != 2073) && ((x.OrgId?.Value ?? -1) != 583)).ToList() ?? new List<Person>();

                // Put into pipedrive target list...
                foreach (var person in allPersons)
                {
                    // Get base pipedrive target object based only on the person fields....
                    pdt = GetPipedriveTargetFromPerson(person, organizations);

                    // Get caclulated fields and see if they are different....
                    bool somethingHasChanged = false;

                    string repHasSoldLOFReit = "No";
                    string repLOFReitCoding = "Not Set";
                    string repHasSoldLFReitIII = "No";
                    string repLFReitIIICoding = "Not Set";

                    var dealsForRep = lofReitSubDeals.Where(x => (x.PersonId?.Value ?? -1) == person.Id).ToList() ?? new List<Deal>();
                    if (dealsForRep.Count > 0)
                        repHasSoldLOFReit = "Yes";

                    dealsForRep = lfReitIIISubDeals.Where(x => (x.PersonId?.Value ?? -1) == person.Id).ToList() ?? new List<Deal>();
                    if (dealsForRep.Count > 0)
                        repHasSoldLFReitIII = "Yes";

                    var dealForRep = repDeals.Where(x => (x.PersonId?.Value ?? -1) == person.Id).ToList() ?? new List<Deal>();
                    if (dealForRep.Count > 0)
                    {
                        var deal = dealForRep[0];
                        if (deal.StageId == RepStages.A.ID) repLFReitIIICoding       = $"{enumFilterCategories.A.ToString()}";
                        else if (deal.StageId == RepStages.B.ID) repLFReitIIICoding  = $"{enumFilterCategories.B.ToString()}";
                        else if (deal.StageId == RepStages.H.ID) repLFReitIIICoding = $"{enumFilterCategories.H.ToString()}";
                        else if (deal.StageId == RepStages.I.ID) repLFReitIIICoding = $"{enumFilterCategories.I.ToString()}";
                        else if (deal.StageId == RepStages.NC.ID) repLFReitIIICoding = $"{enumFilterCategories.NC.ToString()}";
                        else if (deal.StageId == RepStages.P.ID) repLFReitIIICoding  = $"{enumFilterCategories.P.ToString()}";
                        else if (deal.StageId == RepStages.T.ID) repLFReitIIICoding  = $"{enumFilterCategories.T.ToString()}";
                        else if (deal.StageId == RepStages.Z.ID) repLFReitIIICoding  = $"{enumFilterCategories.Z.ToString()}";
                    }

                    // Get Firm's Coding and HasSA information...
                    var organizationList = organizations.Where(x => x.Id == (person.OrgId?.Value ?? -2)).ToList() ?? new List<Organization>();
                    var organization = organizationList.Count == 1 ? organizationList[0] : null;

                    string firmLOFReitHasSA = "No";
                    string firmLOFReitCoding = "";
                    string firmLFReitIIIHasSA = "No";
                    string firmLFReitIIICoding = "";

                    if (organization != null)
                    {
                        if (string.Compare((organization.CustomFields[OrgFields.HasLOFReitSA] as StringCustomField)?.Value ?? "No", OrgFields.HasLOFReitSAs.Yes, true) == 0)
                            firmLOFReitHasSA = "Yes";

                        List<Deal> orgPipelineDeal = lofReitDeals.Where(x => (x.OrgId?.Value ?? -1) == organization.Id).ToList() ?? new List<Deal>();
                        if (orgPipelineDeal.Count > 0)
                        {
                            var deal = orgPipelineDeal[0];
                            if (deal.StageId == LOFStages.ActiveSelling.ID) firmLOFReitCoding = $"{enumFilterCategories.A.ToString()}";
                            else if (deal.StageId == LOFStages.Discovery.ID) firmLOFReitCoding = $"{enumFilterCategories.H.ToString()}";
                            else if (deal.StageId == LOFStages.DueDiligence.ID) firmLOFReitCoding = $"{enumFilterCategories.DD.ToString()}";
                            else if (deal.StageId == LOFStages.Potential.ID) firmLOFReitCoding = $"{enumFilterCategories.NC.ToString()}";
                            else if (deal.StageId == LOFStages.ContactMade.ID) firmLOFReitCoding = $"{enumFilterCategories.P.ToString()}";
                        }

                        if (string.Compare((organization.CustomFields[OrgFields.HasLFReit3SA] as StringCustomField)?.Value ?? "No", OrgFields.HasLFReit3SAs.Yes, true) == 0)
                            firmLFReitIIIHasSA = "Yes";

                        orgPipelineDeal = lfReitIIIDeals.Where(x => (x.OrgId?.Value ?? -1) == organization.Id).ToList() ?? new List<Deal>();
                        if (orgPipelineDeal.Count > 0)
                        {
                            var deal = orgPipelineDeal[0];
                            if (deal.StageId == ReitIIIStages.A.ID) firmLFReitIIICoding = $"{enumFilterCategories.A.ToString()}";
                            else if (deal.StageId == ReitIIIStages.B.ID) firmLFReitIIICoding = $"{enumFilterCategories.B.ToString()}";
                            else if (deal.StageId == ReitIIIStages.H.ID) firmLFReitIIICoding = $"{enumFilterCategories.H.ToString()}";
                            else if (deal.StageId == ReitIIIStages.DD.ID) firmLFReitIIICoding = $"{enumFilterCategories.DD.ToString()}";
                            else if (deal.StageId == ReitIIIStages.NC.ID) firmLFReitIIICoding = $"{enumFilterCategories.NC.ToString()}";
                            else if (deal.StageId == ReitIIIStages.P.ID) firmLFReitIIICoding = $"{enumFilterCategories.P.ToString()}";
                            else if (deal.StageId == ReitIIIStages.T.ID) firmLFReitIIICoding = $"{enumFilterCategories.T.ToString()}";
                            else if (deal.StageId == ReitIIIStages.Z.ID) firmLFReitIIICoding = $"{enumFilterCategories.Z.ToString()}";
                        }
                    }

                    if (string.Compare(firmLOFReitHasSA, pdt.FirmLOFReitHasSA) != 0) somethingHasChanged = true;
                    else if (string.Compare(firmLOFReitCoding, pdt.FirmLOFReitCoding) != 0) somethingHasChanged = true;
                    else if (string.Compare(firmLFReitIIIHasSA, pdt.FirmLFReitIIIHasSA) != 0) somethingHasChanged = true;
                    else if (string.Compare(firmLFReitIIICoding, pdt.FirmLFReitIIICoding) != 0) somethingHasChanged = true;
                    else if (string.Compare(repHasSoldLOFReit, pdt.RepHasSoldLOFReit) != 0) somethingHasChanged = true;
                    else if (string.Compare(repLOFReitCoding, pdt.RepLOFReitCoding) != 0) somethingHasChanged = true;
                    else if (string.Compare(repHasSoldLFReitIII, pdt.RepHasSoldLFReitIII) != 0) somethingHasChanged = true;
                    else if (string.Compare(repLFReitIIICoding, pdt.RepLFReitIIICoding) != 0) somethingHasChanged = true;

                    // Figure out the contact base type...
                    string contactBaseType = "Not Set";
                    if (((repLFReitIIICoding.Length > 0) && (repLFReitIIICoding != "Not Set")) || ((repLOFReitCoding.Length > 0) && (repLOFReitCoding != "Not Set")))
                        contactBaseType = "Advisor";

                    if (somethingHasChanged)
                    {
                        var newPipedriveTarget = new PipedriveTarget(contactOrigin, contactBaseType, pdt.ContactType,
                                                                     pdt.ContactOriginId, pdt.WholeName, pdt.FirstName, pdt.LastName, pdt.NickName, pdt.CRD, pdt.Email, pdt.Category, pdt.Phone,
                                                                     pdt.Address, pdt.Street, pdt.Street2, pdt.City, pdt.State, pdt.Zip,
                                                                     pdt.FirmName, pdt.FirmId, firmLOFReitHasSA, firmLOFReitCoding, firmLFReitIIIHasSA, firmLOFReitCoding,
                                                                     pdt.RVP, pdt.AVP, pdt.DistributionRegion,
                                                                     pdt.Contactability, pdt.DoNotEmail,
                                                                     repHasSoldLOFReit, repLOFReitCoding, repHasSoldLFReitIII, repLFReitIIICoding);
                        result.Add(newPipedriveTarget);
                    }

                }

      ///          foreach (var pt in result)
                //{
                //    var person = new Pipedrive.Person();
                //    person.ToUpdate();
                //}

      ////          result = result.Distinct(new DistinctEmailPipedriveTargetComparer()).ToList();

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            return result;
        }

        static private string GetRepHasSoldString(Person person, List<Deal> repDeals, List<Deal> lofReitSubDeals, List<Deal> lfReitIIISubDeals)
        {
            string result = "";

            try
            {
                var dealsForRep = lofReitSubDeals.Where(x => (x.PersonId?.Value ?? -1) == person.Id).ToList() ?? new List<Deal>();
                if (dealsForRep.Count > 0)
                    result = $"{enumLegendaryProducts.LOFReit.ToString()}{listDelimeter}";

                dealsForRep = lfReitIIISubDeals.Where(x => (x.PersonId?.Value ?? -1) == person.Id).ToList() ?? new List<Deal>();
                if (dealsForRep.Count > 0)
                    result = $"{result}{enumLegendaryProducts.LFReitIII.ToString()}{listDelimeter}";

                if (result.Length > 0) // remove ending comma...
                    result = result.Substring(0, result.Length - 1);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }

            return result;
        }

        static private string GetRepCodingsString(Person person, List<Deal> repDeals, List<Deal> lofReitSubDeals, List<Deal> lfReitIIISubDeals)
        {
            string result = "";

            try
            {
                var dealForRep = repDeals.Where(x => (x.PersonId?.Value ?? -1) == person.Id).ToList() ?? new List<Deal>();
                if (dealForRep.Count > 0)
                {
                    var deal = dealForRep[0];
                    if (deal.StageId == RepStages.A.ID) result = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.A.ToString()}";
                    else if (deal.StageId == RepStages.B.ID) result = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.B.ToString()}";
                    else if (deal.StageId == RepStages.H.ID) result = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.H.ToString()}";
                    else if (deal.StageId == RepStages.NC.ID) result = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.NC.ToString()}";
                    else if (deal.StageId == RepStages.P.ID) result = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.P.ToString()}";
                    else if (deal.StageId == RepStages.T.ID) result = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.T.ToString()}";
                    else if (deal.StageId == RepStages.Z.ID) result = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.Z.ToString()}";
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }

            return result;
        }

        static private string GetFirmsCodingsString(Person person, long firmID, List<Deal> lofReitDeals, List<Deal> lfReitIIIDeals, Organization organization)
        {
            string result = "";

            try
            {
                if (organization != null)
                {
                    string result1 = "";
                    string result2 = "";

                    List<Deal> orgPipelineDeal = lofReitDeals.Where(x => (x.OrgId?.Value ?? -1) == firmID).ToList() ?? new List<Deal>();
                    if (orgPipelineDeal.Count > 0)
                    {
                        var deal = orgPipelineDeal[0];
                        if (deal.StageId == LOFStages.ActiveSelling.ID) result1 = $"{enumLegendaryProducts.LOFReit.ToString()}-{enumFilterCategories.A.ToString()}";
                        else if (deal.StageId == LOFStages.Discovery.ID) result1 = $"{enumLegendaryProducts.LOFReit.ToString()}-{enumFilterCategories.H.ToString()}";
                        else if (deal.StageId == LOFStages.DueDiligence.ID) result1 = $"{enumLegendaryProducts.LOFReit.ToString()}-{enumFilterCategories.DD.ToString()}";
                        else if (deal.StageId == LOFStages.Potential.ID) result1 = $"{enumLegendaryProducts.LOFReit.ToString()}-{enumFilterCategories.NC.ToString()}";
                        else if (deal.StageId == LOFStages.ContactMade.ID) result1 = $"{enumLegendaryProducts.LOFReit.ToString()}-{enumFilterCategories.P.ToString()}";
                    }

                    orgPipelineDeal = lfReitIIIDeals.Where(x => (x.OrgId?.Value ?? -1) == firmID).ToList() ?? new List<Deal>();
                    if (orgPipelineDeal.Count > 0)
                    {
                        var deal = orgPipelineDeal[0];
                        if (deal.StageId == ReitIIIStages.A.ID) result2 = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.A.ToString()}";
                        else if (deal.StageId == ReitIIIStages.B.ID) result2 = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.B.ToString()}";
                        else if (deal.StageId == ReitIIIStages.H.ID) result2 = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.H.ToString()}";
                        else if (deal.StageId == ReitIIIStages.DD.ID) result2 = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.DD.ToString()}";
                        else if (deal.StageId == ReitIIIStages.NC.ID) result2 = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.NC.ToString()}";
                        else if (deal.StageId == ReitIIIStages.P.ID) result2 = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.P.ToString()}";
                        else if (deal.StageId == ReitIIIStages.T.ID) result2 = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.T.ToString()}";
                        else if (deal.StageId == ReitIIIStages.Z.ID) result2 = $"{enumLegendaryProducts.LFReitIII.ToString()}-{enumFilterCategories.Z.ToString()}";
                        if (result1.Length > 0)
                            result2 = $"{listDelimeter}{result2}";
                    }

                    result = $"{result1}{result2}";
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            return result;
        }

        static private string GetFirmHasSAString(Person person, long firmID, List<Deal> lofReitDeals, List<Deal> lfReitIIIDeals, Organization organization)
        {
            string result = "";

            try
            {
                string result1 = "";
                string result2 = "";
                if (organization != null)
                {
                    if (string.Compare((organization.CustomFields[OrgFields.HasLOFReitSA] as StringCustomField)?.Value ?? "No", OrgFields.HasLOFReitSAs.Yes, true) == 0)
                        result1 = $"{enumLegendaryProducts.LOFReit.ToString()}";
                    if (string.Compare((organization.CustomFields[OrgFields.HasLFReit3SA] as StringCustomField)?.Value ?? "No", OrgFields.HasLFReit3SAs.Yes, true) == 0)
                    {
                        result2 = $"{enumLegendaryProducts.LFReitIII.ToString()}";
                        if (result1.Length > 0)
                            result2 = $"{listDelimeter}{result2}";
                    }
                }
                result = $"{result1}{result2}";
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            return result;
        }

        static private List<Person> FilterAudienceFundSubscriptions(List<Deal> deals, List<Person> persons, List<Organization> organizations, PipedriveTargetFilter filter)
        {
            List<Deal> subscriptionDeals = new List<Deal>();
            List<Organization> syndicateOrganizations = new List<Organization>();
            if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LOFReit))
                            subscriptionDeals.AddRange(deals.Where(x => x.PipelineId == PipelineList.LOFSubscriptions.ID).ToList());
                        if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LFReitIII))
                            subscriptionDeals.AddRange(deals.Where(x => x.PipelineId == PipelineList.LF_REIT_III_Subscriptions.ID).ToList());
            var subscriptionPersons = persons.Where(x => subscriptionDeals.Any(y => (y.OrgId?.Value ?? -2) == (x.OrgId?.Value ?? -1))).ToList() ?? new List<Person>();
            return subscriptionPersons;
        }

        static private List<Person> FilterAudienceSellingFirmsReps(List<Deal> deals, List<Person> persons, List<Organization> organizations, PipedriveTargetFilter filter)
        {
            // Get out quick if nothing is set...
            if ((filter.FirmSyndicate == enumFilterYesNoEither.NotSet) &&
                ((filter.ListOfSellingFirms == null) || (filter.ListOfSellingFirms.Count == 0)) &&
                ((filter.ListOfSellingFirmsPersons == null) || (filter.ListOfSellingFirmsPersons.Count == 0)) &&
                ((filter.ListOfSellingCategories == null) || (filter.ListOfSellingCategories.Count == 0)))
                return GetRepPersons(deals, persons, organizations, filter);

            List<Organization> syndicateFirms = new List<Organization>();

            // Initial filter based on syndicate setting...
            if (filter.FirmSyndicate == enumFilterYesNoEither.NotSet)
                syndicateFirms.AddRange(organizations);

            if ((filter.FirmSyndicate == enumFilterYesNoEither.Yes) || (filter.FirmSyndicate == enumFilterYesNoEither.Either))
            {
                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LOFReit))
                    syndicateFirms.AddRange(organizations
                        .Where(x => string.Compare((x.CustomFields[OrgFields.HasLOFReitSA] as StringCustomField)?.Value, OrgFields.HasLOFReitSAs.Yes, true) == 0)
                        .ToList() ?? new List<Organization>());

                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LFReitIII))
                    syndicateFirms.AddRange(organizations
                            .Where(x => string.Compare((x.CustomFields[OrgFields.HasLFReit3SA] as StringCustomField)?.Value, OrgFields.HasLFReit3SAs.Yes, true) == 0)
                            .ToList() ?? new List<Organization>());
            }

            // For NO, we assume NULL is also a matching value...
            if ((filter.FirmSyndicate == enumFilterYesNoEither.No) || (filter.FirmSyndicate == enumFilterYesNoEither.Either))
            {
                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LOFReit))
                    syndicateFirms.AddRange(organizations
                        .Where(x => ((x.CustomFields[OrgFields.HasLOFReitSA] as StringCustomField) == null) ||
                                    (string.Compare((x.CustomFields[OrgFields.HasLOFReitSA] as StringCustomField)?.Value, OrgFields.HasLOFReitSAs.No, true) == 0))
                        .ToList() ?? new List<Organization>());

                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LFReitIII))
                    if ((filter.FirmSyndicate == enumFilterYesNoEither.No) || (filter.FirmSyndicate == enumFilterYesNoEither.Either))
                        syndicateFirms.AddRange(organizations
                            .Where(x => ((x.CustomFields[OrganizationCustomFields.HasLFReit3SA] as StringCustomField) == null) || 
                                        (string.Compare((x.CustomFields[OrgFields.HasLFReit3SA] as StringCustomField)?.Value, OrgFields.HasLFReit3SAs.No, true) == 0))
                            .ToList() ?? new List<Organization>());
            }

            // Filter down the firms some more based on the selling firms settings...
            var typeOfFirms = new List<Organization>();
            if (filter.ListOfSellingFirms.Contains(enumFilterSellingFirms.AnyFirm))
            {
                typeOfFirms = syndicateFirms.ToList();
            }
            else
            {
                if (filter.ListOfSellingFirms.Contains(enumFilterSellingFirms.BrokerDealer))
                    typeOfFirms.AddRange(syndicateFirms
                        .Where(x => string.Compare((x.CustomFields[OrgFields.OrgType] as StringCustomField)?.Value,
                                                    GetCustomFieldStringValueFromEnumValue(OrgFields.OrgTypes, (int)OrgFields.OrgTypeEnum.BrokerDealer), true) == 0)
                        .ToList() ?? new List<Organization>());
                if (filter.ListOfSellingFirms.Contains(enumFilterSellingFirms.RIA))
                    typeOfFirms.AddRange(syndicateFirms
                        .Where(x => string.Compare((x.CustomFields[OrgFields.OrgType] as StringCustomField)?.Value,
                                                   GetCustomFieldStringValueFromEnumValue(OrgFields.OrgTypes, (int)OrgFields.OrgTypeEnum.RIA), true) == 0)
                        .ToList() ?? new List<Organization>());
            }

            // Finally, filter by category if any set...
            var categoryFirms = new List<Organization>();
            if ((filter.ListOfSellingCategories is null) || (filter.ListOfSellingCategories.Count == 0))
            {
                categoryFirms = typeOfFirms.ToList();
            }
            else
            {
                // This gets a little trickier, as up to this point we've only been dealing with Firm information, now we have to tie the firm to deal and see what stage the deal is in...
                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LOFReit))
                {
                    var lofDeals = deals.Where(x => x.PipelineId == PipelineList.LOF_REIT_SAs.ID).ToList();
                    var lofDealsToAdd = new List<Deal>();
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.A)) lofDealsToAdd.AddRange(lofDeals.Where(x => x.StageId == LOFStages.ActiveSelling.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.B)) lofDealsToAdd.AddRange(lofDeals.Where(x => x.StageId == LOFStages.ActiveSelling.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.H)) lofDealsToAdd.AddRange(lofDeals.Where(x => x.StageId == LOFStages.Discovery.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.DD)) lofDealsToAdd.AddRange(lofDeals.Where(x => x.StageId == LOFStages.DueDiligence.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.NC)) lofDealsToAdd.AddRange(lofDeals.Where(x => x.StageId == LOFStages.Potential.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.P)) lofDealsToAdd.AddRange(lofDeals.Where(x => x.StageId == LOFStages.Discovery.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.T)) lofDealsToAdd.AddRange(lofDeals.Where(x => x.StageId == LOFStages.ContactMade.ID).ToList() ?? new List<Deal>());
                    //if (filter.ListOfSellingCategories.Contains(enumFilterCategories.Z))  lofDealsToAdd.AddRange(lofDeals.Where(x => x.StageId == LOFStages.Z.ID).ToList() ?? new List<Deal>());
                    categoryFirms.AddRange(typeOfFirms.Where(x => lofDealsToAdd.Any(y => (y.OrgId?.Value ?? -1) == x.Id)).ToList() ?? new List<Organization>());
                }

                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LFReitIII))
                {
                    var lfDeals = deals.Where(x => x.PipelineId == PipelineList.LF_REIT_III_SAs.ID).ToList();
                    var lfDealsToAdd = new List<Deal>();
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.A)) lfDealsToAdd.AddRange(lfDeals.Where(x => x.StageId == ReitIIIStages.A.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.B)) lfDealsToAdd.AddRange(lfDeals.Where(x => x.StageId == ReitIIIStages.B.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.H)) lfDealsToAdd.AddRange(lfDeals.Where(x => x.StageId == ReitIIIStages.H.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.DD)) lfDealsToAdd.AddRange(lfDeals.Where(x => x.StageId == ReitIIIStages.DD.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.NC)) lfDealsToAdd.AddRange(lfDeals.Where(x => x.StageId == ReitIIIStages.NC.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.P)) lfDealsToAdd.AddRange(lfDeals.Where(x => x.StageId == ReitIIIStages.P.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.T)) lfDealsToAdd.AddRange(lfDeals.Where(x => x.StageId == ReitIIIStages.T.ID).ToList() ?? new List<Deal>());
                    if (filter.ListOfSellingCategories.Contains(enumFilterCategories.Z)) lfDealsToAdd.AddRange(lfDeals.Where(x => x.StageId == ReitIIIStages.Z.ID).ToList() ?? new List<Deal>());
                    categoryFirms.AddRange(typeOfFirms.Where(x => lfDealsToAdd.Any(y => (y.OrgId?.Value ?? -1) == x.Id)).ToList() ?? new List<Organization>());
                }
            }

            // We have the firms now, but we need persons to create a list, so get persons based on PERSONS settings...

            // Get all the persons for all the firms we filtered out...
            var firmPersons = new List<Person>();
            firmPersons.AddRange(persons.Where(x => categoryFirms.Any(y => (y.Id == (x.OrgId?.Value ?? -1)))).ToList() ?? new List<Person>());

            var firmPersonsToAdd = new List<Person>();
            if ((filter.ListOfSellingFirmsPersons == null) || (filter.ListOfSellingFirmsPersons.Count == 0))
            {
                firmPersonsToAdd.AddRange(firmPersons);
            }
            else
            {
                // Get the "Other Home Office" and "Due Diligence" persons from all persons...
                var hoPersons = persons.Where(x => string.Compare((x.CustomFields[PersonCustomFields.HomeOfficeDistribution] as StringCustomField)?.Value,
                                                                  ((int)PersonCustomFields.HomeOfficeDistributionEnum.HomeOfficeOtherContact).ToString(), true) == 0)
                                                    .ToList() ?? new List<Person>();
                var ddPersons = persons.Where(x => string.Compare((x.CustomFields[PersonCustomFields.HomeOfficeDistribution] as StringCustomField)?.Value,
                                                                  ((int)PersonCustomFields.HomeOfficeDistributionEnum.DueDiligence).ToString(), true) == 0)
                                                    .ToList() ?? new List<Person>();

                if (filter.ListOfSellingFirmsPersons.Contains(enumFilterSellingFirmsPersons.HomeOfficeOther))
                    firmPersonsToAdd.AddRange(firmPersons.Where(x => hoPersons.Any(y => y.Id == x.Id)).ToList() ?? new List<Person>());

                if (filter.ListOfSellingFirmsPersons.Contains(enumFilterSellingFirmsPersons.DueDiligence))
                    firmPersonsToAdd.AddRange(firmPersons.Where(x => ddPersons.Any(y => y.Id == x.Id)).ToList() ?? new List<Person>());
            }

            // Call separate function to get all the persons based on the Reps settings...
            var repPersonsToAdd = GetRepPersons(deals, persons, organizations, filter);

            // Add the firm persons to the rep persons to get one big list...
            var allPersons = new List<Person>();
            allPersons.AddRange(firmPersonsToAdd);
            allPersons.AddRange(repPersonsToAdd);

            return allPersons;
        }

        static public List<Person> GetRepPersons(List<Deal> deals, List<Person> persons, List<Organization> organizations, PipedriveTargetFilter filter)
        {
            // Get out quick if nothing is set...
            if ((filter.RepSyndicate == enumFilterYesNoEither.NotSet) && ((filter.ListOfRepPersons == null) || (filter.ListOfRepPersons.Count == 0)) &&
                ((filter.ListOfRepCategories == null) || (filter.ListOfRepCategories.Count == 0)))
                return new List<Person>();

            var repDeals = deals.Where(x => x.PipelineId == PipelineList.RepActivation.ID).ToList();
            var syndicateRepDeals = new List<Deal>();
            var subPersons = new List<Person>();

            if ((filter.RepSyndicate == enumFilterYesNoEither.NotSet))
            {
                syndicateRepDeals.AddRange(repDeals);
            } 

            if ((filter.RepSyndicate == enumFilterYesNoEither.Yes) || (filter.RepSyndicate == enumFilterYesNoEither.Either))
            {
                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LOFReit))
                {
                    syndicateRepDeals.AddRange(repDeals
                        .Where(x => string.Compare((x.CustomFields[DealCustomFields.HasLofReitSA] as Pipedrive.CustomFields.StringCustomField)?.Value,
                                                    DealCustomFields.HasLofReitSAs.Yes, true) == 0)
                        .ToList() ?? new List<Deal>());

                    // But wait, the LOF REIT A and B persons found from Rep Activation where the underlying person has a subscription in LOF REIT, so let's find those persons...
                    var subs = deals.Where(x => x.PipelineId == PipelineList.LOFSubscriptions.ID).ToList();
                    subPersons.AddRange(persons.Where(x => subs.Any(y => x.Id == (y.PersonId?.Value ?? -1))).ToList() ?? new List<Person>());
                }

                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LFReitIII))
                    syndicateRepDeals.AddRange(repDeals
                        .Where(x => string.Compare((x.CustomFields[DealCustomFields.HasLfReit3SA] as Pipedrive.CustomFields.StringCustomField)?.Value,
                                                    DealCustomFields.HasLFReit3SAs.Yes, true) == 0)
                        .ToList() ?? new List<Deal>());
            }

            // For NO, we assume NULL is also a matching value...
            if ((filter.RepSyndicate == enumFilterYesNoEither.No) || (filter.RepSyndicate == enumFilterYesNoEither.Either))
            {
                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LOFReit))
                    syndicateRepDeals.AddRange(repDeals
                        .Where(x => ((x.CustomFields[DealCustomFields.HasLofReitSA] as Pipedrive.CustomFields.StringCustomField) == null) || 
                                    (string.Compare((x.CustomFields[DealCustomFields.HasLofReitSA] as Pipedrive.CustomFields.StringCustomField)?.Value, DealCustomFields.HasLofReitSAs.No, true) == 0))
                        .ToList() ?? new List<Deal>());

                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LFReitIII))
                    syndicateRepDeals.AddRange(repDeals
                        .Where(x => ((x.CustomFields[DealCustomFields.HasLfReit3SA] as Pipedrive.CustomFields.StringCustomField) == null) || 
                                    (string.Compare((x.CustomFields[DealCustomFields.HasLfReit3SA] as Pipedrive.CustomFields.StringCustomField)?.Value, DealCustomFields.HasLFReit3SAs.No, true) == 0))
                        .ToList() ?? new List<Deal>());
            }

            // Now we need to use the deals that match the syndicate, and reduce down to include the proper categories...
            var categoryRepDeals = new List<Deal>();

            if ((filter.ListOfRepCategories == null) || (filter.ListOfRepCategories.Count == 0))
            {
                categoryRepDeals.AddRange(syndicateRepDeals);
            }
            else
            {
                if (filter.ListOfRepCategories.Contains(enumFilterCategories.A)) categoryRepDeals.AddRange(syndicateRepDeals.Where(x => x.StageId == RepStages.A.ID).ToList() ?? new List<Deal>());
                if (filter.ListOfRepCategories.Contains(enumFilterCategories.B)) categoryRepDeals.AddRange(syndicateRepDeals.Where(x => x.StageId == RepStages.B.ID).ToList() ?? new List<Deal>());
                if (filter.ListOfRepCategories.Contains(enumFilterCategories.H)) categoryRepDeals.AddRange(syndicateRepDeals.Where(x => x.StageId == RepStages.H.ID).ToList() ?? new List<Deal>());
                if (filter.ListOfRepCategories.Contains(enumFilterCategories.I)) categoryRepDeals.AddRange(syndicateRepDeals.Where(x => x.StageId == RepStages.I.ID).ToList() ?? new List<Deal>());
                if (filter.ListOfRepCategories.Contains(enumFilterCategories.NC)) categoryRepDeals.AddRange(syndicateRepDeals.Where(x => x.StageId == RepStages.NC.ID).ToList() ?? new List<Deal>());
                if (filter.ListOfRepCategories.Contains(enumFilterCategories.P)) categoryRepDeals.AddRange(syndicateRepDeals.Where(x => x.StageId == RepStages.P.ID).ToList() ?? new List<Deal>());
                if (filter.ListOfRepCategories.Contains(enumFilterCategories.T)) categoryRepDeals.AddRange(syndicateRepDeals.Where(x => x.StageId == RepStages.T.ID).ToList() ?? new List<Deal>());
                if (filter.ListOfRepCategories.Contains(enumFilterCategories.Z)) categoryRepDeals.AddRange(syndicateRepDeals.Where(x => x.StageId == RepStages.Z.ID).ToList() ?? new List<Deal>());
            }

            // Ok, the Deals have been filtered down, so now let's convert into a person list...
            var repPersons = persons.Where(x => categoryRepDeals.Any(y => ((y.PersonId?.Value ?? -1) == x.Id))).ToList() ?? new List<Person>();

            // If, Selling Reps of Record is checked, then add those in too...
            if (filter.ListOfRepCategories.Contains(enumFilterCategories.SellingRepsOfRecord))
            {
                var subs = new List<Deal>();

                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LOFReit))
                    subs.AddRange(deals.Where(x => x.PipelineId == PipelineList.LOFSubscriptions.ID).ToList() ?? new List<Deal>());

                if (filter.LegendaryProducts.Contains(enumLegendaryProducts.LFReitIII))
                    subs.AddRange(deals.Where(x => x.PipelineId == PipelineList.LF_REIT_III_Subscriptions.ID).ToList() ?? new List<Deal>());

                subPersons.AddRange(persons.Where(x => subs.Any(y => x.Id == (y.PersonId?.Value ?? -1))).ToList() ?? new List<Person>());
            }

            // Add in the subscription based persons...
            repPersons.AddRange(subPersons);

            // Last thing is to remove anyone who is in the Z category of rep activation, as they suck, unless we specifically asked for them...
            if ((!filter.ListOfRepCategories.Contains(enumFilterCategories.Z)) && (!filter.KeepZReps))
            {
                var zRepDeals = deals.Where(x => x.StageId == RepStages.Z.ID).ToList();
                var zRepPersons = persons.Where(x => zRepDeals.Any(y => ((y.PersonId?.Value ?? -1) == x.Id))).ToList() ?? new List<Person>();
                repPersons = repPersons.Except(zRepPersons).ToList();
            }

            return repPersons;

        }

    }
}
