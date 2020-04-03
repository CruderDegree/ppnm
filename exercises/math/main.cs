using static System.Console;
using static System.Math;
using static cmath;

class main{
	static void Main(){
		System.Console.WriteLine("EEEEEEEEEEE = {0}",System.Math.E);
		WriteLine("sqrt(2) = {0}",sqrt(2));
		complex I = new complex(0,1);
		WriteLine("e to the power i = {0}", exp(I));
		WriteLine("e to the power pi i = {0}", exp(I*PI));
		WriteLine("i to the power i = {0}", I.pow(I));
		WriteLine($"sin(i*pi) = {sin(I*PI)}");		
	}
}
