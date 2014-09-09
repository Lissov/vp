using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using LissovLog;
using System.Windows.Forms;
using System.Threading;

namespace Tests
{
    /// <summary>
    /// Summary description for GeneralLissovModelsTest
    /// </summary>
    [TestClass]
    public class GeneralLissovModelsTest
    {
        public GeneralLissovModelsTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        AutoResetEvent calculationsFinished = new AutoResetEvent(false);
        [TestMethod]
        public void BasicConfigTest()
        {
            Log.TestMode = true;

            string dirname = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));

            Client.MainForm mform = new Client.MainForm();

            try
            {
                mform.LoadMainForm();

                mform.LoadConfuguration(Path.Combine(dirname, @"Test\Tests\TestData\Basic.config"));
                mform.ExperimentTime = 10;
                Assert.IsNotNull(mform.CurrentConfig, "Configuration not loaded");

                mform.RunExperiment();

                ThreadPool.QueueUserWorkItem(new WaitCallback(TimeoutCheck));
                ThreadPool.QueueUserWorkItem(new WaitCallback(FinishedCheck), mform);

                calculationsFinished.WaitOne();

                Assert.AreEqual(mform.CurrentConfig.GetCalculatingState(), ModelBase.Enums.CalculatingStates.Finished, "Calculation not finished (possible timeout)");
                Assert.AreEqual(mform.CurrentConfig.Models[0].GetCurrentTime(), mform.ExperimentTime + mform.CurrentConfig.Models[0].Step, 0.1, "Calculation finished but Experiment time not calculated");
                Assert.AreEqual(mform.CurrentConfig.Models[0].GetName(), "CVSModel", "First model is not CVS");
                Assert.AreEqual(mform.CurrentConfig.Models[0].GetValueByNameAndTime("PressureArchOfAorta", mform.ExperimentTime), 90, 0.02, "Pressure in ArchOfAorta is not equal to expected 90");
            }
            finally
            {
                mform.Close();
            }
        }

        void TimeoutCheck(object state)
        {
            Thread.Sleep(20000);
            calculationsFinished.Set();
        }

        void FinishedCheck(object state)
        {
            try
            {
                Client.MainForm mform = state as Client.MainForm;
                while (mform.CurrentConfig.GetCalculatingState() == ModelBase.Enums.CalculatingStates.InProcess)
                    Thread.Sleep(100);
            }
            catch (Exception) { }
            calculationsFinished.Set();
        }
    }
}
