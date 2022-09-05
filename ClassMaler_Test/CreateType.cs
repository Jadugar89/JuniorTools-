using Xunit;
using ClassMaker;
using System;
using System.Collections.Generic;

namespace ClassMaler_Test
{
    public class CreateType
    {
        [Fact]
        public void Test1()
        {
            string[] vs = new string[] { "Name", "Surname", "Aged", "Professiob" };
            string[] instances = new string[] { "Jasiu", "Kowalski", "18", "Farmer" };
            ManagerClassMaker managerClass = new ManagerClassMaker();
            Type type = managerClass.CreateTypeFromTable(vs);
            var  Jasiu = managerClass.AddDatatoFields(type, instances);
            var test =managerClass.CreateList(type);
            if(test != null && Jasiu !=null)
            managerClass.AddTolist(test, Jasiu);
            Assert.True(Jasiu!=null);
        }
        [Fact]
        public void AddElementToList()
        {
            List<string> vs = new List<string>();
            
            ManagerClassMaker managerClass = new ManagerClassMaker();

            managerClass.AddTolist(vs,"Jasiu");

            Assert.True(vs.Count != 0);
        }

    }
}