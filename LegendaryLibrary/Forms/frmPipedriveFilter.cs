using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pipedrive;
using LegendaryLibrary.PipedriveAugmentation;

namespace LegendaryLibrary
{
    public partial class frmPipedriveFilter : Form
    {
        public frmPipedriveFilter()
        {
            InitializeComponent();
        }

        private void chkRegions_CheckedChanged(object sender, EventArgs e)
        {
            chkRegionWest.Enabled = chkRegions.Checked;
            chkRegionSouthCentral.Enabled = chkRegions.Checked;
            chkRegionNorthCentral.Enabled = chkRegions.Checked;
            chkSouthEast.Enabled = chkRegions.Checked;
            chkRegionNorthEast.Enabled = chkRegions.Checked;
        }



        private void ClearAllIndividualChecks()
        {
            foreach (var control in this.Controls)
            {
                if (control is CheckBox)
                {
                    var chkBox = (CheckBox)control;
                    if (chkBox.Name.Contains("_NA_") || chkBox.Name.Contains("_RVP_") || chkBox.Name.Contains("_AVP_") ||
                        chkBox.Name.Contains("_CORP_") || chkBox.Name.Contains("_LL_") || chkBox.Name.Contains("_MKT_") ||
                        chkBox.Name.Contains("_PL_") || chkBox.Name.Contains("_NE_") || chkBox.Name.Contains("_ACQ_"))
                        chkBox.Checked = false;
                }
            }
        }

        private void SetChecksWithCodeInName(string code, bool checkedValue, CheckBox exceptCheckBox = null)
        {
            foreach (var control in this.Controls)
            {
                if (control is CheckBox)
                {
                    var chkBox = (CheckBox)control;
                    if (chkBox.Name.Contains(code) && ((exceptCheckBox != null) && (!Object.ReferenceEquals(chkBox,exceptCheckBox))))
                        chkBox.Checked = checkedValue;
                }
            }        
       }

        private void frmPipedriveFilter_Load(object sender, EventArgs e)
        {
            try
            {
                chk_AUD_SellingFirmsReps.Checked = true;
            }
            catch
            {

            }
        }

        private List<Deal> PipedriveDeals = new List<Deal>();
        private List<Person> PipedrivePersons = new List<Person>();
        private List<Pipeline> PipedrivePipelines = new List<Pipeline>();
        private List<Organization> PipedriveOrganizations = new List<Organization>();

