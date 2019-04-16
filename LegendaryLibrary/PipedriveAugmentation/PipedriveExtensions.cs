using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pipedrive;

namespace LegendaryLibrary.PipedriveAugmentation
{
    public static class OrganizationCustomFields
    {
        static public string HasLOFReitSA = "40db3494ad724bcbcf145fdf856aab0ddd499479";
               static public class HasLOFReitSAs { static public string Yes = "166";
                                                   static public string No = "167"; }
        static public string HasLFReit3SA = "7aaf56d89ce219f0de05bd6823ee6c5a50a0cb6f";
               static public class HasLFReit3SAs { static public string Yes = "164";
                                                   static public string No = "165"; }
        static public string PhoneNumber = "87f2bbe6fcb836ffe9803b0f17b954032cf79b78";
        static public string Email = "3d30a7788863c0ca2945c780358018d07dc7dd80";
        static public string NumberOfDiscretionaryAccounts = "ad1936ed5dcb5dfa100be75d957f47f799df6c6d";
        static public string TotalAUM = "38952694c9c11af31b69443b3367bbcc3476583a";
        static public string DiscretionaryAUM = "bb2a084ebdc42dc31d52cfd99b6dcc15308c4083";
        static public string NumberOfHighWorthClients = "0d3eab47c663d4929233f83ed4ac0bf48aaf698b";
        static public string FaxNumber = "67b40f38354ce9d6d5c05f5132a494b9a8292b81";
        static public string MainNumber = "3af767c6ef721edba395e3498746ca7805c85fc9";
        static public string NumberOfReps = "c1fb491803a3b8d84e631d74e299143ecd1b2d14";
        static public string WebSite = "7aa93720e2791e1fee19530c0db1212c9eee014e";
        static public string OrgType = "799204412cd75fd9c5793799f701a5a5ec44c956";
                public enum OrgTypeEnum { BrokerDealer = 75,
                                          RIA = 76,
                                          ServiceProvider = 77,
                                          BankLegalAccounting = 81,
                                          OtherUnknown = 125,
                                          Remove = 124,
                                          Combine = 126 };
                static public (int, string)[] OrgTypes = { (75, "Broker Dealer"),
                                                           (76, "RIA"),
                                                           (77, "Service Provider"),
                                                           (81, "Bank/Legal/Accounting"),
                                                           (125, "Other/Unknown"),
                                                           (124, "Remove"),
                                                           (126, "Combine") };
        static public string CRD = "764cf82ef7cfae0175d4d9a00c390a0b301af46b";
        static public string Note = "8a6a8804714452df3235b506d8ef39948f0650ea";
        static public string HasSA = "0b735115b05b98db60b43dbd05d9401b447bf5b7";
        static public string Codings = "0158935ec466ddd6651a59b4172a799a4f34a4f1";
    }

    public static class DealCustomFields
    {
        static public string DealType = "5ce64b746686534d711d78e53a4366c08281b5ee";
               static public class DealTypes { static public string BrokerDealer = "9";
                                               static public string Rep = "10";
                                               static public string RIA = "11";
                                               static public string DirectInvestment = "12";
                                               static public string InfluencerRep = "50"; }
        static public string RepEvent = "b098123bbc40e0bcbea1f4295cd07fc67f97209b";
               static public class RepEvents { static public string PhoneOnly = "13";
                                               static public string MeetingTheirOffice = "14";
                                               static public string WebDemo = "20";
                                               static public string DDMeeting = "66";
                                               static public string ClientSeminar = "74"; }
        static public string BDInfluencer = "668c5e3fd3b345690fcd5fe53472dd2034cf80e6";
        static public string EventTracking = "7a4af5afbadcd93325cc5de4c41a5614df2a6f1f";
        static public string BDIndustryEvents = "efc2b9001df139a9d01fd11bafa7bb277178d2fd";
        static public string PersonCRD = "e785a81418ca126873a3cc166bdca67cc6cdb0f1";
        static public string Notes = "82f190a30445d63026ad4c97f30cb2b41b22c2ea";
        static public string HasLfReit3SA = "564c70b0777fae7c758b24119f1a550a59a04258";
               static public class HasLFReit3SAs { static public string Yes = "168";
                                                   static public string No = "169"; }
        static public string HasLofReitSA = "a761f54368f1686b5bcc1be5d09f375cc916c232";
               static public class HasLofReitSAs { static public string Yes = "170";
                                                   static public string No = "171"; }
        static public string EmailOptOut = "363abc4a74fb3ac9287f2c3c93d82dbdd99b14d4";
               static public class EmailOptOuts { static public string Yes = "172";
                                                  static public string No = "173"; }
    }

