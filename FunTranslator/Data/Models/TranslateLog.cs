using System.ComponentModel.DataAnnotations;

namespace FunTranslator.Data.Models
{
    public class TranslateLog
    {
        [Key]
        public Guid Id { get; set; }
        public string? RequestData { get; set; }
        public string? ResponseData { get; set; }
        public string? ReturnCode { get; set; }
        public string? ReturnMessage { get; set; }
        public string? TranslationType { get; set; }
        public DateTime LogDate { get; set; }
    }

    public class TranslateLogDto
    {
        public string? RequestData { get; set; }
        public string? ResponseData { get; set; }
        public string? ReturnCode { get; set; }
        public string? ReturnMessage { get; set; }
        public string? TranslationType { get; set; }
    }
}