using System;
using System.ComponentModel.DataAnnotations;

namespace Learning.Mvc.Models
{
    public class ErrorViewModel
    {
        [MaxLength(150)]
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
