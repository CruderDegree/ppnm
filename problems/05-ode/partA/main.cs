using System;
using static System.Console;
using static System.Math;
using static Ode;

class main{
	static void Main(){
		// This main solves the ODE from 0 to pi
		// u'' = -u, u'(0) = 0, u(0) = 1, which has the solution
		// cos(x)
		
		Func<double, vector, vector> dydx = (x, y) => new vector(y[1], -y[0]);
		vector yt = new vector(1.0,0.0);
		double a = 0, b = 2*PI, h = 1/20.0;
		double absAcc = 1/100.0;
		double relAcc = 1/100.0;
		
		AdaptOdeSolver solver = new AdaptOdeSolver(dydx, a, yt, b, h, absAcc, relAcc);

		for(int i = 0; i < solver.yData.Count; i++){
			WriteLine($"{solver.xData[i]} {solver.yData[i][0]} {solver.yData[i][1]}");
		}
	}
}