    public static class PersonCustomFields
    {
        static public string Address = "d7b2107b0b1f6d0acde1b6266bbaa28584b19012";
        static public string Assistant = "ae231497158017d0cb8eb882c8fe60ce600623e8";
        static public string AssetsUnderManagement = "d05b4e47cc657a461458a0a2d84ec0226a3d26b4";
        static public string LicenseType = "726f1b485a4603a9f1d80a31dfd5f5222a27bf83";
        static public string SellsRegD = "fe1e10c89499b301decce25f65dccb1df165413f";
                static public (int, string)[] SellsRegDs = { (3, "Yes"),
                                                             (4, "No") };
        static public string SellsREIT = "60070156b7f1d60cfe4ca4c53d8ce7961abc4faa";
                static public (int, string)[] SellsREITs = { (5, "Yes"),
                                                             (6, "No") };
        static public string Sells1031 = "a5d5ea9df28619528d6076c82a32d16e249a1c34";
                static public (int, string)[] Sells1031s = { (7, "Yes"),
                                                             (8, "No") };
        static public string ContactType = "e0ea461f68fe5d1d85a1e6d3567578a65230dd1b";
                public enum ContactTypeEnum { BD = 45,
                                              Rep = 46,
                                              RIA = 47,
                                              DI = 48,
                                              Other = 49,
                                              BankLegalAccounting = 82 };
                static public (int, string)[] ContactTypes = { (45, "BD"),
                                                               (46, "Rep"),
                                                               (47, "RIA"),
                                                               (48, "DI"),
                                                               (49, "Other"),
                                                               (82, "BankLegalAccounting") };
        static public string Title = "f6a28d41b8541d6a63ee90e42986379c47c3a86d";
        static public string MonthlyUpdate = "95c26cd0edb6765afcf80ded7b1d34b3788b3ab4";
                static public (int, string)[] MonthlyUpdates = { (55, "BD"),
                                                                 (56, "Rep"),
                                                                 (57, "Industry"),
                                                                 (58, "Other"),
                                                                 (59, "No") };
        static public string FirmName = "d1f24d23ce6a5cfc2ca502d4818901b98d708827";
        static public string FirmWebsite = "319471b0c04994875a645d23cc69c2fd2fdb169c";
        static public string FirmLOFReitHasSA = "03286ad139a3481bf6f18705c3825f87abae10d5";
                static public (int, string)[] FirmLOFReitHasSAs = { (222, "Yes"),
                                                                    (223, "No") };
        static public string FirmLOFReitCoding = "3a13198040721079c887481710048ec33ada24c9";
        static public string FirmLFReitIIIHasSA = "a6c72a4992bc2cbb9902f57c32f0d3f2da4a6593";
             static public (int, string)[] FirmLFReitIIIHasSAs = { (224, "Yes"),
                                                                   (225, "No") };
        static public string FirmLFReitIIICoding = "4c90d6dce5de19d2668760ebedd480d2658a471d";
        static public string SendAnnualReport = "9cc6aa6a6863dfe20daa80a89f401a422dfab93c";
                static public (int, string)[] SendAnnualReports = { (60, "Yes"),
                                                                    (61, "No") };
        static public string HasSellingAgreement = "573a0489896b9ec47a2bb7924b9160c80d3b7172";
                static public (int, string)[] HasSellingAgreements = { (78, "Yes"),
                                                                       (79, "No") };
        static public string Events_1 = "54b4c40331217600b7ac42b05a533f1afef4777c";
        static public string Notes = "10c30f276f9fb77acad6f81b536f301f2f40979c";
        static public string CRD = "cd639732af33766a236089c9ad1abf60bcb8f58f";
        static public string BCCEmail = "14bd44a1fcc3f3035f288c3312065c52a4efb23e";
        static public string FaxNumber = "6f0df5a20f2126878966784168f13ba6a25695d5";
        static public string HomeOfficeDistribution = "5ceac4c95d4135a70447fd381d104dc416b4f178";
                public enum HomeOfficeDistributionEnum { NotSet = 0,
                                                         DueDiligence = 162,
                                                         HomeOfficeOtherContact = 163 };
                static public (int, string)[] HomeOfficeDistributions = { (162, "Due Diligence"),
                                                                          (163, "Home Office Other Contact") };
        static public string DoNotContact = "3fc3f48303dc1114f265b7d3233d64c3afff62ee";
                public enum DoNotContactEnum { NotSet = 0, ContactOkay = 184, DoNotContact = 185 };
                static public (int, string)[] DoNotContacts = { (184, "Contact Okay"),
                                                                (185, "Do Not Contact") };
        static public string RVP = "db6a4d6867119fa9593d9d7d6bddd21ccd495303";
                static public (int, string)[] RVPs = { (207, "Mindey Morrison"),
                                                       (208, "Ken Beck"),
                                                       (209, "Todd King"),
                                                       (210, "Craig Gunter"),
                                                       (211, "Chad Whatley") };
        static public string AVP = "d3b7d2ebdebddf9bb2b38d0c1283b52ff384bc01";
                static public (int, string)[] AVPs = { (203, "Erica Blue"),
                                                       (204, "Angela Clakley"),
                                                       (205, "Terry Jones"),
                                                       (206, "Ryan Sullivan") };
        static public string DistributionRegion = "127f142157f4ca35ee6f5d5058d9278935fa3a18";
                static public (int, string)[] DistributionRegions = { (212, "West"),
                                                                      (213, "North Central"),
                                                                      (214, "South Central"),
                                                                      (215, "North East"),
                                                                      (216, "South East") };
        static public string OptedOutOfEmail = "a30c3de6436f5ed5a309b08d6910349726a5d8cb";
                static public (int, string)[] OptedOutOfEmails = { (201, "Yes"),
                                                                   (202, "No") };
        static public string RepLOFReitHasSold = "878b6f7aefb860d0f28d0466f9c99b68ab8f96ee";
                static public (int, string)[] RepLOFReitHasSolds = { (228, "Yes"),
                                                                     (229, "No") };
        static public string RepLOFReitCoding = "1cfc420504060aaec66de1b988c7ad8f4edecf0b";
        static public string RepLFReitIIIHasSold = "2ebecaa94de593a036a6d92ca6ea7f1ac44f6fdb";
                static public (int, string)[] RepLFReitIIIHasSolds = { (230, "Yes"),
                                                                       (231, "No") };
        static public string RepLFReitIIICoding = "f1bdc8532927321a92efffc7a5df20f4f36ed2c5";
    }

