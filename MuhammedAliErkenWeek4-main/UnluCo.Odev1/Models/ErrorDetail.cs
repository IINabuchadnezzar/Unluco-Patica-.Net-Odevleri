using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.Week1.Models
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

        public override bool Equals(object obj)
        {
            ErrorDetail error = obj as ErrorDetail;
            return StatusCode == error.StatusCode && Message == error.Message;
        }

        public override int GetHashCode()
        {
            return $"{StatusCode}{Message}".GetHashCode();
        }
    }
}
