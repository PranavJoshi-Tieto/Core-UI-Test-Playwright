using NUnit.Framework;

namespace PlaywrightFramework.Utils
{
    /// <summary>
    /// Reads all parameters from the active .runsettings file.
    /// Switch environments by selecting dev/staging/prod.runsettings in your IDE or CLI.
    /// </summary>
    public static class TestSettings
    {
         public static string ENV =>
        TestContext.Parameters["ENV"] ?? "Automation";
        public static string BaseUrl =>
        TestContext.Parameters["BaseUrl"] ?? "https://mas-aut-publicnorlrg.dev.360online.com/";
        public static string Browser =>
            TestContext.Parameters["Browser"] ?? "chromium";
        public static bool Headless =>
            bool.Parse(TestContext.Parameters["Headless"] ?? "false");
        public static int SlowMo =>
            int.Parse(TestContext.Parameters["SlowMo"] ?? "0");
        public static int Timeout =>
            int.Parse(TestContext.Parameters["Timeout"] ?? "30000");
        public static int RetryCount =>
            int.Parse(TestContext.Parameters["RetryCount"] ?? "2");
        public static bool ScreenshotOnFailure =>
            bool.Parse(TestContext.Parameters["ScreenshotOnFailure"] ?? "false");
        public static bool VideoOnFailure =>
            bool.Parse(TestContext.Parameters["VideoOnFailure"] ?? "false");
        public static int ViewportWidth =>
           int.Parse(TestContext.Parameters["ViewportWidth"] ?? "1920");
        public static int ViewportHeight =>
            int.Parse(TestContext.Parameters["ViewportHeight"] ?? "1080");
        public static bool StartMaximized =>
            bool.Parse(TestContext.Parameters["StartMaximized"] ?? "false");
        public static bool Incognito =>
            bool.Parse(TestContext.Parameters["Incognito"] ?? "true");

        //-----------------------------------------------------------------------------------------------------------------------------//
        // Credentials for cloud environment - override in .runsettings for different envs or users

        public static string CloudAdminUserName =>
           TestContext.Parameters["CloudAdminUserName"] ?? "360admin@test.p360o.com";
        public static string CloudAdminPassword =>
            TestContext.Parameters["CloudAdminPassword"] ?? "yh0fHn4V1&69";
        public static string CloudAutotestAdminUserName =>
            TestContext.Parameters["CloudAutotestAdminUserName"] ?? "autotestadmin@test.p360o.com";
        public static string CloudAutotestAdminPassword =>
            TestContext.Parameters["CloudAutotestAdminPassword"] ?? "AUtead4321@";
        public static string CloudRegistrarUserName =>
            TestContext.Parameters["CloudRegistrarUserName"] ?? "reg1@test.p360o.com";
        public static string CloudRegistrarPassword =>
            TestContext.Parameters["CloudRegistrarPassword"] ?? "Eaple2021";
        public static string CloudCaseHandlerUserName =>
            TestContext.Parameters["CloudCaseHandlerUserName"] ?? "ch1@test.p360o.com";
        public static string CloudCaseHandlerPassword =>
            TestContext.Parameters["CloudCaseHandlerPassword"] ?? "Eaple2021";
        public static string CloudManagerUserName =>
            TestContext.Parameters["CloudManagerUserName"] ?? "mgr1@test.p360o.com";
        public static string CloudManagerPassword =>
            TestContext.Parameters["CloudManagerPassword"] ?? "Eaple2021";
        public static string CloudBoardHandlerUserName =>
            TestContext.Parameters["CloudBoardHandlerUserName"] ?? "bs1@test.p360o.com";
        public static string CloudBoardHandlerPassword =>
            TestContext.Parameters["CloudBoardHandlerPassword"] ?? "Eaple2021";
        public static string BaseUrl_NOR =>
            TestContext.Parameters["BaseUrl_NOR"] ?? "https://mas-aut-publicnorlrg.dev.360online.com/";
        public static string BaseUrl_FIN =>
            TestContext.Parameters["BaseUrl_FIN"] ?? "https://mas-aut-publicfin.dev.360online.com/";
        public static string BaseUrl_DAN =>
            TestContext.Parameters["BaseUrl_DAN"] ?? "https://mas-aut-publicdan.dev.360online.com/";
        public static string BaseUrl_FFUI =>
            TestContext.Parameters["BaseUrl_FFUI"] ?? "https://mas-aut-publicnorlrg1.dev.360online.com/";

        //public static string GetEnvUrl(string envKey)
        //{
        //    // Returns the URL for the given environment key, or falls back to BaseUrl if not found
        //    return TestContext.Parameters[envKey] ?? TestContext.Parameters["BaseUrl"];
        //}

    }

}
