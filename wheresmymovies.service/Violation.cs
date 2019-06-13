namespace wheresmymovies.service.Validator
{
    public class Violation
    {
        private readonly string _propertyName;
        private readonly object _propertyValue;
        private readonly string _violation;

        public Violation(string propertyName, object propertyValue, string violation)
        {
            _propertyName = propertyName;
            _propertyValue = propertyValue;
            _violation = violation;
        }
    }
}