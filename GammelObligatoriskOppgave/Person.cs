using System;

namespace GammelObligatoriskOppgave
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
        public int DeathYear { get; set; }
        public Person Father { get; set; }
        public Person Mother { get; set; }

        public string GetDescription()
        {
            var descList = new List<string>();
            
            if (FirstName != null) descList.Add($"{FirstName}");
            if (LastName != null) descList.Add($"{LastName}");
            if (Id != 0) descList.Add($"(Id={Id})");
            if (BirthYear != 0) descList.Add($"Født: {BirthYear}");
            if (DeathYear != 0) descList.Add($"Død: {DeathYear}");
            if (Father != null) descList.Add($"Far: {Father.FirstName} (Id={Father.Id})");
            if (Mother != null) descList.Add($"Mor: {Mother.FirstName} (Id={Mother.Id})");

            var description = string.Join(" ", descList);

            return description;
        }
    }
}
