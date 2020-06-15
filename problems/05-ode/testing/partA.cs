using System;
using static System.Console;
using static Ode;

class testA{
	static void Main(){
		testRK12Ez();	
		testRK12exp();
	}

	static void testRK12Ez(){
		// Test rkStep12 for y' = 0, y0 = 1 from 0 to 5 with h = 1
		Func<double, vector, vector> dydx = (x, y) => y*0;
		vector yx = new vector(1.0);
		double h = 1;
		vector yh = new vector(0.0);
		vector err = new vector(0.0);
		double a = 0; double b = 5;
		bool test = true;
		for(double x = a; x < b; x += h){
			rkStep12(dydx, x, yx, h, yh, err);
			if(yh[0] != yx[0]){
				WriteLine($"ERROR RK12 with y' = 0, y0 = 1: yh:{yh[0]} != y:{yx[0]}");
				test = false;
				break;
			}
		}
		if(test) WriteLine("PASSED RK12 with y' = 0, y0 = 1");
	}

	static void testRK12exp(){
		// Test rkStep12 for y' = 0, y0 = 1 from 0 to 1 with h = 1/20
		Func<double, vector, vector> dydx = (x, y) => y;
		vector yx = new vector(1.0);
		double h = 1/20.0;
		vector yh = new vector(0.0);
		vector err = new vector(0.0);
		double a = 0; double b = 1;
		bool test = true;
		for(double x = a; x < b; x += h){
			rkStep12(dydx, x, yx, h, yh, err);
			if(yh[0] < yx[0]){
				WriteLine($"ERROR yh < y: {yh[0]} != {yx[0]}");
				test = false;
				break;
			}
			yx[0] = yh[0];
		}
		if(test) WriteLine($"PASSED RK12 y'=y, y0=1: y(1) = e = {yx[0]} +- {err[0]}");
	}

}