        private PipedriveTargetFilter GetFilterFromForm()
        {
            var legendaryProducts = new List<enumFilterLegendaryProducts>();
            enumFilterMarketingAudiences marketingAudience = enumFilterMarketingAudiences.NotSet;
            enumFilterYesNoEither firmSyndicate = enumFilterYesNoEither.NotSet;
            var sellingFirms = new List<enumFilterSellingFirms>();
            var sellingFirmsPersons = new List<enumFilterSellingFirmsPersons>();
            var sellingFirmsCategories = new List<enumFilterCategories>();
            enumFilterYesNoEither repSyndicate = enumFilterYesNoEither.NotSet;
            var repPersons = new List<enumFilterReps>();
            var repCategories = new List<enumFilterCategories>();
            var regions = new List<enumFilterRegions>();
            bool includeDoNotContact = false;
            bool keepZReps = false;

            if (chkIncludeDoNotContact.Checked)
                includeDoNotContact = true;

            if (chkKeepZReps.Checked)
                keepZReps = true;

            if (chkLOFREIT.Checked)
                legendaryProducts.Add(enumFilterLegendaryProducts.LOFReit);
            if (chkLFREITIII.Checked)
                legendaryProducts.Add(enumFilterLegendaryProducts.LFReitIII);

            if (chk_AUD_SellingFirmsReps.Checked)
                marketingAudience = enumFilterMarketingAudiences.SellingFirms;
            else if (chk_AUD_Subscriptions.Checked)
                marketingAudience = enumFilterMarketingAudiences.FundSubscriptions;
            else if (chk_AUD_ProfessionalServices.Checked)
                marketingAudience = enumFilterMarketingAudiences.ProServices;
            else if (chk_AUD_IndustryGroups.Checked)
                marketingAudience = enumFilterMarketingAudiences.IndustryGroups;
            else if (chk_AUD_TradePress.Checked)
                marketingAudience = enumFilterMarketingAudiences.TradePress;

            if (this.chkFirmSellingAgreement.Checked)
                firmSyndicate = enumFilterYesNoEither.Yes;
            else if (this.chkFirmNOSA.Checked)
                firmSyndicate = enumFilterYesNoEither.No;
            else if (this.chkFirmSAEither.Checked)
                firmSyndicate = enumFilterYesNoEither.Either;

            if (this.chkAnyFirm.Checked)
                sellingFirms.Add(enumFilterSellingFirms.AnyFirm);
            if (this.chkBrokerDealer.Checked)
                sellingFirms.Add(enumFilterSellingFirms.BrokerDealer);
            if (this.chkRIA.Checked)
                sellingFirms.Add(enumFilterSellingFirms.RIA);

            if (this.chkFirmHomeOffice.Checked)
                sellingFirmsPersons.Add(enumFilterSellingFirmsPersons.HomeOfficeOther);
            if (this.chkFirmDueDiligence.Checked)
                sellingFirmsPersons.Add(enumFilterSellingFirmsPersons.DueDiligence);

            if (this.chkRegisteredRep.Checked)
                repPersons.Add(enumFilterReps.RegisteredRep);
            if (this.chkIAR.Checked)
                repPersons.Add(enumFilterReps.IAR);

            if (this.chkRepSellingAgreement.Checked)
                repSyndicate = enumFilterYesNoEither.Yes;
            else if (this.chkRepNOSA.Checked)
                repSyndicate = enumFilterYesNoEither.No;
            else if (this.chkRepSAEither.Checked)
                repSyndicate = enumFilterYesNoEither.Either;

            if (this.chkFirmZ.Checked)  sellingFirmsCategories.Add(enumFilterCategories.Z);
            if (this.chkFirmNC.Checked) sellingFirmsCategories.Add(enumFilterCategories.NC);
            if (this.chkFirmT.Checked)  sellingFirmsCategories.Add(enumFilterCategories.T);
            if (this.chkFirmP.Checked)  sellingFirmsCategories.Add(enumFilterCategories.P);
            if (this.chkFirmH.Checked)  sellingFirmsCategories.Add(enumFilterCategories.H);
            if (this.chkFirmDD.Checked) sellingFirmsCategories.Add(enumFilterCategories.DD);
            if (this.chkFirmB.Checked)  sellingFirmsCategories.Add(enumFilterCategories.B);
            if (this.chkFirmA.Checked)  sellingFirmsCategories.Add(enumFilterCategories.A);

            if (this.chkRepZ.Checked)   repCategories.Add(enumFilterCategories.Z);
            if (this.chkRepNC.Checked)  repCategories.Add(enumFilterCategories.NC);
            if (this.chkRepT.Checked)   repCategories.Add(enumFilterCategories.T);
            if (this.chkRepI.Checked)   repCategories.Add(enumFilterCategories.I);
            if (this.chkRepP.Checked)   repCategories.Add(enumFilterCategories.P);
            if (this.chkRepH.Checked)   repCategories.Add(enumFilterCategories.H);
            if (this.chkRepB.Checked)   repCategories.Add(enumFilterCategories.B);
            if (this.chkRepA.Checked)   repCategories.Add(enumFilterCategories.A);

            if (this.chkRepSellingRepsOfRecord.Checked) repCategories.Add(enumFilterCategories.SellingRepsOfRecord);

            if (this.chkRegionWest.Checked) regions.Add(enumFilterRegions.West);
            if (this.chkRegionNorthCentral.Checked) regions.Add(enumFilterRegions.NorthCentral);
            if (this.chkRegionSouthCentral.Checked) regions.Add(enumFilterRegions.SouthCentral);
            if (this.chkRegionNorthEast.Checked) regions.Add(enumFilterRegions.NorthEast);
            if (this.chkRegionSouthEast.Checked) regions.Add(enumFilterRegions.SouthEast);

            var pipedriveFilter = new PipedriveTargetFilter(legendaryProducts, marketingAudience, firmSyndicate, 
                                                            sellingFirms, sellingFirmsPersons, sellingFirmsCategories,
                                                            repSyndicate, repPersons, repCategories, 
                                                            regions, includeDoNotContact, keepZReps);

            return pipedriveFilter;
        }

