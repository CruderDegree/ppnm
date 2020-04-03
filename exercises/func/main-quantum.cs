using static System.Console;
using static System.Math;
using System;

class quantum{
	static int Main(){
		// Define denominator function
		Func<double, Func<double, double> > den = (a) => { return (x) => Exp(-a*x*x); };
		// Define numerator function
		Func<double, Func<double, double> > num = (a) => 
			{return (x) => (-a*a*x*x/2 + a/2 + x*x/2)*Exp(-a*x*x); };
		
		// Calculate E[a]
		double minf = Double.NegativeInfinity, pinf = Double.PositiveInfinity, result;	
		for(double i=0.1; i<3; i+=0.1){	
			result = quad.o8av(num(i),minf,pinf)/quad.o8av(den(i), minf, pinf);
			WriteLine($"{i}, {result}");
		}
		return 0;
	}
}
		
