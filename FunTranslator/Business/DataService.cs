using FunTranslator.Controllers;
using FunTranslator.Data;
using FunTranslator.Data.Models;
using FunTranslator.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FunTranslator.Business
{
    public class DataService : DataServiceBase
    {
        private readonly ApplicationDbContext _context;

        public DataService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public override void LogTranslate(TranslateLogDto translateLogDto)
        {
            _context.Database.ExecuteSqlRaw("LogTranslate " +
                "@p0, " +
                "@p1, " +
                "@p2, " +
                "@p3, " +
                "@p4",
                parameters: new[]
                {
                    translateLogDto.RequestData!,
                    translateLogDto.ResponseData!,
                    translateLogDto.ReturnCode!,
                    translateLogDto.ReturnMessage!,
                    translateLogDto.TranslationType!
                });
        }

        public override List<TranslateLog> SelectLogs(TranslateSearchModel translateSearchModel)
        {
            return _context!.TranslateLogs!
                .Where(t => (string.IsNullOrWhiteSpace(translateSearchModel.Text) || t.RequestData!.Contains(translateSearchModel.Text) 
                && t.LogDate.Date >= translateSearchModel.StartDate && t.LogDate.Date <= translateSearchModel.EndDate)
                ).ToList();
        }
    }
}