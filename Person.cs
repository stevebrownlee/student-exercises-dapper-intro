namespace nss
{
    public class Person
    {
        protected string _firstName;
        protected string _lastName;

        public string Name
        {
            get => $"{_firstName} {_lastName}";
        }
    }
}