    static public class PipelineList
    {
        static public (int ID, string Name) LOF_REIT_SAs = (1, "LOF-REIT SAs");
                static public class LOF_REIT_SAsStages { static public (int ID, string Name) Potential     = (1, "Potential");
                                                         static public (int ID, string Name) ContactMade   = (2, "Contact Made");
                                                         static public (int ID, string Name) Discovery     = (3, "Discovery");
                                                         static public (int ID, string Name) DueDiligence  = (4, "Due Dil");
                                                         static public (int ID, string Name) ActiveSelling = (5, "Active Selling"); }
        static public (int ID, string Name) LF_REIT_III_SAs = (18, "REIT III SAs");
                static public class LF_REIT_III_SAsStages { static public (int ID, string Name) Z  = (100, "Z - Zero Prospect");
                                                            static public (int ID, string Name) NC = (99,  "NC - Not Coded");
                                                            static public (int ID, string Name) T  = (98,  "T - Target");
                                                            static public (int ID, string Name) P  = (97,  "P - Potential B");
                                                            static public (int ID, string Name) H  = (96,  "H - Hot A");
                                                            static public (int ID, string Name) DD = (95,  "DD - In Diligence");
                                                            static public (int ID, string Name) B  = (94,  "B - SA");
                                                            static public (int ID, string Name) A  = (93,  "A - SA"); }
        static public (int ID, string Name) LinconlTicII = (4, "Lincoln TIC II");
                static public class LinconlTicIIStages { static public (int ID, string Name) Potential = (14, "Potential");
                                                         static public (int ID, string Name) DueDiligence = (16, "Due Dil");
                                                         static public (int ID, string Name) Active = (15, "Active"); }
        static public (int ID, string Name) RepActivation = (14, "Rep Activation");
                static public class RepActivationStages { static public (int ID, string Name) Z  = (92,  "Z (Zero/Low Prospect)");
                                                          static public (int ID, string Name) NC = (75,  "NC (Not Coded)");
                                                          static public (int ID, string Name) T  = (76,  "T (Target)");
                                                          static public (int ID, string Name) I  = (101, "I (Influencer)");
                                                          static public (int ID, string Name) P  = (77,  "P (Potential B)");
                                                          static public (int ID, string Name) H  = (79,  "H (Hot Potential A)");
                                                          static public (int ID, string Name) B  = (78,  "B Producer");
                                                          static public (int ID, string Name) A  = (80,  "A Producer"); }
        static public (int ID, string Name) DirectInvestment = (15, "Direct Investment");
                static public class DirectInvestmentStages { static public (int ID, string Name) Potential  = (81, "Potential");
                                                             static public (int ID, string Name) Considering = (82, "Considering");
                                                             static public (int ID, string Name) Sold  = (83, "Sold"); }
        static public (int ID, string Name) RIAProspecting = (16, "RIA Prospecting");
                static public class RIAProspectingStages { static public (int ID, string Name) Z =  (92, "Z (Zero/Low Prospect)");
                                                           static public (int ID, string Name) NC = (84, "NC (Not Coded)");
                                                           static public (int ID, string Name) T =  (85, "T (Target)");
                                                           static public (int ID, string Name) P =  (86, "P (Potential B)");
                                                           static public (int ID, string Name) H =  (87, "H (Hot Potential A)");
                                                           static public (int ID, string Name) B =  (88, "B Producer");
                                                           static public (int ID, string Name) A =  (89, "A Producer"); }
        static public (int ID, string Name) LOFSubscriptions = (17, "LOF Subscriptions");
                static public class LOFSubscriptionsStages { static public (int ID, string Name) Won    = (90, "Won");
                                                             static public (int ID, string Name) Closed = (91, "Closed"); }
        static public (int ID, string Name) LF_REIT_III_Subscriptions = (19, "LF REIT III Subscriptions");
                static public class LF_REIT_III_SubscriptionsStages { static public (int ID, string Name) Won = (102, "Won"); }

