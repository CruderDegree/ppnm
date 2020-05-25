using static System.Console;
using static System.Math;
class readcmdline{
	static void Main(string[] args){
		WriteLine("from read-cmdline");
		WriteLine("x, sin(x), cos(x)");
		foreach (string s in args){
			int x = int.Parse(s);
			WriteLine($"{x}, {Sin(x)}, {Cos(x)}");
		}
	}
}
