using FunTranslator.Data.Models;
using FunTranslator.Models;

namespace FunTranslator.Business
{
    public abstract class DataServiceBase : IDataService
    {
        public abstract void LogTranslate(TranslateLogDto translateLogDto);

        public abstract List<TranslateLog> SelectLogs(TranslateSearchModel translateSearchModel);
       
    }
}
