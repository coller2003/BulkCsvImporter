using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Model
{
    public class ActionResult<T>
    {
        private List<string> _errors = null;

        public ActionResult()
        {
            _errors = new List<string>();
        }

        public bool Success { get; set; }

        public string AllMessages
        {
            get
            {
                return string.Join(",", _errors);
            }
        }

        public T Data { get; set; }
    }

    public class ActionResult : ActionResult<bool>
    {
        public ActionResult() : base()
        {

        }
    }
}
