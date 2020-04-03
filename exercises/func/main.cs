using System;
using static System.Console;
using static System.Math;

class main{
	static int Main(){
		// f is ln(x)/sqrt(x)
		Func<double,double> f = (x) => Log(x)/Sqrt(x);
	       	double a=0,b=1, result;
		result = quad.o8av(f, a, b);
		WriteLine("int 0 to 1 of log(x)/sqrt(x) calculation and tabulated value");
		WriteLine($"Calc: {result, 10:f5}");
		WriteLine($"Tabl: {-4.0, 10:f5}");		
		
		// g is exp(-x**2)
		Func<double,double> g = (x) => Exp(-x*x);
	       	a= Double.NegativeInfinity; 
		b=Double.PositiveInfinity;
		result = quad.o8av(g, a, b);
		WriteLine("int from -inf to +inf of exp(-x**2): calculation and tabulated value");
		WriteLine($"Calc: {result, 10:f5}");
		WriteLine($"Tabl: {Sqrt(PI), 10:f5}");		
		
		// h is ln(1/x)**p
		// p = 2
		Func<double,double> h = (x) => Pow(Log(1/x),2);
	       	a= 0; 
		b= 1;
		result = quad.o8av(h, a, b);
		WriteLine("int from 0 to 1 of ln(1/x)**2: calculation and tabulated value");
		WriteLine($"Calc: {result, 10:f5}");
		WriteLine($"Tabl: {2.0, 10:f5}");		
		
		// h is ln(1/x)**p
		// p = 4
		h = (x) => Pow(Log(1/x),4);
	       	a= 0; 
		b= 1;
		result = quad.o8av(h, a, b);
		WriteLine("int from 0 to 1 of ln(1/x)**4: calculation and tabulated value");
		WriteLine($"Calc: {result, 10:f5}");
		WriteLine($"Tabl: {24.0, 10:f5}");		

		
		// h is ln(1/x)**p
		// p = 8
		h = (x) => Pow(Log(1/x),8);
	       	a= 0; 
		b= 1;
		result = quad.o8av(h, a, b, acc:1e-8, eps:1e-8);
		WriteLine("int from 0 to 1 of ln(1/x)**8: calculation and tabulated value");
		WriteLine($"Calc: {result, 10:f5}");
		WriteLine($"Tabl: {40320.0, 10:f5}");		
		
		WriteLine("Definite Integrals");
		

		// f is now sqrt(x) *exp(-x)
		f = (x) => Sqrt(x)*Exp(-x);
	       	a= 0; 
		b= Double.PositiveInfinity;
		result = quad.o8av(f, a, b);
		WriteLine("int from 0 to +inf of sqrt(x)*exp(-x): calculation and tabulated value");
		WriteLine($"Calc: {result, 10:f5}");
		WriteLine($"Tabl: {Sqrt(PI)/2.0, 10:f5}");		

		// g is now x/(exp(x)-1)
		g = (x) => x/(Exp(x)-1);
	       	a= 0; 
		b= Double.PositiveInfinity;
		result = quad.o8av(g, a, b);
		WriteLine("int from 0 to +inf of x/(e**x - 1): calculation and tabulated value");
		WriteLine($"Calc: {result, 10:f5}");
		WriteLine($"Tabl: {PI*PI/6.0, 10:f5}");		
		
		// h is now sin(x)/x
		h = (x) => Sin(x)/x;
	       	a= 0; 
		b= Double.PositiveInfinity;
		result = quad.o8av(h, a, b);
		WriteLine("int from 0 to +inf of sinc(x): calculation and tabulated value");
		WriteLine($"Calc: {result, 10:f5}");
		WriteLine($"Tabl: {PI/2.0, 10:f5}");		
		return 0;
	}
}