        private async Task ExecuteCreationOfMailingList()
        {
            try
            {
                var headerValue = new ProductHeaderValue("PipedriveSampleClient");
                PipedriveClient pipedriveClient = new PipedriveClient(headerValue, Legendary.PipedriveApiUrl, Legendary.PipedriveAPIKey);

                if (! Legendary.StatusStopNow)
                    PipedriveDeals = await PipedriveExtensions.GetAllDeals(pipedriveClient);

                if (!Legendary.StatusStopNow)
                    PipedrivePersons = await PipedriveExtensions.GetAllPersons(pipedriveClient);

                if (!Legendary.StatusStopNow)
                    PipedrivePipelines = await PipedriveExtensions.GetAllPipelines(pipedriveClient);

                if (!Legendary.StatusStopNow)
                    PipedriveOrganizations = await PipedriveExtensions.GetAllOrganizations(pipedriveClient);

                //var myDeal = PipedriveDeals.Where(x => x.PersonName == "Ernesto Chavez").ToList();
                //var myPerson = PipedrivePersons.Where(x => x.Name == "John B. Darmanian").ToList();
                //var myPerson2 = PipedrivePersons.Where(x => x.Name == "John Arnold Pallo").ToList();
                //var myPerson3 = PipedrivePersons.Where(x => x.Name == "Donald Blauner").ToList();
                //var myPerson4 = PipedrivePersons.Where(x => x.Name == "Ernesto Chavez").ToList();

                FilterBasedOnFormChoices();

                if (Legendary.StatusStopNow)
                    Legendary.UpdateStatus("Cancelled by User");
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            finally
            {
                this.btnRetrieveAndCreate.Text = "RETRIEVE && CREATE";
                this.btnRetrieveAndCreate.Refresh();
                Legendary.StatusStopNow = false;
            }
        }

        private void FilterBasedOnFormChoices()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Legendary.UpdateStatus("Filter Results...");
                PipedriveTargetFilter pipedriveFilter = null;
                List<PipedriveTarget> mailingList = null;
                if (!Legendary.StatusStopNow)
                    pipedriveFilter = GetFilterFromForm();
                if (!Legendary.StatusStopNow)
                    mailingList = PipedriveTargetFilter.Filter(PipedriveDeals, PipedrivePersons, PipedriveOrganizations, pipedriveFilter);

                mailingList.Sort(new PipedriveTargetComparer());

                var form = new frmPipedriveResults();
                form.ShowPipedriveData(mailingList);
                form.SetFilterDescription(pipedriveFilter.Description);
                form.Text = $@"PD:{mailingList.Count}";
                form.MdiParent = this.MdiParent;
                form.Show();
                Legendary.UpdateStatus("Filter Results... Finished!");
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnRetrieveAndCreate.Text.Contains("CREATE"))
                {
                    this.btnRetrieveAndCreate.Text = "STOP";
                    Legendary.StatusStopNow = false;
                    ExecuteCreationOfMailingList();
                }
                else
                {
                    Legendary.StatusStopNow = true;
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
                this.btnRetrieveAndCreate.Text = "RETRIEVE && CREATE";
                this.btnRetrieveAndCreate.Refresh();
            }
            finally
            {
            }
        }


