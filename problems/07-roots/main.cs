using static System.Console;
using System;
using static Roots;

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
	}
}
