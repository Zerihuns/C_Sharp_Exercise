namespace Calc
{

    class Program
    {
        static void Main()
        {
            ///

            MyGenericClass<int> integerGenericClass = new MyGenericClass<int>(10);
            int val = integerGenericClass.GenericMethod(200);
            Console.WriteLine(val);

            ///

            MyGenericClass<string> stringGenericClass = new MyGenericClass<string>("Hello Generic World");
            stringGenericClass.GenericProperty = "This is a generic property example.";
            string result = stringGenericClass.GenericMethod("Generic Parameter");
            Console.WriteLine(result);

            ///

            Console.WriteLine("Generics Class Example in C#");
            //Instantiate GenericClass, string is the type argument
            GenericClass<string> myGenericClass = new GenericClass<string>
            {
                Message = "Welcome to DotNetTutorials"
            };
            myGenericClass.GenericMethod("Pranata Rout", "Bhubaneswar");


            ///

            Console.WriteLine("Generic Method Example in C#");
            SomeClass s = new SomeClass();
            //While calling the method we need to specify the type for both T1 and T2
            s.GenericMethod<int, int>(10, 20);
            s.GenericMethod<double, string>(10.5, "Hello");
            s.GenericMethod<string, float>("Anurag", 20.5f);

            ///

            var obj1 = new GenericClass<int, string>(100, "One Hundred");
            Console.WriteLine($"{obj1.Param1} : {obj1.Param2}");
            var obj2 = new GenericClass<string, string>("Dot Net Tutorails", "Welcome to C#.NET");
            Console.WriteLine($"{obj2.Param1} : {obj2.Param2}");
            var obj3 = new GenericClass<int, int>(100, 200);
            Console.WriteLine($"{obj3.Param1} : {obj3.Param2}");

            ////

            bool IsEqual = ClsCalculator.AreEqual<double>(10.5, 20.5);
           
            var results = IsEqual ? "Both are Equal" : "Both are Not Equal";
            Console.WriteLine(results);

            Console.ReadKey();

        }
    }

    public class ClsCalculator
    {
        public static bool AreEqual<T>(T value1, T value2)
        {
            return value1.Equals(value2);
        }
    }

    public class GenericClass<T1, T2>
    {
        public GenericClass(T1 Parameter1, T2 Parameter2)
        {
            Param1 = Parameter1;
            Param2 = Parameter2;
        }
        public T1 Param1 { get; }
        public T2 Param2 { get; }
    }


    public class SomeClass
    {
        //Making the Method as Generic by using the <T1, T2> 
        //Then using T1 and T2 as parameters of the method
        public void GenericMethod<T1, T2>(T1 Param1, T2 Param2)
        {
            Console.WriteLine($"Parameter T1 type: {typeof(T1)}: Parameter T2 type: {typeof(T2)}");
            Console.WriteLine($"Parameter 1: {Param1} : Parameter 2: {Param2}");
        }
    }

    public class GenericClass<T>
    {
        public T Message;
        public void GenericMethod(T Name, T Location)
        {
            Console.WriteLine($"Message: {Message}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Location: {Location}");
        }
    }

    //MyGenericClass is a Generic Class
    //Here T specifies the Data Types of the class Members
    class MyGenericClass<T>
    {
        //Generic variable
       
        private T GenericMemberVariable;
        //Generic Constructor
              public MyGenericClass(T value)
        {
            GenericMemberVariable = value;
        }
        //Generic Method
   
        public T GenericMethod(T GenericParameter)
        {
            Console.WriteLine($"Parameter type: {typeof(T).ToString()}, Value: {GenericParameter}");
            Console.WriteLine($"Return type: {typeof(T).ToString()}, Value: {GenericMemberVariable}");
            return GenericMemberVariable;
        }
        //Generic Property
     
        public T GenericProperty { get; set; }
    }
}