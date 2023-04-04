using System;

namespace GammelObligatoriskOppgave.Test
{
    internal class FamilyAppTest
    {
        [Test]
        public void TestWrongCommand()
        {
            var sverreMagnus = new Person { Id = 1, FirstName = "Sverre Magnus", BirthYear = 2005 };

            var app = new FamilyApp(sverreMagnus);
            var actualResponse = app.HandleCommand("blah");
            var expectedResponse = "Ukjent kommando";
            Assert.AreEqual(expectedResponse, actualResponse);
        }
        [Test]
        public void TestShowFamilyWithChildren()
        {
            var sverreMagnus = new Person { Id = 1, FirstName = "Sverre Magnus", BirthYear = 2005 };
            var ingridAlexandra = new Person { Id = 2, FirstName = "Ingrid Alexandra", BirthYear = 2004 };
            var haakon = new Person { Id = 3, FirstName = "Haakon Magnus", BirthYear = 1973 };
            var harald = new Person { Id = 6, FirstName = "Harald", BirthYear = 1937 };
            sverreMagnus.Father = haakon;
            ingridAlexandra.Father = haakon;
            haakon.Father = harald;

            var app = new FamilyApp(sverreMagnus, ingridAlexandra, haakon);
            var actualResponse = app.HandleCommand("vis 3");
            var expectedResponse = "Haakon Magnus (Id=3) Født: 1973 Far: Harald (Id=6)\n"
                + " Barn:\n"
                + " Sverre Magnus (Id=1) Født: 2005\n"
                + " Ingrid Alexandra (Id=2) Født: 2004\n";
            Assert.AreEqual(expectedResponse, actualResponse);
        }
        [Test]
        public void TestShowFamilyWithNoChildren()
        {
            var sverreMagnus = new Person { Id = 1, FirstName = "Sverre Magnus", BirthYear = 2005 };
            var ingridAlexandra = new Person { Id = 2, FirstName = "Ingrid Alexandra", BirthYear = 2004 };
            var haakon = new Person { Id = 3, FirstName = "Haakon Magnus", BirthYear = 1973 };
            sverreMagnus.Father = haakon;

            var app = new FamilyApp(sverreMagnus, ingridAlexandra, haakon);
            var actualResponse = app.HandleCommand("vis 1");
            var expectedResponse = "Sverre Magnus (Id=1) Født: 2005 Far: Haakon Magnus (Id=3)";

            Assert.AreEqual(expectedResponse, actualResponse);
        }
        [Test]
        public void TestNoIdFound()
        {
            var sverreMagnus = new Person { Id = 1, FirstName = "Sverre Magnus", BirthYear = 2005 };

            var app = new FamilyApp(sverreMagnus);
            var actualResponse = app.HandleCommand("vis 2");
            var expectedResponse = "Personen finnes ikke";

            Assert.AreEqual(expectedResponse, actualResponse);
        }
        [Test]
        public void TestShowHelpOption()
        {
            var sverreMagnus = new Person { Id = 1, FirstName = "Sverre Magnus", BirthYear = 2005 };

            var app = new FamilyApp(sverreMagnus);
            var actualResponse = app.HandleCommand("hjelp");
            var expectedResponse = "hjelp => viser en hjelpetekst som forklarer alle kommandoene\n" +
                "liste => lister alle personer med id, fornavn, fødselsår, dødsår og navn og id på mor og far om det finnes registrert.\n" +
                "vis<id> => viser en bestemt person med mor, far og barn(og id for disse, slik at man lett kan vise en av dem)";

            Assert.AreEqual(expectedResponse, actualResponse);
        }
        [Test]
        public void TestShowAllPeople()
        {
            var sverreMagnus = new Person { Id = 1, FirstName = "Sverre Magnus", BirthYear = 2005 };
            var ingridAlexandra = new Person { Id = 2, FirstName = "Ingrid Alexandra", BirthYear = 2004 };
            var haakon = new Person { Id = 3, FirstName = "Haakon Magnus", BirthYear = 1973 };
            var metteMarit = new Person { Id = 4, FirstName = "Mette-Marit", BirthYear = 1973 };
            var marius = new Person { Id = 5, FirstName = "Marius", LastName = "Borg Høiby", BirthYear = 1997 };
            var harald = new Person { Id = 6, FirstName = "Harald", BirthYear = 1937 };
            var sonja = new Person { Id = 7, FirstName = "Sonja", BirthYear = 1937 };
            var olav = new Person { Id = 8, FirstName = "Olav", BirthYear = 1903 };

            sverreMagnus.Father = haakon;
            sverreMagnus.Mother = metteMarit;
            ingridAlexandra.Father = haakon;
            ingridAlexandra.Mother = metteMarit;
            marius.Mother = metteMarit;
            haakon.Father = harald;
            haakon.Mother = sonja;
            harald.Father = olav;

            var app = new FamilyApp(sverreMagnus, ingridAlexandra, haakon,
                metteMarit, marius, harald, sonja, olav);

            var actualResponse = app.HandleCommand("liste");
            var expectedResponse =
                "Sverre Magnus (Id=1) Født: 2005 Far: Haakon Magnus (Id=3) Mor: Mette-Marit (Id=4)\n" +
                "Ingrid Alexandra (Id=2) Født: 2004 Far: Haakon Magnus (Id=3) Mor: Mette-Marit (Id=4)\n" +
                "Haakon Magnus (Id=3) Født: 1973 Far: Harald (Id=6) Mor: Sonja (Id=7)\n" +
                "Mette-Marit (Id=4) Født: 1973\n" +
                "Marius Borg Høiby (Id=5) Født: 1997 Mor: Mette-Marit (Id=4)\n" +
                "Harald (Id=6) Født: 1937 Far: Olav (Id=8)\n" +
                "Sonja (Id=7) Født: 1937\n" +
                "Olav (Id=8) Født: 1903\n";
            Assert.AreEqual(expectedResponse, actualResponse);
        }
    }
}
