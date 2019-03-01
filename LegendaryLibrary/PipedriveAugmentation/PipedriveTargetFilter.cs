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

    public enum enumFilterLegendaryProducts
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
        public PipedriveTargetFilter(List<enumFilterLegendaryProducts> legendaryProducts = null,
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

        public List<enumFilterLegendaryProducts> LegendaryProducts { get; } = null;
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
                foreach (var product in LegendaryProducts ?? new List<enumFilterLegendaryProducts>())
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
                                                                      PersonCustomFields.DoNotContacts.DoNotContact, true) == 0))
                                           .ToList() ?? new List<Person>();

                // Put into pipedrive target list...
                foreach (var person in allPersons)
                {
                    var address = person.CustomFields?[PersonCustomFields.Address] as Pipedrive.CustomFields.AddressCustomField;
                    var email = person.Email[0]?.Value;

                    string category = "Not Set";
                    if (string.Compare((person.CustomFields[PersonCustomFields.HomeOfficeDistribution] as StringCustomField)?.Value,
                                        PersonCustomFields.HomeOfficeDistributions.HomeOfficeOther, true) == 0)
                        category = "Other Home Office Contact";
                    else if (string.Compare((person.CustomFields[PersonCustomFields.HomeOfficeDistribution] as StringCustomField)?.Value,
                                        PersonCustomFields.HomeOfficeDistributions.DueDiligence, true) == 0)
                        category = "Due Diligence";

                    long crd = (person.CustomFields[PersonCustomFields.CRD] as LongCustomField)?.Value ?? 0;

                    string contactability = (person.CustomFields[PersonCustomFields.DoNotContact] as StringCustomField)?.Value ?? "";
                    if (string.Compare(contactability, PersonCustomFields.DoNotContacts.ContactOkay, true) == 0)
                        contactability = "Contact Okay";
                    else if (string.Compare(contactability, PersonCustomFields.DoNotContacts.DoNotContact, true) == 0)
                        contactability = "Do Not Contact";
                    else
                        contactability = "Not Set";

                    if (!((email != null) && email.Contains("lodgingfund.com")))
                    {
                        pipedriveTarget = new PipedriveTarget(person.Id, person.FirstName, person.LastName, crd, person.Email[0]?.Value, address?.FormattedAddress,
                                                              category, (person.OrgName ?? ""), (person.OrgId?.Value ?? 0), contactability);
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

        static private List<Person> FilterAudienceFundSubscriptions(List<Deal> deals, List<Person> persons, List<Organization> organizations, PipedriveTargetFilter filter)
        {
            List<Deal> subscriptionDeals = new List<Deal>();
            List<Organization> syndicateOrganizations = new List<Organization>();
            if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LOFReit))
                            subscriptionDeals.AddRange(deals.Where(x => x.PipelineId == PipelineList.LOFSubscriptions.ID).ToList());
                        if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LFReitIII))
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
                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LOFReit))
                    syndicateFirms.AddRange(organizations
                        .Where(x => string.Compare((x.CustomFields[OrgFields.HasLOFReitSA] as StringCustomField)?.Value, OrgFields.HasLOFReitSAs.Yes, true) == 0)
                        .ToList() ?? new List<Organization>());

                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LFReitIII))
                    syndicateFirms.AddRange(organizations
                            .Where(x => string.Compare((x.CustomFields[OrgFields.HasLFReit3SA] as StringCustomField)?.Value, OrgFields.HasLFReit3SAs.Yes, true) == 0)
                            .ToList() ?? new List<Organization>());
            }

            // For NO, we assume NULL is also a matching value...
            if ((filter.FirmSyndicate == enumFilterYesNoEither.No) || (filter.FirmSyndicate == enumFilterYesNoEither.Either))
            {
                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LOFReit))
                    syndicateFirms.AddRange(organizations
                        .Where(x => ((x.CustomFields[OrgFields.HasLOFReitSA] as StringCustomField) == null) ||
                                    (string.Compare((x.CustomFields[OrgFields.HasLOFReitSA] as StringCustomField)?.Value, OrgFields.HasLOFReitSAs.No, true) == 0))
                        .ToList() ?? new List<Organization>());

                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LFReitIII))
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
                        .Where(x => string.Compare((x.CustomFields[OrgFields.OrgType] as StringCustomField)?.Value, OrgFields.OrgTypes.BrokerDealer, true) == 0)
                        .ToList() ?? new List<Organization>());
                if (filter.ListOfSellingFirms.Contains(enumFilterSellingFirms.RIA))
                    typeOfFirms.AddRange(syndicateFirms
                        .Where(x => string.Compare((x.CustomFields[OrgFields.OrgType] as StringCustomField)?.Value, OrgFields.OrgTypes.RIA, true) == 0)
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
                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LOFReit))
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

                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LFReitIII))
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
                                                                                PersonCustomFields.HomeOfficeDistributions.HomeOfficeOther, true) == 0)
                                                    .ToList() ?? new List<Person>();
                var ddPersons = persons.Where(x => string.Compare((x.CustomFields[PersonCustomFields.HomeOfficeDistribution] as StringCustomField)?.Value,
                                                                                PersonCustomFields.HomeOfficeDistributions.DueDiligence, true) == 0)
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
                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LOFReit))
                {
                    syndicateRepDeals.AddRange(repDeals
                        .Where(x => string.Compare((x.CustomFields[DealCustomFields.HasLofReitSA] as Pipedrive.CustomFields.StringCustomField)?.Value,
                                                    DealCustomFields.HasLofReitSAs.Yes, true) == 0)
                        .ToList() ?? new List<Deal>());

                    // But wait, the LOF REIT A and B persons found from Rep Activation where the underlying person has a subscription in LOF REIT, so let's find those persons...
                    var subs = deals.Where(x => x.PipelineId == PipelineList.LOFSubscriptions.ID).ToList();
                    subPersons.AddRange(persons.Where(x => subs.Any(y => x.Id == (y.PersonId?.Value ?? -1))).ToList() ?? new List<Person>());
                }

                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LFReitIII))
                    syndicateRepDeals.AddRange(repDeals
                        .Where(x => string.Compare((x.CustomFields[DealCustomFields.HasLfReit3SA] as Pipedrive.CustomFields.StringCustomField)?.Value,
                                                    DealCustomFields.HasLFReit3SAs.Yes, true) == 0)
                        .ToList() ?? new List<Deal>());
            }

            // For NO, we assume NULL is also a matching value...
            if ((filter.RepSyndicate == enumFilterYesNoEither.No) || (filter.RepSyndicate == enumFilterYesNoEither.Either))
            {
                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LOFReit))
                    syndicateRepDeals.AddRange(repDeals
                        .Where(x => ((x.CustomFields[DealCustomFields.HasLofReitSA] as Pipedrive.CustomFields.StringCustomField) == null) || 
                                    (string.Compare((x.CustomFields[DealCustomFields.HasLofReitSA] as Pipedrive.CustomFields.StringCustomField)?.Value, DealCustomFields.HasLofReitSAs.No, true) == 0))
                        .ToList() ?? new List<Deal>());

                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LFReitIII))
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

                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LOFReit))
                    subs.AddRange(deals.Where(x => x.PipelineId == PipelineList.LOFSubscriptions.ID).ToList() ?? new List<Deal>());

                if (filter.LegendaryProducts.Contains(enumFilterLegendaryProducts.LFReitIII))
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
