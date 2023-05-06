using FunTranslator.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FunTranslator.Models
{
    public class TranslateViewModel
    {
        [Required(ErrorMessage = "Text Required")]
        public string? text { get; set; }
    }
    public class TranslateListModel
    {
        public string? message { get; set; }
        public List<TranslateLog> Logs { get; set; } = new List<TranslateLog>();
    }

    public class TranslateSuccessResponse
    {
        public Success success { get; set; }
        public Contents contents { get; set; }
    }

    public class Success
    {
        public int total { get; set; }
    }

    public class Contents
    {
        public string translated { get; set; }
        public string text { get; set; }
        public string translation { get; set; }
    }

    public class TranslateErrorResponse
    {
        public Error error { get; set; }
    }

    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class TranslateSearchModel
    {
        public string? Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}