        static public bool IsValid(long id)
        {
            if ((LOF_REIT_SAs.ID == id) ||
                (LF_REIT_III_SAs.ID == id) ||
                (LinconlTicII.ID == id) ||
                (RepActivation.ID == id) ||
                (DirectInvestment.ID == id) ||
                (RIAProspecting.ID == id) ||
                (LOFSubscriptions.ID == id) ||
                (LF_REIT_III_Subscriptions.ID == id))
                return true;
            return false;
        }
        static public bool IsValid(List<long> ids)
        {
            bool isValid = true;
            foreach (var value in ids)
                isValid &= IsValid(value);
            return isValid;
        }
    }



    public static class PipedriveExtensions
    {

        static public List<Deal> OpenDeals(this IEnumerable<Deal> source)
        {
            return source.Where(x => x.Status == DealStatus.open).ToList<Deal>();
        }

        static public async Task<List<Deal>> GetAllDeals(PipedriveClient pipedriveClient)
        {
            try
            {
                Legendary.UpdateStatus("Retrieving DEALS from PIPEDRIVE ...");
                Legendary.UpdateProgress(0);

                IReadOnlyList<Deal> myDeals = null;
                List<Deal> allDeals = new List<Deal>();

                double percentComplete = 0;
                double percentIncrement = (double)((double)(500.0 / 14000.0) * 100.0);
                bool done = false;
                int count = 0;

                Legendary.UpdateProgress(percentComplete);

                var dealFilters = new DealFilters() { StartPage = 0, PageCount = 1, PageSize = 500 };
                while ((!done) && (count < 100))
                {
                    count += 1;
                    myDeals = await pipedriveClient.Deal.GetAll(dealFilters);
                    allDeals.AddRange(myDeals);
                    if ((myDeals.Count < 500) || (Legendary.StatusStopNow))
                        done = true;
                    percentComplete += percentIncrement;
                    Legendary.UpdateProgress(percentComplete);
                    dealFilters = new DealFilters() { StartPage = (dealFilters.StartPage + 500), PageCount = 1, PageSize = 500 };
                }

                Legendary.UpdateStatus("Retrieving DEALS from PIPEDRIVE ... Completed");
                Legendary.UpdateProgress(100);

                return allDeals;

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
                return new List<Deal>();
            }
        }

