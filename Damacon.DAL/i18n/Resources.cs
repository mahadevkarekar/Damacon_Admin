using System;
using Damacon.DAL.i18n.Abstract;
using Damacon.DAL.i18n.Concrete;
using System.Globalization;
    
namespace Damacon.DAL.i18n {
        public partial class Resources {
            private static IResourceProvider resourceProvider = new DbResourceProvider();

        public static string GetResource(string resourceName)
        {
            return (string)resourceProvider.GetResource(resourceName, CultureInfo.CurrentUICulture.Name);
        }
                
        
        public static string AccessMessage {
               get {
                   return (string) resourceProvider.GetResource("AccessMessage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string AccountBalance {
               get {
                   return (string) resourceProvider.GetResource("AccountBalance", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string AccountBalanceMessage {
               get {
                   return (string) resourceProvider.GetResource("AccountBalanceMessage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Active {
               get {
                   return (string) resourceProvider.GetResource("Active", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Address {
               get {
                   return (string) resourceProvider.GetResource("Address", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Administration {
               get {
                   return (string) resourceProvider.GetResource("Administration", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string AllSelected {
               get {
                   return (string) resourceProvider.GetResource("AllSelected", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string AllStores {
               get {
                   return (string) resourceProvider.GetResource("AllStores", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Ascending {
               get {
                   return (string) resourceProvider.GetResource("Ascending", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string AsPreviousYear {
               get {
                   return (string) resourceProvider.GetResource("AsPreviousYear", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string AutumWinter {
               get {
                   return (string) resourceProvider.GetResource("AutumWinter", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string AvailableInHoliday {
               get {
                   return (string) resourceProvider.GetResource("AvailableInHoliday", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string AvailableReceiptForBonus {
               get {
                   return (string) resourceProvider.GetResource("AvailableReceiptForBonus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string AvailableReceiptForBonusToolTip {
               get {
                   return (string) resourceProvider.GetResource("AvailableReceiptForBonusToolTip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string AvailableVoucher {
               get {
                   return (string) resourceProvider.GetResource("AvailableVoucher", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BackupCreated {
               get {
                   return (string) resourceProvider.GetResource("BackupCreated", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BackupCreationWaitingText {
               get {
                   return (string) resourceProvider.GetResource("BackupCreationWaitingText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Birthday {
               get {
                   return (string) resourceProvider.GetResource("Birthday", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BirthdayFrom {
               get {
                   return (string) resourceProvider.GetResource("BirthdayFrom", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BirthdayTo {
               get {
                   return (string) resourceProvider.GetResource("BirthdayTo", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BirthPlace {
               get {
                   return (string) resourceProvider.GetResource("BirthPlace", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Bonus {
               get {
                   return (string) resourceProvider.GetResource("Bonus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BonusAdded {
               get {
                   return (string) resourceProvider.GetResource("BonusAdded", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BonusAddedTooltip {
               get {
                   return (string) resourceProvider.GetResource("BonusAddedTooltip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BonusBalance {
               get {
                   return (string) resourceProvider.GetResource("BonusBalance", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BonusHeader {
               get {
                   return (string) resourceProvider.GetResource("BonusHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BonusUsed {
               get {
                   return (string) resourceProvider.GetResource("BonusUsed", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BonusUsedTooltip {
               get {
                   return (string) resourceProvider.GetResource("BonusUsedTooltip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Brand {
               get {
                   return (string) resourceProvider.GetResource("Brand", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BrandBonusPercentage {
               get {
                   return (string) resourceProvider.GetResource("BrandBonusPercentage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BrandBonusUsageLimit {
               get {
                   return (string) resourceProvider.GetResource("BrandBonusUsageLimit", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BrandColor {
               get {
                   return (string) resourceProvider.GetResource("BrandColor", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BrandContactEmail {
               get {
                   return (string) resourceProvider.GetResource("BrandContactEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BrandContactName {
               get {
                   return (string) resourceProvider.GetResource("BrandContactName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BrandContactPhone {
               get {
                   return (string) resourceProvider.GetResource("BrandContactPhone", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BrandName {
               get {
                   return (string) resourceProvider.GetResource("BrandName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BrandSort {
               get {
                   return (string) resourceProvider.GetResource("BrandSort", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BrandSortDirection {
               get {
                   return (string) resourceProvider.GetResource("BrandSortDirection", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BrandStores {
               get {
                   return (string) resourceProvider.GetResource("BrandStores", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Buyer {
               get {
                   return (string) resourceProvider.GetResource("Buyer", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string BuyerHeader {
               get {
                   return (string) resourceProvider.GetResource("BuyerHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CalcExtraPay {
               get {
                   return (string) resourceProvider.GetResource("CalcExtraPay", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Cancel {
               get {
                   return (string) resourceProvider.GetResource("Cancel", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CardCode {
               get {
                   return (string) resourceProvider.GetResource("CardCode", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ChangeEmail {
               get {
                   return (string) resourceProvider.GetResource("ChangeEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ChangeExpiryTooltip {
               get {
                   return (string) resourceProvider.GetResource("ChangeExpiryTooltip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ChangePassword {
               get {
                   return (string) resourceProvider.GetResource("ChangePassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string City {
               get {
                   return (string) resourceProvider.GetResource("City", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Client {
               get {
                   return (string) resourceProvider.GetResource("Client", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientAvailableReceiptForBonusToolTip {
               get {
                   return (string) resourceProvider.GetResource("ClientAvailableReceiptForBonusToolTip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientBonusBalanceEmailTask {
               get {
                   return (string) resourceProvider.GetResource("ClientBonusBalanceEmailTask", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientBonusBalancePrintLine1 {
               get {
                   return (string) resourceProvider.GetResource("ClientBonusBalancePrintLine1", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientBonusBalancePrintLine2 {
               get {
                   return (string) resourceProvider.GetResource("ClientBonusBalancePrintLine2", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientBonusBalancePrintLine3 {
               get {
                   return (string) resourceProvider.GetResource("ClientBonusBalancePrintLine3", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientBonusBalancePrintLine4 {
               get {
                   return (string) resourceProvider.GetResource("ClientBonusBalancePrintLine4", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientBonusBalancePrintLine5 {
               get {
                   return (string) resourceProvider.GetResource("ClientBonusBalancePrintLine5", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientBonusBalancePrintLine6 {
               get {
                   return (string) resourceProvider.GetResource("ClientBonusBalancePrintLine6", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientBonusBalancePrintLine7 {
               get {
                   return (string) resourceProvider.GetResource("ClientBonusBalancePrintLine7", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientBonusBalancePrintLine8 {
               get {
                   return (string) resourceProvider.GetResource("ClientBonusBalancePrintLine8", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientBonusBalancePrintLine9 {
               get {
                   return (string) resourceProvider.GetResource("ClientBonusBalancePrintLine9", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientEmailNotRegistered {
               get {
                   return (string) resourceProvider.GetResource("ClientEmailNotRegistered", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientWebsiteLoginPageLine1 {
               get {
                   return (string) resourceProvider.GetResource("ClientWebsiteLoginPageLine1", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientWebsiteLoginPageLine2 {
               get {
                   return (string) resourceProvider.GetResource("ClientWebsiteLoginPageLine2", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientWebsiteLoginPageLine3 {
               get {
                   return (string) resourceProvider.GetResource("ClientWebsiteLoginPageLine3", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientWebsiteLoginPageLine4 {
               get {
                   return (string) resourceProvider.GetResource("ClientWebsiteLoginPageLine4", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientWebsiteLoginPageLine5 {
               get {
                   return (string) resourceProvider.GetResource("ClientWebsiteLoginPageLine5", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientWebsiteLoginPageLine6 {
               get {
                   return (string) resourceProvider.GetResource("ClientWebsiteLoginPageLine6", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ClientWebsiteLoginPageLineBottom {
               get {
                   return (string) resourceProvider.GetResource("ClientWebsiteLoginPageLineBottom", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Clone {
               get {
                   return (string) resourceProvider.GetResource("Clone", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CloneDetails {
               get {
                   return (string) resourceProvider.GetResource("CloneDetails", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CloneToDay {
               get {
                   return (string) resourceProvider.GetResource("CloneToDay", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Code {
               get {
                   return (string) resourceProvider.GetResource("Code", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CodFis {
               get {
                   return (string) resourceProvider.GetResource("CodFis", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ComGlobalStore {
               get {
                   return (string) resourceProvider.GetResource("ComGlobalStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Commision {
               get {
                   return (string) resourceProvider.GetResource("Commision", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CommisionHeader {
               get {
                   return (string) resourceProvider.GetResource("CommisionHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CommisionType {
               get {
                   return (string) resourceProvider.GetResource("CommisionType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Company {
               get {
                   return (string) resourceProvider.GetResource("Company", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CompanyName {
               get {
                   return (string) resourceProvider.GetResource("CompanyName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CompanysEmployees {
               get {
                   return (string) resourceProvider.GetResource("CompanysEmployees", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CompanyShortName {
               get {
                   return (string) resourceProvider.GetResource("CompanyShortName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CompanysStore {
               get {
                   return (string) resourceProvider.GetResource("CompanysStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ComparisonHours {
               get {
                   return (string) resourceProvider.GetResource("ComparisonHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ComparisonPercentage {
               get {
                   return (string) resourceProvider.GetResource("ComparisonPercentage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ComPersEmplSold {
               get {
                   return (string) resourceProvider.GetResource("ComPersEmplSold", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CompVacationHours {
               get {
                   return (string) resourceProvider.GetResource("CompVacationHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CompVacationHoursHeader {
               get {
                   return (string) resourceProvider.GetResource("CompVacationHoursHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ConfirmNewPassword {
               get {
                   return (string) resourceProvider.GetResource("ConfirmNewPassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ContactUSMessage {
               get {
                   return (string) resourceProvider.GetResource("ContactUSMessage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ContractEndDate {
               get {
                   return (string) resourceProvider.GetResource("ContractEndDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ContractHireLevel {
               get {
                   return (string) resourceProvider.GetResource("ContractHireLevel", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ContractHours {
               get {
                   return (string) resourceProvider.GetResource("ContractHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ContractHoursHeader {
               get {
                   return (string) resourceProvider.GetResource("ContractHoursHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ContractStartDate {
               get {
                   return (string) resourceProvider.GetResource("ContractStartDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ContractState {
               get {
                   return (string) resourceProvider.GetResource("ContractState", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ConvStdToHol {
               get {
                   return (string) resourceProvider.GetResource("ConvStdToHol", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CopyrightMessage {
               get {
                   return (string) resourceProvider.GetResource("CopyrightMessage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CopyToThisEmployee {
               get {
                   return (string) resourceProvider.GetResource("CopyToThisEmployee", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Corrections {
               get {
                   return (string) resourceProvider.GetResource("Corrections", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CorrectionsHeader {
               get {
                   return (string) resourceProvider.GetResource("CorrectionsHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Country {
               get {
                   return (string) resourceProvider.GetResource("Country", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CountryPhoneCode {
               get {
                   return (string) resourceProvider.GetResource("CountryPhoneCode", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CreateBackup {
               get {
                   return (string) resourceProvider.GetResource("CreateBackup", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CreateNew {
               get {
                   return (string) resourceProvider.GetResource("CreateNew", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Current {
               get {
                   return (string) resourceProvider.GetResource("Current", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CurrentBonusBalanceFrom {
               get {
                   return (string) resourceProvider.GetResource("CurrentBonusBalanceFrom", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CurrentBonusBalanceTo {
               get {
                   return (string) resourceProvider.GetResource("CurrentBonusBalanceTo", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CurrentEmail {
               get {
                   return (string) resourceProvider.GetResource("CurrentEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string CurrentPassword {
               get {
                   return (string) resourceProvider.GetResource("CurrentPassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DailyGlobalIncome {
               get {
                   return (string) resourceProvider.GetResource("DailyGlobalIncome", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DataDate {
               get {
                   return (string) resourceProvider.GetResource("DataDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DataDateToday {
               get {
                   return (string) resourceProvider.GetResource("DataDateToday", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DataDateYestarday {
               get {
                   return (string) resourceProvider.GetResource("DataDateYestarday", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Date {
               get {
                   return (string) resourceProvider.GetResource("Date", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DateEntered {
               get {
                   return (string) resourceProvider.GetResource("DateEntered", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DateOfBirth {
               get {
                   return (string) resourceProvider.GetResource("DateOfBirth", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DefaultExtraPayStore {
               get {
                   return (string) resourceProvider.GetResource("DefaultExtraPayStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DefaultExtraPayStoreToolTip {
               get {
                   return (string) resourceProvider.GetResource("DefaultExtraPayStoreToolTip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Delete {
               get {
                   return (string) resourceProvider.GetResource("Delete", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DeleteExtraPay {
               get {
                   return (string) resourceProvider.GetResource("DeleteExtraPay", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DeltaWorkedHours {
               get {
                   return (string) resourceProvider.GetResource("DeltaWorkedHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DeltaWorkedHoursHeader {
               get {
                   return (string) resourceProvider.GetResource("DeltaWorkedHoursHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Descending {
               get {
                   return (string) resourceProvider.GetResource("Descending", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Description {
               get {
                   return (string) resourceProvider.GetResource("Description", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Difference {
               get {
                   return (string) resourceProvider.GetResource("Difference", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DifferentialPercent {
               get {
                   return (string) resourceProvider.GetResource("DifferentialPercent", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Disabled {
               get {
                   return (string) resourceProvider.GetResource("Disabled", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Distribution {
               get {
                   return (string) resourceProvider.GetResource("Distribution", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DocumentationTitle {
               get {
                   return (string) resourceProvider.GetResource("DocumentationTitle", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DocumentationType {
               get {
                   return (string) resourceProvider.GetResource("DocumentationType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DocumentDate {
               get {
                   return (string) resourceProvider.GetResource("DocumentDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DocumentDateFrom {
               get {
                   return (string) resourceProvider.GetResource("DocumentDateFrom", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DocumentDateTo {
               get {
                   return (string) resourceProvider.GetResource("DocumentDateTo", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DocumentID {
               get {
                   return (string) resourceProvider.GetResource("DocumentID", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string DocumentTypeText {
               get {
                   return (string) resourceProvider.GetResource("DocumentTypeText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Edit {
               get {
                   return (string) resourceProvider.GetResource("Edit", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string EditConfiguration {
               get {
                   return (string) resourceProvider.GetResource("EditConfiguration", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Email {
               get {
                   return (string) resourceProvider.GetResource("Email", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string EmailAlertEnable {
               get {
                   return (string) resourceProvider.GetResource("EmailAlertEnable", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string EmailRecipients {
               get {
                   return (string) resourceProvider.GetResource("EmailRecipients", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string EmailText {
               get {
                   return (string) resourceProvider.GetResource("EmailText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string EmailType {
               get {
                   return (string) resourceProvider.GetResource("EmailType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Employee {
               get {
                   return (string) resourceProvider.GetResource("Employee", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string EmployeeAttribute1 {
               get {
                   return (string) resourceProvider.GetResource("EmployeeAttribute1", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string EmployeesCommisionType {
               get {
                   return (string) resourceProvider.GetResource("EmployeesCommisionType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string EmployeesCommisionTypeTooltipText {
               get {
                   return (string) resourceProvider.GetResource("EmployeesCommisionTypeTooltipText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string EnableBonus {
               get {
                   return (string) resourceProvider.GetResource("EnableBonus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Enabled {
               get {
                   return (string) resourceProvider.GetResource("Enabled", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string EnterCardCodeOrEmail {
               get {
                   return (string) resourceProvider.GetResource("EnterCardCodeOrEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Excluded {
               get {
                   return (string) resourceProvider.GetResource("Excluded", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExcludeFromClassifica {
               get {
                   return (string) resourceProvider.GetResource("ExcludeFromClassifica", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExpectedIncome {
               get {
                   return (string) resourceProvider.GetResource("ExpectedIncome", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExpectedIncomeType {
               get {
                   return (string) resourceProvider.GetResource("ExpectedIncomeType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Expiration {
               get {
                   return (string) resourceProvider.GetResource("Expiration", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Expired {
               get {
                   return (string) resourceProvider.GetResource("Expired", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExpireDateFrom {
               get {
                   return (string) resourceProvider.GetResource("ExpireDateFrom", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExpireDateTo {
               get {
                   return (string) resourceProvider.GetResource("ExpireDateTo", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExpiredValue {
               get {
                   return (string) resourceProvider.GetResource("ExpiredValue", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExportToExcel {
               get {
                   return (string) resourceProvider.GetResource("ExportToExcel", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExportToPDF {
               get {
                   return (string) resourceProvider.GetResource("ExportToPDF", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExtraAmount {
               get {
                   return (string) resourceProvider.GetResource("ExtraAmount", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExtraBonus {
               get {
                   return (string) resourceProvider.GetResource("ExtraBonus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExtraHolidayAmount {
               get {
                   return (string) resourceProvider.GetResource("ExtraHolidayAmount", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExtraPaymentHoursNorm {
               get {
                   return (string) resourceProvider.GetResource("ExtraPaymentHoursNorm", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExtraPaymentHoursNormHeader {
               get {
                   return (string) resourceProvider.GetResource("ExtraPaymentHoursNormHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ExtraPayStore {
               get {
                   return (string) resourceProvider.GetResource("ExtraPayStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Fax {
               get {
                   return (string) resourceProvider.GetResource("Fax", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Female {
               get {
                   return (string) resourceProvider.GetResource("Female", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string FidelityBonus {
               get {
                   return (string) resourceProvider.GetResource("FidelityBonus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string FidelityCard {
               get {
                   return (string) resourceProvider.GetResource("FidelityCard", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string FidelityCardBalance {
               get {
                   return (string) resourceProvider.GetResource("FidelityCardBalance", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string FileOverwriteConfirmation {
               get {
                   return (string) resourceProvider.GetResource("FileOverwriteConfirmation", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string FirstName {
               get {
                   return (string) resourceProvider.GetResource("FirstName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string FiscalIncome {
               get {
                   return (string) resourceProvider.GetResource("FiscalIncome", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ForgetPassword {
               get {
                   return (string) resourceProvider.GetResource("ForgetPassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string From {
               get {
                   return (string) resourceProvider.GetResource("From", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Gender {
               get {
                   return (string) resourceProvider.GetResource("Gender", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GeneralSort {
               get {
                   return (string) resourceProvider.GetResource("GeneralSort", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GenerateBonus {
               get {
                   return (string) resourceProvider.GetResource("GenerateBonus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GenerateBonusAndEmail {
               get {
                   return (string) resourceProvider.GetResource("GenerateBonusAndEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GenerateBonusAndPrint {
               get {
                   return (string) resourceProvider.GetResource("GenerateBonusAndPrint", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GeneratePassword {
               get {
                   return (string) resourceProvider.GetResource("GeneratePassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GenericSearch {
               get {
                   return (string) resourceProvider.GetResource("GenericSearch", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GlobalIncome {
               get {
                   return (string) resourceProvider.GetResource("GlobalIncome", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoodByeAndThanks {
               get {
                   return (string) resourceProvider.GetResource("GoodByeAndThanks", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoodsDescription {
               get {
                   return (string) resourceProvider.GetResource("GoodsDescription", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoodsInOut {
               get {
                   return (string) resourceProvider.GetResource("GoodsInOut", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoodsInOutDate {
               get {
                   return (string) resourceProvider.GetResource("GoodsInOutDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoodsTypeText {
               get {
                   return (string) resourceProvider.GetResource("GoodsTypeText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoToBonusManagement {
               get {
                   return (string) resourceProvider.GetResource("GoToBonusManagement", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoToClientManagement {
               get {
                   return (string) resourceProvider.GetResource("GoToClientManagement", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoToEmployeeManagement {
               get {
                   return (string) resourceProvider.GetResource("GoToEmployeeManagement", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoToSales {
               get {
                   return (string) resourceProvider.GetResource("GoToSales", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoToStores {
               get {
                   return (string) resourceProvider.GetResource("GoToStores", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string GoToUsersList {
               get {
                   return (string) resourceProvider.GetResource("GoToUsersList", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HalfHour {
               get {
                   return (string) resourceProvider.GetResource("HalfHour", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HexColorCode {
               get {
                   return (string) resourceProvider.GetResource("HexColorCode", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HiredBy {
               get {
                   return (string) resourceProvider.GetResource("HiredBy", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_About {
               get {
                   return (string) resourceProvider.GetResource("HM_About", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Backup {
               get {
                   return (string) resourceProvider.GetResource("HM_Backup", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_BonusManagement {
               get {
                   return (string) resourceProvider.GetResource("HM_BonusManagement", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_BonusManagementNew {
               get {
                   return (string) resourceProvider.GetResource("HM_BonusManagementNew", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Brand {
               get {
                   return (string) resourceProvider.GetResource("HM_Brand", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Buyer {
               get {
                   return (string) resourceProvider.GetResource("HM_Buyer", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_BuyerManagement {
               get {
                   return (string) resourceProvider.GetResource("HM_BuyerManagement", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Classifica {
               get {
                   return (string) resourceProvider.GetResource("HM_Classifica", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_ClassificaPeriodComparison {
               get {
                   return (string) resourceProvider.GetResource("HM_ClassificaPeriodComparison", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Clients {
               get {
                   return (string) resourceProvider.GetResource("HM_Clients", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Companies {
               get {
                   return (string) resourceProvider.GetResource("HM_Companies", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Comparison {
               get {
                   return (string) resourceProvider.GetResource("HM_Comparison", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_ComparisonByYearReport {
               get {
                   return (string) resourceProvider.GetResource("HM_ComparisonByYearReport", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_DailyIncomeReport {
               get {
                   return (string) resourceProvider.GetResource("HM_DailyIncomeReport", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Damacon_srl {
               get {
                   return (string) resourceProvider.GetResource("HM_Damacon_srl", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Dashboard {
               get {
                   return (string) resourceProvider.GetResource("HM_Dashboard", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Documentation {
               get {
                   return (string) resourceProvider.GetResource("HM_Documentation", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Documents {
               get {
                   return (string) resourceProvider.GetResource("HM_Documents", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_DocumentsRegistration {
               get {
                   return (string) resourceProvider.GetResource("HM_DocumentsRegistration", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_EmailServices {
               get {
                   return (string) resourceProvider.GetResource("HM_EmailServices", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_EmployeeCompanyContract {
               get {
                   return (string) resourceProvider.GetResource("HM_EmployeeCompanyContract", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Employees {
               get {
                   return (string) resourceProvider.GetResource("HM_Employees", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_EmployeesLevelCost {
               get {
                   return (string) resourceProvider.GetResource("HM_EmployeesLevelCost", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_EmployeesWorkedHoursReport {
               get {
                   return (string) resourceProvider.GetResource("HM_EmployeesWorkedHoursReport", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_EmployeesXP {
               get {
                   return (string) resourceProvider.GetResource("HM_EmployeesXP", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_FidelityCardsAndBonus {
               get {
                   return (string) resourceProvider.GetResource("HM_FidelityCardsAndBonus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Goods {
               get {
                   return (string) resourceProvider.GetResource("HM_Goods", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Languages {
               get {
                   return (string) resourceProvider.GetResource("HM_Languages", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_MonthlyIncomeReport {
               get {
                   return (string) resourceProvider.GetResource("HM_MonthlyIncomeReport", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_MonthlyWorkedHoursByStoreReport {
               get {
                   return (string) resourceProvider.GetResource("HM_MonthlyWorkedHoursByStoreReport", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Periods {
               get {
                   return (string) resourceProvider.GetResource("HM_Periods", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_RemoteConnection {
               get {
                   return (string) resourceProvider.GetResource("HM_RemoteConnection", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_RemoteConnectionMac {
               get {
                   return (string) resourceProvider.GetResource("HM_RemoteConnectionMac", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_RemoteConnectionWindows {
               get {
                   return (string) resourceProvider.GetResource("HM_RemoteConnectionWindows", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Report {
               get {
                   return (string) resourceProvider.GetResource("HM_Report", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Report14 {
               get {
                   return (string) resourceProvider.GetResource("HM_Report14", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Reports {
               get {
                   return (string) resourceProvider.GetResource("HM_Reports", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Sales {
               get {
                   return (string) resourceProvider.GetResource("HM_Sales", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_SentEmails {
               get {
                   return (string) resourceProvider.GetResource("HM_SentEmails", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Settings {
               get {
                   return (string) resourceProvider.GetResource("HM_Settings", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_ShoppingCenterReport {
               get {
                   return (string) resourceProvider.GetResource("HM_ShoppingCenterReport", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Store {
               get {
                   return (string) resourceProvider.GetResource("HM_Store", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_StoreHolidays {
               get {
                   return (string) resourceProvider.GetResource("HM_StoreHolidays", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Stores {
               get {
                   return (string) resourceProvider.GetResource("HM_Stores", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Summary {
               get {
                   return (string) resourceProvider.GetResource("HM_Summary", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Supplier {
               get {
                   return (string) resourceProvider.GetResource("HM_Supplier", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Table {
               get {
                   return (string) resourceProvider.GetResource("HM_Table", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Users {
               get {
                   return (string) resourceProvider.GetResource("HM_Users", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Variables {
               get {
                   return (string) resourceProvider.GetResource("HM_Variables", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_Version {
               get {
                   return (string) resourceProvider.GetResource("HM_Version", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_WindowsDresser {
               get {
                   return (string) resourceProvider.GetResource("HM_WindowsDresser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_WorkersTime {
               get {
                   return (string) resourceProvider.GetResource("HM_WorkersTime", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_WorkersTimetable {
               get {
                   return (string) resourceProvider.GetResource("HM_WorkersTimetable", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_WorkersTypeTime {
               get {
                   return (string) resourceProvider.GetResource("HM_WorkersTypeTime", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HM_XPSetting {
               get {
                   return (string) resourceProvider.GetResource("HM_XPSetting", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HolidayWorkedHours {
               get {
                   return (string) resourceProvider.GetResource("HolidayWorkedHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HolidayWorkedHoursConv {
               get {
                   return (string) resourceProvider.GetResource("HolidayWorkedHoursConv", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HolidayWorkedHoursHeader {
               get {
                   return (string) resourceProvider.GetResource("HolidayWorkedHoursHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HolidayWorkedHoursToPay {
               get {
                   return (string) resourceProvider.GetResource("HolidayWorkedHoursToPay", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HolidayWorkedHoursUsed {
               get {
                   return (string) resourceProvider.GetResource("HolidayWorkedHoursUsed", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HomePhone {
               get {
                   return (string) resourceProvider.GetResource("HomePhone", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HourlyWage {
               get {
                   return (string) resourceProvider.GetResource("HourlyWage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string HourlyWageHeader {
               get {
                   return (string) resourceProvider.GetResource("HourlyWageHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Hours {
               get {
                   return (string) resourceProvider.GetResource("Hours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string In {
               get {
                   return (string) resourceProvider.GetResource("In", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Included {
               get {
                   return (string) resourceProvider.GetResource("Included", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string IncludeInClassifica {
               get {
                   return (string) resourceProvider.GetResource("IncludeInClassifica", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Income {
               get {
                   return (string) resourceProvider.GetResource("Income", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string InStatistic {
               get {
                   return (string) resourceProvider.GetResource("InStatistic", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string InternalInvoices {
               get {
                   return (string) resourceProvider.GetResource("InternalInvoices", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string InTheStore {
               get {
                   return (string) resourceProvider.GetResource("InTheStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string InvalidData {
               get {
                   return (string) resourceProvider.GetResource("InvalidData", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string IsMainContract {
               get {
                   return (string) resourceProvider.GetResource("IsMainContract", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string IsStoreOffice {
               get {
                   return (string) resourceProvider.GetResource("IsStoreOffice", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Iva {
               get {
                   return (string) resourceProvider.GetResource("Iva", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Landscape {
               get {
                   return (string) resourceProvider.GetResource("Landscape", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LastBackupCreatedByUser {
               get {
                   return (string) resourceProvider.GetResource("LastBackupCreatedByUser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LastBackupFileCreated {
               get {
                   return (string) resourceProvider.GetResource("LastBackupFileCreated", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LastBalance {
               get {
                   return (string) resourceProvider.GetResource("LastBalance", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LASTCHANGEDATEHOUR {
               get {
                   return (string) resourceProvider.GetResource("LASTCHANGEDATEHOUR", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LastChangeExtraPay {
               get {
                   return (string) resourceProvider.GetResource("LastChangeExtraPay", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LastSentDateTime {
               get {
                   return (string) resourceProvider.GetResource("LastSentDateTime", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LastSentResult {
               get {
                   return (string) resourceProvider.GetResource("LastSentResult", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LastShopping {
               get {
                   return (string) resourceProvider.GetResource("LastShopping", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LastYear {
               get {
                   return (string) resourceProvider.GetResource("LastYear", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LeaveType {
               get {
                   return (string) resourceProvider.GetResource("LeaveType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Level {
               get {
                   return (string) resourceProvider.GetResource("Level", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LocalHoliday {
               get {
                   return (string) resourceProvider.GetResource("LocalHoliday", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Lock {
               get {
                   return (string) resourceProvider.GetResource("Lock", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LockedBy {
               get {
                   return (string) resourceProvider.GetResource("LockedBy", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LockedOn {
               get {
                   return (string) resourceProvider.GetResource("LockedOn", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LOGIN {
               get {
                   return (string) resourceProvider.GetResource("LOGIN", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LoginEmailTask {
               get {
                   return (string) resourceProvider.GetResource("LoginEmailTask", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LoginPrintLine1 {
               get {
                   return (string) resourceProvider.GetResource("LoginPrintLine1", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LoginPrintLine2 {
               get {
                   return (string) resourceProvider.GetResource("LoginPrintLine2", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LoginPrintLine3 {
               get {
                   return (string) resourceProvider.GetResource("LoginPrintLine3", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LoginPrintLine4 {
               get {
                   return (string) resourceProvider.GetResource("LoginPrintLine4", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LoginPrintLine5 {
               get {
                   return (string) resourceProvider.GetResource("LoginPrintLine5", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LoginPrintLine6 {
               get {
                   return (string) resourceProvider.GetResource("LoginPrintLine6", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LunchTickets {
               get {
                   return (string) resourceProvider.GetResource("LunchTickets", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string LunchTicketsHeader {
               get {
                   return (string) resourceProvider.GetResource("LunchTicketsHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_AutoLogOffContinue {
               get {
                   return (string) resourceProvider.GetResource("M_AutoLogOffContinue", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_AutoLogOffSignOut {
               get {
                   return (string) resourceProvider.GetResource("M_AutoLogOffSignOut", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_AutoLogOffTimeRemaining {
               get {
                   return (string) resourceProvider.GetResource("M_AutoLogOffTimeRemaining", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_AutoLogOffTitle {
               get {
                   return (string) resourceProvider.GetResource("M_AutoLogOffTitle", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_BonusUsedMoreThanRecieptAmountError {
               get {
                   return (string) resourceProvider.GetResource("M_BonusUsedMoreThanRecieptAmountError", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_BrandAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_BrandAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_BrandColorAlreadyInUse {
               get {
                   return (string) resourceProvider.GetResource("M_BrandColorAlreadyInUse", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_ButtonDeleteEPHelp {
               get {
                   return (string) resourceProvider.GetResource("M_ButtonDeleteEPHelp", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_BuyerAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_BuyerAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CalcExtraPayNoStoreSel {
               get {
                   return (string) resourceProvider.GetResource("M_CalcExtraPayNoStoreSel", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CalcExtraPayOverwrite {
               get {
                   return (string) resourceProvider.GetResource("M_CalcExtraPayOverwrite", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CantChangeCompanyInEmployeeContact {
               get {
                   return (string) resourceProvider.GetResource("M_CantChangeCompanyInEmployeeContact", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CantChangeContractDateRangeInEmployeeContact {
               get {
                   return (string) resourceProvider.GetResource("M_CantChangeContractDateRangeInEmployeeContact", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CantChangeStoreInEmployeeContact {
               get {
                   return (string) resourceProvider.GetResource("M_CantChangeStoreInEmployeeContact", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CantDeleteContractBecauseOfExtraPayStore {
               get {
                   return (string) resourceProvider.GetResource("M_CantDeleteContractBecauseOfExtraPayStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CantDeleteRecord {
               get {
                   return (string) resourceProvider.GetResource("M_CantDeleteRecord", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CantDeleteStoreBecauseOfExtraPayStore {
               get {
                   return (string) resourceProvider.GetResource("M_CantDeleteStoreBecauseOfExtraPayStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CardCodeAlreadyExists {
               get {
                   return (string) resourceProvider.GetResource("M_CardCodeAlreadyExists", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CardCodeOrEmailNotCorrect {
               get {
                   return (string) resourceProvider.GetResource("M_CardCodeOrEmailNotCorrect", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_ClientEmailNoSet {
               get {
                   return (string) resourceProvider.GetResource("M_ClientEmailNoSet", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_ClientWithEmailExists {
               get {
                   return (string) resourceProvider.GetResource("M_ClientWithEmailExists", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_ClientWithPhoneExists {
               get {
                   return (string) resourceProvider.GetResource("M_ClientWithPhoneExists", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CloneDestinationLocked {
               get {
                   return (string) resourceProvider.GetResource("M_CloneDestinationLocked", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CloneSuccessfully {
               get {
                   return (string) resourceProvider.GetResource("M_CloneSuccessfully", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CloneWorkedHourAllEmployeeError {
               get {
                   return (string) resourceProvider.GetResource("M_CloneWorkedHourAllEmployeeError", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CloneWorkersTimeTableContractNotFound {
               get {
                   return (string) resourceProvider.GetResource("M_CloneWorkersTimeTableContractNotFound", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CloneWorkersTimeTableDataAlreadyExists {
               get {
                   return (string) resourceProvider.GetResource("M_CloneWorkersTimeTableDataAlreadyExists", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CompanyAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_CompanyAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CreateExtraPayComplete {
               get {
                   return (string) resourceProvider.GetResource("M_CreateExtraPayComplete", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CurrentPasswordNotCorrect {
               get {
                   return (string) resourceProvider.GetResource("M_CurrentPasswordNotCorrect", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_CurrentPreviousShouldNotBeSame {
               get {
                   return (string) resourceProvider.GetResource("M_CurrentPreviousShouldNotBeSame", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_DeleteExtraPay {
               get {
                   return (string) resourceProvider.GetResource("M_DeleteExtraPay", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_DeleteExtraPayComplete {
               get {
                   return (string) resourceProvider.GetResource("M_DeleteExtraPayComplete", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_DifferentialAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_DifferentialAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_DocumentationAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_DocumentationAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_DocumentTypeAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_DocumentTypeAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EitherDocumentDateOrGoodsInOutIsRequired {
               get {
                   return (string) resourceProvider.GetResource("M_EitherDocumentDateOrGoodsInOutIsRequired", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EitherFirstNameOrCompanyIsRequired {
               get {
                   return (string) resourceProvider.GetResource("M_EitherFirstNameOrCompanyIsRequired", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EitherPhoneOrEmailIsRequired {
               get {
                   return (string) resourceProvider.GetResource("M_EitherPhoneOrEmailIsRequired", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EitherReceiptOrExtraBonusIsRequired {
               get {
                   return (string) resourceProvider.GetResource("M_EitherReceiptOrExtraBonusIsRequired", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmailHelpText {
               get {
                   return (string) resourceProvider.GetResource("M_EmailHelpText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmailNoSet {
               get {
                   return (string) resourceProvider.GetResource("M_EmailNoSet", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmailNotConfiguredInStore {
               get {
                   return (string) resourceProvider.GetResource("M_EmailNotConfiguredInStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmailSentSuccessfully {
               get {
                   return (string) resourceProvider.GetResource("M_EmailSentSuccessfully", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmailServiceItemAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_EmailServiceItemAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmailUpdatedSuccessfully {
               get {
                   return (string) resourceProvider.GetResource("M_EmailUpdatedSuccessfully", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmployeeAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_EmployeeAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmployeeCompanyContractAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_EmployeeCompanyContractAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmployeeDataContractMissing {
               get {
                   return (string) resourceProvider.GetResource("M_EmployeeDataContractMissing", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmployeesEXPAlreadyFound_P1 {
               get {
                   return (string) resourceProvider.GetResource("M_EmployeesEXPAlreadyFound_P1", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_EmployeesEXPAlreadyFound_P2 {
               get {
                   return (string) resourceProvider.GetResource("M_EmployeesEXPAlreadyFound_P2", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_ErrorInEditShopping {
               get {
                   return (string) resourceProvider.GetResource("M_ErrorInEditShopping", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_ErrorInUsingSelectedVouchers {
               get {
                   return (string) resourceProvider.GetResource("M_ErrorInUsingSelectedVouchers", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_ExtraPayStoreContractDoesntExistInContract {
               get {
                   return (string) resourceProvider.GetResource("M_ExtraPayStoreContractDoesntExistInContract", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_FieldRequired {
               get {
                   return (string) resourceProvider.GetResource("M_FieldRequired", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_FromToDateError {
               get {
                   return (string) resourceProvider.GetResource("M_FromToDateError", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_GoodsTypeAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_GoodsTypeAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_HomePhoneHelpText {
               get {
                   return (string) resourceProvider.GetResource("M_HomePhoneHelpText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_HourConversionRateMissing {
               get {
                   return (string) resourceProvider.GetResource("M_HourConversionRateMissing", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_InactiveUser  {
               get {
                   return (string) resourceProvider.GetResource("M_InactiveUser ", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_IncorrectUserNameorPasssword {
               get {
                   return (string) resourceProvider.GetResource("M_IncorrectUserNameorPasssword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_InSufficientReceiptValueForVoucher {
               get {
                   return (string) resourceProvider.GetResource("M_InSufficientReceiptValueForVoucher", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_InternalServerError {
               get {
                   return (string) resourceProvider.GetResource("M_InternalServerError", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_InvalidColorCode {
               get {
                   return (string) resourceProvider.GetResource("M_InvalidColorCode", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_InvalidEmail {
               get {
                   return (string) resourceProvider.GetResource("M_InvalidEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_InvalidEmailCredentials {
               get {
                   return (string) resourceProvider.GetResource("M_InvalidEmailCredentials", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_InvalidPhoneNumber {
               get {
                   return (string) resourceProvider.GetResource("M_InvalidPhoneNumber", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_InvalidUser {
               get {
                   return (string) resourceProvider.GetResource("M_InvalidUser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_KindlyAcceptPrivacyPolicy {
               get {
                   return (string) resourceProvider.GetResource("M_KindlyAcceptPrivacyPolicy", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_LanguageResourceAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_LanguageResourceAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_Lock {
               get {
                   return (string) resourceProvider.GetResource("M_Lock", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_MainContractAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_MainContractAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_MaximumNumberOfContractsReached {
               get {
                   return (string) resourceProvider.GetResource("M_MaximumNumberOfContractsReached", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_MobilePhoneHelpText {
               get {
                   return (string) resourceProvider.GetResource("M_MobilePhoneHelpText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_MobilePhoneMustBeMinimun8Digit {
               get {
                   return (string) resourceProvider.GetResource("M_MobilePhoneMustBeMinimun8Digit", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_MoreVoucherThanNeeded {
               get {
                   return (string) resourceProvider.GetResource("M_MoreVoucherThanNeeded", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_NewPasswordMinLength {
               get {
                   return (string) resourceProvider.GetResource("M_NewPasswordMinLength", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_NoBuyerDataEntered {
               get {
                   return (string) resourceProvider.GetResource("M_NoBuyerDataEntered", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_OnlyLastRecordCanBeDeleted {
               get {
                   return (string) resourceProvider.GetResource("M_OnlyLastRecordCanBeDeleted", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_OnlyLastRecordCanBeEdited {
               get {
                   return (string) resourceProvider.GetResource("M_OnlyLastRecordCanBeEdited", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_OnlyNumbersAreAllowed {
               get {
                   return (string) resourceProvider.GetResource("M_OnlyNumbersAreAllowed", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_OnlyPositiveNumberAllowed {
               get {
                   return (string) resourceProvider.GetResource("M_OnlyPositiveNumberAllowed", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_PasswordConfirmPasswordDontMatch {
               get {
                   return (string) resourceProvider.GetResource("M_PasswordConfirmPasswordDontMatch", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_PasswordHelpText {
               get {
                   return (string) resourceProvider.GetResource("M_PasswordHelpText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_PasswordRecoveryEmailSent {
               get {
                   return (string) resourceProvider.GetResource("M_PasswordRecoveryEmailSent", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_PasswordUpdatedSuccessfully {
               get {
                   return (string) resourceProvider.GetResource("M_PasswordUpdatedSuccessfully", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_PleaseSelectStore {
               get {
                   return (string) resourceProvider.GetResource("M_PleaseSelectStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_SalesLocked {
               get {
                   return (string) resourceProvider.GetResource("M_SalesLocked", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_SalesNumberOfRecieptsNotEntered {
               get {
                   return (string) resourceProvider.GetResource("M_SalesNumberOfRecieptsNotEntered", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_SameBuyerSameDateNotAllowed {
               get {
                   return (string) resourceProvider.GetResource("M_SameBuyerSameDateNotAllowed", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_SameWorkerRecordForSameTimeForOtherStore {
               get {
                   return (string) resourceProvider.GetResource("M_SameWorkerRecordForSameTimeForOtherStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_SchedExtraPayDisabled {
               get {
                   return (string) resourceProvider.GetResource("M_SchedExtraPayDisabled", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_SchedExtraPayDisabledSetHolidayPage {
               get {
                   return (string) resourceProvider.GetResource("M_SchedExtraPayDisabledSetHolidayPage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_ShoppingDateCantBeOlder {
               get {
                   return (string) resourceProvider.GetResource("M_ShoppingDateCantBeOlder", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_SpecialCharactersAndSpaceNotAllowed {
               get {
                   return (string) resourceProvider.GetResource("M_SpecialCharactersAndSpaceNotAllowed", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_SpecialCharactersNotAllowed {
               get {
                   return (string) resourceProvider.GetResource("M_SpecialCharactersNotAllowed", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_SupplierAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_SupplierAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_SupplierNotEntered {
               get {
                   return (string) resourceProvider.GetResource("M_SupplierNotEntered", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_TicketValueMissing {
               get {
                   return (string) resourceProvider.GetResource("M_TicketValueMissing", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_TypeOfGoodsNotEntered {
               get {
                   return (string) resourceProvider.GetResource("M_TypeOfGoodsNotEntered", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_UnableToCreateDeveloperUser {
               get {
                   return (string) resourceProvider.GetResource("M_UnableToCreateDeveloperUser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_UnableToDeleteClient {
               get {
                   return (string) resourceProvider.GetResource("M_UnableToDeleteClient", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_UnableToDeleteVoucher {
               get {
                   return (string) resourceProvider.GetResource("M_UnableToDeleteVoucher", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_UnableToEmail {
               get {
                   return (string) resourceProvider.GetResource("M_UnableToEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_UnableToPrint {
               get {
                   return (string) resourceProvider.GetResource("M_UnableToPrint", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_UnLock {
               get {
                   return (string) resourceProvider.GetResource("M_UnLock", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_UserAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_UserAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_UserNameHelpText {
               get {
                   return (string) resourceProvider.GetResource("M_UserNameHelpText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_UserSessionExpired {
               get {
                   return (string) resourceProvider.GetResource("M_UserSessionExpired", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_UserSessionExpiredWithManualLogout {
               get {
                   return (string) resourceProvider.GetResource("M_UserSessionExpiredWithManualLogout", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_VerifiedByNotEntered {
               get {
                   return (string) resourceProvider.GetResource("M_VerifiedByNotEntered", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_WorkedHoursSoldNotEntered {
               get {
                   return (string) resourceProvider.GetResource("M_WorkedHoursSoldNotEntered", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string M_WorkerTimeTypeAlreadyExist {
               get {
                   return (string) resourceProvider.GetResource("M_WorkerTimeTypeAlreadyExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Male {
               get {
                   return (string) resourceProvider.GetResource("Male", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ManualInsert {
               get {
                   return (string) resourceProvider.GetResource("ManualInsert", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string MaxTicket {
               get {
                   return (string) resourceProvider.GetResource("MaxTicket", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string MissingReceiptForBonus {
               get {
                   return (string) resourceProvider.GetResource("MissingReceiptForBonus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string MissingReceiptForBonusToolTip {
               get {
                   return (string) resourceProvider.GetResource("MissingReceiptForBonusToolTip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string MobilePhone {
               get {
                   return (string) resourceProvider.GetResource("MobilePhone", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Month {
               get {
                   return (string) resourceProvider.GetResource("Month", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string MonthlyBonusAmount {
               get {
                   return (string) resourceProvider.GetResource("MonthlyBonusAmount", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string MonthlyStoreRefoud {
               get {
                   return (string) resourceProvider.GetResource("MonthlyStoreRefoud", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Name {
               get {
                   return (string) resourceProvider.GetResource("Name", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string New {
               get {
                   return (string) resourceProvider.GetResource("New", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewBrand {
               get {
                   return (string) resourceProvider.GetResource("NewBrand", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewBuyer {
               get {
                   return (string) resourceProvider.GetResource("NewBuyer", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewBuyerPurchase {
               get {
                   return (string) resourceProvider.GetResource("NewBuyerPurchase", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewClient {
               get {
                   return (string) resourceProvider.GetResource("NewClient", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewCompany {
               get {
                   return (string) resourceProvider.GetResource("NewCompany", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewDifferential {
               get {
                   return (string) resourceProvider.GetResource("NewDifferential", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewDocumentRegistration {
               get {
                   return (string) resourceProvider.GetResource("NewDocumentRegistration", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewDocumentType {
               get {
                   return (string) resourceProvider.GetResource("NewDocumentType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewEmail {
               get {
                   return (string) resourceProvider.GetResource("NewEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewEmailTask {
               get {
                   return (string) resourceProvider.GetResource("NewEmailTask", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewEmployee {
               get {
                   return (string) resourceProvider.GetResource("NewEmployee", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewEmployeeCompanyContract {
               get {
                   return (string) resourceProvider.GetResource("NewEmployeeCompanyContract", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewExtraPay {
               get {
                   return (string) resourceProvider.GetResource("NewExtraPay", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewGoodsType {
               get {
                   return (string) resourceProvider.GetResource("NewGoodsType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewLevel {
               get {
                   return (string) resourceProvider.GetResource("NewLevel", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewPassword {
               get {
                   return (string) resourceProvider.GetResource("NewPassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewShoppingRegistration {
               get {
                   return (string) resourceProvider.GetResource("NewShoppingRegistration", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewStore {
               get {
                   return (string) resourceProvider.GetResource("NewStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewSupplier {
               get {
                   return (string) resourceProvider.GetResource("NewSupplier", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewUser {
               get {
                   return (string) resourceProvider.GetResource("NewUser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NewWorkerTimeType {
               get {
                   return (string) resourceProvider.GetResource("NewWorkerTimeType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string No {
               get {
                   return (string) resourceProvider.GetResource("No", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Notes {
               get {
                   return (string) resourceProvider.GetResource("Notes", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NoVoucherAvailable {
               get {
                   return (string) resourceProvider.GetResource("NoVoucherAvailable", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NumberOfReceipts {
               get {
                   return (string) resourceProvider.GetResource("NumberOfReceipts", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NumberOfShoppers {
               get {
                   return (string) resourceProvider.GetResource("NumberOfShoppers", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string NumberOfYearsInReport {
               get {
                   return (string) resourceProvider.GetResource("NumberOfYearsInReport", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Other {
               get {
                   return (string) resourceProvider.GetResource("Other", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Out {
               get {
                   return (string) resourceProvider.GetResource("Out", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Parameter {
               get {
                   return (string) resourceProvider.GetResource("Parameter", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Password {
               get {
                   return (string) resourceProvider.GetResource("Password", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PasswordRecovery {
               get {
                   return (string) resourceProvider.GetResource("PasswordRecovery", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Phone {
               get {
                   return (string) resourceProvider.GetResource("Phone", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Pieces {
               get {
                   return (string) resourceProvider.GetResource("Pieces", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Points {
               get {
                   return (string) resourceProvider.GetResource("Points", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Port {
               get {
                   return (string) resourceProvider.GetResource("Port", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Portrait {
               get {
                   return (string) resourceProvider.GetResource("Portrait", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Previous {
               get {
                   return (string) resourceProvider.GetResource("Previous", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PreviousBonusBalance {
               get {
                   return (string) resourceProvider.GetResource("PreviousBonusBalance", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PreviousIncome {
               get {
                   return (string) resourceProvider.GetResource("PreviousIncome", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PrintBonusBalance {
               get {
                   return (string) resourceProvider.GetResource("PrintBonusBalance", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PrintClientLogin {
               get {
                   return (string) resourceProvider.GetResource("PrintClientLogin", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PrintLogin {
               get {
                   return (string) resourceProvider.GetResource("PrintLogin", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Privacy {
               get {
                   return (string) resourceProvider.GetResource("Privacy", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PrivacyAndCookies {
               get {
                   return (string) resourceProvider.GetResource("PrivacyAndCookies", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Prov {
               get {
                   return (string) resourceProvider.GetResource("Prov", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PublicWebsiteAvailableShoppingValue {
               get {
                   return (string) resourceProvider.GetResource("PublicWebsiteAvailableShoppingValue", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PublicWebsiteVoucherAmount {
               get {
                   return (string) resourceProvider.GetResource("PublicWebsiteVoucherAmount", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PurchaseFidelity {
               get {
                   return (string) resourceProvider.GetResource("PurchaseFidelity", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string PurchaseReturnFidelity {
               get {
                   return (string) resourceProvider.GetResource("PurchaseReturnFidelity", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Quantity {
               get {
                   return (string) resourceProvider.GetResource("Quantity", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReadAndAcceptedPrivacy {
               get {
                   return (string) resourceProvider.GetResource("ReadAndAcceptedPrivacy", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Receipt {
               get {
                   return (string) resourceProvider.GetResource("Receipt", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReceiptAmountFrom {
               get {
                   return (string) resourceProvider.GetResource("ReceiptAmountFrom", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReceiptAmountTo {
               get {
                   return (string) resourceProvider.GetResource("ReceiptAmountTo", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReceiptAmountTooltip {
               get {
                   return (string) resourceProvider.GetResource("ReceiptAmountTooltip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReceiptDate {
               get {
                   return (string) resourceProvider.GetResource("ReceiptDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReceiptFromDate {
               get {
                   return (string) resourceProvider.GetResource("ReceiptFromDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReceiptToDate {
               get {
                   return (string) resourceProvider.GetResource("ReceiptToDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Recepients {
               get {
                   return (string) resourceProvider.GetResource("Recepients", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string RecordAlreadyExists {
               get {
                   return (string) resourceProvider.GetResource("RecordAlreadyExists", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string RecordDoesntExist {
               get {
                   return (string) resourceProvider.GetResource("RecordDoesntExist", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string RecordType {
               get {
                   return (string) resourceProvider.GetResource("RecordType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string RecoverClientPasswordEmailTask {
               get {
                   return (string) resourceProvider.GetResource("RecoverClientPasswordEmailTask", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Recovery {
               get {
                   return (string) resourceProvider.GetResource("Recovery", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string RefreshCache {
               get {
                   return (string) resourceProvider.GetResource("RefreshCache", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string RegisteredBy {
               get {
                   return (string) resourceProvider.GetResource("RegisteredBy", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string RegisteredOn {
               get {
                   return (string) resourceProvider.GetResource("RegisteredOn", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string RegistrationDate {
               get {
                   return (string) resourceProvider.GetResource("RegistrationDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Regulation {
               get {
                   return (string) resourceProvider.GetResource("Regulation", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string RegulationPageTitle {
               get {
                   return (string) resourceProvider.GetResource("RegulationPageTitle", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string RemainingValueForVoucher {
               get {
                   return (string) resourceProvider.GetResource("RemainingValueForVoucher", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_AI {
               get {
                   return (string) resourceProvider.GetResource("Report_AI", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_Comparison_BuyerPart {
               get {
                   return (string) resourceProvider.GetResource("Report_Comparison_BuyerPart", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_Employees_GraphEmployeesSales {
               get {
                   return (string) resourceProvider.GetResource("Report_Employees_GraphEmployeesSales", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_Employees_WorkedHours {
               get {
                   return (string) resourceProvider.GetResource("Report_Employees_WorkedHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_Employees_WorkedHoursByStore {
               get {
                   return (string) resourceProvider.GetResource("Report_Employees_WorkedHoursByStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_GenerateReport {
               get {
                   return (string) resourceProvider.GetResource("Report_GenerateReport", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_MonthlyIncomeDetailed {
               get {
                   return (string) resourceProvider.GetResource("Report_MonthlyIncomeDetailed", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_MonthlyIncomeDetailedWithBuyer {
               get {
                   return (string) resourceProvider.GetResource("Report_MonthlyIncomeDetailedWithBuyer", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_MonthlyIncomeWithBuyer {
               get {
                   return (string) resourceProvider.GetResource("Report_MonthlyIncomeWithBuyer", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_ORE {
               get {
                   return (string) resourceProvider.GetResource("Report_ORE", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_PE {
               get {
                   return (string) resourceProvider.GetResource("Report_PE", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_ReportChoice {
               get {
                   return (string) resourceProvider.GetResource("Report_ReportChoice", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_ReportChoice_IncomeComparison {
               get {
                   return (string) resourceProvider.GetResource("Report_ReportChoice_IncomeComparison", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_ReportChoice_IncomeComparisonByCompany {
               get {
                   return (string) resourceProvider.GetResource("Report_ReportChoice_IncomeComparisonByCompany", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_ReportChoice_IncomeComparisonByYears {
               get {
                   return (string) resourceProvider.GetResource("Report_ReportChoice_IncomeComparisonByYears", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_ReportMonthlyIncome {
               get {
                   return (string) resourceProvider.GetResource("Report_ReportMonthlyIncome", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Report_TotalIncome {
               get {
                   return (string) resourceProvider.GetResource("Report_TotalIncome", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReportCompare {
               get {
                   return (string) resourceProvider.GetResource("ReportCompare", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReportGroupBy {
               get {
                   return (string) resourceProvider.GetResource("ReportGroupBy", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReportOrientation {
               get {
                   return (string) resourceProvider.GetResource("ReportOrientation", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReportPeriod {
               get {
                   return (string) resourceProvider.GetResource("ReportPeriod", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Return {
               get {
                   return (string) resourceProvider.GetResource("Return", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReturnAmount {
               get {
                   return (string) resourceProvider.GetResource("ReturnAmount", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReturnAmountTooltip {
               get {
                   return (string) resourceProvider.GetResource("ReturnAmountTooltip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReturnAmountValidation {
               get {
                   return (string) resourceProvider.GetResource("ReturnAmountValidation", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReturnDate {
               get {
                   return (string) resourceProvider.GetResource("ReturnDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReturnType {
               get {
                   return (string) resourceProvider.GetResource("ReturnType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReturnTypeBonus {
               get {
                   return (string) resourceProvider.GetResource("ReturnTypeBonus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ReturnTypeCash {
               get {
                   return (string) resourceProvider.GetResource("ReturnTypeCash", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SalesNWorkedHourNotCompiled {
               get {
                   return (string) resourceProvider.GetResource("SalesNWorkedHourNotCompiled", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SaveAndInsertBonusRegistration {
               get {
                   return (string) resourceProvider.GetResource("SaveAndInsertBonusRegistration", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SearchMinimumCharacters {
               get {
                   return (string) resourceProvider.GetResource("SearchMinimumCharacters", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SelectedVoucher {
               get {
                   return (string) resourceProvider.GetResource("SelectedVoucher", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SelectHoliday {
               get {
                   return (string) resourceProvider.GetResource("SelectHoliday", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SelectMonthHolidays {
               get {
                   return (string) resourceProvider.GetResource("SelectMonthHolidays", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SelectStores {
               get {
                   return (string) resourceProvider.GetResource("SelectStores", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SelectWeekDays {
               get {
                   return (string) resourceProvider.GetResource("SelectWeekDays", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SendBonusBalanceByEmail {
               get {
                   return (string) resourceProvider.GetResource("SendBonusBalanceByEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Sender {
               get {
                   return (string) resourceProvider.GetResource("Sender", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SendLoginByEmail {
               get {
                   return (string) resourceProvider.GetResource("SendLoginByEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SendTime {
               get {
                   return (string) resourceProvider.GetResource("SendTime", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SentDateTime {
               get {
                   return (string) resourceProvider.GetResource("SentDateTime", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SentFrom {
               get {
                   return (string) resourceProvider.GetResource("SentFrom", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SentTo {
               get {
                   return (string) resourceProvider.GetResource("SentTo", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppedInStores {
               get {
                   return (string) resourceProvider.GetResource("ShoppedInStores", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Shopping {
               get {
                   return (string) resourceProvider.GetResource("Shopping", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingEmailTask {
               get {
                   return (string) resourceProvider.GetResource("ShoppingEmailTask", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingPrintLine1 {
               get {
                   return (string) resourceProvider.GetResource("ShoppingPrintLine1", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingPrintLine2 {
               get {
                   return (string) resourceProvider.GetResource("ShoppingPrintLine2", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingPrintLine3 {
               get {
                   return (string) resourceProvider.GetResource("ShoppingPrintLine3", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingPrintLine3_1 {
               get {
                   return (string) resourceProvider.GetResource("ShoppingPrintLine3_1", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingPrintLine4 {
               get {
                   return (string) resourceProvider.GetResource("ShoppingPrintLine4", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingPrintLine5 {
               get {
                   return (string) resourceProvider.GetResource("ShoppingPrintLine5", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingReturnEmailTask {
               get {
                   return (string) resourceProvider.GetResource("ShoppingReturnEmailTask", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingReturnPrintLine1 {
               get {
                   return (string) resourceProvider.GetResource("ShoppingReturnPrintLine1", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingReturnPrintLine2 {
               get {
                   return (string) resourceProvider.GetResource("ShoppingReturnPrintLine2", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingReturnTooltip {
               get {
                   return (string) resourceProvider.GetResource("ShoppingReturnTooltip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingValueForVoucher {
               get {
                   return (string) resourceProvider.GetResource("ShoppingValueForVoucher", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShoppingWithVoucher {
               get {
                   return (string) resourceProvider.GetResource("ShoppingWithVoucher", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ShowDeleted {
               get {
                   return (string) resourceProvider.GetResource("ShowDeleted", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SicksHours {
               get {
                   return (string) resourceProvider.GetResource("SicksHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SicksHoursHeader {
               get {
                   return (string) resourceProvider.GetResource("SicksHoursHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SignIn {
               get {
                   return (string) resourceProvider.GetResource("SignIn", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SmtpPassword {
               get {
                   return (string) resourceProvider.GetResource("SmtpPassword", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SmtpServer {
               get {
                   return (string) resourceProvider.GetResource("SmtpServer", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SmtpUser {
               get {
                   return (string) resourceProvider.GetResource("SmtpUser", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Sold {
               get {
                   return (string) resourceProvider.GetResource("Sold", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SomeStores {
               get {
                   return (string) resourceProvider.GetResource("SomeStores", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SortDirection {
               get {
                   return (string) resourceProvider.GetResource("SortDirection", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SpareField {
               get {
                   return (string) resourceProvider.GetResource("SpareField", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SpringSummer {
               get {
                   return (string) resourceProvider.GetResource("SpringSummer", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SSL {
               get {
                   return (string) resourceProvider.GetResource("SSL", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StandardAmount {
               get {
                   return (string) resourceProvider.GetResource("StandardAmount", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StandardHoursNorm {
               get {
                   return (string) resourceProvider.GetResource("StandardHoursNorm", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StandardHoursNormHeader {
               get {
                   return (string) resourceProvider.GetResource("StandardHoursNormHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string State {
               get {
                   return (string) resourceProvider.GetResource("State", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Status {
               get {
                   return (string) resourceProvider.GetResource("Status", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StoreList {
               get {
                   return (string) resourceProvider.GetResource("StoreList", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StoreManagerUsers {
               get {
                   return (string) resourceProvider.GetResource("StoreManagerUsers", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StoreName {
               get {
                   return (string) resourceProvider.GetResource("StoreName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StoreRecipient {
               get {
                   return (string) resourceProvider.GetResource("StoreRecipient", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StoreRefoud {
               get {
                   return (string) resourceProvider.GetResource("StoreRefoud", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StoreRefoundHeader {
               get {
                   return (string) resourceProvider.GetResource("StoreRefoundHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StoreSender {
               get {
                   return (string) resourceProvider.GetResource("StoreSender", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StoreShortName {
               get {
                   return (string) resourceProvider.GetResource("StoreShortName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string StoreWorkers {
               get {
                   return (string) resourceProvider.GetResource("StoreWorkers", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Subject {
               get {
                   return (string) resourceProvider.GetResource("Subject", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string SupplierIfNotInList {
               get {
                   return (string) resourceProvider.GetResource("SupplierIfNotInList", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Surname {
               get {
                   return (string) resourceProvider.GetResource("Surname", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Tag {
               get {
                   return (string) resourceProvider.GetResource("Tag", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TestEmail {
               get {
                   return (string) resourceProvider.GetResource("TestEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TestEmailRecipient {
               get {
                   return (string) resourceProvider.GetResource("TestEmailRecipient", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TicketValue {
               get {
                   return (string) resourceProvider.GetResource("TicketValue", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string To {
               get {
                   return (string) resourceProvider.GetResource("To", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ToPay {
               get {
                   return (string) resourceProvider.GetResource("ToPay", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Total {
               get {
                   return (string) resourceProvider.GetResource("Total", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalDocumentAmount {
               get {
                   return (string) resourceProvider.GetResource("TotalDocumentAmount", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalHours {
               get {
                   return (string) resourceProvider.GetResource("TotalHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalIncome {
               get {
                   return (string) resourceProvider.GetResource("TotalIncome", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalPayment {
               get {
                   return (string) resourceProvider.GetResource("TotalPayment", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalPaymentHeader {
               get {
                   return (string) resourceProvider.GetResource("TotalPaymentHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalPieces {
               get {
                   return (string) resourceProvider.GetResource("TotalPieces", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalPoints {
               get {
                   return (string) resourceProvider.GetResource("TotalPoints", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalVacancy {
               get {
                   return (string) resourceProvider.GetResource("TotalVacancy", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalWeekHours {
               get {
                   return (string) resourceProvider.GetResource("TotalWeekHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalWorkedHours {
               get {
                   return (string) resourceProvider.GetResource("TotalWorkedHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TotalWorkedHoursHeader {
               get {
                   return (string) resourceProvider.GetResource("TotalWorkedHoursHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TranfertRefoundHeader {
               get {
                   return (string) resourceProvider.GetResource("TranfertRefoundHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TranfertRefund {
               get {
                   return (string) resourceProvider.GetResource("TranfertRefund", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TypeOfContract {
               get {
                   return (string) resourceProvider.GetResource("TypeOfContract", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TypeOfDocument {
               get {
                   return (string) resourceProvider.GetResource("TypeOfDocument", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TypeOfGoods {
               get {
                   return (string) resourceProvider.GetResource("TypeOfGoods", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string TypeOfWork {
               get {
                   return (string) resourceProvider.GetResource("TypeOfWork", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UnauthorizedAccessMessage {
               get {
                   return (string) resourceProvider.GetResource("UnauthorizedAccessMessage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Unlock {
               get {
                   return (string) resourceProvider.GetResource("Unlock", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Update {
               get {
                   return (string) resourceProvider.GetResource("Update", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UpdateAndEmail {
               get {
                   return (string) resourceProvider.GetResource("UpdateAndEmail", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UpdateAndPrint {
               get {
                   return (string) resourceProvider.GetResource("UpdateAndPrint", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UploadDate {
               get {
                   return (string) resourceProvider.GetResource("UploadDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UploadDocumentation {
               get {
                   return (string) resourceProvider.GetResource("UploadDocumentation", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UploadedFile {
               get {
                   return (string) resourceProvider.GetResource("UploadedFile", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UploadFromDate {
               get {
                   return (string) resourceProvider.GetResource("UploadFromDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UploadImage {
               get {
                   return (string) resourceProvider.GetResource("UploadImage", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UploadPhoto {
               get {
                   return (string) resourceProvider.GetResource("UploadPhoto", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UploadToDate {
               get {
                   return (string) resourceProvider.GetResource("UploadToDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UsableUpTo {
               get {
                   return (string) resourceProvider.GetResource("UsableUpTo", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UseBefore {
               get {
                   return (string) resourceProvider.GetResource("UseBefore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Used {
               get {
                   return (string) resourceProvider.GetResource("Used", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Username {
               get {
                   return (string) resourceProvider.GetResource("Username", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UserType {
               get {
                   return (string) resourceProvider.GetResource("UserType", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UseThisBonus {
               get {
                   return (string) resourceProvider.GetResource("UseThisBonus", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UseThisBonusToolTip {
               get {
                   return (string) resourceProvider.GetResource("UseThisBonusToolTip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string UseYourFidelityCardNumebr {
               get {
                   return (string) resourceProvider.GetResource("UseYourFidelityCardNumebr", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VacationHours {
               get {
                   return (string) resourceProvider.GetResource("VacationHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VacationHoursHeader {
               get {
                   return (string) resourceProvider.GetResource("VacationHoursHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VacationHoursNorm {
               get {
                   return (string) resourceProvider.GetResource("VacationHoursNorm", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VacationHoursNormHeader {
               get {
                   return (string) resourceProvider.GetResource("VacationHoursNormHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Value {
               get {
                   return (string) resourceProvider.GetResource("Value", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ValueExtraPaymentHoursNorm {
               get {
                   return (string) resourceProvider.GetResource("ValueExtraPaymentHoursNorm", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ValueExtraPaymentHoursNormHeader {
               get {
                   return (string) resourceProvider.GetResource("ValueExtraPaymentHoursNormHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ValueHolidayWorkedHoursToPay {
               get {
                   return (string) resourceProvider.GetResource("ValueHolidayWorkedHoursToPay", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ValueStandardHoursNorm {
               get {
                   return (string) resourceProvider.GetResource("ValueStandardHoursNorm", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ValueStandardHoursNormHeader {
               get {
                   return (string) resourceProvider.GetResource("ValueStandardHoursNormHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ValueVacationHoursNorm {
               get {
                   return (string) resourceProvider.GetResource("ValueVacationHoursNorm", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ValueVacationHoursNormHeader {
               get {
                   return (string) resourceProvider.GetResource("ValueVacationHoursNormHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VerifiedBy {
               get {
                   return (string) resourceProvider.GetResource("VerifiedBy", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VerifiedByIfNotInList {
               get {
                   return (string) resourceProvider.GetResource("VerifiedByIfNotInList", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VirtualReceipt {
               get {
                   return (string) resourceProvider.GetResource("VirtualReceipt", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VirtualShopping {
               get {
                   return (string) resourceProvider.GetResource("VirtualShopping", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VisitTo {
               get {
                   return (string) resourceProvider.GetResource("VisitTo", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherAmount {
               get {
                   return (string) resourceProvider.GetResource("VoucherAmount", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherAvailable {
               get {
                   return (string) resourceProvider.GetResource("VoucherAvailable", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherCreatedSucessfully {
               get {
                   return (string) resourceProvider.GetResource("VoucherCreatedSucessfully", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherCreation {
               get {
                   return (string) resourceProvider.GetResource("VoucherCreation", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherEmailTask {
               get {
                   return (string) resourceProvider.GetResource("VoucherEmailTask", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherNumber {
               get {
                   return (string) resourceProvider.GetResource("VoucherNumber", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherPrintLine1 {
               get {
                   return (string) resourceProvider.GetResource("VoucherPrintLine1", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherPrintLine2 {
               get {
                   return (string) resourceProvider.GetResource("VoucherPrintLine2", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherPrintLine3 {
               get {
                   return (string) resourceProvider.GetResource("VoucherPrintLine3", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherPrintLine4 {
               get {
                   return (string) resourceProvider.GetResource("VoucherPrintLine4", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string VoucherPrintLine5 {
               get {
                   return (string) resourceProvider.GetResource("VoucherPrintLine5", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WebAddress {
               get {
                   return (string) resourceProvider.GetResource("WebAddress", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WebEnable {
               get {
                   return (string) resourceProvider.GetResource("WebEnable", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WebLink {
               get {
                   return (string) resourceProvider.GetResource("WebLink", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WebName {
               get {
                   return (string) resourceProvider.GetResource("WebName", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Week {
               get {
                   return (string) resourceProvider.GetResource("Week", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WeeklyHoursContract {
               get {
                   return (string) resourceProvider.GetResource("WeeklyHoursContract", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WeeklyWorkingDays {
               get {
                   return (string) resourceProvider.GetResource("WeeklyWorkingDays", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WeekNumber {
               get {
                   return (string) resourceProvider.GetResource("WeekNumber", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Weeks {
               get {
                   return (string) resourceProvider.GetResource("Weeks", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WindowsDresserToolTip {
               get {
                   return (string) resourceProvider.GetResource("WindowsDresserToolTip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WorkedDate {
               get {
                   return (string) resourceProvider.GetResource("WorkedDate", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WorkedHours {
               get {
                   return (string) resourceProvider.GetResource("WorkedHours", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WorkedHoursHeader {
               get {
                   return (string) resourceProvider.GetResource("WorkedHoursHeader", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WorkersTimetableCloneHelp {
               get {
                   return (string) resourceProvider.GetResource("WorkersTimetableCloneHelp", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WorkerTimeTypeText {
               get {
                   return (string) resourceProvider.GetResource("WorkerTimeTypeText", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WorkerTimeTypeTooltip {
               get {
                   return (string) resourceProvider.GetResource("WorkerTimeTypeTooltip", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WorkingInTheFollowingStore {
               get {
                   return (string) resourceProvider.GetResource("WorkingInTheFollowingStore", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string WorkingTime {
               get {
                   return (string) resourceProvider.GetResource("WorkingTime", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Year {
               get {
                   return (string) resourceProvider.GetResource("Year", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string YearIncome {
               get {
                   return (string) resourceProvider.GetResource("YearIncome", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string YearMonth {
               get {
                   return (string) resourceProvider.GetResource("YearMonth", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Years {
               get {
                   return (string) resourceProvider.GetResource("Years", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string Yes {
               get {
                   return (string) resourceProvider.GetResource("Yes", CultureInfo.CurrentUICulture.Name);
               }
            }
            
        
        public static string ZipCode {
               get {
                   return (string) resourceProvider.GetResource("ZipCode", CultureInfo.CurrentUICulture.Name);
               }
            }

        }        
}
