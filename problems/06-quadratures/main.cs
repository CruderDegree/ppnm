using System;
using static System.Console;
using static System.Math;
using static Quadratures;

class main{
	static void Main(){
		WriteLine("--- Part A ---");
		Func<double, double> f = (x) => Sqrt(x);
		double d = 1.0/100000, e = 1.0/100000;
		double res = recAdaptIntegrator(f, 0, 1, d, e);
		WriteLine("int from 0 to 1 of sqrt(x) dx:");
		WriteLine("Calculation: \t\t Actual:");
		WriteLine($"{res} \t {2.0/3} \n");

		f = (x) => 4*Sqrt(1-x*x);
		res = recAdaptIntegrator(f, 0, 1, d, e);
		WriteLine("int from 0 to 1 of 4*Sqrt(1-x*x) dx:");
		WriteLine("Calculation: \t\t Actual:");
		WriteLine($"{res} \t {PI} \n");
	}	
}
