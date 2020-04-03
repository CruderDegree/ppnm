using System;
using System.IO;
class readStdIn{
	static int Main(){
		TextReader stdin = Console.In;
		TextWriter stdout = Console.Out;
		stdout.WriteLine("From read-stdin");
		stdout.WriteLine("x, sin(x), cos(x)");
		string s = "hack";
		do{
			s = stdin.ReadLine();
			if(s == null) break;
			string[] words = s.Split(' ');
			foreach (string word in words){
				double x = double.Parse(word);
				stdout.WriteLine($"{x}, {Math.Sin(x)}, {Math.Cos(x)}");
			}
		}while(s != null);
	return 0;
	}
}
