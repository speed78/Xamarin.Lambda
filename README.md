# #Xamarin Lambda Parser

This is a lib used  in Xamarin to parse code at runtime. The program language is C#.
Nuget https://www.nuget.org/packages/Xamarin.Lambda/1.1.0

#####Example1 - string concat and number calculate:
```C#
string code = "2.ToString()+(4*2)"; // C# code
Func<string> func = ExpressionParser.Compile<Func<string>>(code); // compile code
string result = func(); // result = "28"
```

#####Example2 - input parameter:
```C#
Delegate dele = ExpressionParser.Compile("(int m)=>-m");
var result = (int)dele.DynamicInvoke(10); // result = -10
```

#####Example3 - access property in anonymous type:
```C#

object obj = new { Name = "Salvatore", Id = 18 }; // maybe get [obj] from method return value
int result = obj.E<int>("Id"); // result = 18
```

This Project is Inspire by Zuchai project:  https://github.com/zhucai/lambda-parser/blob/master/lambda-parser/Test_Zhucai.LambdaParser/ExpressionParserTest.cs


