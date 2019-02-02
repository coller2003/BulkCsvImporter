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

        public bool Success
        {
            get
            {
                return _errors.Count <= 0;
            }

        }

        public string AllMessages
        {
            get
            {
                return string.Join(",", _errors);
            }
        }

        public void AddError(string error)
        {
            this._errors.Add(error);
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
