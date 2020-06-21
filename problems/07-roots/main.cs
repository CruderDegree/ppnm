using static System.Console;
using System;
using System.IO;
using static Roots;
using static Ode;

public class main{
	static void Main(){
		WriteLine("--- Part A: Search for extremums of the Rosenbrock Valley Function ---");
		Func<vector, vector> f = (x) => 
		new vector(2*(200*x[0]*x[0]*x[0] - 200*x[0]*x[1] + x[0] - 1), 
			   200*(x[1] - x[0]*x[0]));
		vector p = new vector(10, 20);
		p.print("Starting point: ");
		vector roots = newton(f, p);
		roots.print("Found solution: ");
		p = new vector(-123, 123);
		p.print("Starting point: ");
		roots = newton(f, p);
		roots.print("Found solution: ");
		p = new vector(-123, 123);
		p.print("Starting point: ");
		roots = newton(f, p);
		roots.print("Found solution: ");
		p = new vector(-100, 0);
		p.print("Starting point: ");
		roots = newton(f, p);
		roots.print("Found solution: ");
		p = new vector(100, -123);
		p.print("Starting point: ");
		roots = newton(f, p);
		roots.print("Found solution: ");

		WriteLine("\n\n--- Part B ---");
		double rmax = 8;
		Func<vector, vector> M = (x) => new vector(f_E(x[0],rmax));
		p = new vector(-2.0);
		roots = newton(M, p);
		WriteLine($"For rmax = {rmax} we get an energy estimate of {roots[0]},\ncompared to the analytical result of -1/2");
		using(StreamWriter sw = new StreamWriter("data.txt")){
			for(double r = 0; r <= rmax; r += 1.0/64) sw.WriteLine($"{r}\t{f_E(roots[0],r)}");
		}
	}

	static double f_E(double E, double r){
		Func<double, vector, vector> swave = (x, y) => new vector(y[1], -2*(1/x + E) * y[0]);
		double rmin = 1e-3;
		vector frmin = new vector(rmin*(1-rmin), 1 - 2*rmin);
		AdaptOdeSolver solver = new AdaptOdeSolver(swave, rmin, frmin, r, 1e-3, 1e-3, 1e-3);
		return solver.yData[solver.yData.Count - 1][0];
	}
}
