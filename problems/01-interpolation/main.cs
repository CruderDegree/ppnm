using System;
using static System.Console;
using static System.Math;

class interpolater{
	static int Main(String[] args){
		double[] xs, ys;
		// Make linear spline
		if (args[0] == "lin"){
			// f(x) = x
			if(args[1] == "1"){
				xs = new double[] {1, 2, 3, 4, 5};
				ys = new double[] {1, 2, 3, 4, 5};	
			}
			// f(x) = x**2
			else if(args[1] == "2"){
				xs = new double[] {1, 2, 3, 4, 5};
				ys = new double[] {1, 4, 9, 16, 25};	
			}
			// f(x) = sin(x)
			else{
				xs = new double[] {0, PI/4, PI/2, 3*PI/4, PI, PI*5/4, PI*3/2,
				       	PI*7/4, 2*PI};
				ys = new double[] {0, 1/Sqrt(2), 1, 1/Sqrt(2), 0, -1/Sqrt(2), -1,
				-1/Sqrt(2), 0};	
			}
			double eps = 1.0/16;
			for(double z=xs[0]; z<=xs[xs.Length - 1]; z+= eps){
				WriteLine($"{z} {linterp(xs,ys,z)} {linterpInteg(xs,ys,z)}");
			}
		}
		else if (args[0] == "quad"){
			qspline s;
			if(args[1] == "1"){
				xs = new double[] {1, 2, 3, 4, 5};
				ys = new double[] {1, 2, 3, 4, 5};
			}
			else if(args[1] == "2"){
				xs = new double[] {1, 2, 3, 4, 5};
				ys = new double[] {2, 3, 2, 4, 2};
			}
			else{
				xs = new double[] {1, 2, 3,  4,  5,  6, 7, 8, 9};
				ys = new double[] {1, 4, 9, 16, 25, 16, 9, 4, 1};
			}
			s = new qspline(xs, ys);
			double eps = 1.0/16;
			for(double z=xs[0]; z<=xs[xs.Length - 1]; z+= eps){
				// data printed as z spline(z) deriv(z) int(z)
				WriteLine($"{z} {s.spline(z)} {s.derivative(z)} {s.integral(z)}");
			}
		}
		return 0;
	}
	// Linear interpolator function
	static double linterp(double[] x, double[] y, double z){
		int i = 0, j = x.Length-1;
		while(j-i > 1){
			int mid = (i+j)/2;
			if(z>x[mid]) i = mid; else j = mid;
		}
		return y[i] + (y[i+1] - y[i])/(x[i+1]-x[i]) * (z - x[i]);
	}

	// Linear interpolation integrator
	// integrates from x[0] to z
	static double linterpInteg(double[] x, double[] y, double z){
		double result = 0;
		int i = 0;
		double p = (y[i+1] - y[i])/(x[i+1]-x[i]);
		while(i+1 < x.Length && x[i+1]<z){
			result += 1.0/2 * (x[i+1]-x[i]) * (p*(x[i+1] - x[i]) + 2*y[i]);
			i++;
			p =  (y[i+1] - y[i])/(x[i+1]-x[i]);
		}	
		result += 1.0/2 * (z-x[i]) * (p*(z - x[i]) + 2*y[i]);
		return result;
	}
}
