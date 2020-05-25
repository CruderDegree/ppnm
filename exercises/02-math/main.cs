using static System.Console;
using static System.Math;
using static cmath;

class main{
	static void Main(){
		WriteLine("Calculation of e = {0}",System.Math.E);
		WriteLine("Actual: 2.718281828459...");
		WriteLine("sqrt(2) = {0}",sqrt(2));
		WriteLine("Actual: 1.414213562373...");
		complex I = new complex(0,1);
		WriteLine("e^i = {0}", exp(I));
		WriteLine("Actual: 0.54030230.. + i 0.84147098..");
		WriteLine("exp(i*pi) = {0}", exp(I*PI));
		WriteLine("Actual: -1");
		WriteLine("i^i = {0}", I.pow(I));
		WriteLine("Actual: 0.20787957...");
		WriteLine($"sin(i*pi) = {sin(I*PI)}");		
		WriteLine("Actual: i*11.54873935...");
	}
}
