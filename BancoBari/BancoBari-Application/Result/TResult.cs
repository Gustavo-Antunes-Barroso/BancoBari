using System.Collections.Generic;

namespace BancoBari_Application.Result
{
    public class TResult
    {
        public TResult()
        {
            Errors = new List<string>();
        }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public object Object { get; set; }
    }
}
