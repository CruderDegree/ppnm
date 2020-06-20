using System;
using static System.Console;
using static System.Math;
using static Quadratures;

class main{
	static void Main(){
		WriteLine("--- Part A ---");
		Func<double, double> f = (x) => Sqrt(x);
		double d = 1.0/1000, e = 1.0/1000;
		(double res, double err) = recAdaptIntegrator(f, 0, 1, d, e);
		WriteLine("∫dx √x from 0 to 1:");
		WriteLine("Calculation: \t\t Actual:");
		WriteLine($"{res} \t {2.0/3} \n");

		f = (x) => 4*Sqrt(1-x*x);
		(res, err) = recAdaptIntegrator(f, 0, 1, d, e);
		WriteLine("∫dx 4*√(1-x*x) from 0 to 1:");
		WriteLine("Calculation: \t\t Actual:");
		WriteLine($"{res} \t {PI} \n");


		WriteLine("\n--- Part B---");
		WriteLine("Compare number of integrand evaluations \nwith and without CC-variable transformation");
		int evals = 0;

		WriteLine("Func 1: ∫dx 1/√(x) from 0 to 1");
		f = delegate(double x){
			evals++;
			return 1/Sqrt(x);
		};
		(res, err) = recAdaptIntegrator(f, 0, 1, d, e);
		WriteLine($"No transformation. Calc:{res} Exact:{2} Evaluations: {evals}");
		evals = 0;
		(res, err) = rescaledCCIntegrator(f, 0, 1, d, e);
		WriteLine($"CC transformation. Calc:{res} Exact:{2} Evaluations: {evals}");

		WriteLine("\nFunc 2: ∫dx ln(x)/√(x) from 0 to 1");
		evals = 0;
		f = delegate(double x){
			evals++;
			//WriteLine($"evals: {evals} x:{x}");
			return Log(x)/Sqrt(x);
		};
		(res, err) = recAdaptIntegrator(f, 0, 1, d, e);
		WriteLine($"No transformation. Calc:{res} Exact:{-4} Evaluations: {evals}");
		evals = 0;
		(res, err) = rescaledCCIntegrator(f, 0, 1, d, e);
		WriteLine($"CC transformation. Calc:{res} Exact:{-4} Evaluations: {evals}");
		
		WriteLine("\nFunc 3: ∫dx 4*√(1 - x*x) from 0 to 1  = PI, \nwith as many significant digits as possible");
		evals = 0;
		f = delegate(double x){
			evals++;
			return 4*Sqrt(1-x*x);
		};
		d = 1e-9;
		e = 1e-9;
		WriteLine("For delta = epsilon = 1e-9, we get:");
		(res, err) = recAdaptIntegrator(f, 0, 1, d, e);
		WriteLine($"No transformation. Calc:{res} Deviation:{PI - res} \t Evaluations: {evals}");
		evals = 0;
		(res, err) = rescaledCCIntegrator(f, 0, 1, d, e);
		WriteLine($"CC transformation. Calc:{res} Deviation:{PI - res} \t Evaluations: {evals}");
		evals = 0;
		res = quad.o8av(f, 0, 1, d, e);
		WriteLine($"o8av-routine.      Calc:{res} Deviation:{PI - res} \t Evaluations: {evals}");
		
		WriteLine("\n--- Part C ---");
		d = 1e-2; e = d;

		WriteLine("\nFunc 1: ∫dx 1/(1+x*x) from 0 to +inf");
		evals = 0;
		f = delegate(double x){
			evals++;
			//WriteLine($"evals: {evals} x:{x}");
			return 1/(1+x*x);
		};
		double exact = PI/2;
		(res, err) = integrator(f, 0, Double.PositiveInfinity, d, e);
		WriteLine($"Integrator:\t Calc:{res} Exact:{exact} Evaluations: {evals} Error: {err}");
		evals = 0;
		res = quad.o8av(f, 0, Double.PositiveInfinity, d, e);
		WriteLine($"08av-routine:\t Calc:{res} Exact:{exact} Evaluations: {evals}");

		WriteLine("\nFunc 2: ∫dx exp(-x*x) from -inf to +inf");
		evals = 0;
		f = delegate(double x){
			evals++;
			return Exp(-x*x);
		};
		exact = Sqrt(PI);
		(res, err) = integrator(f, Double.NegativeInfinity, Double.PositiveInfinity, d, e);
		WriteLine($"Integrator:\t Calc:{res} Exact:{exact} Evaluations: {evals} Error: {err}");
		evals = 0;
		res = quad.o8av(f, Double.NegativeInfinity, Double.PositiveInfinity, d, e);
		WriteLine($"08av-routine:\t Calc:{res} Exact:{exact} Evaluations: {evals}");
		
		WriteLine("\nFunc 3: ∫dx 1/cosh(x) from +inf to -inf");
		evals = 0;
		f = delegate(double x){
			evals++;
			//WriteLine($"evals: {evals} x:{x}");
			return 1/Cosh(x);
		};
		exact = -PI;
		(res, err) = integrator(f, Double.PositiveInfinity, Double.NegativeInfinity, d, e);
		WriteLine($"Integrator:\t Calc:{res} Exact:{exact} Evaluations: {evals} Error: {err}");
		evals = 0;
		res = quad.o8av(f, Double.PositiveInfinity, Double.NegativeInfinity, d, e);
		WriteLine($"08av-routine:\t Calc:{res} Exact:{exact} Evaluations: {evals}");
	}	
}
