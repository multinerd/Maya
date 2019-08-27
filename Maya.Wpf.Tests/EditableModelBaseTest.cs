using Maya.Wpf.Tests.Model;
using Xbehave;
using Xunit;

namespace Maya.Wpf.Tests
{
    public static class EditableModelBaseTest
    {
        [Scenario]
        public static void CancelEdit(Person person)
        {
            "Given a person"
                .x(() => person = Person.Create());

            "Whose name is 'Sara'"
                .x(() => Assert.Equal("Sara", person.Name));

            "Will be changed to 'Mike', and then canceled"
                .x(() =>
                {
                    person.BeginEdit();
                    person.Name = "Mike";
                    person.CancelEdit();
                });

            "Name should still be 'Sara'"
                .x(() => Assert.Equal("Sara", person.Name));
        }

        [Scenario]
        public static void EndEdit(Person person)
        {
            "Given a person"
                .x(() => person = Person.Create());

            "Whose name is 'Sara'"
                .x(() => Assert.Equal("Sara", person.Name));

            "Will be changed to 'Mike', and then saved"
                .x(() =>
                {
                    person.BeginEdit();
                    person.Name = "Mike";
                    person.EndEdit();
                });

            "Name should now be 'Mike'"
                .x(() => Assert.Equal("Mike", person.Name));
        }
    }
}
