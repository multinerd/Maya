namespace Maya.Wpf.Tests.Model
{
    public class Person : EditableModelBase<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person() { }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }


        public static Person Create()
        {
            return new Person("Sara", 24);
        }
    }
}