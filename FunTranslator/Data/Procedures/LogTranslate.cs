using static Humanizer.On;
using System.Runtime.Intrinsics.X86;

namespace FunTranslator.Data.Procedures
{
    public static class LogTranslate
    {
        public static string CreateScript()
        {
            var sql = @"             
            CREATE PROCEDURE LogTranslate
    @RequestData nvarchar(max),   
    @ResponseData nvarchar(max),
	@ReturnCode nvarchar(max),
	@ReturnMessage nvarchar(max),
    @TranslationType nvarchar(max)
AS
    insert into dbo.TranslateLogs
    (
        Id,
        RequestData,
        ResponseData,
        ReturnCode,
        ReturnMessage,
        TranslationType,
        LogDate
    )

    values
    (
        NEWID(),
        @RequestData,
        @ResponseData,
        @ReturnCode,
        @ReturnMessage,
        @TranslationType,
        GETDATE()
    )
GO
  ";
            return sql;
        }

        public static string DropScript()
        {
            var sql = "DROP PROCEDURE LogTranslate;";
            return sql;
        }
    }
}
