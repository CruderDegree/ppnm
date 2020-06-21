using static System.Console;
using System;
using static Roots;

public class test{
	static void Main(){
		WriteLine("Testing to find roots of f(x,y) = (x*x - 9, 81 - y*y)");
		Func<vector, vector> f = (x) => new vector(x[0]*x[0] - 9, 81 - x[1]*x[1]);
		vector p = new vector(10, 20);
		vector roots = newton(f, p);
		roots.print("Found solution: ");
		p = new vector(-10, -20);
		roots = newton(f,p);
		roots.print("Found second solution: ");
		p = new vector(-13, 27);
		roots = newton(f,p);
		roots.print("Found third solution: ");
		p = new vector(42, -65);
		roots = newton(f,p);
		roots.print("Found fourth solution: ");
	}
}
