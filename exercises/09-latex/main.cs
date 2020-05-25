using System;
using static System.Console;

class main{
	static int Main(){
		Func<double,double> f = (x) => 1/x;
		Func<double,double> ln = (x) => quad.o8a(f, 1, x);
		
		double eps = 1.0/64;
		for(double z=0+eps; z<8; z += eps){
			WriteLine($"{z} {ln(z)}");
		}
		return 0;
	}
}
