using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoPoco;
using AddressBook;
using AutoPoco.Engine;
using AutoPoco.DataSources;
using System.Collections.Generic;

namespace AddressBookTestMsTest
{
    [TestClass]
    public class PersonTest
    {
        IGenerationSessionFactory factory;
        IList<Person> people;

        [TestInitialize]
        public void TestInitialize()
        {
            factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => c.UseDefaultConventions());
                x.AddFromAssemblyContainingType<Person>();
                x.Include<Person>()
                    .Setup(c => c.FirstName).Use<FirstNameSource>()
                    .Setup(c => c.LastName).Use<LastNameSource>();
            });
        }

        [TestMethod]
        public void Generate100InstancesOfPerson()
        {
            IGenerationSession session = factory.CreateSession();

            people = session.List<Person>(100).Get();

            Assert.IsTrue(people.Count == 100);

            foreach (var p in people)
            {
                Console.WriteLine("{0} {1}", p.FirstName, p.LastName);
            }
        }

        [TestMethod]
        public void Generate100InstancesOfPersonWithImpose()
        {
            IGenerationSession session = factory.CreateSession();

            people = session.List<Person>(100)
                .First(50)
                    .Impose(x => x.FirstName, "Bob")
                .Next(50)
                    .Impose(x => x.FirstName, "Alice")
                .All()
                    .Impose(x => x.LastName, "Hanselman")
                .Random(25)
                    .Impose(x => x.LastName, "Blue")
                .All().Random(40)
                    .Impose(x => x.LastName, "Red")
                .All()
                .Get();

            Assert.IsTrue(people.Count == 100);

            foreach(var p in people)
            {
                Console.WriteLine("{0} {1}", p.FirstName, p.LastName);
            }
        }
    }
}
