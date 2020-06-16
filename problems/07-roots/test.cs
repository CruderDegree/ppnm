using static System.Console;
using System;
using static Roots;

public class test{
	static void Main(){
		Func<vector, vector> f = (x) => new vector(x[0]*x[0] - 9, 81 - x[1]*x[1]);
		vector p = new vector(10, 20);
		vector roots = newton(f, p);
		roots.print("Found solution: ");
		p = new vector(-10, -20);
		roots = newton(f,p);
		roots.print("Found second solution?: ");
	}
}
