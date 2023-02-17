using Xunit;
using ClassMaker;
using System;
using System.Collections.Generic;

namespace ClassMaler_Test
{
    public class CreateType
    {
        public class TableType
        {
           
            public string _Name
            {
                get { return Name; }
                set { Name = value; }
            }
            private string Name;
            

            public string _Surname
            {
                get { return Surname; }
                set { Surname = value; }
            }
            private string Surname;
            
            public string _Aged
            {
                get { return Aged; }
                set { Aged = value; }
            }
            private string Aged;

            public string _Profession
            {
                get { return Profession; }
                set { Profession = value; }
            }
            private string Profession;

            public TableType(string name, string surname, string aged, string profession)
            {
                Name = name;
                Surname = surname;
                Aged = aged;
                Profession = profession;
            }
        }
        public static IEnumerable<object[]> ClassData()
        {
            yield return new object[]
            {
            new string[] { "Name", "Surname", "Aged", "Profession" },
            new string[] { "Jasiu", "Kowalski", "18", "Farmer" },
            new TableType("Jasiu", "Kowalski", "18", "Farmer"),   
            };
        }


        [Theory]
        [MemberData(nameof(ClassData))]
        public void CreateTypeDynamic(string[] vs,string[] instances, TableType expected)
        {
            
            ManagerClassMaker managerClass = new ManagerClassMaker();
            Type type = managerClass.CreateTypeFromTable(vs);
            var  Jasiu = managerClass.AddDatatoFields(type, instances);
            Assert.Equal(expected, Jasiu);
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