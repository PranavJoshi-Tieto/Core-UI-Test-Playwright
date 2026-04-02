namespace PlaywrightFramework.Utils
{
    /// <summary>
    /// Contains all constant values used across the framework.
    /// </summary>
    public static class ConstantValues
    {
        public static string ClassToSearch = "000";
        public static string ReceivedDate = "dd";
        public static string DefaultCase = "24/13712";
        public static string AutomationDefaultCase = "Automation-DataSet-Case";

        public static int DefaultColumn = 0;
        public static int CaseTitleColumnInMyPinItem = 2;

        public static int CaseTitleColumnInAdvSearch = 2;
        public static int DocumentTitleColumnInAdvSearch = 5;
        public static int ProjectTitleColumnInAdvSearch = 4;
        public static int ActivityTitleColumnInAdvSearch = 1;

        public static int CaseTitleColumnInHistory = 4;
        public static int DocumentTitleColumnInHistory = 4;
        public static int ProjectTitleColumnInHistory = 2;

        public static int DocumentTitleColumnInProjectView = 3;
        public static int CaseTitleColumnInProjectView = 2;

        public static int DocumentTitleColumnInNotSignOffWebpart = 6;

        public static int FileTitleColumnInDocumentPage = 2;
        public static int FileVersionHistoryRevisionNumber = 3;
        public static int FileVariantColumnInVersionHistory = 4;
        public static int FileTitleInFileRenameDialog = 1;
        public static int SortOrderInFileRenumberDialog = 1;

        public static string FileNameInFileSection = "TestWordFile";
        public static string TestFileNameInFileSection = "Test";
        public static string FileTypeInFileRenameDialog = "Covering letter";
        public static string FileEditTitle = "Edited";
        public static string ProductionFormatFileVariantValue = "0";
        public static string ArchiveFormatFileVariantValue = "1";
        public static string PublicFormatFileVariantValue = "2";
        public static string SignedDocumentFileVariantValue = "3";
        public static string DisplayFormatFileVariantValue = "4";
        public static string Attachment_RelatedAsInRenumberFilesDialog = "2";
        public static string CaseProposition_RelatedAsInRenumberFilesDialog = "6";
        public static string FrontMainCoverDocument_RelatedAsInRenumberFilesDialog = "3";
        public static string More_RelatedAsInRenumberFilesDialog = "5";
        public static string CoveringLetter_RelatedAsInRenumberFilesDialog = "4";

        public static int DocumentTitleColumnInDocumentSection = 4;
        public static int DocumentTitleColumnInDocumentExtSection = 5;
        public static int DocumentTitleColumnInTabView = 5;

        public static int SweDocumentTitleColumnInDocumentSection = 3;
        public static int SweDocumentTitleColumnInExtDocView = 4;

        public static string ResponsibleREG1 = "REG1%";
        public static string ResponsibleCH1 = "CH1%";
        public static string ResponsibleMGR1 = "MGR1%";
        public static string ResponsibleDepartment1 = "Department 1";
        public static string ResponsibleBS1 = "BS1";
        public static string ContactToSearchMGRB = "MGRB";
        public static string PersonalStandInCHC = "CHC - Sec C case handler";
        public static string ResponsibleMGRDept1 = "MGR1 - Dept 1 manager";
        public static string ResponsibleSIProduct = "SI Products";
        public static string ResponsiblePersonToSearch = "CH1 - Dept 1 case handler";
        public static string AccessCode = "Restricted";
        public static string RecipientToSearch = "CH1%";

        public static string DefaultSender = "%";
        public static string DefaultCaseToSearch = "%";
        public static string SenderSI = "SI";
        public static string DefaultClassCode = "%";
        public static string SenderAnkita = "Ankita%";
        public static string RequesterAnkita = "Ankita%";
        public static string RequesterEmailPvt = "Email Pvt Contact%";
        public static string UnregContactName = "TestAuto123";
        public static string UnregCaseContactName = "CaseAutoContact";
        public static string SenderAnkitaPS = "Temp%";
        public static string RequesterAnkitaPS = "Temp%";
        public static string InternalContact = "Vidyadhar%";

        public static string PDFStampIncomingMacro = "Mysstad kommun, Ankomstdatum:[received_date] Ärendenummer: [document_number]";
        public static string PDFStampOutgoingMacro = "Nynashamns kommun, Arendenummer: [document_number]";
        public static string PDFStampApprovalMacro = "Mysstads kommun, Godkänt av [approved_by] , Godkännandedatum [approval_date]";
        public static string PDFStampTextTransparency = "30";
        public static string PDFStampTextFontSize = "10";
        public static string PDFDocumentArchivesVal = "Case document";


        public static string TestFileText = "TestFile.txt";
        public static string ImportConfigurationFile = "ImportConfigurationFile.json";
        public static string TestWordFile = "TestWordFile.docx";
        public static string TestPDFFile = "TestPDFFile.pdf";
        public static string SearchTestWordFile = "TestWordFile";
        public static string TestImageFile = "TestImage.JPG";
        public static string TestLargeFile = "30MB-TESTFILE.pdf";
        public static string TestMsgFile = "TestEmail.msg";

        public static string CaseStatusClosedByCaseworker = "17";
        public static string CaseStatusReserved = "4";
        public static string CaseStatusInprogress = "5";
        public static string CaseStatusClosed = "6";
        public static string CaseStatusNoFollowUp = "7";
        public static string CaseStatusCancelled = "8";
        public static string CaseStatusCopiedExtract = "9";
        public static string AuditTrailEditText = "Case updated";
        public static string AuditTrailCreateText = "Case created";
        public static string ClosedStatus = "Closed";
        public static string CancelledStatus = "Cancelled";
        public static string AuditTrailCreateDocumentText = "Document created";
        public static string AuditTrailCreateProjectText = "Project created";
        public static string AuditTrailEditProjectText = "Project updated";
        public static string NewRemarkCase = "New remark added on case";
        public static string ProjectStatusPlanning = "Planning";
        public static string ProjectStatusOnHold = "On hold";
        public static string ProjectStatusClosed = "Closed";
        public static string OutboundStampText = "Nynashamns kommun, Arendenummer:";
        public static string IncomingStampText = "Mysstad kommun,";
        public static string ApprovedStampText = "Mysstads kommun,";
        public static string AddedCaseLinkAssertion = "Case";


        public static string DocumentStatusReserved = "1";
        public static string DocumentStatusOfficiallyRecordedVal = "6";
        public static string DocumentStatusPreliminaryRecordedVal = "2";
        public static string DocumentStatusRegistrationCompletedVal = "7";
        public static string DocumentStatusCancelledVal = "8";
        public static string DocumentStatusFinalizedVal = "6";
        public static string DocumentStatusInprogressVal = "100";
        public static string DocumentStatusCompletedBusinessVal = "101";
        public static string DocumentStatusApprovedBusinessVal = "50000";
        public static string DocumentStatusApprovedSharedBusinessVal = "10";
        public static string PolicyDocumentStatusInprogressDataVal = "100";
        public static string PolicyDocumentStatusCompletedDataVal = "101";
        public static string PolicyDocumentStatusInrevisionDataVal = "112";
        public static string PolicyDocumentStatusVoidDataVal = "104";
        public static string PolicyDocumentStatusApprovedDataVal = "50000";
        public static string PolicyDocumentStatusInapprovalprocessDataVal = "50002";

        public static string DocumentStatusOfficiallyRecorded = "Officially recorded";
        public static string DocumentStatusPreliminaryRecorded = "Preliminary recorded";
        public static string DocumentStatusFinalized = "Finalized";
        public static string DocumentStatusRegistrationCompleted = "Registration completed";
        public static string DocumentStatusCancelled = "Cancelled";
        public static string DocumentStatusApproved = "Approved";
        public static string DocumentStatusInProgress = "In progress";

        public static string TaskStatusClosed = "Closed";
        public static string TaskStatusCancelled = "Cancelled";
        public static string ActivitySearchResult = "Task";

        public static string DispChannelTypeEmail = "1";
        public static string DispChannelTypePrint = "2";
        public static string DispChannelTypePost = "Post";

        public static string IncomingDocumentDataVal = "110";
        public static string OutboundDocumentDataVal = "111";
        public static string MinutesDataVal = "112";
        public static string InternalMemoWithFollowupDataVal = "113";
        public static string IncomingemailDataVal = "114";
        public static string OutboundemailDataVal = "115";
        public static string SummonsDataVal = "118";
        public static string PropositionDataVal = "218";
        public static string InternalMemoWithoutFollowupDataVal = "60005";
        public static string DecisionDataVal = "60006";

        public static string PersonnelhandbookDataVal = "95509";
        public static string UserguideDataVal = "120";
        public static string InstructionsDataVal = "121";
        public static string ProcedureDataVal = "122";
        public static string FormDataVal = "123";
        public static string PlanDataVal = "124";
        public static string QuarterlyDataVal = "1";
        public static string SemiannualDataVal = "2";
        public static string AnnualDataVal = "3";
        public static string OneventDataVal = "4";

        public static string RevisionInprogressDataVal = "100";
        public static string RevisionCompletedDataVal = "101";
        public static string RevisionVoidDataVal = "104";
        public static string RevisionAppovedDataVal = "50000";
        public static string ShowRevisionVal01 = "01";
        public static string ShowRevisionVal02 = "02";
        public static int ShowRevisionColumn = 1;
        public static string RestrictedAccessCodeVal = "1";
        public static string KeepFromPublicAccessAccessCodeVal = "18";

        public static int GlobalSearchColumn = 0;
        public static string DecisionValue = "%";
        public static string TagSection = "Tags";

        public static string TestRemark = "TestRemark";
        public static string WorkflowStatusApproved = "Approved";
        public static string WorkflowDelegatedRemark = "Remaks Delegated";
        public static string WorkflowApprovedRemark = "Remaks Approved";
        public static string WorkflowTypeParallel = "50000";
        public static string WorkflowTypeSequential = "50001";
        public static int WorkflowStatusColumnInDocumentSection = 5;
        public static string WorkflowStatusApprovalOpen = "Approval - Open";
        public static string ReferencedFilesDocument = "AUTOMATION DOCUMENT WITH FILE";
        public static string EditExtDueDateTitle = "EditExtDueDateTitle";

        public static string DepartmentSiProductsVal = "506";
        public static string actionValueDropDownFin = "200128";
        public static string recordTypeDropDownFin = "200129";
        public static string userName1 = "Pranav";
        public static string userName2 = "Pooja";
        public static string userName3 = "sonali fin";
        public static string eSignUsername = "esigning.p360@tietoevry.com";
        public static string eSignUserPassword = "esigningp360";
        public static string eSignPranavUsername = "pranav.joshi@tietoevry.com";
        public static string eSignPranavPassword = "esigningp360";
        public static string eSignPoojaUsername = "pooja.kadam@tietoevry.com";
        public static string eSignPoojaPassword = "tietosign123";
        public static string eSignAutomationUser = "AutomationUser";
        public static string eSignSonaliUsername = "sonali.pingle+fin@tietoevry.com";
        public static string eSignSonaliPassword = "tietosign1234";


        public static string Bookmark = "Bookmark";
        public static string Bookmarked = "Bookmarked";

        public static string DocumentDetailsURL = "locator/DMS/Document/Details";
        public static string IFARCaseDetailsURL = "locator/DMS/Case/Details";
        public static string projectDetailsURL = "locator/CRM/Project/Details";
        public static string IFARActivityeDetailsURL = "locator/CRM/Activity/Details";

        public static string GrantedDataVal = "1";
        public static string PartiallyDataVal = "2";
        public static string DeniedDataVal = "3";
        public static string Granted = "Granted";
        public static string Partiallygranted = "Partially granted";
        public static string Denied = "Denied";
        public static string BlankDataVal = "-1";

        public static string DispatchChannelTypePrint = "Print";
        public static string DispatchChannelTypeEmail = "Email";
        public static int DispatchLinkColumn = 4;

        public static string ContactTypeEnterprise = "Enterprise";
        public static string ContactTypeContactPerson = "Contact person";
        public static string ContactTypePrivatePerson = "Private person";
        public static string ContactTypeBoard = "Board";
        public static string ExistingEnterpriseName = "TestEntp_1";

        public static string BaseEstateToSearch = "BaseEstate";
        public static string CaseTitle = "AutomationCase_DontEdit";

        public static string MimirQuestion = "How to create a case?";
        public static string MimirAnswer = "Response based on local routines";

        public static string AuditTrailRemText = "Activity updated";

        public static string FFUI_ProjectTitleName = "24-1-Automation Project";
        public static string FFUI_AccessCodePreliminaryLocked = "XX";
        public static string FFUI_SystemSettingPreview = "Preview";
        public static string FFUI_sectionName = "Automation-Section";

        public static string FFUI_Casetype_PropertiesTab = "Properties";
        public static string FFUI_Casetype_PermissionsTab = "Permissions";
        public static string FFUI_Casetype_CustomFieldsTab = "Custom fields";

        public static string ProjectConnection_NO = "No";
        public static string ProjectConnection_Mandatory = "Yes - Mandatory";
        public static string ProjectConnection_Optional = "Yes - Optional";

        public static string FFUI_Operator_Equal = "Is equal";
        public static string FFUI_Action_Hide = "Hide";
        public static string FFUI_Field_ProgressPlan = "Progress plan";
        public static string FFUI_Field_StartDate = "Start date";

        public static string FFUI_ActivityTypes_Header = "Activity types";
        public static string FFUI_TaskUIConfig_Header = "Task UI configuration";
    
    }
}
    