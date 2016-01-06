using System.Collections.Generic;

namespace LuckyDraw.BusinessModel
{
    public class ExecuteResult
    {
        public ExecuteResult()
        {
            Error = new List<string>();
        }

        public List<string> Error { get; set; }

        public bool Success
        {
            get { return Error.Count == 0; }
        }
    }
}