        static public async Task<List<Person>> GetAllPersons(PipedriveClient pipedriveClient)
        {
            try
            {
                Legendary.UpdateStatus("Retrieving CONTACTS from PIPEDRIVE ...");
                Legendary.UpdateProgress(0);

                IReadOnlyList<Person> persons = null;
                List<Person> allPersons = new List<Person>();

                var personFilters = new PersonFilters() { StartPage = 0, PageCount = 1, PageSize = 500 };

                double percentComplete = 0;
                double percentIncrement = (double)((double)(500.0 / 11000.0) * 100.0);
                bool done = false;
                int count = 0;

                while ((!done) && (count < 100))
                {
                    count += 1;
                    persons = await pipedriveClient.Person.GetAll(personFilters);
                    allPersons.AddRange(persons);
                    if ((persons.Count < 500) || (Legendary.StatusStopNow))
                        done = true;
                    percentComplete += percentIncrement;
                    Legendary.UpdateProgress(percentComplete);
                    personFilters = new PersonFilters() { StartPage = (personFilters.StartPage + 500), PageCount = 1, PageSize = 500 };
                }

                Legendary.UpdateStatus("Retrieving CONTACTS from PIPEDRIVE ... Completed");
                Legendary.UpdateProgress(100);

                return allPersons;

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
                return new List<Person>();
            }
        }

        static public async Task<List<Organization>> GetAllOrganizations(PipedriveClient pipedriveClient)
        {
            try
            {
                Legendary.UpdateStatus("Retrieving ORGANIZATIONS from PIPEDRIVE ...");
                Legendary.UpdateProgress(0);

                IReadOnlyList<Organization> organizations = null;
                List<Organization> allOrganizations = new List<Organization>();

                var organizationFilters = new OrganizationFilters() { StartPage = 0, PageCount = 1, PageSize = 500 };

                double percentComplete = 0;
                double percentIncrement = (double)((double)(500.0 / 3500.0) * 100.0);
                bool done = false;
                int count = 0;

                while ((!done) && (count < 100))
                {
                    count += 1;
                    organizations = await pipedriveClient.Organization.GetAll(organizationFilters);
                    allOrganizations.AddRange(organizations);
                    if ((organizations.Count < 500) || (Legendary.StatusStopNow))
                        done = true;
                    percentComplete += percentIncrement;
                    Legendary.UpdateProgress(percentComplete);
                    organizationFilters = new OrganizationFilters() { StartPage = (organizationFilters.StartPage + 500), PageCount = 1, PageSize = 500 };
                }

                Legendary.UpdateStatus("Retrieving ORGANIZATIONS from PIPEDRIVE ... Completed");
                Legendary.UpdateProgress(100);

                return allOrganizations;

            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
                return new List<Organization>();
            }
        }

        static public async Task<List<Pipeline>> GetAllPipelines(PipedriveClient pipedriveClient)
        {
            try
            {
                Legendary.UpdateStatus("Retrieving PIPELINES from PIPEDRIVE ...");
                Legendary.UpdateProgress(0);

                IReadOnlyList<Pipeline> pipelines = null;
                List<Pipeline> allPipelines = new List<Pipeline>();

                double percentComplete = 0;
                double percentIncrement = (double)(100.0);

                pipelines = await pipedriveClient.Pipeline.GetAll();
                allPipelines.AddRange(pipelines);
                percentComplete += percentIncrement;
                Legendary.UpdateProgress(percentComplete);

                Legendary.UpdateStatus("Retrieving PIPELINES from PIPEDRIVE ... Completed");
                Legendary.UpdateProgress(100);

                return allPipelines;
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
                return new List<Pipeline>();
            }
        }

        static public async Task EditPerson(PipedriveClient pipedriveClient, PipedriveTarget pipedriveTarget)
        {
            //var fixture = pipedriveClient.Person;

            //var newPerson = new NewPerson("new-name");
            //var person = await fixture.Create(newPerson);

            //var editPerson = person.ToUpdate();
            //editPerson.Name = "updated-name";
            //editPerson.Email = new List<Email>
            //{
            //    { new Email { Value = "test@example.com", Primary = true } }
            //};

            //var updatedPerson = await fixture.Edit(person.Id, editPerson);

            //Assert.Equal("updated-name", updatedPerson.Name);
            //Assert.Equal("test@example.com", updatedPerson.Email[0].Value);
            //Assert.True(updatedPerson.Email[0].Primary);

            //// Cleanup
            //await fixture.Delete(updatedPerson.Id);
        }
    }
}
