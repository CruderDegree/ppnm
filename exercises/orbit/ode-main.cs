using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

class main{
	static int Main(){
		// method solves the ode y' = y*(1-y) w. initial cond. y(0)=0.5
		// and plots the data from x=0 to x=3
		double a = 0, b = 3;
		vector ya = new vector(0.5);
		Func<double, vector, vector> f = (x, y) => new vector(y[0]*(1-y[0]));
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();
		ode.rk23(f, a, ya, b, xlist:xs, ylist:ys);
		for(int i=0; i<xs.Count; i++){
			WriteLine($"{xs[i]} {ys[i][0]}");
		}

		return 0;
	}
}

		
