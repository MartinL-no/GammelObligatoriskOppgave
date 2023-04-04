using System;

namespace GammelObligatoriskOppgave
{
    public class FamilyApp
    {
        private readonly List<Person> _people;
        public string WelcomeMessage => "Welcome to the family tree app!\nChoose one of the following options: hjelp, liste or vis <id>";
        public string CommandPrompt => "Enter a command: ";
        public FamilyApp(params Person[] people)
        {
            _people = new List<Person>(people);
        }
        public string HandleCommand(string command)
        {
            if (command == "hjelp") return GetHelpInformation();
            else if (command == "liste") return GetAllPersonsDetails();
            else if (command.StartsWith("vis"))
            {
                var idString = command.Split(" ")[1];
                int id;
                
                if (int.TryParse(idString, out id) && _people.Any(p => p.Id == id))
                {
                    return GetPersonInformation(command);
                }
                return "Personen finnes ikke";
            }
            else return "Ukjent kommando";
        }
        private string GetPersonInformation(string command)
        {
            var requestedId = Int32.Parse(command.Split(" ")[1]);
            var familyMember = _people.FirstOrDefault(p => p.Id == requestedId);
            var description = familyMember.GetDescription();

            var children = _people.Where(p => p.Father == familyMember || p.Mother == familyMember).ToList();

            if (children.Count > 0)
            {
                description += "\n Barn:\n";
                foreach (var child in children)
                {
                    description += $" {child.GetDescription().Split(" Far")[0]}\n";
                }
            }
            return description;
        }
        private string GetAllPersonsDetails()
        {
            var allPersonsDetails = "";

            foreach (var person in _people)
            {
                allPersonsDetails += $"{person.GetDescription()}\n";
            }
            return allPersonsDetails;
        }
        private string GetHelpInformation()
        {
            return "hjelp => viser en hjelpetekst som forklarer alle kommandoene\n" +
                   "liste => lister alle personer med id, fornavn, fødselsår, dødsår og navn og id på mor og far om det finnes registrert.\n" +
                   "vis<id> => viser en bestemt person med mor, far og barn(og id for disse, slik at man lett kan vise en av dem)";
        }
    }
}