        private void chkChooseIndividuals_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void btnCreateOnly_Click(object sender, EventArgs e)
        {
            try
            {
                FilterBasedOnFormChoices();
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkLegendaryLeadership_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SetChecksWithCodeInName("_LL_", chkLegendaryLeadership.Checked);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkProductLeadership_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SetChecksWithCodeInName("_PL_", chkProductLeadership.Checked);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkNationalAccountTeam_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SetChecksWithCodeInName("_NA_", chkNationalAccountTeam.Checked);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkNationalEventsTeam_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SetChecksWithCodeInName("_NE_", chkNationalEventsTeam.Checked);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkRegionalWholesaleTeam_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SetChecksWithCodeInName("_RVP_", chkRegionalWholesaleTeam.Checked);
                SetChecksWithCodeInName("_AVP_", chkRegionalWholesaleTeam.Checked);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chk_AUD_SellingFirmsReps_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlFirmSyndicateOffice.Enabled = chk_AUD_SellingFirmsReps.Checked;
                if (chk_AUD_SellingFirmsReps.Checked)
                    SetChecksWithCodeInName("_AUD_", false, chk_AUD_SellingFirmsReps);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chk_AUD_Subscriptions_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_AUD_Subscriptions.Checked)
                    SetChecksWithCodeInName("_AUD_", false, chk_AUD_Subscriptions);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chk_AUD_ProfessionalServices_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_AUD_ProfessionalServices.Checked)
                    SetChecksWithCodeInName("_AUD_", false, chk_AUD_ProfessionalServices);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chk_AUD_IndustryGroups_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_AUD_IndustryGroups.Checked)
                    SetChecksWithCodeInName("_AUD_", false, chk_AUD_IndustryGroups);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chk_AUD_TradePress_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_AUD_TradePress.Checked)
                    SetChecksWithCodeInName("_AUD_", false, chk_AUD_TradePress);
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkBrokerDealer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBrokerDealer.Checked || chkRIA.Checked || chkAnyFirm.Checked)
                {
                    chkFirmHomeOffice.Enabled = true;
                    chkFirmDueDiligence.Enabled = true;
                }
                else
                {
                    chkFirmHomeOffice.Enabled = false;
                    chkFirmDueDiligence.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkRIA_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBrokerDealer.Checked || chkRIA.Checked || chkAnyFirm.Checked)
                {
                    chkFirmHomeOffice.Enabled = true;
                    chkFirmDueDiligence.Enabled = true;
                }
                else
                {
                    chkFirmHomeOffice.Enabled = false;
                    chkFirmDueDiligence.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkSellingAgreement_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkFirmSellingAgreement.Checked)
                {
                    chkFirmNOSA.Checked = false;
                    chkFirmSAEither.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkNOSA_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkFirmNOSA.Checked)
                {
                    chkFirmSellingAgreement.Checked = false;
                    chkFirmSAEither.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkSAEither_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkFirmSAEither.Checked)
                {
                    chkFirmSellingAgreement.Checked = false;
                    chkFirmNOSA.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkAnyFirm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAnyFirm.Checked)
                {
                    chkBrokerDealer.Checked = false;
                    chkBrokerDealer.Enabled = false;
                    chkRIA.Checked = false;
                    chkRIA.Enabled = false;
                }
                else
                {
                    chkBrokerDealer.Checked = false;
                    chkBrokerDealer.Enabled = true;
                    chkRIA.Checked = false;
                    chkRIA.Enabled = true;
                }

                if (chkBrokerDealer.Checked || chkRIA.Checked || chkAnyFirm.Checked)
                {
                    chkFirmHomeOffice.Enabled = true;
                    chkFirmDueDiligence.Enabled = true;
                }
                else
                {
                    chkFirmHomeOffice.Enabled = false;
                    chkFirmDueDiligence.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }

        private void chkRepAnyRep_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRepAnyRep.Checked)
                {
                    chkRegisteredRep.Enabled = false;
                    chkRegisteredRep.Checked = false;
                    chkIAR.Enabled = false;
                    chkIAR.Checked = false;
                }
                else
                {
                    chkRegisteredRep.Enabled = true;
                    chkRegisteredRep.Checked = false;
                    chkIAR.Enabled = true;
                    chkIAR.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Legendary.UpdateStatus(ex);
            }
        }
    }
}
