using System;
using static System.Math;

public class Quadratures{
	
	public static double recAdaptIntegrator(Func<double,double> f, double a, double b, double d, double e){
		// Calculates points to be reused in integral
		double f2 = f(a + 2*(b-a)/6), f3 = f(a + 4*(b-a)/6);
		return actualRecAdaptIntegrator(f, a, b, d, e, f2, f3);
	}

	private static double actualRecAdaptIntegrator(Func<double, double> f, double a,
			double b, double d, double e, double f2, double f3){
		// Recursive, adaptive integrator method
		// This method reuses 2 of previous found points
		double f1 = f(a+(b-a)/6), f4 = f(a + 5*(b-a)/6);
		double Q = (2*f1 + f2 + f3 + 2*f4) / 6 * (b-a);
		double q = (f1 + f2 + f3 + f4) / 4 * (b-a);
		double err = Abs(Q-q);
		if(err < d + e*Abs(Q)) return Q;
		else return actualRecAdaptIntegrator(f, a, (a+b)/2, d/Sqrt(2), e, f1, f2) +
			actualRecAdaptIntegrator(f, (a+b)/2, b, d/Sqrt(2), e, f3, f4);
	}
}
