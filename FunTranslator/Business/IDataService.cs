using FunTranslator.Data.Models;
using FunTranslator.Models;

namespace FunTranslator.Business
{
    public interface IDataService
    {
        void LogTranslate(TranslateLogDto translateLogDto);
        List<TranslateLog> SelectLogs(TranslateSearchModel translateSearchModel);
    }
}