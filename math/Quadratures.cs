using System;
using static System.Math;

public class Quadratures{
	
	public static (double, double) recAdaptIntegrator(Func<double,double> f, double a, double b, double d, double e){
		// Calculates points to be reused in integral
		// f: Function to be integrated
		// a and b: Lower and upper limit
		// d absolute uncertainty
		// e relative uncertainty
		double f2 = f(a + 2*(b-a)/6), f3 = f(a + 4*(b-a)/6);
		return actualRecAdaptIntegrator(f, a, b, d, e, f2, f3);
	}

	private static (double, double) actualRecAdaptIntegrator(Func<double, double> f, double a,
			double b, double d, double e, double f2, double f3){
		// Recursive, adaptive integrator method using an open set of x values
		// This method reuses 2 of previous found points
		double f1 = f(a+(b-a)/6), f4 = f(a + 5*(b-a)/6);
		double Q = (2*f1 + f2 + f3 + 2*f4) / 6 * (b-a);
		double q = (f1 + f2 + f3 + f4) / 4 * (b-a);
		double err = Abs(Q-q);
		if(err < d + e*Abs(Q)) return (Q, err);
		else{
			double res1, res2, err1, err2;
			(res1, err1) = actualRecAdaptIntegrator(f, a, (a+b)/2, d/Sqrt(2), e, f1, f2);
			(res2, err2) = actualRecAdaptIntegrator(f, (a+b)/2, b, d/Sqrt(2), e, f3, f4);
			return (res1 + res2, err1+err2);
		}
	}

	public static (double, double) rescaledCCIntegrator(Func<double, double> f, double a, double b, double d, double e){
		// Rescale integral limits to make
		// Clenshaw-Curtis transformation and then integrate
		Func<double, double> ft = (theta) => f((a+b)/2 + (b-a)/2 * Cos(theta)) * Sin(theta) * (b-a)/2;
		return recAdaptIntegrator(ft, 0, PI, d, e);
	}

	public static (double, double) integrator(Func<double,double> f, double a, double b, double delta, double epsilon){
		double posInf = Double.PositiveInfinity;
		double negInf = Double.NegativeInfinity;
		Func<double, double> g;

		if(b < a){
			(double res, double err) = integrator(f, b, a, delta, epsilon);
			return (-res, err);
		}
		
		if(a == negInf){
			if(b == posInf){
				g = (t) => f(t/(1-t*t)) * (1+t*t)/(1-t*t)/(1-t*t);
				return rescaledCCIntegrator(g, -1, 1, delta, epsilon);
			}

			else{
				g = (t) => f(b - (1-t)/t) /t /t;
				return rescaledCCIntegrator(g,0,1, delta, epsilon);
			}
		}

		if(b == posInf){
			g = (t) => f(a + (1-t)/t) /t /t;
			return rescaledCCIntegrator(g, 0, 1, delta, epsilon);
		}

		return rescaledCCIntegrator(f, a, b, delta, epsilon);
	}
}
