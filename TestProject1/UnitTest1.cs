using FunTranslator.Business;
using FunTranslator.Data;
using FunTranslator.Data.Models;
using FunTranslator.Models;
using Microsoft.EntityFrameworkCore;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestApi()
        {
            var funTranslatorApi = new FunTranslatorApi();
            var result = funTranslatorApi.Translate("I'd like a pint of ale", "https://api.funtranslations.com/translate/", "mandalorian");

            Assert.Equal("OK", result!.StatusCode.ToString());
        }

        [Fact]
        public void TestDb()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "FunTranslator")
            .Options;

            var context = new ApplicationDbContext(dbContextOptions);

            context.TranslateLogs!.AddRange(GetFakeLogList());
            context.SaveChanges();

            var dataService = new DataService(context);
            TranslateSearchModel translateSearchModel = new TranslateSearchModel();
            translateSearchModel.Text = "Request";
            translateSearchModel.StartDate = DateTime.Today.AddMonths(-3);
            translateSearchModel.EndDate = DateTime.Today;
            var list = dataService.SelectLogs(translateSearchModel);

            Assert.Equal(2, list.Count);
        }

        public static List<TranslateLog> GetFakeLogList()
        {
            return new List<TranslateLog>()
    {
        new TranslateLog
        {
            Id = Guid.NewGuid(),
            LogDate= DateTime.Now,
            RequestData="RequestData1",
            ResponseData="ResponseData1",
            ReturnCode="OK",
            ReturnMessage="OK",
            TranslationType = "yoda"
        },
        new TranslateLog
        {
            Id = Guid.NewGuid(),
            LogDate= DateTime.Now,
            RequestData="RequestData2",
            ResponseData=null,
            ReturnCode="NOK",
            ReturnMessage="Error",
            TranslationType = "yoda"
        }
    };
        }
    }
}