using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_Ex
{
    static class Where
    {
        static List<Farmers> farmerList;
        static Where()
        {
            farmerList = new List<Farmers> {
                new Farmers {
                    ID = 1, Name = "Sekar L", Location = "Krishnagiri", Income = 21000
                },
                new Farmers {
                    ID = 2, Name = "Mohan S", Location = "Krishnagiri", Income = 40000
                },
                new Farmers {
                    ID = 3, Name = "Sathis S", Location = "Krishnagiri", Income = 18000
                },
                new Farmers {
                    ID = 4, Name = "Subash S", Location = "Ooty", Income = 25000
                },
                new Farmers {
                    ID = 5, Name = "Robert B", Location = "Banglore", Income = 28000
                },
            };
        }

        static public void ExecuteWithQuery()
        {
            #region Linq Deffered Query  
            var result = from farmer in farmerList
                         where farmer.Income > 25000
                         where farmer.Income < 40000
                         select farmer;
            #endregion
            #region Linq Immediate Query Result  
            foreach (var farmer in result)
            {
                Console.WriteLine("ID : " + farmer.ID + " Name : " + farmer.Name + "Income : " + farmer.Income);
            }
            #endregion
        }
        static public void ExecuteWithLambda()
        {
            #region Lambda deffered query  
            var result1 = farmerList.Where(a => a.Income > 25000).Where(a => a.Income < 40000);
            #endregion

            #region Lambda Immediate Query Result  
            foreach (var farmer in result1)
            {
                Console.WriteLine("ID : " + farmer.ID + " Name : " + farmer.Name + "Income : " + farmer.Income);
            }
            #endregion
        }
        #region fearmer_Class
        public class Farmers
        {
            public int ID
            {
                get;
                set;
            }
            public string Name
            {
                get;
                set;
            }
            public string Location
            {
                get;
                set;
            }
            public double Income
            {
                get;
                set;
            }
        }
        #endregion
    }
}
