using static System.Console;
using static System.Math;
using static Minimization;
using System;

class main{
	static void Main(){
		Func<vector, double> rosenBrock = delegate(vector z){
			double x = z[0], y = z[1];
			return Pow(1-x, 2) + 100 * Pow(y - x*x, 2);
		};
		vector p = new vector(0.0, 0.0);
		double eps = 1.0/1000;
		(vector roots,int steps)= qNewton(rosenBrock, p, eps);
		WriteLine("--- Part A ---");
		WriteLine("Searching for a minimum of the Rosenbrock Valley function...");
		p.print("Starting point:");
		roots.print($"Minimum found ({steps} steps)");
		WriteLine("Global minimum: (1,1)");

		Func<vector, double> himmelblau = delegate(vector z){
			double x = z[0], y = z[1];
			return Pow(x*x + y - 11, 2) + Pow(x + y*y - 7, 2);
		};
		p = new vector(0.0, 0.0);
		eps = 1.0/1000;
		(roots,steps)= qNewton(himmelblau, p, eps);
		WriteLine("Searching for a minimum of the Himmelblau function...");
		p.print("Starting point:");
		roots.print($"Minimum found ({steps} steps)");
		WriteLine("Local minimum: (3,2)");
	}
}
