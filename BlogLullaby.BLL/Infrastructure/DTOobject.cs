using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlogLullaby.BLL.Infrastructure
{
    public abstract class DTOobject
    {
        public virtual OperationDetails GetValidateError()
        {
            var results = new List<ValidationResult>();
            results.Add(new ValidationResult("Objet not valid."));
            var context = new ValidationContext(this);
            if (!Validator.TryValidateObject(this, context, results, true))
            {
                var errorStrings = results.Select(x => x.ErrorMessage);
                return new OperationDetails(false, errorStrings);
            }
            return new OperationDetails(true);
        }

        public virtual bool IsValid()
        {
            if (!Validator.TryValidateObject(this, new ValidationContext(this), new List<ValidationResult>(), true))
            {
                return false;
            }
            return true;
        }
    }
}
