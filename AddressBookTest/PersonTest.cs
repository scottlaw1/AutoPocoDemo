using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco;
using AddressBook;
using AutoPoco.Engine;

namespace AddressBookTest
{
    [TestFixture]
    public class PersonTest
    {
        IGenerationSessionFactory factory;
        IList<Person> people;

        [SetUp]
        public void SetUp()
        {
            factory = AutoPocoContainer.Configure(x => 
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<Person>();
            });

            IGenerationSession session = factory.CreateSession();

            people = session.List<Person>(100).Get();
        }

        [Test]
        public void SetUpCreates100Users()
        {
            Assert.That(people.Count == 100);
        }
    }
}
